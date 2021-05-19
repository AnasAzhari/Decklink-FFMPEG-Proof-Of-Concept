namespace SIR_Online
{
    partial class DataBlockPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataBlockTB = new System.Windows.Forms.TextBox();
            this.DataBlockOrderCB = new System.Windows.Forms.ComboBox();
            this.DataBlockTypeCB = new System.Windows.Forms.ComboBox();
            this.DataBlockLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DataBlockTB
            // 
            this.DataBlockTB.Location = new System.Drawing.Point(51, 4);
            this.DataBlockTB.Name = "DataBlockTB";
            this.DataBlockTB.Size = new System.Drawing.Size(135, 20);
            this.DataBlockTB.TabIndex = 0;
            // 
            // DataBlockOrderCB
            // 
            this.DataBlockOrderCB.FormattingEnabled = true;
            this.DataBlockOrderCB.Location = new System.Drawing.Point(192, 3);
            this.DataBlockOrderCB.Name = "DataBlockOrderCB";
            this.DataBlockOrderCB.Size = new System.Drawing.Size(57, 21);
            this.DataBlockOrderCB.TabIndex = 1;
            // 
            // DataBlockTypeCB
            // 
            this.DataBlockTypeCB.FormattingEnabled = true;
            this.DataBlockTypeCB.Location = new System.Drawing.Point(255, 3);
            this.DataBlockTypeCB.Name = "DataBlockTypeCB";
            this.DataBlockTypeCB.Size = new System.Drawing.Size(121, 21);
            this.DataBlockTypeCB.TabIndex = 2;
            // 
            // DataBlockLbl
            // 
            this.DataBlockLbl.AutoSize = true;
            this.DataBlockLbl.ForeColor = System.Drawing.Color.White;
            this.DataBlockLbl.Location = new System.Drawing.Point(10, 7);
            this.DataBlockLbl.Name = "DataBlockLbl";
            this.DataBlockLbl.Size = new System.Drawing.Size(21, 13);
            this.DataBlockLbl.TabIndex = 3;
            this.DataBlockLbl.Text = "No";
            // 
            // DataBlockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DataBlockLbl);
            this.Controls.Add(this.DataBlockTypeCB);
            this.Controls.Add(this.DataBlockOrderCB);
            this.Controls.Add(this.DataBlockTB);
            this.Name = "DataBlockPanel";
            this.Size = new System.Drawing.Size(383, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DataBlockTB;
        private System.Windows.Forms.ComboBox DataBlockOrderCB;
        private System.Windows.Forms.ComboBox DataBlockTypeCB;
        private System.Windows.Forms.Label DataBlockLbl;
    }
}
