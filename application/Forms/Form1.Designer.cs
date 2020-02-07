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
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericseconds = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.repeatcountcheckbox = new System.Windows.Forms.CheckBox();
            this.numericcount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.moblityCheckBox = new System.Windows.Forms.CheckBox();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.BasicSettings = new System.Windows.Forms.GroupBox();
            this.Mobility = new System.Windows.Forms.GroupBox();
            this.graphpanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.savegraphbutton = new System.Windows.Forms.Button();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.cartesianChartMain = new LiveCharts.Wpf.CartesianChart();
            this.panel4 = new System.Windows.Forms.Panel();
            this.LoadConfigButton = new System.Windows.Forms.Button();
            this.SaveConfigButton = new System.Windows.Forms.Button();
            this.projectName = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttongradient = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericcount)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.BasicSettings.SuspendLayout();
            this.Mobility.SuspendLayout();
            this.graphpanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(203, 616);
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
            this.numericseconds.DecimalPlaces = 2;
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
            this.numericseconds.TabIndex = 2;
            this.numericseconds.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericseconds.ValueChanged += new System.EventHandler(this.SendConfigurationToArduino);
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
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.repeatcountcheckbox);
            this.panel1.Controls.Add(this.numericcount);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numericseconds);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(0, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 275);
            this.panel1.TabIndex = 11;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(136, 85);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(92, 24);
            this.comboBox2.TabIndex = 15;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.SendConfigurationToArduino);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(136, 120);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 24);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.SendConfigurationToArduino);
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
            this.repeatcountcheckbox.CheckedChanged += new System.EventHandler(this.SendConfigurationToArduino);
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
            60,
            0,
            0,
            0});
            this.numericcount.ValueChanged += new System.EventHandler(this.SendConfigurationToArduino);
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.moblityCheckBox);
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(119, 165);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "aplikovať";
            // 
            // moblityCheckBox
            // 
            this.moblityCheckBox.Location = new System.Drawing.Point(200, 164);
            this.moblityCheckBox.Name = "moblityCheckBox";
            this.moblityCheckBox.Size = new System.Drawing.Size(30, 24);
            this.moblityCheckBox.TabIndex = 0;
            this.moblityCheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // numericUpDown7
            // 
            this.numericUpDown7.DecimalPlaces = 2;
            this.numericUpDown7.Location = new System.Drawing.Point(81, 124);
            this.numericUpDown7.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown7.Name = "numericUpDown7";
            this.numericUpDown7.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown7.TabIndex = 8;
            this.numericUpDown7.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericUpDown7.ValueChanged += new System.EventHandler(this.SaveChangeMobilityValues);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 17);
            this.label9.TabIndex = 7;
            this.label9.Text = "U (kV)";
            this.label9.MouseHover += new System.EventHandler(this.label9_MouseHover);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.DecimalPlaces = 2;
            this.numericUpDown6.Location = new System.Drawing.Point(81, 88);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown6.TabIndex = 6;
            this.numericUpDown6.Value = new decimal(new int[] {
            293,
            0,
            0,
            0});
            this.numericUpDown6.ValueChanged += new System.EventHandler(this.SaveChangeMobilityValues);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "T (K)";
            this.label8.MouseHover += new System.EventHandler(this.label8_MouseHover);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.DecimalPlaces = 2;
            this.numericUpDown5.Location = new System.Drawing.Point(81, 49);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown5.TabIndex = 4;
            this.numericUpDown5.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.SaveChangeMobilityValues);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "p (mbar)";
            this.label7.MouseHover += new System.EventHandler(this.label7_MouseHover);
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.DecimalPlaces = 2;
            this.numericUpDown4.Location = new System.Drawing.Point(81, 12);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown4.TabIndex = 2;
            this.numericUpDown4.Value = new decimal(new int[] {
            1305,
            0,
            0,
            131072});
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.SaveChangeMobilityValues);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "L (cm)";
            this.label6.MouseHover += new System.EventHandler(this.label6_MouseHover);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.AutoSize = true;
            this.button5.BackColor = System.Drawing.Color.Linen;
            this.button5.Cursor = System.Windows.Forms.Cursors.Default;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(0, 675);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(1063, 45);
            this.button5.TabIndex = 26;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // BasicSettings
            // 
            this.BasicSettings.Controls.Add(this.panel1);
            this.BasicSettings.Location = new System.Drawing.Point(12, 37);
            this.BasicSettings.Name = "BasicSettings";
            this.BasicSettings.Size = new System.Drawing.Size(235, 298);
            this.BasicSettings.TabIndex = 29;
            this.BasicSettings.TabStop = false;
            this.BasicSettings.Text = "Basic Settings";
            // 
            // Mobility
            // 
            this.Mobility.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Mobility.Controls.Add(this.panel2);
            this.Mobility.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Mobility.Location = new System.Drawing.Point(12, 341);
            this.Mobility.Name = "Mobility";
            this.Mobility.Size = new System.Drawing.Size(235, 217);
            this.Mobility.TabIndex = 30;
            this.Mobility.TabStop = false;
            this.Mobility.Text = "Mobility";
            // 
            // graphpanel
            // 
            this.graphpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphpanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.graphpanel.Controls.Add(this.panel3);
            this.graphpanel.Controls.Add(this.elementHost1);
            this.graphpanel.Controls.Add(this.button5);
            this.graphpanel.Location = new System.Drawing.Point(278, 9);
            this.graphpanel.Name = "graphpanel";
            this.graphpanel.Size = new System.Drawing.Size(1063, 720);
            this.graphpanel.TabIndex = 33;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.buttongradient);
            this.panel3.Controls.Add(this.savegraphbutton);
            this.panel3.Location = new System.Drawing.Point(0, 639);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1063, 38);
            this.panel3.TabIndex = 10;
            // 
            // savegraphbutton
            // 
            this.savegraphbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.savegraphbutton.BackColor = System.Drawing.Color.LimeGreen;
            this.savegraphbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.savegraphbutton.Location = new System.Drawing.Point(910, 0);
            this.savegraphbutton.Name = "savegraphbutton";
            this.savegraphbutton.Size = new System.Drawing.Size(153, 32);
            this.savegraphbutton.TabIndex = 27;
            this.savegraphbutton.Text = "Save Graph";
            this.savegraphbutton.UseVisualStyleBackColor = false;
            this.savegraphbutton.Click += new System.EventHandler(this.savegraphbutton_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(1060, 637);
            this.elementHost1.TabIndex = 35;
            this.elementHost1.Text = "W";
            this.elementHost1.Child = this.cartesianChartMain;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.LoadConfigButton);
            this.panel4.Controls.Add(this.SaveConfigButton);
            this.panel4.Controls.Add(this.projectName);
            this.panel4.Controls.Add(this.BasicSettings);
            this.panel4.Controls.Add(this.Mobility);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(260, 729);
            this.panel4.TabIndex = 34;
            // 
            // LoadConfigButton
            // 
            this.LoadConfigButton.Location = new System.Drawing.Point(134, 564);
            this.LoadConfigButton.Name = "LoadConfigButton";
            this.LoadConfigButton.Size = new System.Drawing.Size(113, 38);
            this.LoadConfigButton.TabIndex = 36;
            this.LoadConfigButton.Text = "Load config";
            this.LoadConfigButton.UseVisualStyleBackColor = true;
            this.LoadConfigButton.Click += new System.EventHandler(this.LoadConfigButton_Click);
            // 
            // SaveConfigButton
            // 
            this.SaveConfigButton.Location = new System.Drawing.Point(12, 564);
            this.SaveConfigButton.Name = "SaveConfigButton";
            this.SaveConfigButton.Size = new System.Drawing.Size(113, 38);
            this.SaveConfigButton.TabIndex = 35;
            this.SaveConfigButton.Text = "Save config";
            this.SaveConfigButton.UseVisualStyleBackColor = true;
            this.SaveConfigButton.Click += new System.EventHandler(this.SaveConfigButton_Click);
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
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // ButtonAndrej
            // 
            this.buttongradient.Location = new System.Drawing.Point(0, 0);
            this.buttongradient.Name = "buttongradient";
            this.buttongradient.Size = new System.Drawing.Size(153, 32);
            this.buttongradient.TabIndex = 28;
            this.buttongradient.Text = "gradient color";
            this.buttongradient.UseVisualStyleBackColor = true;
            this.buttongradient.Click += new System.EventHandler(this.gradientbutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numericseconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericcount)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.BasicSettings.ResumeLayout(false);
            this.BasicSettings.PerformLayout();
            this.Mobility.ResumeLayout(false);
            this.graphpanel.ResumeLayout(false);
            this.graphpanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NumericUpDown numericseconds;
        private System.Windows.Forms.Label label3;
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
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox BasicSettings;
        private System.Windows.Forms.GroupBox Mobility;
        private System.Windows.Forms.Panel graphpanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox projectName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericcount;
        private System.Windows.Forms.CheckBox repeatcountcheckbox;
        private System.Windows.Forms.CheckBox moblityCheckBox;
        private System.Windows.Forms.Button LoadConfigButton;
        private System.Windows.Forms.Button SaveConfigButton;
        private System.Windows.Forms.Button savegraphbutton;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private LiveCharts.Wpf.CartesianChart cartesianChartMain;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttongradient;
    }
}

