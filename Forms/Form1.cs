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
        private bool heatisVisible = false;
        private bool isStarted = false;

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
            toolTip1.SetToolTip(label5, "Precision of measurement");
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
            DecideAction();
            //ChartManagment();
            //OpenHeatMap();
        }

        private void DecideAction()
        {
            AgregateForm myMessageBoxh = new AgregateForm();
            myMessageBoxh.ShowDialog();
        }

        public void OpenHeatMap()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "csv|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Backend.Model.AggregatedData ad = new Backend.Model.AggregatedData();
                ad.path = ofd.FileName;
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

        private void CreateHeatMap()
        {
            ResizePanel();
            Chart heatchart = new Chart();
            heatchart.Size = new Size(524,355);
            heatchart.Left = 0;
            heatchart.Top = panel3.Height - 400;
            heatchart.Legends.Add(new Legend("Heat"));
            //heatchart.Anchor = AnchorStyles.Bottom;
            //heatchart.Anchor = AnchorStyles.Left;
            panel3.Controls.Add(heatchart);
        }

        private void ResizePanel()
        {
            panel3.Size = new Size(panel3.Width, panel3.Height + 400);
        }

        private void ChartManagment()
        {
            chart2.Enabled = !chart2.Enabled;
            chart3.Enabled = !chart3.Enabled;
            chart2.Visible = !chart2.Visible;
            chart3.Visible = !chart3.Visible;

            if (!heatisVisible)
            {
                chart1.Size = new Size(chart1.Size.Width, chart1.Size.Height / 2);
            }
            else
            {
                chart1.Size = new Size(chart1.Size.Width, chart1.Size.Height * 2);
            }

            heatisVisible = !heatisVisible;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeSettings();
            InitializeMobility();
            InitializeGraphSettings();
        }

        private void InitializeSettings()
        {
            numericUpDown1.Value = (decimal)Backend.Model.Settings.repeatSeconds;
            numericUpDown2.Value = Backend.Model.Settings.sampling;
            numericUpDown3.Value = Backend.Model.Settings.repeatCycles;
            ComboBoxIntialize();
        }

        private void ComboBoxIntialize()
        {
            object[] ItemObject = new object[10];
            int index = Backend.Model.Settings.gate;
            for (int i = 0; i < 10; i++)
            {
                ItemObject[i] = "Width " + i + " pulse";
            }
            comboBox1.Items.AddRange(ItemObject);
            comboBox1.SelectedIndex = index;
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
                //button1.Image = Properties.Resources.Stop;
            }
            else
            {
                //stop
                //tak ako je nazvany image v resources
                //button1.Image = Properties.Resources.Play;
            }
        }
    }
}
