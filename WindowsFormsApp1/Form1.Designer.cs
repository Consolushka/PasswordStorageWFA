namespace WindowsFormsApp1
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.получитьДанныеАккаунтаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьАккаунтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьДанныеАккаунтаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accessPass = new System.Windows.Forms.TextBox();
            this.accessPassCopy = new System.Windows.Forms.TextBox();
            this.учетнаяЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.получитьДанныеАккаунтаToolStripMenuItem,
            this.добавитьАккаунтToolStripMenuItem,
            this.изменитьДанныеАккаунтаToolStripMenuItem,
            this.учетнаяЗаписьToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(720, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // получитьДанныеАккаунтаToolStripMenuItem
            // 
            this.получитьДанныеАккаунтаToolStripMenuItem.Name = "получитьДанныеАккаунтаToolStripMenuItem";
            this.получитьДанныеАккаунтаToolStripMenuItem.Size = new System.Drawing.Size(172, 20);
            this.получитьДанныеАккаунтаToolStripMenuItem.Text = "Получить Данные Аккаунта";
            this.получитьДанныеАккаунтаToolStripMenuItem.Click += new System.EventHandler(this.получитьДанныеАккаунтаToolStripMenuItem_Click);
            // 
            // добавитьАккаунтToolStripMenuItem
            // 
            this.добавитьАккаунтToolStripMenuItem.Name = "добавитьАккаунтToolStripMenuItem";
            this.добавитьАккаунтToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.добавитьАккаунтToolStripMenuItem.Text = "Добавить Аккаунт";
            this.добавитьАккаунтToolStripMenuItem.Click += new System.EventHandler(this.добавитьАккаунтToolStripMenuItem_Click);
            // 
            // изменитьДанныеАккаунтаToolStripMenuItem
            // 
            this.изменитьДанныеАккаунтаToolStripMenuItem.Name = "изменитьДанныеАккаунтаToolStripMenuItem";
            this.изменитьДанныеАккаунтаToolStripMenuItem.Size = new System.Drawing.Size(172, 20);
            this.изменитьДанныеАккаунтаToolStripMenuItem.Text = "Изменить Данные Аккаунта";
            this.изменитьДанныеАккаунтаToolStripMenuItem.Click += new System.EventHandler(this.изменитьДанныеАккаунтаToolStripMenuItem_Click);
            // 
            // accessPass
            // 
            this.accessPass.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accessPass.Location = new System.Drawing.Point(237, 128);
            this.accessPass.Name = "accessPass";
            this.accessPass.Size = new System.Drawing.Size(241, 28);
            this.accessPass.TabIndex = 1;
            // 
            // accessPassCopy
            // 
            this.accessPassCopy.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accessPassCopy.Location = new System.Drawing.Point(237, 175);
            this.accessPassCopy.Name = "accessPassCopy";
            this.accessPassCopy.Size = new System.Drawing.Size(241, 28);
            this.accessPassCopy.TabIndex = 2;
            // 
            // учетнаяЗаписьToolStripMenuItem
            // 
            this.учетнаяЗаписьToolStripMenuItem.Name = "учетнаяЗаписьToolStripMenuItem";
            this.учетнаяЗаписьToolStripMenuItem.Size = new System.Drawing.Size(246, 20);
            this.учетнаяЗаписьToolStripMenuItem.Text = "Изменить пароль для входа в хранилище";
            this.учетнаяЗаписьToolStripMenuItem.Click += new System.EventHandler(this.учетнаяЗаписьToolStripMenuItem_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 289);
            this.Controls.Add(this.accessPassCopy);
            this.Controls.Add(this.accessPass);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "main";
            this.Text = "Войти в хранилище паролей";
            this.Load += new System.EventHandler(this.main_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem получитьДанныеАккаунтаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьАккаунтToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьДанныеАккаунтаToolStripMenuItem;
        private System.Windows.Forms.TextBox accessPass;
        private System.Windows.Forms.TextBox accessPassCopy;
        private System.Windows.Forms.ToolStripMenuItem учетнаяЗаписьToolStripMenuItem;
    }
}

