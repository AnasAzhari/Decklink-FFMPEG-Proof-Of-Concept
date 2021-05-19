namespace SIR_Online
{
    partial class StartDivingSession
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentProjectLbl = new System.Windows.Forms.Label();
            this.StructureList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(91, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Project";
            // 
            // CurrentProjectLbl
            // 
            this.CurrentProjectLbl.AutoSize = true;
            this.CurrentProjectLbl.Location = new System.Drawing.Point(88, 31);
            this.CurrentProjectLbl.Name = "CurrentProjectLbl";
            this.CurrentProjectLbl.Size = new System.Drawing.Size(69, 13);
            this.CurrentProjectLbl.TabIndex = 2;
            this.CurrentProjectLbl.Text = "Project name";
            // 
            // StructureList
            // 
            this.StructureList.FormattingEnabled = true;
            this.StructureList.Location = new System.Drawing.Point(91, 76);
            this.StructureList.Name = "StructureList";
            this.StructureList.Size = new System.Drawing.Size(121, 21);
            this.StructureList.TabIndex = 3;
            this.StructureList.SelectedIndexChanged += new System.EventHandler(this.StructureList_SelectedIndexChanged);
            // 
            // StartDivingSession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 180);
            this.Controls.Add(this.StructureList);
            this.Controls.Add(this.CurrentProjectLbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "StartDivingSession";
            this.Text = "StartDivingSession";
            this.Shown += new System.EventHandler(this.StartDivingSession_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CurrentProjectLbl;
        private System.Windows.Forms.ComboBox StructureList;
    }
}