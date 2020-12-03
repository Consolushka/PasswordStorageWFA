namespace WindowsFormsApp2
{
    partial class main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.loginTB = new System.Windows.Forms.TextBox();
            this.accessPassCopy = new System.Windows.Forms.TextBox();
            this.accessPass = new System.Windows.Forms.TextBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.GetAccDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addAccMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeAccDataMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AccPasswordStorageMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Repeat The Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Pick a Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Enter Login";
            // 
            // loginTB
            // 
            this.loginTB.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loginTB.Location = new System.Drawing.Point(236, 48);
            this.loginTB.Name = "loginTB";
            this.loginTB.Size = new System.Drawing.Size(241, 31);
            this.loginTB.TabIndex = 10;
            this.loginTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // accessPassCopy
            // 
            this.accessPassCopy.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accessPassCopy.Location = new System.Drawing.Point(236, 118);
            this.accessPassCopy.Name = "accessPassCopy";
            this.accessPassCopy.Size = new System.Drawing.Size(241, 28);
            this.accessPassCopy.TabIndex = 12;
            this.accessPassCopy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // accessPass
            // 
            this.accessPass.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accessPass.Location = new System.Drawing.Point(236, 82);
            this.accessPass.Name = "accessPass";
            this.accessPass.Size = new System.Drawing.Size(241, 28);
            this.accessPass.TabIndex = 11;
            this.accessPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // menu
            // 
            this.menu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GetAccDataMenu,
            this.addAccMenu,
            this.ChangeAccDataMenu,
            this.AccPasswordStorageMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(713, 24);
            this.menu.TabIndex = 13;
            this.menu.Text = "menuStrip1";
            // 
            // GetAccDataMenu
            // 
            this.GetAccDataMenu.Name = "GetAccDataMenu";
            this.GetAccDataMenu.Size = new System.Drawing.Size(109, 20);
            this.GetAccDataMenu.Text = "Get Account Data";
            this.GetAccDataMenu.Click += new System.EventHandler(this.GetAccDataMenu_Click);
            // 
            // addAccMenu
            // 
            this.addAccMenu.Name = "addAccMenu";
            this.addAccMenu.Size = new System.Drawing.Size(85, 20);
            this.addAccMenu.Text = "Add Account";
            this.addAccMenu.Click += new System.EventHandler(this.addAccMenu_Click);
            // 
            // ChangeAccDataMenu
            // 
            this.ChangeAccDataMenu.Name = "ChangeAccDataMenu";
            this.ChangeAccDataMenu.Size = new System.Drawing.Size(131, 20);
            this.ChangeAccDataMenu.Text = "Change Account Data";
            this.ChangeAccDataMenu.Click += new System.EventHandler(this.ChangeAccDataMenu_Click);
            // 
            // AccPasswordStorageMenu
            // 
            this.AccPasswordStorageMenu.Name = "AccPasswordStorageMenu";
            this.AccPasswordStorageMenu.Size = new System.Drawing.Size(217, 20);
            this.AccPasswordStorageMenu.Text = "Change Password of PasswordStorage";
            this.AccPasswordStorageMenu.Click += new System.EventHandler(this.AccPasswordStorageMenu_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 195);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.loginTB);
            this.Controls.Add(this.accessPassCopy);
            this.Controls.Add(this.accessPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.Text = "Entry to Password Storage";
            this.Load += new System.EventHandler(this.main_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox loginTB;
        private System.Windows.Forms.TextBox accessPassCopy;
        private System.Windows.Forms.TextBox accessPass;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem GetAccDataMenu;
        private System.Windows.Forms.ToolStripMenuItem addAccMenu;
        private System.Windows.Forms.ToolStripMenuItem ChangeAccDataMenu;
        private System.Windows.Forms.ToolStripMenuItem AccPasswordStorageMenu;
    }
}

