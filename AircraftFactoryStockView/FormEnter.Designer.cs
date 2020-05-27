namespace AircraftFactoryStockView
{
    partial class FormEnter
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
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // labelEmail
            //
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(20, 21);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(41, 13);
            this.labelEmail.TabIndex = 0;
            this.labelEmail.Text = "Логин:";
            //
            // textBoxEmail
            //
            this.textBoxEmail.Location = new System.Drawing.Point(74, 18);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(200, 20);
            this.textBoxEmail.TabIndex = 1;
            //
            // labelPassword
            //
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(20, 64);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(48, 13);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Пароль:";
            //
            // textBoxPassword
            //
            this.textBoxPassword.Location = new System.Drawing.Point(74, 61);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(200, 20);
            this.textBoxPassword.TabIndex = 3;
            //
            // buttonLogin
            //
            this.buttonLogin.Location = new System.Drawing.Point(174, 106);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 5;
            this.buttonLogin.Text = "Вход";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelEmail);
            this.Text = "Вход";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLogin;
    }
}

