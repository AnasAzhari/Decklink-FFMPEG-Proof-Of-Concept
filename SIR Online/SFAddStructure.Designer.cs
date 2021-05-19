namespace SIR_Online
{
    partial class form_add_structure
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
            this.borderLayout_add_structure = new Syncfusion.Windows.Forms.Tools.BorderLayout(this.components);
            this.lbl_structure_name = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txt_structure_name = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.radio_air_diving = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radio_sat_diving = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.radio_rov = new Syncfusion.Windows.Forms.Tools.RadioButtonAdv();
            this.btn_create_add_structure = new Syncfusion.Windows.Forms.ButtonAdv();
            this.btn_cancel = new Syncfusion.Windows.Forms.ButtonAdv();
            ((System.ComponentModel.ISupportInitialize)(this.borderLayout_add_structure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_structure_name)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_air_diving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_sat_diving)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_rov)).BeginInit();
            this.SuspendLayout();
            // 
            // borderLayout_add_structure
            // 
            this.borderLayout_add_structure.ContainerControl = this;
            this.borderLayout_add_structure.HGap = 0;
            this.borderLayout_add_structure.VGap = 0;
            // 
            // lbl_structure_name
            // 
            this.lbl_structure_name.DX = -75;
            this.lbl_structure_name.DY = 3;
            this.lbl_structure_name.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_structure_name.LabeledControl = this.txt_structure_name;
            this.lbl_structure_name.Location = new System.Drawing.Point(36, 43);
            this.lbl_structure_name.Name = "lbl_structure_name";
            this.lbl_structure_name.Size = new System.Drawing.Size(71, 16);
            this.lbl_structure_name.TabIndex = 10;
            this.lbl_structure_name.Text = "Structure";
            // 
            // txt_structure_name
            // 
            this.txt_structure_name.BeforeTouchSize = new System.Drawing.Size(263, 23);
            this.txt_structure_name.Enabled = false;
            this.txt_structure_name.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_structure_name.Location = new System.Drawing.Point(111, 40);
            this.txt_structure_name.Metrocolor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.borderLayout_add_structure.SetMinimumSize(this.txt_structure_name, new System.Drawing.Size(263, 23));
            this.txt_structure_name.Name = "txt_structure_name";
            this.borderLayout_add_structure.SetPreferredSize(this.txt_structure_name, new System.Drawing.Size(263, 23));
            this.txt_structure_name.Size = new System.Drawing.Size(263, 23);
            this.txt_structure_name.TabIndex = 9;
            this.txt_structure_name.TextChanged += new System.EventHandler(this.txt_project_name_TextChanged);
            // 
            // radio_air_diving
            // 
            this.radio_air_diving.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radio_air_diving.BeforeTouchSize = new System.Drawing.Size(100, 21);
            this.radio_air_diving.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.radio_air_diving.Checked = true;
            this.radio_air_diving.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_air_diving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.radio_air_diving.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.radio_air_diving.Location = new System.Drawing.Point(111, 86);
            this.radio_air_diving.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.borderLayout_add_structure.SetMinimumSize(this.radio_air_diving, new System.Drawing.Size(100, 21));
            this.radio_air_diving.Name = "radio_air_diving";
            this.borderLayout_add_structure.SetPreferredSize(this.radio_air_diving, new System.Drawing.Size(100, 21));
            this.radio_air_diving.Size = new System.Drawing.Size(100, 21);
            this.radio_air_diving.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2016White;
            this.radio_air_diving.TabIndex = 11;
            this.radio_air_diving.Text = "Air Diving";
            this.radio_air_diving.ThemesEnabled = false;
            this.radio_air_diving.CheckChanged += new System.EventHandler(this.radio_structure_type_CheckChanged);
            // 
            // radio_sat_diving
            // 
            this.radio_sat_diving.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radio_sat_diving.BeforeTouchSize = new System.Drawing.Size(101, 21);
            this.radio_sat_diving.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.radio_sat_diving.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_sat_diving.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.radio_sat_diving.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.radio_sat_diving.Location = new System.Drawing.Point(217, 86);
            this.radio_sat_diving.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.borderLayout_add_structure.SetMinimumSize(this.radio_sat_diving, new System.Drawing.Size(101, 21));
            this.radio_sat_diving.Name = "radio_sat_diving";
            this.borderLayout_add_structure.SetPreferredSize(this.radio_sat_diving, new System.Drawing.Size(101, 21));
            this.radio_sat_diving.Size = new System.Drawing.Size(101, 21);
            this.radio_sat_diving.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2016White;
            this.radio_sat_diving.TabIndex = 12;
            this.radio_sat_diving.TabStop = false;
            this.radio_sat_diving.Text = "Sat Diving";
            this.radio_sat_diving.ThemesEnabled = false;
            this.radio_sat_diving.CheckChanged += new System.EventHandler(this.radioButtonAdv1_CheckChanged_1);
            // 
            // radio_rov
            // 
            this.radio_rov.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.radio_rov.BeforeTouchSize = new System.Drawing.Size(59, 21);
            this.radio_rov.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.radio_rov.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_rov.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.radio_rov.HotBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.radio_rov.Location = new System.Drawing.Point(324, 86);
            this.radio_rov.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(89)))), ((int)(((byte)(91)))));
            this.borderLayout_add_structure.SetMinimumSize(this.radio_rov, new System.Drawing.Size(59, 21));
            this.radio_rov.Name = "radio_rov";
            this.borderLayout_add_structure.SetPreferredSize(this.radio_rov, new System.Drawing.Size(59, 21));
            this.radio_rov.Size = new System.Drawing.Size(59, 21);
            this.radio_rov.Style = Syncfusion.Windows.Forms.Tools.RadioButtonAdvStyle.Office2016White;
            this.radio_rov.TabIndex = 13;
            this.radio_rov.TabStop = false;
            this.radio_rov.Text = "ROV";
            this.radio_rov.ThemesEnabled = false;
            // 
            // btn_create_add_structure
            // 
            this.btn_create_add_structure.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2016White;
            this.btn_create_add_structure.BeforeTouchSize = new System.Drawing.Size(96, 35);
            this.btn_create_add_structure.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_create_add_structure.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_create_add_structure.IsBackStageButton = false;
            this.btn_create_add_structure.Location = new System.Drawing.Point(278, 136);
            this.btn_create_add_structure.Name = "btn_create_add_structure";
            this.btn_create_add_structure.Size = new System.Drawing.Size(96, 35);
            this.btn_create_add_structure.TabIndex = 38;
            this.btn_create_add_structure.Text = "Add";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Appearance = Syncfusion.Windows.Forms.ButtonAppearance.Office2016White;
            this.btn_cancel.BeforeTouchSize = new System.Drawing.Size(96, 35);
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_cancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.IsBackStageButton = false;
            this.btn_cancel.Location = new System.Drawing.Point(166, 136);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(96, 35);
            this.btn_cancel.TabIndex = 39;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Click += new System.EventHandler(this.buttonAdv1_Click);
            // 
            // form_add_structure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(457, 202);
            this.Controls.Add(this.txt_structure_name);
            this.Controls.Add(this.radio_air_diving);
            this.Controls.Add(this.radio_sat_diving);
            this.Controls.Add(this.radio_rov);
            this.Controls.Add(this.lbl_structure_name);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_create_add_structure);
            this.Name = "form_add_structure";
            this.Text = "Add Structure";
            ((System.ComponentModel.ISupportInitialize)(this.borderLayout_add_structure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_structure_name)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_air_diving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_sat_diving)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radio_rov)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.BorderLayout borderLayout_add_structure;
        private Syncfusion.Windows.Forms.Tools.AutoLabel lbl_structure_name;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txt_structure_name;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radio_air_diving;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radio_sat_diving;
        private Syncfusion.Windows.Forms.Tools.RadioButtonAdv radio_rov;
        private Syncfusion.Windows.Forms.ButtonAdv btn_cancel;
        private Syncfusion.Windows.Forms.ButtonAdv btn_create_add_structure;
    }
}