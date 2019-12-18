using Arduin.Properties;
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

namespace Arduin
{
    public partial class Form1 : Form
    {
        private bool isStarted = false;
        private bool heatIsStarted = false;

        private int heatSizeX = 524;
        private int heatSizeY = 355;

        private int heatPanelSizeX = 1060;
        private int heatPanelSizeY = 455;

        List<Tuple<Panel, IntensityData>> allPanels = new List<Tuple<Panel, IntensityData>>();
        Tuple<Panel, IntensityData> livePanel;
        LiveCharts.WinForms.CartesianChart liveheatchart;

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label2, "Measured in seconds (s)");
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label3, "Measured in mý (μs)");
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label4, "Width of pulse");
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label5, "Repeat 'Count' times");
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label6, "Measured in cm");
        }

        private void label7_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label7, "Measured in mbar");
        }

        private void label8_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label8, "Measured in K");
        }

        private void label9_MouseHover(object sender, EventArgs e)
        {
            toolTip1.ToolTipTitle = "Hint";
            toolTip1.SetToolTip(label9, "Measured in kV");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //CreateHeatFromCurrent(); kde je live graf?
            DecideAction();
        }

        private void DecideAction()
        {
            AgregateForm myMessageBoxh = new AgregateForm(this);
            myMessageBoxh.ShowDialog();
        }

        public void OpenHeatMap()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "csv|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Backend.Model.IntensityData idata = Backend.FileService.Instance.loadIntensityData(ofd.FileName);
                CreateHeatFromFile(idata);
            } else
            {
                DialogResult dr = MessageBox.Show("Please choose a file", "File not found", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Retry)
                {
                    OpenHeatMap();
                }
            }
        }

        public void CreateHeatFromCurrent()
        {
            Panel heatCurrentPanel = CreateHeatPanel();
            IntensityData idata = new IntensityData();
          
            liveheatchart = new LiveCharts.WinForms.CartesianChart();
            liveheatchart.Size = new Size(heatSizeX, heatSizeY);
            liveheatchart.Left = 0;
            liveheatchart.Top = 50;
            heatCurrentPanel.Controls.Add(liveheatchart);

            AddButtons(heatCurrentPanel, true);
            graphpanel.Controls.Add(heatCurrentPanel);
            livePanel = new Tuple<Panel, IntensityData>(heatCurrentPanel, idata);            
        }

        private async void AddHeatChartFromCurrent(AggregatedData adata){
            livePanel.Item2.intensityData.Add(adata);
            if (liveheatchart.Series.ElementAt(0) != null){
                ChartValues<HeatPoint> values = new ChartValues<HeatPoint>();
                for (int j = 0; j < adata.aggregatedData.Count(); j++) {
                       liveheatchart.Series.ElementAt(0).Values.Add(new HeatPoint(j, livePanel.Item2.intensityData.Count(), adata.aggregatedData[j]));
                }
                liveheatchart.Series.Add(new HeatSeries
                {
                    Values = values,
                    //DataLabels = true, cisla na jednotlivych polickach grafu
                    GradientStopCollection = new GradientStopCollection
                    {
                        new GradientStop(System.Windows.Media.Color.FromRgb(51, 51, 255), 0), //from 0.65 to 0.75
                        new GradientStop(System.Windows.Media.Color.FromRgb(51, 255, 51), 0.20), // from 0 to 0.5
                        new GradientStop(System.Windows.Media.Color.FromRgb(153, 255, 51), .40), //from 0.5 to 0.65                   
                        new GradientStop(System.Windows.Media.Color.FromRgb(255, 153, 51), .60), //from 0.75 to 0.85
                        new GradientStop(System.Windows.Media.Color.FromRgb(255, 0, 0), .80) //from 0.85 to 1(max value)
                }
                });
                liveheatchart.AxisX.Add(new LiveCharts.Wpf.Axis
                {
                    Title = "x-values",
                    LabelFormatter = value => value.ToString(),
                    Separator = new Separator {Step = 1},
                    Foreground = System.Windows.Media.Brushes.White,
                    MinValue = 0,
                    MaxValue = liveheatchart.Series.ElementAt(0).Values.Count, 
                });
                liveheatchart.AxisY.Add(new LiveCharts.Wpf.Axis
                {
                    Title = "y-values",
                    Foreground = System.Windows.Media.Brushes.White,
                    LabelFormatter = value => value.ToString(),
                });
            }
            else{
                for (int j = 0; j < adata.aggregatedData.Count(); j++) {
                       liveheatchart.Series.ElementAt(0).Values.Add(new HeatPoint(j, livePanel.Item2.intensityData.Count(), adata.aggregatedData[j]));
                }
            }
        }

        private void CreateHeatFromFile(Backend.Model.IntensityData idata)
        {
            Panel heatpanel = CreateHeatPanel();
            AddHeatChartFromFile(heatpanel, idata);
            AddButtons(heatpanel);
            graphpanel.Controls.Add(heatpanel);
            allPanels.Add(new Tuple<Panel, IntensityData>(heatpanel, idata));
        }
        
        private void AddHeatChartFromFile(Panel heatpanel, IntensityData idata)
        {
            LiveCharts.WinForms.CartesianChart heatchart;
            heatchart = new LiveCharts.WinForms.CartesianChart();
            heatchart.Size = new Size(heatSizeX, heatSizeY);
            heatchart.Left = 0;
            heatchart.Top = 50;

            ChartValues<HeatPoint> values = new ChartValues<HeatPoint>();
            for (int i = 0; i < idata.intensityData.Count(); i++) {
                Backend.Model.AggregatedData agregateddata = idata.intensityData[i];
                for (int j = 0; j < agregateddata.aggregatedData.Count(); j++) {
                       values.Add(new HeatPoint(j, i, agregateddata.aggregatedData[j]));
                }
            }

            heatchart.Series.Add(new HeatSeries
            {
                Values = values,
                GradientStopCollection = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Color.FromRgb(51, 51, 255), 0), //from 0.65 to 0.75
                    new GradientStop(System.Windows.Media.Color.FromRgb(51, 255, 51), 0.20), // from 0 to 0.5
                    new GradientStop(System.Windows.Media.Color.FromRgb(153, 255, 51), .40), //from 0.5 to 0.65                   
                    new GradientStop(System.Windows.Media.Color.FromRgb(255, 153, 51), .60), //from 0.75 to 0.85
                    new GradientStop(System.Windows.Media.Color.FromRgb(255, 0, 0), .80) //from 0.85 to 1(max value)
               }
            });

            heatchart.AxisX.Add(new LiveCharts.Wpf.Axis //preco sa nezobrazuje?? *
            {
                Title = "x-values",
                LabelFormatter = value => value.ToString(),
                Separator = new Separator {Step = 1},
                Foreground = System.Windows.Media.Brushes.White,
                MinValue = 1,
                MaxValue =  idata.intensityData[0].aggregatedData.Count(),                
            });
            heatchart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "y-values",
                Foreground = System.Windows.Media.Brushes.White,
                LabelFormatter = value => value.ToString(),
                Separator = new Separator {Step = 1},
            });

            heatpanel.Controls.Add(heatchart);
        }

        private Panel CreateHeatPanel()
        {
            Panel heatpanel = new Panel();
            heatpanel.Size = new Size(heatPanelSizeX, heatPanelSizeY);
            heatpanel.Left = 0;
            heatpanel.Top = graphpanel.Height;
            return heatpanel;
        }

        private void AddButtons(Panel heatpanel, bool fromCurrent = false)
        {
            int buttonX = 100;
            int buttonY = 50;

            AddCancelButton(heatpanel, buttonX, buttonY);

            if (fromCurrent)
            {
                SaveButton(heatpanel, buttonX, buttonY);
                StartStopButton(heatpanel, buttonX, buttonY);
            }
        }

        private void AddCancelButton(Panel heatpanel, int buttonX, int buttonY)
        {
            Button cancel = new Button();
            cancel.Size = new Size(buttonX, buttonY);
            cancel.Text = "Cancel";
            cancel.Left = heatSizeX - buttonX;          cancel.Top = 0;

            cancel.Click += (s, e) =>
            {
                //PanelReorder(heatpanel);
            };

            heatpanel.Controls.Add(cancel);
        }

        /*private void PanelReorder(Panel pan)  nefunguje pre list[tuple()]
        {
            foreach (Panel i in allPanels)
            {
                graphpanel.Controls.Remove(i);
            }
            allPanels.Remove(pan);

            int k = 0;
            foreach (Panel i in allPanels)
            {
                i.Left = 0;
                i.Top = graphpanel.Height + k * heatPanelSizeY + k * 45;
                graphpanel.Controls.Add(i);
                k++;
            }
        }*/

        private void SaveButton(Panel heatpanel, int buttonX, int buttonY)
        {
            Button save = new Button();
            save.Size = new Size(buttonX, buttonY);
            save.Text = "Save";
            save.Left = heatSizeX - buttonX;
            save.Top = heatSizeY + buttonY;
            heatpanel.Controls.Add(save);
        }

        private void StartStopButton(Panel heatpanel, int buttonX, int buttonY)
        {
            Button startstop = new Button();
            startstop.Size = new Size(buttonX, buttonY);
            startstop.Left = heatSizeX - 2 * buttonX;
            startstop.Top = heatSizeY + buttonY;
            startstop.Text = "Stop";
            startstop.Click += (s, e) =>
            {
                heatIsStarted = !heatIsStarted;
                if (!heatIsStarted)
                {
                    startstop.Text = "Stop";
                    // spusti vykreslovanie

                }
                else
                {
                    startstop.Text = "Start";
                    // zastavi vyreslovanie
                }
            };

            heatpanel.Controls.Add(startstop);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeSettings();
            InitializeMobility();
            InitializeGraphSettings();
            EnableScrolling();
            projectName.Text = Backend.Model.Settings.projectName;
        }

        private async void DrawGraph()
        {
            cartesianChartMain.AxisX.Clear();
            cartesianChartMain.AxisY.Clear();
            //AggregatedData aggregatedData = await DataManagementService.Instance.getAggregatedData(); //  odkomentovat 
            int[] aggregatedData = { 1, 1, 1, 1, 2, 3, 5, 8, 13, 18, 25, 18, 13, 8, 5, 3, 1, 1, 1, 1 ,2,3,4,5,4,3,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,5,8,15,22,15,8,5,1,1,1}; // zakomentovat
            cartesianChartMain.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Main Graph",
                    //Values = new ChartValues<int>(aggregatedData.aggregatedData) // odkomentovat
                    Values = new ChartValues<int>(aggregatedData)  /// zakomentovat
                }
            };

            cartesianChartMain.AxisX.Add(new Axis
            {
                Title = "Doplnit X-os",
                LabelFormatter = value => value.ToString(),
                Separator = new Separator { Step = 1 }/*,
                MinValue = aggregatedData.aggregatedData[0],
                MaxValue = aggregatedData.aggregatedData[0]*/

            }
            );

            cartesianChartMain.AxisY.Add(new Axis
            {
                Title = "Doplnit Y-os",
                LabelFormatter = value => value.ToString(),
                Separator = new Separator { Step = 1 }
            });
        }

        private void EnableScrolling()
        {
            graphpanel.AutoScroll = false;
            graphpanel.VerticalScroll.Enabled = false;
            graphpanel.VerticalScroll.Visible = false;
            graphpanel.VerticalScroll.Maximum = 0;
            graphpanel.AutoScroll = true;
        }

        private void InitializeSettings()
        {
            numericseconds.Value = (decimal)Backend.Model.Settings.repeatSeconds;
            numericsampling.Value = Backend.Model.Settings.sampling;
            numericcount.Value = Backend.Model.Settings.repeatCycles;
            numericgate.Value = Backend.Model.Settings.gate;
            repeatcountcheckbox.Checked = Backend.Model.Settings.applyRepeatSeconds;
        }

        private void InitializeMobility()
        {
            numericUpDown4.Value = (decimal)Backend.Model.Mobility.L;
            numericUpDown5.Value = (decimal)Backend.Model.Mobility.p;
            numericUpDown6.Value = (decimal)Backend.Model.Mobility.T;
            numericUpDown7.Value = (decimal)Backend.Model.Mobility.U;
        }

        private void InitializeGraphSettings()
        {
            //Default Y
            int ymax = 10;
            textBox4.Text = ymax.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isStarted = !isStarted;
            if (isStarted) {
                // start
                //tak ako je nazvany image v resources
                button1.Image = Resources.Stop;
                /*while (isStarted)
                {
                    DrawGraph();
                }*/
                DrawGraph();

            }
            else
            {
                //stop
                //tak ako je nazvany image v resources
                button1.Image = Resources.Play;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            // aplikovat mobilitu
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Backend.Model.Settings.projectName = projectName.Text;
            this.Text = Backend.Model.Settings.projectName;
        }

        private void savegraphbutton_Click(object sender, EventArgs e)
        {

        }

        private void LoadConfigButton_Click(object sender, EventArgs e)
        {

        }

        private void SaveConfigButton_Click(object sender, EventArgs e)
        {

        }
    }
}
