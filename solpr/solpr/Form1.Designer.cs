namespace solpr
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage0 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button7 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage0);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(123, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(662, 439);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage0
            // 
            this.tabPage0.Controls.Add(this.button4);
            this.tabPage0.Controls.Add(this.dataGridView1);
            this.tabPage0.Location = new System.Drawing.Point(4, 22);
            this.tabPage0.Name = "tabPage0";
            this.tabPage0.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage0.Size = new System.Drawing.Size(654, 413);
            this.tabPage0.TabIndex = 0;
            this.tabPage0.Text = "ПК";
            this.tabPage0.UseVisualStyleBackColor = true;
            this.tabPage0.Enter += new System.EventHandler(this.tabPage0_Enter);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(509, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(139, 40);
            this.button4.TabIndex = 1;
            this.button4.Text = "Добавить ПК";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(500, 400);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.dataGridView2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(654, 413);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Периферия";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(512, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 54);
            this.label1.TabIndex = 4;
            this.label1.Text = "*удаляет периферию выбранную в таблице слева";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(509, 52);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(139, 39);
            this.button5.TabIndex = 3;
            this.button5.Text = "Удалить периферию";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(509, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 40);
            this.button2.TabIndex = 2;
            this.button2.Text = "Добавить периферию";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 6);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(500, 400);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.dataGridView3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(654, 413);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Комплектующие";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(509, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить комплектующее";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(6, 6);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(500, 400);
            this.dataGridView3.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button7);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.dataGridView4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(654, 413);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Сотрудники";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Enter += new System.EventHandler(this.tabPage3_Enter);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(512, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 54);
            this.label2.TabIndex = 3;
            this.label2.Text = "*удаляет сотрудника выбранного в таблице слева";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(509, 53);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(139, 38);
            this.button6.TabIndex = 2;
            this.button6.Text = "Удалить сотрудника";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(509, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 40);
            this.button3.TabIndex = 1;
            this.button3.Text = "Добавить сотрудника";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(6, 6);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(500, 400);
            this.dataGridView4.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(654, 413);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Отчеты";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(509, 142);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(139, 39);
            this.button7.TabIndex = 4;
            this.button7.Text = "Добавить отдел";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 477);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage0;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

