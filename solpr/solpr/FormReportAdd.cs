﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.IO.Font.Constants;
using iText.IO.Util;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Colors;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;


namespace solpr
{
    public partial class FormReportAdd : Form
    {
        ParkDBEntities db;


        string path = "reports/temp.pdf";

        iText.Kernel.Colors.Color headerBg = new DeviceRgb(235, 235, 235);
        string fontpathV = "C:/Windows/Fonts/Verdana.ttf";

        public FormReportAdd()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
        }

        private void createPdf(bool temp)
        {
            db = new ParkDBEntities();
            
            if (temp)
            {
                FileInfo file = new FileInfo(path);
                if (!file.Directory.Exists) file.Directory.Create();
            }
            else
            {
                saveFileDialog1.InitialDirectory = @"\reports";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file = new FileInfo(saveFileDialog1.FileName);

                }
            }
            

            PdfWriter wr = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(wr);
            Document doc = new Document(pdf, PageSize.A4);
            doc.SetMargins(25, 25, 25, 25);
            PdfFont font = PdfFontFactory.CreateFont(fontpathV, "Identity-H", true);

            Paragraph header = new Paragraph(string.Format("Отчет от {0}", DateTime.Today.ToString("dd-MM-yyyy"))).SetFont(font);
            header.SetTextAlignment(TextAlignment.CENTER);

            doc.Add(header);

            if (radioButton1.Checked)
            {

                if (checkedListBox1.CheckedIndices.Contains(0))
                {
                    doc.Add(new Paragraph("ПК").SetFont(font));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4 }));

                    foreach (DataGridViewColumn column in Program.mf.dataGridView1.Columns)
                    {
                        Cell cell = new Cell().Add(new Paragraph(column.HeaderText).SetFont(font));
                        cell.SetBackgroundColor(headerBg);
                        table.AddHeaderCell(cell);
                    }

                    foreach (DataGridViewRow row in Program.mf.dataGridView1.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            table.AddCell(cell.Value.ToString()).SetFont(font);
                        }
                    }
                    doc.Add(table);
                }
                if (checkedListBox1.CheckedIndices.Contains(1))
                {
                    doc.Add(new Paragraph("Периферия").SetFont(font));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 4, 4, 4 }));


                    foreach (DataGridViewColumn column in Program.mf.dataGridView2.Columns)
                    {
                        Cell cell = new Cell().Add(new Paragraph(column.HeaderText).SetFont(font));
                        cell.SetBackgroundColor(headerBg);
                        table.AddHeaderCell(cell);
                    }

                    foreach (DataGridViewRow row in Program.mf.dataGridView2.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            table.AddCell(cell.Value.ToString()).SetFont(font);
                        }
                    }
                    doc.Add(table);
                }
                if (checkedListBox1.CheckedIndices.Contains(2))
                {
                    doc.Add(new Paragraph("Комплектующие").SetFont(font));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 4, 6 }));

                    foreach (DataGridViewColumn column in Program.mf.dataGridView3.Columns)
                    {
                        Cell cell = new Cell().Add(new Paragraph(column.HeaderText).SetFont(font));
                        cell.SetBackgroundColor(headerBg);
                        table.AddHeaderCell(cell);
                    }

                    foreach (DataGridViewRow row in Program.mf.dataGridView3.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            //if (cell.ColumnIndex == )
                            table.AddCell(cell.Value.ToString()).SetFont(font);
                        }
                    }
                    doc.Add(table);
                }
            }

            if (radioButton2.Checked)
            {
                if (checkedListBox2.CheckedIndices.Contains(0))
                {
                    addPCwithStatus(doc, font, 1);
                }
                if (checkedListBox2.CheckedIndices.Contains(1))
                {
                    addPCwithStatus(doc, font, 2);
                }
                if (checkedListBox2.CheckedIndices.Contains(2))
                {
                    addPCwithStatus(doc, font, 3);
                }
            }
            doc.Close();
            webBrowser1.Navigate(AppDomain.CurrentDomain.BaseDirectory + path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createPdf(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            createPdf(true);
        }
        
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox1.Enabled = radioButton1.Checked;
            checkedListBox1.Visible = radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox2.Enabled = radioButton2.Checked;
            checkedListBox2.Visible = radioButton2.Checked;
        }

        private void addPCwithStatus (Document doc, PdfFont font, int status)
        {
            
            var pcs = from pc in db.Computers
                      join main in db.Maintenance on pc.Id equals main.ComputerId
                      join emplo in db.Employees on pc.EmployeeId equals emplo.Id
                      select new
                      {
                          MaintenanceId = main.Id,
                          ComputerId = pc.Id,
                          pc.EmployeeId,
                          pc.Status,
                          main.RepairStart,
                          main.RepairFinish,
                          main.Description
                      };
            switch (status)
            {
                case 1:
                    doc.Add(new Paragraph("Отремонтированные ПК").SetFont(font));
                    pcs = pcs.Where(x => x.Status == ComputerStatus.ok);


                    if (checkBox1.Checked)
                    {
                        
                    }
                    if (checkBox2.Checked)
                    {

                    }
                    break;
                case 2:
                    doc.Add(new Paragraph("ПК в ремонте").SetFont(font));
                    pcs = pcs.Where(x => x.Status == ComputerStatus.under_repair);
                    if (checkBox1.Checked)
                    {


                    }
                    if (checkBox2.Checked)
                    {

                    }
                    break;
                case 3:
                    doc.Add(new Paragraph("Списанные ПК").SetFont(font));
                    pcs = pcs.Where(x => x.Status == ComputerStatus.scrapped);
                    break;
            }

            Employee empl;
            //List<Component> comp;

            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3 }));


            Cell cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph("ID ремонта").SetFont(font));
            table.AddHeaderCell(cell);
            cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph("ID компьютера").SetFont(font));
            table.AddHeaderCell(cell);
            cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph("Сотрудник").SetFont(font));
            table.AddHeaderCell(cell);
            /*cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph("Комплектующие").SetFont(font));
            table.AddHeaderCell(cell);*/
            if (status == 1 || status == 2) {
                cell = new Cell();
                cell.SetBackgroundColor(headerBg);
                cell.Add(new Paragraph("Начало ремонта").SetFont(font));
                table.AddHeaderCell(cell);
            }
            if (status == 1)
            {
                cell = new Cell();
                cell.SetBackgroundColor(headerBg);
                cell.Add(new Paragraph("Конец ремонта").SetFont(font));
                table.AddHeaderCell(cell);
            }
            cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph("Описание").SetFont(font));
            table.AddHeaderCell(cell);


            foreach (var pc in pcs)
            {
                table.AddCell(pc.MaintenanceId.ToString()).SetFont(font);
                table.AddCell(pc.ComputerId.ToString()).SetFont(font);
                empl = db.Employees.Where(x => x.Id == pc.EmployeeId).FirstOrDefault();
                table.AddCell(empl.Surname.ToString() + " " + empl.Name.ToString() + " " + empl.Patronymic_Name.ToString()).SetFont(font);
                if (status == 1 || status == 2)
                {
                    table.AddCell(pc.RepairStart.ToString()).SetFont(font);
                }
                if (status == 1)
                {
                    table.AddCell(pc.RepairFinish.ToString()).SetFont(font);
                }
                table.AddCell(pc.Description.ToString()).SetFont(font);
                /*string comps = "";
                
                foreach (Component cc in pc.Components)
                {
                    comps += comps.Manufacturer + " " + comps.Model + "\n";
                }*/

            }
            doc.Add(table);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker2.Enabled = false;
            }
        }
    }
    
}
