﻿namespace solpr
{
    partial class FormPeripheryEdit
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
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Model = new System.Windows.Forms.ComboBox();
            this.manufac = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.type = new System.Windows.Forms.ComboBox();
            this.Spe = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Spe)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 311);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Сотрудник";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(151, 308);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 27;
            // 
            // Model
            // 
            this.Model.FormattingEnabled = true;
            this.Model.Location = new System.Drawing.Point(151, 122);
            this.Model.Name = "Model";
            this.Model.Size = new System.Drawing.Size(121, 21);
            this.Model.TabIndex = 25;
            // 
            // manufac
            // 
            this.manufac.FormattingEnabled = true;
            this.manufac.Location = new System.Drawing.Point(151, 70);
            this.manufac.Name = "manufac";
            this.manufac.Size = new System.Drawing.Size(121, 21);
            this.manufac.TabIndex = 24;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 356);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Редактировать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Характеристики";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Модель";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Производитель";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Тип";
            // 
            // type
            // 
            this.type.FormattingEnabled = true;
            this.type.Location = new System.Drawing.Point(151, 21);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(121, 21);
            this.type.TabIndex = 17;
            // 
            // Spe
            // 
            this.Spe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Spe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.Spe.Location = new System.Drawing.Point(151, 170);
            this.Spe.Name = "Spe";
            this.Spe.RowHeadersVisible = false;
            this.Spe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Spe.Size = new System.Drawing.Size(203, 116);
            this.Spe.TabIndex = 29;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Значение";
            this.Column2.MaxInputLength = 100;
            this.Column2.Name = "Column2";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Название";
            this.Column1.MaxInputLength = 100;
            this.Column1.Name = "Column1";
            // 
            // FormPeripheryEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 415);
            this.Controls.Add(this.Spe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Model);
            this.Controls.Add(this.manufac);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.type);
            this.Name = "FormPeripheryEdit";
            this.Text = "FormPeripheryEdit";
            ((System.ComponentModel.ISupportInitialize)(this.Spe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox Model;
        private System.Windows.Forms.ComboBox manufac;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.DataGridView Spe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}