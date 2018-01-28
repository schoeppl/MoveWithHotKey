namespace MoveWithHotKey
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lab_plus = new System.Windows.Forms.Label();
            this.lb_ModifierKeys = new System.Windows.Forms.ListBox();
            this.tb_key = new System.Windows.Forms.TextBox();
            this.lab_key = new System.Windows.Forms.Label();
            this.lab_ModifierKeys = new System.Windows.Forms.Label();
            this.but_save = new System.Windows.Forms.Button();
            this.but_load = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rb_rename = new System.Windows.Forms.RadioButton();
            this.rb_overwrite = new System.Windows.Forms.RadioButton();
            this.rb_absolut = new System.Windows.Forms.RadioButton();
            this.rb_relativ = new System.Windows.Forms.RadioButton();
            this.tb_path = new System.Windows.Forms.TextBox();
            this.lab_path = new System.Windows.Forms.Label();
            this.lab_header = new System.Windows.Forms.Label();
            this.lab_tip = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lab_plus);
            this.groupBox1.Controls.Add(this.lb_ModifierKeys);
            this.groupBox1.Controls.Add(this.tb_key);
            this.groupBox1.Controls.Add(this.lab_key);
            this.groupBox1.Controls.Add(this.lab_ModifierKeys);
            this.groupBox1.Location = new System.Drawing.Point(33, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 128);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hotkey Config";
            // 
            // lab_plus
            // 
            this.lab_plus.AutoSize = true;
            this.lab_plus.Location = new System.Drawing.Point(193, 44);
            this.lab_plus.Name = "lab_plus";
            this.lab_plus.Size = new System.Drawing.Size(13, 13);
            this.lab_plus.TabIndex = 12;
            this.lab_plus.Text = "+";
            // 
            // lb_ModifierKeys
            // 
            this.lb_ModifierKeys.FormattingEnabled = true;
            this.lb_ModifierKeys.Location = new System.Drawing.Point(6, 44);
            this.lb_ModifierKeys.Name = "lb_ModifierKeys";
            this.lb_ModifierKeys.Size = new System.Drawing.Size(181, 69);
            this.lb_ModifierKeys.TabIndex = 11;
            // 
            // tb_key
            // 
            this.tb_key.Location = new System.Drawing.Point(212, 44);
            this.tb_key.Name = "tb_key";
            this.tb_key.Size = new System.Drawing.Size(112, 20);
            this.tb_key.TabIndex = 9;
            this.tb_key.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_key_KeyDown);
            this.tb_key.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_key_KeyPress);
            // 
            // lab_key
            // 
            this.lab_key.AutoSize = true;
            this.lab_key.Location = new System.Drawing.Point(209, 22);
            this.lab_key.Name = "lab_key";
            this.lab_key.Size = new System.Drawing.Size(25, 13);
            this.lab_key.TabIndex = 10;
            this.lab_key.Text = "Key";
            // 
            // lab_ModifierKeys
            // 
            this.lab_ModifierKeys.AutoSize = true;
            this.lab_ModifierKeys.Location = new System.Drawing.Point(3, 22);
            this.lab_ModifierKeys.Name = "lab_ModifierKeys";
            this.lab_ModifierKeys.Size = new System.Drawing.Size(67, 13);
            this.lab_ModifierKeys.TabIndex = 9;
            this.lab_ModifierKeys.Text = "ModifierKeys";
            // 
            // but_save
            // 
            this.but_save.Location = new System.Drawing.Point(378, 294);
            this.but_save.Name = "but_save";
            this.but_save.Size = new System.Drawing.Size(75, 23);
            this.but_save.TabIndex = 12;
            this.but_save.Text = "Save";
            this.but_save.UseVisualStyleBackColor = true;
            this.but_save.Click += new System.EventHandler(this.but_save_Click);
            // 
            // but_load
            // 
            this.but_load.Location = new System.Drawing.Point(33, 294);
            this.but_load.Name = "but_load";
            this.but_load.Size = new System.Drawing.Size(75, 23);
            this.but_load.TabIndex = 13;
            this.but_load.Text = "Reload";
            this.but_load.UseVisualStyleBackColor = true;
            this.but_load.Click += new System.EventHandler(this.but_load_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.rb_absolut);
            this.groupBox2.Controls.Add(this.rb_relativ);
            this.groupBox2.Controls.Add(this.tb_path);
            this.groupBox2.Controls.Add(this.lab_path);
            this.groupBox2.Location = new System.Drawing.Point(33, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 100);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Path / File Config";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rb_rename);
            this.groupBox3.Controls.Add(this.rb_overwrite);
            this.groupBox3.Location = new System.Drawing.Point(202, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(207, 51);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "When file already exits?";
            // 
            // rb_rename
            // 
            this.rb_rename.AutoSize = true;
            this.rb_rename.Checked = true;
            this.rb_rename.Enabled = false;
            this.rb_rename.Location = new System.Drawing.Point(92, 23);
            this.rb_rename.Name = "rb_rename";
            this.rb_rename.Size = new System.Drawing.Size(65, 17);
            this.rb_rename.TabIndex = 24;
            this.rb_rename.TabStop = true;
            this.rb_rename.Text = "Rename";
            this.rb_rename.UseVisualStyleBackColor = true;
            // 
            // rb_overwrite
            // 
            this.rb_overwrite.AutoSize = true;
            this.rb_overwrite.Enabled = false;
            this.rb_overwrite.Location = new System.Drawing.Point(16, 23);
            this.rb_overwrite.Name = "rb_overwrite";
            this.rb_overwrite.Size = new System.Drawing.Size(70, 17);
            this.rb_overwrite.TabIndex = 23;
            this.rb_overwrite.Text = "Overwrite";
            this.rb_overwrite.UseVisualStyleBackColor = true;
            // 
            // rb_absolut
            // 
            this.rb_absolut.AutoSize = true;
            this.rb_absolut.Enabled = false;
            this.rb_absolut.Location = new System.Drawing.Point(127, 71);
            this.rb_absolut.Name = "rb_absolut";
            this.rb_absolut.Size = new System.Drawing.Size(60, 17);
            this.rb_absolut.TabIndex = 18;
            this.rb_absolut.Text = "Absolut";
            this.rb_absolut.UseVisualStyleBackColor = true;
            // 
            // rb_relativ
            // 
            this.rb_relativ.AutoSize = true;
            this.rb_relativ.Checked = true;
            this.rb_relativ.Enabled = false;
            this.rb_relativ.Location = new System.Drawing.Point(6, 71);
            this.rb_relativ.Name = "rb_relativ";
            this.rb_relativ.Size = new System.Drawing.Size(58, 17);
            this.rb_relativ.TabIndex = 19;
            this.rb_relativ.TabStop = true;
            this.rb_relativ.Text = "Relativ";
            this.rb_relativ.UseVisualStyleBackColor = true;
            // 
            // tb_path
            // 
            this.tb_path.Location = new System.Drawing.Point(6, 45);
            this.tb_path.Name = "tb_path";
            this.tb_path.Size = new System.Drawing.Size(181, 20);
            this.tb_path.TabIndex = 16;
            // 
            // lab_path
            // 
            this.lab_path.AutoSize = true;
            this.lab_path.Location = new System.Drawing.Point(3, 23);
            this.lab_path.Name = "lab_path";
            this.lab_path.Size = new System.Drawing.Size(69, 13);
            this.lab_path.TabIndex = 17;
            this.lab_path.Text = "Path / Folder";
            // 
            // lab_header
            // 
            this.lab_header.AutoSize = true;
            this.lab_header.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_header.Location = new System.Drawing.Point(12, 9);
            this.lab_header.Name = "lab_header";
            this.lab_header.Size = new System.Drawing.Size(140, 25);
            this.lab_header.TabIndex = 14;
            this.lab_header.Text = "Configuration";
            // 
            // lab_tip
            // 
            this.lab_tip.AutoSize = true;
            this.lab_tip.Location = new System.Drawing.Point(211, 18);
            this.lab_tip.Name = "lab_tip";
            this.lab_tip.Size = new System.Drawing.Size(242, 13);
            this.lab_tip.TabIndex = 15;
            this.lab_tip.Text = "Minimize the config window to aktivate the hotkey";
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 323);
            this.Controls.Add(this.lab_tip);
            this.Controls.Add(this.lab_header);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.but_load);
            this.Controls.Add(this.but_save);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigurationForm";
            this.Text = "Move to Folder with HotKey";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationForm_FormClosing);
            this.Resize += new System.EventHandler(this.ConfigurationForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lb_ModifierKeys;
        private System.Windows.Forms.TextBox tb_key;
        private System.Windows.Forms.Label lab_key;
        private System.Windows.Forms.Label lab_ModifierKeys;
        private System.Windows.Forms.Button but_load;
        private System.Windows.Forms.Button but_save;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rb_rename;
        private System.Windows.Forms.RadioButton rb_overwrite;
        private System.Windows.Forms.RadioButton rb_absolut;
        private System.Windows.Forms.RadioButton rb_relativ;
        private System.Windows.Forms.TextBox tb_path;
        private System.Windows.Forms.Label lab_path;
        private System.Windows.Forms.Label lab_plus;
        private System.Windows.Forms.Label lab_header;
        private System.Windows.Forms.Label lab_tip;
    }
}

