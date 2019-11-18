namespace Arduin
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericseconds = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericsampling = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.repeatcountcheckbox = new System.Windows.Forms.CheckBox();
            this.numericcount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericgate = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button5 = new System.Windows.Forms.Button();
            this.BasicSettings = new System.Windows.Forms.GroupBox();
            this.Mobility = new System.Windows.Forms.GroupBox();
            this.graphpanel = new System.Windows.Forms.Panel();
            this.savegraphbutton = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.projectName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericsampling)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericcount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericgate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.BasicSettings.SuspendLayout();
            this.Mobility.SuspendLayout();
            this.graphpanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 659);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "1.1";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(203, 645);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 45);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Repeat (s)";
            this.label2.MouseHover += new System.EventHandler(this.label2_MouseHover);
            // 
            // numericseconds
            // 
            this.numericseconds.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericseconds.Location = new System.Drawing.Point(137, 17);
            this.numericseconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericseconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericseconds.Name = "numericseconds";
            this.numericseconds.Size = new System.Drawing.Size(91, 23);
            this.numericseconds.TabIndex = 3;
            this.numericseconds.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sampling (μs)";
            this.label3.MouseHover += new System.EventHandler(this.label3_MouseHover);
            // 
            // numericsampling
            // 
            this.numericsampling.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericsampling.Location = new System.Drawing.Point(136, 86);
            this.numericsampling.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericsampling.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericsampling.Name = "numericsampling";
            this.numericsampling.Size = new System.Drawing.Size(91, 23);
            this.numericsampling.TabIndex = 5;
            this.numericsampling.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Gate (Pulse)";
            this.label4.MouseHover += new System.EventHandler(this.label4_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(19, 184);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 50);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.repeatcountcheckbox);
            this.panel1.Controls.Add(this.numericcount);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.numericgate);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.numericsampling);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numericseconds);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 304);
            this.panel1.TabIndex = 11;
            // 
            // repeatcountcheckbox
            // 
            this.repeatcountcheckbox.AutoSize = true;
            this.repeatcountcheckbox.Location = new System.Drawing.Point(19, 157);
            this.repeatcountcheckbox.Name = "repeatcountcheckbox";
            this.repeatcountcheckbox.Size = new System.Drawing.Size(114, 21);
            this.repeatcountcheckbox.TabIndex = 14;
            this.repeatcountcheckbox.Text = "Repeat Count";
            this.repeatcountcheckbox.UseVisualStyleBackColor = true;
            // 
            // numericcount
            // 
            this.numericcount.Location = new System.Drawing.Point(136, 51);
            this.numericcount.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericcount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericcount.Name = "numericcount";
            this.numericcount.Size = new System.Drawing.Size(91, 23);
            this.numericcount.TabIndex = 13;
            this.numericcount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Repeat (Count)";
            this.label5.MouseHover += new System.EventHandler(this.label5_MouseHover);
            // 
            // numericgate
            // 
            this.numericgate.Location = new System.Drawing.Point(136, 121);
            this.numericgate.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericgate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericgate.Name = "numericgate";
            this.numericgate.Size = new System.Drawing.Size(91, 23);
            this.numericgate.TabIndex = 11;
            this.numericgate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(169, 269);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 26);
            this.button3.TabIndex = 10;
            this.button3.Text = "Apply";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(167, 244);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Rescale";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(19, 269);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(45, 23);
            this.textBox4.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 244);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 17);
            this.label15.TabIndex = 4;
            this.label15.Text = "Y max";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.numericUpDown7);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.numericUpDown6);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.numericUpDown5);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.numericUpDown4);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(0, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(235, 194);
            this.panel2.TabIndex = 15;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(19, 163);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(113, 21);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "Apply mobility";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.Location = new System.Drawing.Point(81, 124);
            this.numericUpDown7.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown7.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "U";
            this.label9.MouseHover += new System.EventHandler(this.label9_MouseHover);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(81, 88);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown6.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "T";
            this.label8.MouseHover += new System.EventHandler(this.label8_MouseHover);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(81, 49);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown5.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "p";
            this.label7.MouseHover += new System.EventHandler(this.label7_MouseHover);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(81, 12);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown4.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "L";
            this.label6.MouseHover += new System.EventHandler(this.label6_MouseHover);
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Cross;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(1060, 637);
            this.chart1.TabIndex = 25;
            this.chart1.Text = "chart1";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Linen;
            this.button5.Cursor = System.Windows.Forms.Cursors.Default;
            this.button5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(0, 675);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(1060, 45);
            this.button5.TabIndex = 26;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // BasicSettings
            // 
            this.BasicSettings.Controls.Add(this.panel1);
            this.BasicSettings.Location = new System.Drawing.Point(12, 37);
            this.BasicSettings.Name = "BasicSettings";
            this.BasicSettings.Size = new System.Drawing.Size(235, 327);
            this.BasicSettings.TabIndex = 29;
            this.BasicSettings.TabStop = false;
            this.BasicSettings.Text = "Basic Settings";
            // 
            // Mobility
            // 
            this.Mobility.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Mobility.Controls.Add(this.panel2);
            this.Mobility.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Mobility.Location = new System.Drawing.Point(12, 370);
            this.Mobility.Name = "Mobility";
            this.Mobility.Size = new System.Drawing.Size(235, 217);
            this.Mobility.TabIndex = 30;
            this.Mobility.TabStop = false;
            this.Mobility.Text = "Mobility";
            // 
            // graphpanel
            // 
            this.graphpanel.AutoScroll = true;
            this.graphpanel.Controls.Add(this.savegraphbutton);
            this.graphpanel.Controls.Add(this.chart1);
            this.graphpanel.Controls.Add(this.button5);
            this.graphpanel.Location = new System.Drawing.Point(278, 9);
            this.graphpanel.Name = "graphpanel";
            this.graphpanel.Size = new System.Drawing.Size(1060, 720);
            this.graphpanel.TabIndex = 33;
            // 
            // savegraphbutton
            // 
            this.savegraphbutton.BackColor = System.Drawing.Color.LimeGreen;
            this.savegraphbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savegraphbutton.Location = new System.Drawing.Point(907, 637);
            this.savegraphbutton.Name = "savegraphbutton";
            this.savegraphbutton.Size = new System.Drawing.Size(153, 32);
            this.savegraphbutton.TabIndex = 27;
            this.savegraphbutton.Text = "Save Graph";
            this.savegraphbutton.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.button6);
            this.panel4.Controls.Add(this.button4);
            this.panel4.Controls.Add(this.projectName);
            this.panel4.Controls.Add(this.BasicSettings);
            this.panel4.Controls.Add(this.Mobility);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(260, 729);
            this.panel4.TabIndex = 34;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(134, 593);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(113, 38);
            this.button6.TabIndex = 36;
            this.button6.Text = "Load config";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 593);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(113, 38);
            this.button4.TabIndex = 35;
            this.button4.Text = "Save config";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // projectName
            // 
            this.projectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.projectName.Location = new System.Drawing.Point(12, 8);
            this.projectName.Name = "projectName";
            this.projectName.Size = new System.Drawing.Size(235, 23);
            this.projectName.TabIndex = 33;
            this.projectName.Text = "Project name";
            this.projectName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Linen;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.graphpanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1366, 768);
            this.Name = "Form1";
            this.Text = "Doplnit";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericseconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericsampling)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericcount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericgate)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.BasicSettings.ResumeLayout(false);
            this.BasicSettings.PerformLayout();
            this.Mobility.ResumeLayout(false);
            this.graphpanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numericseconds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericsampling;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox BasicSettings;
        private System.Windows.Forms.GroupBox Mobility;
        private System.Windows.Forms.Panel graphpanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox projectName;
        private System.Windows.Forms.NumericUpDown numericgate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericcount;
        private System.Windows.Forms.CheckBox repeatcountcheckbox;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button savegraphbutton;
    }
}

