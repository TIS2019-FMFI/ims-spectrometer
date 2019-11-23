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
using System.Windows.Forms.DataVisualization.Charting;

namespace Arduin
{
    public partial class Form1 : Form
    {
        private bool isStarted = false;
        private bool heatIsStarted = false;

        // Velkost intezitneho grafu X, Y
        private int heatSizeX = 524;
        private int heatSizeY = 355;

        // Velkost panelu kde su vsetky komponenty intensity grafu
        private int heatPanelSizeX = 1060;
        private int heatPanelSizeY = 455; // 380

        List<Panel> allPanels = new List<Panel>();

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
            //CreateHeatMap();
            //CreateHeatFromCurrent();
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
                Backend.Model.AggregatedData ad = new Backend.Model.AggregatedData();
                CreateHeatMap();
            } else
            {
                DialogResult dr = MessageBox.Show("Please choose a file", "File not found", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Retry)
                {
                    OpenHeatMap();
                }
            }
        }

        //nejde dorabam
        public void CreateHeatFromCurrent()
        {
            Panel heatCurrentPanel = CreateHeatPanel();
            AddHeatChart(heatCurrentPanel);
            AddButtons(heatCurrentPanel, true);
            graphpanel.Controls.Add(heatCurrentPanel);
            allPanels.Add(heatCurrentPanel);
        }

        private void CreateHeatMap()
        {
            Panel heatpanel = CreateHeatPanel();

            AddHeatChart(heatpanel);
            AddButtons(heatpanel);
            graphpanel.Controls.Add(heatpanel);
            allPanels.Add(heatpanel);
        }

        private Panel CreateHeatPanel()
        {
            Panel heatpanel = new Panel();
            heatpanel.Size = new Size(heatPanelSizeX, heatPanelSizeY);
            heatpanel.Left = 0;
            heatpanel.Top = graphpanel.Height;
            return heatpanel;
        }

        private void AddHeatChart(Panel heatpanel)
        {
            Chart heatchart = new Chart();
            heatchart.Size = new Size(heatSizeX, heatSizeY);
            heatchart.Left = 0;
            heatchart.Top = 50;
            heatchart.Legends.Add(new Legend("Heat"));
            heatpanel.Controls.Add(heatchart);
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
                // este dorobim neskor
                //allPanels.Remove(heatpanel);
                heatpanel.Dispose();
                //PanelReorder(heatpanel);
            };

            heatpanel.Controls.Add(cancel);
        }

        private void PanelReorder(Panel pan)
        {
            foreach (Panel i in allPanels)
            {
                graphpanel.Controls.Remove(i);
            }
            allPanels.Remove(pan);

            foreach (Panel i in allPanels)
            {
                i.Left = 0;
                i.Top = graphpanel.Height;
                graphpanel.Controls.Add(i);
            }
        }

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
                button1.Image = Properties.Resources.Stop;
            }
            else
            {
                //stop
                //tak ako je nazvany image v resources
                button1.Image = Properties.Resources.Play;
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
    }
}
