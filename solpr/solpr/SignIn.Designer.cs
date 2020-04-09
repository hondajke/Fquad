namespace solpr
{
    partial class SignIn
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
            this.login = new System.Windows.Forms.TextBox();
            this.pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkIn = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // login
            // 
            this.login.Location = new System.Drawing.Point(137, 98);
            this.login.Multiline = true;
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(115, 20);
            this.login.TabIndex = 0;
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(137, 151);
            this.pass.Multiline = true;
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(115, 20);
            this.pass.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(87, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Окно авторизации";
            // 
            // checkIn
            // 
            this.checkIn.Location = new System.Drawing.Point(208, 222);
            this.checkIn.Name = "checkIn";
            this.checkIn.Size = new System.Drawing.Size(75, 23);
            this.checkIn.TabIndex = 5;
            this.checkIn.Text = "Войти";
            this.checkIn.UseVisualStyleBackColor = true;
            this.checkIn.Click += new System.EventHandler(this.checkIn_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(63, 222);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(75, 23);
            this.exit.TabIndex = 6;
            this.exit.Text = "Выйти";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // SignIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 307);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.checkIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.login);
            this.Name = "SignIn";
            this.Text = "SignIn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button checkIn;
        private System.Windows.Forms.Button exit;
    }
}