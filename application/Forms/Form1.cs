using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Media;
using Arduin.Backend;
using Arduin.Backend.Model;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;
using System.Windows.Threading;

namespace Arduin
{
    public partial class Form1 : Form {
        private bool isStarted = false;
        private volatile bool heatIsStarted = false; // thread sharing attribute

        // true if loading settings and mobility from file, prevents value overwriting
        private bool ignoreInsertedValues = false;

        private bool applyMobility = false;

        // Velkost intezitneho grafu X, Y
        private int heatSizeX = 1060;
        private int heatSizeY = 355;

        private int heatPanelSizeX = 1060;
        private int heatPanelSizeY = 455;

        private int currentSizeX = 1366;
        private int heatPanelOffset = 0;

        private AggregatedData aggData;
  
        Tuple<Panel, IntensityData> livePanel;
        LiveCharts.WinForms.CartesianChart liveheatchart;
        List<Tuple<Panel, IntensityData>> allPanelsIntensityData = new List<Tuple<Panel, IntensityData>>();

        public Form1() {
            InitializeComponent();

            // start arduino if stopped
            try {
                ArduinoConnectionService.Instance.start();
            } catch (Exception error) {
                MessageBox.Show("Erorr occured : " + error.Message);
            }

        }

        private void label2_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label2, "Measured in seconds (s)");
        }

        private void label3_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label3, "Measured in mý (μs)");
        }

        private void label4_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label4, "Width of pulse");
        }

        private void label5_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label5, "Repeat 'Count' times");
        }

        private void label6_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label6, "Measured in cm");
        }

        private void label7_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label7, "Measured in mbar");
        }

        private void label8_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label8, "Measured in K");
        }

        private void label9_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label9, "Measured in kV");
        }

        private void button5_Click(object sender, EventArgs e) {
            AgregateForm myMessageBoxh = new AgregateForm(this);
            myMessageBoxh.ShowDialog();
        }

        public void OpenHeatMap() {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "csv|*.csv";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Backend.Model.IntensityData idata = Backend.FileService.Instance.loadIntensityData(ofd.FileName);
                CreateHeatFromFile(idata);
            } else {
                DialogResult dr = MessageBox.Show("Please choose a file", "File not found", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Retry) {
                    OpenHeatMap();
                }
            }
        }

        public void CreateHeatFromCurrent()
        {
            if (livePanel is null){ // <-- only ONE live heat graph is allowed!
            Panel heatCurrentPanel = CreateHeatPanel();
            IntensityData idata = new IntensityData();
          
            liveheatchart = new LiveCharts.WinForms.CartesianChart();
            liveheatchart.Size = new Size(heatSizeX, heatSizeY);
            liveheatchart.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            liveheatchart.Left = 0;
            liveheatchart.Top = 50;
            liveheatchart.DisableAnimations = true;
            liveheatchart.Hoverable = false;
            liveheatchart.DataTooltip = null;

            heatCurrentPanel.Controls.Add(liveheatchart);

            AddButtons(heatCurrentPanel, true);
            graphpanel.Controls.Add(heatCurrentPanel);
            livePanel = new Tuple<Panel, IntensityData>(heatCurrentPanel, idata);
            this.heatIsStarted = true;
            }
        }

        private void CreateHeatFromFile(Backend.Model.IntensityData idata)
        {
            Panel heatpanel = CreateHeatPanel();
            AddHeatChartFromFile(heatpanel, idata);
            AddButtons(heatpanel);
            graphpanel.Controls.Add(heatpanel);
            allPanelsIntensityData.Add(new Tuple<Panel, IntensityData>(heatpanel, idata));
        }
        
        private Panel CreateHeatPanel() {
            Panel heatpanel = new Panel();
            heatpanel.Size = new Size(heatPanelSizeX, heatPanelSizeY);
            heatpanel.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            heatpanel.Left = 0;
            heatpanel.Top = graphpanel.Height;
            return heatpanel;
        }

        private void AddButtons(Panel heatpanel, bool fromCurrent = false) {
            int buttonX = 100;
            int buttonY = 50;
            this.Anchor = (AnchorStyles.Right);

            AddCancelButton(heatpanel, buttonX, buttonY);

            if (fromCurrent) {
                SaveButton(heatpanel, buttonX, buttonY);
                StartStopButton(heatpanel, buttonX, buttonY);
            }
        }

        private void AddCancelButton(Panel heatpanel, int buttonX, int buttonY) {
            Button cancel = new Button();
            cancel.Size = new Size(buttonX, buttonY);
            cancel.Text = "Cancel";
            cancel.Left = heatSizeX - buttonX; cancel.Top = 0;
            cancel.Anchor = (AnchorStyles.Right);


            cancel.Click += (s, e) => {
                PanelReorder(heatpanel);
            };

            heatpanel.Controls.Add(cancel);
        }

        private void PanelReorder(Panel pan) {
            if (livePanel != null){
                allPanelsIntensityData.Add(livePanel);
            }
            foreach (Tuple<Panel, IntensityData> i in allPanelsIntensityData) {
                graphpanel.Controls.Remove(i.Item1);
            }
   
            foreach (Tuple<Panel, IntensityData> i in allPanelsIntensityData) {
                if (pan.Equals(i.Item1)) {
                    allPanelsIntensityData.Remove(i);
                    break;
                }
            }

            int k = 0;
            foreach (Tuple<Panel, IntensityData> i in allPanelsIntensityData) {
                i.Item1.Left = 0;
                i.Item1.Top = graphpanel.Height + k * heatPanelSizeY + k * 45;
                graphpanel.Controls.Add(i.Item1);
                k++;
            }
            if (allPanelsIntensityData.Contains(livePanel)){
                allPanelsIntensityData.Remove(livePanel);
            }
            else{
                if (livePanel != null){
                    livePanel = null;
                }
            }
        }

        private void SaveButton(Panel heatpanel, int buttonX, int buttonY) {
            Button save = new Button();
            save.Size = new Size(buttonX, buttonY);
            save.Anchor = AnchorStyles.Right;
            save.Text = "Save";
            save.Left = heatSizeX - buttonX;
            save.Top = heatSizeY + buttonY;
            heatpanel.Controls.Add(save);
        }

        private void StartStopButton(Panel heatpanel, int buttonX, int buttonY) {
            Button startstop = new Button();
            startstop.Size = new Size(buttonX, buttonY);
            startstop.Anchor = AnchorStyles.Right;
            startstop.Left = heatSizeX - 2 * buttonX;
            startstop.Top = heatSizeY + buttonY;
            startstop.Text = "Stop";
            startstop.Click += (s, e) => {
                /*heatIsStarted = !heatIsStarted;
                if (heatIsStarted == true) {*/
                    startstop.Text = "Stopped";
                    heatIsStarted = false;
                     //spusti vykreslovanie
                //}
                /*else {
                    startstop.Text = "Start";
                    // zastavi vyreslovanie
                }*/
                
            };

            heatpanel.Controls.Add(startstop);
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            currentSizeX = this.Size.Width;
            heatPanelSizeX = currentSizeX - 306;
            heatSizeX = currentSizeX - 306;
        }

        private void Form1_Load(object sender, EventArgs e) {
            heatPanelOffset = currentSizeX - heatPanelSizeX;
            GateFill();
            SamplingFill();
            InitializeSettings();
            InitializeMobility();
            InitializeGraphSettings();
            EnableScrolling();
            projectName.Text = Backend.Model.Settings.projectName;
        }

        private void SamplingFill()
        {
            List<object> ItemObject = new List<object>();
            for (int i = 5; i <= 45; i += 5)
            {
                ItemObject.Add(i);
            }
            comboBox2.Items.AddRange(ItemObject.ToArray());
            comboBox2.SelectedItem = comboBox2.Items[comboBox2.Items.IndexOf(Settings.sampling)];
        }

        private void GateFill()
        {
            List<object> ItemObject = new List<object>();
            for (int i = 2; i <= 16; i += 2)
            {
                ItemObject.Add(i);
            }
            comboBox1.Items.AddRange(ItemObject.ToArray());
            comboBox1.SelectedItem = comboBox1.Items[comboBox1.Items.IndexOf(Settings.gate)];
        }

        private void AddHeatChartFromFile(Panel heatpanel, IntensityData idata) {
            LiveCharts.WinForms.CartesianChart heatchart;
            heatchart = new LiveCharts.WinForms.CartesianChart();
            heatchart.Size = new Size(heatSizeX, heatSizeY);
            heatchart.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
            heatchart.Left = 0;
            heatchart.Top = 50;
            heatchart.DisableAnimations = true;
            heatchart.Hoverable = false;
            heatchart.DataTooltip = null;

            ChartValues<HeatPoint> values = new ChartValues<HeatPoint>();
            List<HeatPoint> buffer = new List<HeatPoint>();

            for (int i = 0; i < idata.intensityData.Count(); i++) {
                Backend.Model.AggregatedData agregateddata = idata.intensityData[i];
                for (int j = 0; j < agregateddata.aggregatedData.Count(); j++) {
                    buffer.Add(new HeatPoint(j, i, agregateddata.aggregatedData[j]));
                }
            }
            values.AddRange(buffer);

            heatchart.Series.Add(new HeatSeries {
                Values = values,
                GradientStopCollection = new GradientStopCollection {
                    new GradientStop(System.Windows.Media.Color.FromRgb(51, 51, 255), 0), //from 0.65 to 0.75
                    new GradientStop(System.Windows.Media.Color.FromRgb(51, 255, 51), 0.20), // from 0 to 0.5
                    new GradientStop(System.Windows.Media.Color.FromRgb(153, 255, 51), .40), //from 0.5 to 0.65                   
                    new GradientStop(System.Windows.Media.Color.FromRgb(255, 153, 51), .60), //from 0.75 to 0.85
                    new GradientStop(System.Windows.Media.Color.FromRgb(255, 0, 0), .80) //from 0.85 to 1(max value)
               }
            });
            heatpanel.Controls.Add(heatchart);
        }

        internal void AddHeatChartFromCurrent() {
            AggregatedData lastInserted = this.livePanel.Item2.intensityData[this.livePanel.Item2.intensityData.Count -1];
            ChartValues<HeatPoint> temporalvalues = new ChartValues<HeatPoint>();
                for (int j = 0; j < lastInserted.aggregatedData.Count(); j++) {
                    temporalvalues.Add(new HeatPoint(j, livePanel.Item2.intensityData.Count(), lastInserted.aggregatedData[j]));
                }
            if (liveheatchart.Series.Count() == 0){
                liveheatchart.Series.Add(new HeatSeries {
                    Values = temporalvalues,
                    GradientStopCollection = new GradientStopCollection {
                            new GradientStop(System.Windows.Media.Color.FromRgb(51, 51, 255), 0), //from 0.65 to 0.75
                            new GradientStop(System.Windows.Media.Color.FromRgb(51, 255, 51), 0.20), // from 0 to 0.5
                            new GradientStop(System.Windows.Media.Color.FromRgb(153, 255, 51), .40), //from 0.5 to 0.65                   
                            new GradientStop(System.Windows.Media.Color.FromRgb(255, 153, 51), .60), //from 0.75 to 0.85
                            new GradientStop(System.Windows.Media.Color.FromRgb(255, 0, 0), .80) //from 0.85 to 1(max value)
                        }
                });
            }
            else{
                for (int j = 0; j < lastInserted.aggregatedData.Count(); j++) {
                    liveheatchart.Series.Last().Values.Add(new HeatPoint(j, livePanel.Item2.intensityData.Count(), lastInserted.aggregatedData[j]));
                }               
            }
        }

        internal async void DrawGraph() {
            while (isStarted) {
                    try
                {
                    cartesianChartMain.AxisY.Clear();
                    cartesianChartMain.AxisX.Clear();

                    // disalbe animation for faster rendering
                    cartesianChartMain.DisableAnimations = true;
                    cartesianChartMain.Hoverable = false;
                    cartesianChartMain.DataTooltip = null;

                    // MAIN CHART
                    // test -------------------------------------
                    Random rnd = new Random();
                    this.aggData = new AggregatedData();
                    int[] aggregatedData = new int[500];
                    for (int i = 0; i < 500; i++)
                    {
                        aggregatedData[i] = (i > 250 && i < 300) ? rnd.Next(100, 180) : rnd.Next(52);
                    }
                    this.aggData.aggregatedData = aggregatedData;
                    await Task.Run(() => Thread.Sleep(2000));
                    // ----------------------------------------

                    // this.aggData =  await Task.Run(() =>  DataManagementService.Instance.getAggregatedData());


                    // HEAT MAP  - if user pressed rending heap map
                    if (heatIsStarted)
                    {
                        this.livePanel.Item2.intensityData.Add(this.aggData);
                        this.AddHeatChartFromCurrent();
                    }

                    ValuesFill();

                }
                catch (Exception err) {
                    MessageBox.Show("Connection not found : " + err.Message);
                    isStarted = !isStarted;
                }
            }
        }

        private void ValuesFill()
        {
            if (applyMobility)
            {
                double[] tmp = DataManagementService.Instance.calculateMobilities(aggData);
                cartesianChartMain.Series = new SeriesCollection {new LineSeries {
                        Title = "Main Graph",
                        PointGeometrySize = 0,
                        Values = new ChartValues<double>(tmp)
                        }};
            }
            else
            {
                cartesianChartMain.Series = new SeriesCollection {new LineSeries {
                        Title = "Main Graph",
                        PointGeometrySize = 0,
                        Values = new ChartValues<int>(this.aggData.aggregatedData)
                        }};
            }
        }

        public void SetIsStarted() {
            isStarted = !isStarted;
        }

        private void EnableScrolling() {
            graphpanel.AutoScroll = false;
            graphpanel.VerticalScroll.Enabled = false;
            graphpanel.VerticalScroll.Visible = false;
            graphpanel.VerticalScroll.Maximum = 0;
            graphpanel.AutoScroll = true;
        }

        private void InitializeSettings() {
            numericseconds.Text = Settings.repeatSeconds.ToString();
            numericcount.Text = Settings.repeatCycles.ToString();
            repeatcountcheckbox.Checked = Settings.applyRepeatCount;
        }

        private void InitializeMobility() {
            numericUpDown4.Text = Backend.Model.Mobility.L.ToString();
            numericUpDown5.Text = Backend.Model.Mobility.p.ToString();
            numericUpDown6.Text = Backend.Model.Mobility.T.ToString();
            numericUpDown7.Text = Backend.Model.Mobility.U.ToString();
        }

        private void InitializeGraphSettings() {
            //Default Y
            int ymax = 10;
            textBox4.Text = ymax.ToString();
        }

        private  void button1_Click(object sender, EventArgs e) {
            isStarted = !isStarted;

            this.DrawGraph();

            if (isStarted) {
                button1.Image = Arduin.Properties.Resources.Stop;
            } else {
                button1.Image = Arduin.Properties.Resources.Play;
                timer1.Stop();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            Backend.Model.Settings.projectName = projectName.Text;
            this.Text = Backend.Model.Settings.projectName;
        }

        private void savegraphbutton_Click(object sender, EventArgs e) {
            if (this.aggData == null) {
                MessageBox.Show("No data to save has been found");
                return;
            }
            try {
                FileService.Instance.saveAggregatedData(this.aggData);
            } catch (Exception error) {
                MessageBox.Show("Erorr occured : " + error.Message);
            }

        }

        private void LoadConfigButton_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) {
                try {
                    this.ignoreInsertedValues = true;
                    FileService.Instance.loadSettingsAndMobility(ofd.FileName);
                    this.InitializeSettings();
                    this.InitializeMobility();
                } catch(Exception error) {
                    MessageBox.Show("Erorr occured loading configuration : " + error.Message);
                } finally {
                    this.ignoreInsertedValues = false;
                }
            } 
        }

        private void SaveConfigButton_Click(object sender, EventArgs e) {
            try {
                FileService.Instance.saveSettingsAndMobility();
                MessageBox.Show("Settings and Mobility has been saved ");
            } catch(Exception error) {
                MessageBox.Show("Erorr occured during saving : " + error.Message);
            }
        }

        private void SendConfigurationToArduino(object sender, EventArgs e) {
            if (ignoreInsertedValues) {
                return;
            }

            int oldSampling = Settings.sampling;
            int oldGate = Settings.gate;

            Settings.repeatSeconds = float.Parse(numericseconds.Text);
            Settings.repeatCycles = Convert.ToInt32(numericcount.Text);
            Settings.sampling = Convert.ToInt32(comboBox2.SelectedItem);
            Settings.gate = Convert.ToInt32(comboBox1.SelectedItem);
            Settings.applyRepeatCount = Convert.ToBoolean(repeatcountcheckbox.Checked);

            // do not send data into arduino if gate or sampling was not changed
            if(oldSampling == Settings.sampling && oldGate == Settings.gate) {
                return;
            }

            try {
                ArduinoConnectionService.Instance.sendSettingsToArduino();
                MessageBox.Show("Settings was sent to arduino");
            } catch (Exception error) {
                MessageBox.Show("Erorr occured sending settings to arduino: " + error.Message);
            }
        }

        private void SaveChangeMobilityValues(object sender, EventArgs e) {
            if (ignoreInsertedValues) {
                return;
            }

            Backend.Model.Mobility.L = Convert.ToDouble(numericUpDown4.Text);
            Backend.Model.Mobility.p = Convert.ToDouble(numericUpDown5.Text);
            Backend.Model.Mobility.T = Convert.ToDouble(numericUpDown6.Text);
            Backend.Model.Mobility.U = Convert.ToDouble(numericUpDown7.Text);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            this.applyMobility = Convert.ToBoolean(moblityCheckBox.Checked);
            ValuesFill();
        }
    }
}
