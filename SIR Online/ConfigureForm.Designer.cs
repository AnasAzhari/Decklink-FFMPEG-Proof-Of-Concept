namespace SIR_Online
{
    partial class ConfigureForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ConfigPanel = new System.Windows.Forms.Panel();
            this.DataBlockGB = new System.Windows.Forms.GroupBox();
            this.ApplyDataBlockBtn = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.IsCommaChkBox = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.CaptureOptionGroupbox = new System.Windows.Forms.GroupBox();
            this.RecordingGB = new System.Windows.Forms.GroupBox();
            this.DataStringGB = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.northingLbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.eastingLbl = new System.Windows.Forms.Label();
            this.kpsurveyLbl = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TimeSurveyLbl = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.IsPortOpenLbl = new System.Windows.Forms.Label();
            this.OpenPortButton = new System.Windows.Forms.Button();
            this.StopBitsCB = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DataBitsCB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ParityCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BaudRateCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.COMportCB = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.CutOffCB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboBoxFullDuplexDevice = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxCapturePixelFormat = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCaptureVideoMode = new System.Windows.Forms.ComboBox();
            this.checkBoxEnableFormatDetection = new System.Windows.Forms.CheckBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.ConfigPanel.SuspendLayout();
            this.DataBlockGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.CaptureOptionGroupbox.SuspendLayout();
            this.RecordingGB.SuspendLayout();
            this.DataStringGB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ConfigPanel, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 601);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "Capture & Playback",
            "Data String",
            "Audio Setting",
            "Recording",
            "Survey Data String"});
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(126, 595);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // ConfigPanel
            // 
            this.ConfigPanel.Controls.Add(this.CaptureOptionGroupbox);
            this.ConfigPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigPanel.Location = new System.Drawing.Point(135, 3);
            this.ConfigPanel.Name = "ConfigPanel";
            this.ConfigPanel.Size = new System.Drawing.Size(746, 595);
            this.ConfigPanel.TabIndex = 1;
            this.ConfigPanel.Visible = false;
            // 
            // DataBlockGB
            // 
            this.DataBlockGB.Controls.Add(this.ApplyDataBlockBtn);
            this.DataBlockGB.Controls.Add(this.label18);
            this.DataBlockGB.Controls.Add(this.label17);
            this.DataBlockGB.Controls.Add(this.label16);
            this.DataBlockGB.Controls.Add(this.label15);
            this.DataBlockGB.Controls.Add(this.flowLayoutPanel1);
            this.DataBlockGB.Controls.Add(this.IsCommaChkBox);
            this.DataBlockGB.Controls.Add(this.numericUpDown1);
            this.DataBlockGB.Controls.Add(this.label12);
            this.DataBlockGB.Location = new System.Drawing.Point(45, 56);
            this.DataBlockGB.Name = "DataBlockGB";
            this.DataBlockGB.Size = new System.Drawing.Size(677, 589);
            this.DataBlockGB.TabIndex = 13;
            this.DataBlockGB.TabStop = false;
            this.DataBlockGB.Text = "Data String setting";
            // 
            // ApplyDataBlockBtn
            // 
            this.ApplyDataBlockBtn.Location = new System.Drawing.Point(24, 516);
            this.ApplyDataBlockBtn.Name = "ApplyDataBlockBtn";
            this.ApplyDataBlockBtn.Size = new System.Drawing.Size(75, 23);
            this.ApplyDataBlockBtn.TabIndex = 10;
            this.ApplyDataBlockBtn.Text = "Apply";
            this.ApplyDataBlockBtn.UseVisualStyleBackColor = true;
            this.ApplyDataBlockBtn.Click += new System.EventHandler(this.ApplyDataBlockBtn_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(285, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 9;
            this.label18.Text = "Type";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(208, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 13);
            this.label17.TabIndex = 8;
            this.label17.Text = "Order";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(79, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "Name";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(36, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(21, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "No";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(24, 72);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(389, 421);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // IsCommaChkBox
            // 
            this.IsCommaChkBox.AutoSize = true;
            this.IsCommaChkBox.Checked = true;
            this.IsCommaChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsCommaChkBox.Location = new System.Drawing.Point(236, 26);
            this.IsCommaChkBox.Name = "IsCommaChkBox";
            this.IsCommaChkBox.Size = new System.Drawing.Size(113, 17);
            this.IsCommaChkBox.TabIndex = 4;
            this.IsCommaChkBox.Text = "Comma Separated";
            this.IsCommaChkBox.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(153, 23);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(38, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "String Data block number";
            // 
            // CaptureOptionGroupbox
            // 
            this.CaptureOptionGroupbox.Controls.Add(this.DataBlockGB);
            this.CaptureOptionGroupbox.Controls.Add(this.RecordingGB);
            this.CaptureOptionGroupbox.Controls.Add(this.label5);
            this.CaptureOptionGroupbox.Controls.Add(this.ComboBoxFullDuplexDevice);
            this.CaptureOptionGroupbox.Controls.Add(this.label4);
            this.CaptureOptionGroupbox.Controls.Add(this.comboBoxCapturePixelFormat);
            this.CaptureOptionGroupbox.Controls.Add(this.label2);
            this.CaptureOptionGroupbox.Controls.Add(this.comboBoxCaptureVideoMode);
            this.CaptureOptionGroupbox.Controls.Add(this.checkBoxEnableFormatDetection);
            this.CaptureOptionGroupbox.Location = new System.Drawing.Point(21, 3);
            this.CaptureOptionGroupbox.Name = "CaptureOptionGroupbox";
            this.CaptureOptionGroupbox.Size = new System.Drawing.Size(631, 419);
            this.CaptureOptionGroupbox.TabIndex = 0;
            this.CaptureOptionGroupbox.TabStop = false;
            this.CaptureOptionGroupbox.Text = "Capture Options";
            // 
            // RecordingGB
            // 
            this.RecordingGB.Controls.Add(this.DataStringGB);
            this.RecordingGB.Controls.Add(this.label13);
            this.RecordingGB.Controls.Add(this.CutOffCB);
            this.RecordingGB.Location = new System.Drawing.Point(51, 19);
            this.RecordingGB.Name = "RecordingGB";
            this.RecordingGB.Size = new System.Drawing.Size(786, 583);
            this.RecordingGB.TabIndex = 4;
            this.RecordingGB.TabStop = false;
            this.RecordingGB.Text = "Recording Settings";
            // 
            // DataStringGB
            // 
            this.DataStringGB.Controls.Add(this.groupBox1);
            this.DataStringGB.Controls.Add(this.IsPortOpenLbl);
            this.DataStringGB.Controls.Add(this.OpenPortButton);
            this.DataStringGB.Controls.Add(this.StopBitsCB);
            this.DataStringGB.Controls.Add(this.label8);
            this.DataStringGB.Controls.Add(this.DataBitsCB);
            this.DataStringGB.Controls.Add(this.label7);
            this.DataStringGB.Controls.Add(this.label6);
            this.DataStringGB.Controls.Add(this.ParityCB);
            this.DataStringGB.Controls.Add(this.label3);
            this.DataStringGB.Controls.Add(this.BaudRateCB);
            this.DataStringGB.Controls.Add(this.label1);
            this.DataStringGB.Controls.Add(this.COMportCB);
            this.DataStringGB.Location = new System.Drawing.Point(6, 0);
            this.DataStringGB.Name = "DataStringGB";
            this.DataStringGB.Size = new System.Drawing.Size(499, 525);
            this.DataStringGB.TabIndex = 1;
            this.DataStringGB.TabStop = false;
            this.DataStringGB.Text = "Data String setting";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.northingLbl);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.eastingLbl);
            this.groupBox1.Controls.Add(this.kpsurveyLbl);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.TimeSurveyLbl);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(258, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 142);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // northingLbl
            // 
            this.northingLbl.AutoSize = true;
            this.northingLbl.Location = new System.Drawing.Point(131, 100);
            this.northingLbl.Name = "northingLbl";
            this.northingLbl.Size = new System.Drawing.Size(19, 13);
            this.northingLbl.TabIndex = 19;
            this.northingLbl.Text = "00";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(49, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Northing :";
            // 
            // eastingLbl
            // 
            this.eastingLbl.AutoSize = true;
            this.eastingLbl.Location = new System.Drawing.Point(131, 75);
            this.eastingLbl.Name = "eastingLbl";
            this.eastingLbl.Size = new System.Drawing.Size(19, 13);
            this.eastingLbl.TabIndex = 17;
            this.eastingLbl.Text = "00";
            // 
            // kpsurveyLbl
            // 
            this.kpsurveyLbl.AutoSize = true;
            this.kpsurveyLbl.Location = new System.Drawing.Point(131, 51);
            this.kpsurveyLbl.Name = "kpsurveyLbl";
            this.kpsurveyLbl.Size = new System.Drawing.Size(19, 13);
            this.kpsurveyLbl.TabIndex = 16;
            this.kpsurveyLbl.Text = "00";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(54, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Easting :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Kilometer Pipeline :";
            // 
            // TimeSurveyLbl
            // 
            this.TimeSurveyLbl.AutoSize = true;
            this.TimeSurveyLbl.Location = new System.Drawing.Point(131, 24);
            this.TimeSurveyLbl.Name = "TimeSurveyLbl";
            this.TimeSurveyLbl.Size = new System.Drawing.Size(19, 13);
            this.TimeSurveyLbl.TabIndex = 13;
            this.TimeSurveyLbl.Text = "00";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(66, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Time :";
            // 
            // IsPortOpenLbl
            // 
            this.IsPortOpenLbl.AutoSize = true;
            this.IsPortOpenLbl.Location = new System.Drawing.Point(87, 274);
            this.IsPortOpenLbl.Name = "IsPortOpenLbl";
            this.IsPortOpenLbl.Size = new System.Drawing.Size(84, 13);
            this.IsPortOpenLbl.TabIndex = 11;
            this.IsPortOpenLbl.Text = "Port Is Not open";
            // 
            // OpenPortButton
            // 
            this.OpenPortButton.Location = new System.Drawing.Point(90, 235);
            this.OpenPortButton.Name = "OpenPortButton";
            this.OpenPortButton.Size = new System.Drawing.Size(75, 23);
            this.OpenPortButton.TabIndex = 10;
            this.OpenPortButton.Text = "Open Port";
            this.OpenPortButton.UseVisualStyleBackColor = true;
            this.OpenPortButton.Click += new System.EventHandler(this.OpenPortButton_Click);
            // 
            // StopBitsCB
            // 
            this.StopBitsCB.FormattingEnabled = true;
            this.StopBitsCB.Location = new System.Drawing.Point(90, 192);
            this.StopBitsCB.Name = "StopBitsCB";
            this.StopBitsCB.Size = new System.Drawing.Size(121, 21);
            this.StopBitsCB.TabIndex = 9;
            this.StopBitsCB.SelectedIndexChanged += new System.EventHandler(this.StopBitsCB_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Stop Bits";
            // 
            // DataBitsCB
            // 
            this.DataBitsCB.FormattingEnabled = true;
            this.DataBitsCB.Location = new System.Drawing.Point(90, 152);
            this.DataBitsCB.Name = "DataBitsCB";
            this.DataBitsCB.Size = new System.Drawing.Size(121, 21);
            this.DataBitsCB.TabIndex = 7;
            this.DataBitsCB.SelectedIndexChanged += new System.EventHandler(this.DataBitsCB_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 155);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Data bits";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Parity";
            // 
            // ParityCB
            // 
            this.ParityCB.FormattingEnabled = true;
            this.ParityCB.Location = new System.Drawing.Point(90, 114);
            this.ParityCB.Name = "ParityCB";
            this.ParityCB.Size = new System.Drawing.Size(121, 21);
            this.ParityCB.TabIndex = 4;
            this.ParityCB.SelectedIndexChanged += new System.EventHandler(this.ParityCB_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Baud Rate";
            // 
            // BaudRateCB
            // 
            this.BaudRateCB.FormattingEnabled = true;
            this.BaudRateCB.Location = new System.Drawing.Point(90, 74);
            this.BaudRateCB.Name = "BaudRateCB";
            this.BaudRateCB.Size = new System.Drawing.Size(121, 21);
            this.BaudRateCB.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "COM Port";
            // 
            // COMportCB
            // 
            this.COMportCB.FormattingEnabled = true;
            this.COMportCB.Location = new System.Drawing.Point(90, 28);
            this.COMportCB.Name = "COMportCB";
            this.COMportCB.Size = new System.Drawing.Size(121, 21);
            this.COMportCB.TabIndex = 0;
            this.COMportCB.SelectedIndexChanged += new System.EventHandler(this.COMportCB_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "CutOff Duration";
            // 
            // CutOffCB
            // 
            this.CutOffCB.FormattingEnabled = true;
            this.CutOffCB.Location = new System.Drawing.Point(109, 37);
            this.CutOffCB.Name = "CutOffCB";
            this.CutOffCB.Size = new System.Drawing.Size(121, 21);
            this.CutOffCB.TabIndex = 0;
            this.CutOffCB.SelectedIndexChanged += new System.EventHandler(this.CutOffCB_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = " Device";
            // 
            // ComboBoxFullDuplexDevice
            // 
            this.ComboBoxFullDuplexDevice.FormattingEnabled = true;
            this.ComboBoxFullDuplexDevice.Location = new System.Drawing.Point(51, 44);
            this.ComboBoxFullDuplexDevice.Name = "ComboBoxFullDuplexDevice";
            this.ComboBoxFullDuplexDevice.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxFullDuplexDevice.TabIndex = 12;
            this.ComboBoxFullDuplexDevice.SelectedIndexChanged += new System.EventHandler(this.ComboBoxFullDuplexDevice_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Pixel Format";
            // 
            // comboBoxCapturePixelFormat
            // 
            this.comboBoxCapturePixelFormat.FormattingEnabled = true;
            this.comboBoxCapturePixelFormat.Location = new System.Drawing.Point(51, 232);
            this.comboBoxCapturePixelFormat.Name = "comboBoxCapturePixelFormat";
            this.comboBoxCapturePixelFormat.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCapturePixelFormat.TabIndex = 10;
            this.comboBoxCapturePixelFormat.SelectedIndexChanged += new System.EventHandler(this.comboBoxCapturePixelFormat_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Video Mode";
            // 
            // comboBoxCaptureVideoMode
            // 
            this.comboBoxCaptureVideoMode.FormattingEnabled = true;
            this.comboBoxCaptureVideoMode.Location = new System.Drawing.Point(51, 167);
            this.comboBoxCaptureVideoMode.Name = "comboBoxCaptureVideoMode";
            this.comboBoxCaptureVideoMode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCaptureVideoMode.TabIndex = 8;
            this.comboBoxCaptureVideoMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxCaptureVideoMode_SelectedIndexChanged);
            // 
            // checkBoxEnableFormatDetection
            // 
            this.checkBoxEnableFormatDetection.AutoSize = true;
            this.checkBoxEnableFormatDetection.Location = new System.Drawing.Point(51, 71);
            this.checkBoxEnableFormatDetection.Name = "checkBoxEnableFormatDetection";
            this.checkBoxEnableFormatDetection.Size = new System.Drawing.Size(93, 17);
            this.checkBoxEnableFormatDetection.TabIndex = 4;
            this.checkBoxEnableFormatDetection.Text = "Detect Format";
            this.checkBoxEnableFormatDetection.UseVisualStyleBackColor = true;
            this.checkBoxEnableFormatDetection.CheckedChanged += new System.EventHandler(this.checkBoxEnableFormatDetection_CheckedChanged);
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 616);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConfigureForm";
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigureForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ConfigPanel.ResumeLayout(false);
            this.DataBlockGB.ResumeLayout(false);
            this.DataBlockGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.CaptureOptionGroupbox.ResumeLayout(false);
            this.CaptureOptionGroupbox.PerformLayout();
            this.RecordingGB.ResumeLayout(false);
            this.RecordingGB.PerformLayout();
            this.DataStringGB.ResumeLayout(false);
            this.DataStringGB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel ConfigPanel;
        private System.Windows.Forms.GroupBox CaptureOptionGroupbox;
        private System.Windows.Forms.CheckBox checkBoxEnableFormatDetection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxCaptureVideoMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxCapturePixelFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ComboBoxFullDuplexDevice;
        private System.Windows.Forms.GroupBox DataStringGB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox COMportCB;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ParityCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox BaudRateCB;
        private System.Windows.Forms.ComboBox DataBitsCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label IsPortOpenLbl;
        private System.Windows.Forms.Button OpenPortButton;
        private System.Windows.Forms.ComboBox StopBitsCB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label northingLbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label eastingLbl;
        private System.Windows.Forms.Label kpsurveyLbl;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label TimeSurveyLbl;
        private System.Windows.Forms.GroupBox RecordingGB;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox CutOffCB;
        private System.Windows.Forms.GroupBox DataBlockGB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox IsCommaChkBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button ApplyDataBlockBtn;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}