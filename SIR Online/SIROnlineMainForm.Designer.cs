namespace SIR_Online
{
    partial class SIROnlineMainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDivingSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureOverlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureIntroPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LogEventGroup = new System.Windows.Forms.GroupBox();
            this.logEventDGV = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LogEvent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Anomaly = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.syncEvent = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ScreenPanel = new System.Windows.Forms.Panel();
            this.ButtonFullScreen = new System.Windows.Forms.Button();
            this.windowsPreview1 = new SIR_Online.WindowsPreview();
            this.windowsPreviewOutput2 = new SIR_Online.WindowsPreviewOutput();
            this.panel1 = new System.Windows.Forms.Panel();
            this.volumeMeter2 = new NAudio.Gui.VolumeMeter();
            this.volumeMeter1 = new NAudio.Gui.VolumeMeter();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MicLevelLProgressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.MicLevelRProgressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.MarkingToolCB = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ShortClipbtn = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.RcdOutBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.strtVidOvlyBtn = new System.Windows.Forms.Button();
            this.labelInvalidInput = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.RecordingBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.LogEventGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logEventDGV)).BeginInit();
            this.ScreenPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.manualHelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1733, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.newDivingSessionToolStripMenuItem,
            this.saveProjectToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.newToolStripMenuItem.Text = "New Project";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.openToolStripMenuItem.Text = "Open Project";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // newDivingSessionToolStripMenuItem
            // 
            this.newDivingSessionToolStripMenuItem.Name = "newDivingSessionToolStripMenuItem";
            this.newDivingSessionToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.newDivingSessionToolStripMenuItem.Text = "Start Diving Session";
            this.newDivingSessionToolStripMenuItem.Click += new System.EventHandler(this.newDivingSessionToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.configureOverlayToolStripMenuItem,
            this.configureIntroPageToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.configureToolStripMenuItem.Text = "Settings";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.configurationToolStripMenuItem.Text = "Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // configureOverlayToolStripMenuItem
            // 
            this.configureOverlayToolStripMenuItem.Name = "configureOverlayToolStripMenuItem";
            this.configureOverlayToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.configureOverlayToolStripMenuItem.Text = "Configure Overlay";
            this.configureOverlayToolStripMenuItem.Click += new System.EventHandler(this.configureOverlayToolStripMenuItem_Click);
            // 
            // configureIntroPageToolStripMenuItem
            // 
            this.configureIntroPageToolStripMenuItem.Name = "configureIntroPageToolStripMenuItem";
            this.configureIntroPageToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.configureIntroPageToolStripMenuItem.Text = "Configure Intro Page";
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.projectToolStripMenuItem.Text = "Project";
            // 
            // manualHelpToolStripMenuItem
            // 
            this.manualHelpToolStripMenuItem.Name = "manualHelpToolStripMenuItem";
            this.manualHelpToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.manualHelpToolStripMenuItem.Text = "Manual/Help";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1329, 619);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Project";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.33353F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.66647F));
            this.tableLayoutPanel1.Controls.Add(this.LogEventGroup, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ScreenPanel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.84363F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.15637F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1323, 600);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // LogEventGroup
            // 
            this.LogEventGroup.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LogEventGroup.Controls.Add(this.logEventDGV);
            this.LogEventGroup.Controls.Add(this.syncEvent);
            this.LogEventGroup.Location = new System.Drawing.Point(166, 484);
            this.LogEventGroup.Name = "LogEventGroup";
            this.LogEventGroup.Size = new System.Drawing.Size(1154, 113);
            this.LogEventGroup.TabIndex = 6;
            this.LogEventGroup.TabStop = false;
            this.LogEventGroup.Text = "Log Event";
            // 
            // logEventDGV
            // 
            this.logEventDGV.AllowUserToDeleteRows = false;
            this.logEventDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.logEventDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logEventDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.LogEvent,
            this.Anomaly,
            this.Comment});
            this.logEventDGV.Location = new System.Drawing.Point(18, 38);
            this.logEventDGV.Name = "logEventDGV";
            this.logEventDGV.ReadOnly = true;
            this.logEventDGV.Size = new System.Drawing.Size(1431, 82);
            this.logEventDGV.TabIndex = 2;
            // 
            // Time
            // 
            this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Time.HeaderText = "Time ";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 200;
            // 
            // LogEvent
            // 
            this.LogEvent.HeaderText = "Event";
            this.LogEvent.Name = "LogEvent";
            this.LogEvent.ReadOnly = true;
            // 
            // Anomaly
            // 
            this.Anomaly.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Anomaly.HeaderText = "Anomaly";
            this.Anomaly.Name = "Anomaly";
            this.Anomaly.ReadOnly = true;
            this.Anomaly.Width = 50;
            // 
            // Comment
            // 
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            // 
            // syncEvent
            // 
            this.syncEvent.Location = new System.Drawing.Point(228, 9);
            this.syncEvent.Name = "syncEvent";
            this.syncEvent.Size = new System.Drawing.Size(75, 23);
            this.syncEvent.TabIndex = 1;
            this.syncEvent.Text = "Sync Eventing";
            this.syncEvent.UseVisualStyleBackColor = true;
            this.syncEvent.Click += new System.EventHandler(this.syncEvent_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(11, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(140, 22);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "Video Functions";
            // 
            // ScreenPanel
            // 
            this.ScreenPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ScreenPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ScreenPanel.Controls.Add(this.ButtonFullScreen);
            this.ScreenPanel.Controls.Add(this.windowsPreview1);
            this.ScreenPanel.Controls.Add(this.windowsPreviewOutput2);
            this.ScreenPanel.Location = new System.Drawing.Point(166, 41);
            this.ScreenPanel.Name = "ScreenPanel";
            this.ScreenPanel.Size = new System.Drawing.Size(1154, 437);
            this.ScreenPanel.TabIndex = 7;
            // 
            // ButtonFullScreen
            // 
            this.ButtonFullScreen.Location = new System.Drawing.Point(537, 9);
            this.ButtonFullScreen.Name = "ButtonFullScreen";
            this.ButtonFullScreen.Size = new System.Drawing.Size(49, 40);
            this.ButtonFullScreen.TabIndex = 2;
            this.ButtonFullScreen.Text = "FullScreen";
            this.ButtonFullScreen.UseVisualStyleBackColor = true;
            this.ButtonFullScreen.Click += new System.EventHandler(this.ButtonFullScreen_Click);
            // 
            // windowsPreview1
            // 
            this.windowsPreview1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.windowsPreview1.BackColor = System.Drawing.Color.Wheat;
            this.windowsPreview1.Location = new System.Drawing.Point(592, -62);
            this.windowsPreview1.Name = "windowsPreview1";
            this.windowsPreview1.Size = new System.Drawing.Size(726, 573);
            this.windowsPreview1.TabIndex = 0;
            this.windowsPreview1.Text = "windowsPreview1";
            this.windowsPreview1.Click += new System.EventHandler(this.windowsPreview1_Click);
            // 
            // windowsPreviewOutput2
            // 
            this.windowsPreviewOutput2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.windowsPreviewOutput2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.windowsPreviewOutput2.IsDrawing = false;
            this.windowsPreviewOutput2.Location = new System.Drawing.Point(-172, -62);
            this.windowsPreviewOutput2.Name = "windowsPreviewOutput2";
            this.windowsPreviewOutput2.Size = new System.Drawing.Size(703, 573);
            this.windowsPreviewOutput2.TabIndex = 1;
            this.windowsPreviewOutput2.Text = "windowsPreviewOutput2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.volumeMeter2);
            this.panel1.Controls.Add(this.volumeMeter1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.MicLevelLProgressBar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.MicLevelRProgressBar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.MarkingToolCB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(163, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 38);
            this.panel1.TabIndex = 10;
            // 
            // volumeMeter2
            // 
            this.volumeMeter2.Amplitude = 0F;
            this.volumeMeter2.Location = new System.Drawing.Point(342, 24);
            this.volumeMeter2.MaxDb = 18F;
            this.volumeMeter2.MinDb = -60F;
            this.volumeMeter2.Name = "volumeMeter2";
            this.volumeMeter2.Size = new System.Drawing.Size(133, 11);
            this.volumeMeter2.TabIndex = 8;
            this.volumeMeter2.Text = "volumeMeter2";
            // 
            // volumeMeter1
            // 
            this.volumeMeter1.Amplitude = 0F;
            this.volumeMeter1.Location = new System.Drawing.Point(342, 7);
            this.volumeMeter1.MaxDb = 18F;
            this.volumeMeter1.MinDb = -60F;
            this.volumeMeter1.Name = "volumeMeter1";
            this.volumeMeter1.Size = new System.Drawing.Size(133, 11);
            this.volumeMeter1.TabIndex = 7;
            this.volumeMeter1.Text = "volumeMeter1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(588, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "L";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(586, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "R";
            // 
            // MicLevelLProgressBar
            // 
            this.MicLevelLProgressBar.ForeColor = System.Drawing.Color.Lime;
            this.MicLevelLProgressBar.Location = new System.Drawing.Point(610, 24);
            this.MicLevelLProgressBar.Name = "MicLevelLProgressBar";
            this.MicLevelLProgressBar.Size = new System.Drawing.Size(248, 10);
            this.MicLevelLProgressBar.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(516, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mic Level";
            // 
            // MicLevelRProgressBar
            // 
            this.MicLevelRProgressBar.ForeColor = System.Drawing.Color.Lime;
            this.MicLevelRProgressBar.Location = new System.Drawing.Point(610, 7);
            this.MicLevelRProgressBar.Name = "MicLevelRProgressBar";
            this.MicLevelRProgressBar.Size = new System.Drawing.Size(248, 11);
            this.MicLevelRProgressBar.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "MarkingTool";
            // 
            // MarkingToolCB
            // 
            this.MarkingToolCB.FormattingEnabled = true;
            this.MarkingToolCB.Location = new System.Drawing.Point(140, 5);
            this.MarkingToolCB.Name = "MarkingToolCB";
            this.MarkingToolCB.Size = new System.Drawing.Size(121, 21);
            this.MarkingToolCB.TabIndex = 0;
            this.MarkingToolCB.SelectedIndexChanged += new System.EventHandler(this.MarkingToolCB_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ShortClipbtn);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.RcdOutBtn);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.RecordingBtn);
            this.panel2.Location = new System.Drawing.Point(3, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(157, 437);
            this.panel2.TabIndex = 11;
            // 
            // ShortClipbtn
            // 
            this.ShortClipbtn.Location = new System.Drawing.Point(33, 345);
            this.ShortClipbtn.Name = "ShortClipbtn";
            this.ShortClipbtn.Size = new System.Drawing.Size(75, 36);
            this.ShortClipbtn.TabIndex = 7;
            this.ShortClipbtn.Text = "Start ShortClip";
            this.ShortClipbtn.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(33, 272);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 40);
            this.button4.TabIndex = 6;
            this.button4.Text = "Snap Shot with Overlay";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // RcdOutBtn
            // 
            this.RcdOutBtn.Location = new System.Drawing.Point(33, 230);
            this.RcdOutBtn.Name = "RcdOutBtn";
            this.RcdOutBtn.Size = new System.Drawing.Size(75, 36);
            this.RcdOutBtn.TabIndex = 5;
            this.RcdOutBtn.Text = "Record Output";
            this.RcdOutBtn.UseVisualStyleBackColor = true;
            this.RcdOutBtn.Click += new System.EventHandler(this.RcdOutBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 568);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "SnapShot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.strtVidOvlyBtn);
            this.groupBox3.Controls.Add(this.labelInvalidInput);
            this.groupBox3.Controls.Add(this.textBox10);
            this.groupBox3.Location = new System.Drawing.Point(1, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(203, 206);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Capture Properties";
            // 
            // strtVidOvlyBtn
            // 
            this.strtVidOvlyBtn.Location = new System.Drawing.Point(32, 120);
            this.strtVidOvlyBtn.Name = "strtVidOvlyBtn";
            this.strtVidOvlyBtn.Size = new System.Drawing.Size(93, 39);
            this.strtVidOvlyBtn.TabIndex = 9;
            this.strtVidOvlyBtn.Text = "Start Video with Overlay";
            this.strtVidOvlyBtn.UseVisualStyleBackColor = true;
            this.strtVidOvlyBtn.Click += new System.EventHandler(this.strtVidOvlyBtn_Click);
            // 
            // labelInvalidInput
            // 
            this.labelInvalidInput.BackColor = System.Drawing.SystemColors.Control;
            this.labelInvalidInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.labelInvalidInput.Location = new System.Drawing.Point(32, 187);
            this.labelInvalidInput.Name = "labelInvalidInput";
            this.labelInvalidInput.Size = new System.Drawing.Size(100, 13);
            this.labelInvalidInput.TabIndex = 7;
            this.labelInvalidInput.Text = "No Valid Input Signal";
            this.labelInvalidInput.TextChanged += new System.EventHandler(this.labelInvalidInput_TextChanged);
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.SystemColors.Control;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Location = new System.Drawing.Point(11, 19);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(100, 13);
            this.textBox10.TabIndex = 1;
            this.textBox10.Text = "Input Device";
            // 
            // RecordingBtn
            // 
            this.RecordingBtn.Location = new System.Drawing.Point(33, 524);
            this.RecordingBtn.Name = "RecordingBtn";
            this.RecordingBtn.Size = new System.Drawing.Size(75, 38);
            this.RecordingBtn.TabIndex = 0;
            this.RecordingBtn.Text = "Record Video";
            this.RecordingBtn.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 484);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Info ";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SIROnlineMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1733, 842);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SIROnlineMainForm";
            this.Text = "SIR Online";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SIROnlineMainForm_FormClosing);
            this.Load += new System.EventHandler(this.SIROnlineMainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SIROnlineMainForm_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.LogEventGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logEventDGV)).EndInit();
            this.ScreenPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualHelpToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button RecordingBtn;
        //private previewWindow previewWindow1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox labelInvalidInput;
        private System.Windows.Forms.TextBox textBox10;
        private WindowsPreview previewWindow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button strtVidOvlyBtn;
        private WindowsPreviewOutput windowsPreviewOutput1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button RcdOutBtn;
        private System.Windows.Forms.Button ShortClipbtn;
        private System.Windows.Forms.ToolStripMenuItem configureOverlayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureIntroPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDivingSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private WindowsPreview windowsPreview1;
        private WindowsPreviewOutput windowsPreviewOutput2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox MarkingToolCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar MicLevelRProgressBar;
        private System.Windows.Forms.ProgressBar MicLevelLProgressBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private NAudio.Gui.VolumeMeter volumeMeter1;
        private NAudio.Gui.VolumeMeter volumeMeter2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox LogEventGroup;
        private System.Windows.Forms.DataGridView logEventDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn LogEvent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Anomaly;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.Button syncEvent;
        private System.Windows.Forms.Panel ScreenPanel;
        private System.Windows.Forms.Button ButtonFullScreen;
    }
}

