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

namespace Arduin
{
    public partial class Form1 : Form {
        private bool isStarted = false;
        private bool heatIsStarted = false;

        // true if loading settings and mobility from file, prevents value overwriting
        private bool ignoreInsertedValues = false;

        // Velkost intezitneho grafu X, Y
        private int heatSizeX = 524;
        private int heatSizeY = 355;

        // Velkost panelu kde su vsetky komponenty intensity grafu
        private int heatPanelSizeX = 1060;
        private int heatPanelSizeY = 455; // 380

        private AggregatedData aggData;

        List<Panel> allPanels = new List<Panel>();

        public Form1() {
            InitializeComponent();
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
            //CreateHeatMap();
            //CreateHeatFromCurrent();
            DecideAction();
        }

        private void DecideAction() {
            AgregateForm myMessageBoxh = new AgregateForm(this);
            myMessageBoxh.ShowDialog();
        }

        public void OpenHeatMap() {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "csv|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK) {
                Backend.Model.AggregatedData ad = new Backend.Model.AggregatedData();
                CreateHeatMap();
            } else {
                DialogResult dr = MessageBox.Show("Please choose a file", "File not found", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Retry) {
                    OpenHeatMap();
                }
            }
        }

        //nejde dorabam
        public void CreateHeatFromCurrent() {
            Panel heatCurrentPanel = CreateHeatPanel();
            AddHeatChart(heatCurrentPanel);
            AddButtons(heatCurrentPanel, true);
            graphpanel.Controls.Add(heatCurrentPanel);
            allPanels.Add(heatCurrentPanel);
        }

        private void CreateHeatMap() {
            Panel heatpanel = CreateHeatPanel();

            AddHeatChart(heatpanel);
            AddButtons(heatpanel);
            graphpanel.Controls.Add(heatpanel);
            allPanels.Add(heatpanel);
        }

        private Panel CreateHeatPanel() {
            Panel heatpanel = new Panel();
            heatpanel.Size = new Size(heatPanelSizeX, heatPanelSizeY);
            heatpanel.Left = 0;
            heatpanel.Top = graphpanel.Height;
            return heatpanel;
        }

        private void AddHeatChart(Panel heatpanel) {

            /*Chart heatchart = new Chart();
            heatchart.Size = new Size(heatSizeX, heatSizeY);
            heatchart.Left = 0;
            heatchart.Top = 50;
            heatchart.Legends.Add(new Legend("Heat"));
            heatpanel.Controls.Add(heatchart);*/
        }

        private void AddButtons(Panel heatpanel, bool fromCurrent = false) {
            int buttonX = 100;
            int buttonY = 50;

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

            cancel.Click += (s, e) => {
                PanelReorder(heatpanel);
            };

            heatpanel.Controls.Add(cancel);
        }

        private void PanelReorder(Panel pan) {
            foreach (Panel i in allPanels) {
                graphpanel.Controls.Remove(i);
            }
            allPanels.Remove(pan);

            int k = 0;
            foreach (Panel i in allPanels) {
                i.Left = 0;
                i.Top = graphpanel.Height + k * heatPanelSizeY + k * 45;
                graphpanel.Controls.Add(i);
                k++;
            }
        }

        private void SaveButton(Panel heatpanel, int buttonX, int buttonY) {
            Button save = new Button();
            save.Size = new Size(buttonX, buttonY);
            save.Text = "Save";
            save.Left = heatSizeX - buttonX;
            save.Top = heatSizeY + buttonY;
            heatpanel.Controls.Add(save);
        }

        private void StartStopButton(Panel heatpanel, int buttonX, int buttonY) {
            Button startstop = new Button();
            startstop.Size = new Size(buttonX, buttonY);
            startstop.Left = heatSizeX - 2 * buttonX;
            startstop.Top = heatSizeY + buttonY;
            startstop.Text = "Stop";
            startstop.Click += (s, e) => {
                heatIsStarted = !heatIsStarted;
                if (!heatIsStarted) {
                    startstop.Text = "Stop";
                    // spusti vykreslovanie

                } else {
                    startstop.Text = "Start";
                    // zastavi vyreslovanie
                }
            };

            heatpanel.Controls.Add(startstop);
        }

        private void Form1_Load(object sender, EventArgs e) {
            InitializeSettings();
            InitializeMobility();
            InitializeGraphSettings();
            EnableScrolling();
            projectName.Text = Backend.Model.Settings.projectName;
        }

        private async Task DrawGraph() {
            Debug.WriteLine("idem");
            cartesianChartMain.AxisX.Clear();
            cartesianChartMain.AxisY.Clear();
            this.aggData = await DataManagementService.Instance.getAggregatedData(); //  odkomentovat 
                                                                                     //int[] aggregatedData = { 1, 1, 1, 1, 2, 3, 5, 8, 13, 18, 25, 18, 13, 8, 5, 3, 1, 1, 1, 1 ,2,3,4,5,4,3,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,5,8,15,22,15,8,5,1,1,1}; // zakomentovat
                                                                                     //AggData = aggregatedData;
                                                                                     //Array.ForEach(AggData, Console.WriteLine);
            cartesianChartMain.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Main Graph",
                    Values = new ChartValues<int>(this.aggData.aggregatedData) // odkomentovat
                    //Values = new ChartValues<int>(aggregatedData)  /// zakomentovat
                }
            };

            cartesianChartMain.AxisX.Add(new Axis {
                Title = "Doplnit X-os",
                LabelFormatter = value => value.ToString(),
                Separator = new Separator { Step = 1 }/*,
                MinValue = aggregatedData.Min(),
                MaxValue = aggregatedData.Max()*/

            }
            );

            cartesianChartMain.AxisY.Add(new Axis {
                Title = "Doplnit Y-os",
                LabelFormatter = value => value.ToString(),
                Separator = new Separator { Step = 1 }
            });

            Debug.WriteLine("koniec");
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
            numericsampling.Text = Settings.sampling.ToString();
            numericcount.Text = Settings.repeatCycles.ToString();
            numericgate.Text = Settings.gate.ToString();
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

        private async void button1_Click(object sender, EventArgs e) {
            isStarted = !isStarted;
            //await DrawGraph();
            while (isStarted) {
                try {
                    await DrawGraph();
                } catch (Exception err) {
                    MessageBox.Show("Connection not found : " + err.Message);
                    isStarted = !isStarted;
                }
                //await DrawGraph();
            }

            if (isStarted) {
                // start
                //tak ako je nazvany image v resources
                button1.Image = Arduin.Properties.Resources.Stop;
                //await DrawGraph();
                /*timer1.Interval = 2000;
                timer1.Tick += async (s,er) => await DrawGraph();
                timer1.Start();*/
            } else {
                //stop
                //tak ako je nazvany image v resources
                button1.Image = Arduin.Properties.Resources.Play;
                timer1.Stop();
            }
        }


        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            // aplikovat mobilitu
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

            Settings.repeatSeconds = float.Parse(numericseconds.Text);
            Settings.repeatCycles = Convert.ToInt32(numericcount.Text);
            Settings.sampling = Convert.ToInt32(numericsampling.Text);
            Settings.gate = Convert.ToInt32(numericgate.Text);
            Settings.applyRepeatCount = Convert.ToBoolean(repeatcountcheckbox.Checked);

            try {
                ArduinoConnectionService.Instance.sendSettingsToArduino();
                MessageBox.Show("Settings was sent to arduino");
            } catch (Exception error) {
                MessageBox.Show("Erorr occured : " + error.Message);
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

    }
}
