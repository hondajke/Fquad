using System;
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


        string path = "reports/asd.pdf";

        iText.Kernel.Colors.Color headerBg = new DeviceRgb(235, 235, 235);
        string fontpathV = "C:/Windows/Fonts/Verdana.ttf";

        public FormReportAdd()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            db = new ParkDBEntities();

            FileInfo file = new FileInfo(path);
            if (!file.Directory.Exists) file.Directory.Create();

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

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 4, 6 }));

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
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
            var result = from pc in db.Computers
                         join empl in db.Employees on pc.EmployeeId equals empl.Id
                         select pc;

            switch (status)
            {
                case 1:
                    doc.Add(new Paragraph("Работающие ПК").SetFont(font));
                    result = result.Where(x => x.Status == ComputerStatus.ok);
                    break;
                case 2:
                    doc.Add(new Paragraph("ПК в ремонте").SetFont(font));
                    result = result.Where(x => x.Status == ComputerStatus.under_repair);
                    break;
                case 3:
                    doc.Add(new Paragraph("Списанные ПК").SetFont(font));
                    result = result.Where(x => x.Status == ComputerStatus.scrapped);
                    break;

                default:
                    break;
            }
            
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 4, 6 }));


            Cell cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph("ID").SetFont(font));
            table.AddHeaderCell(cell);
            cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph("Сотрудник").SetFont(font));
            table.AddHeaderCell(cell);
            cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph("Комплектующие").SetFont(font));
            table.AddHeaderCell(cell);


            foreach (Computer pc in result)
            {
                table.AddCell(pc.Id.ToString()).SetFont(font);
                table.AddCell(pc.Employee.ToString()).SetFont(font);
                string comps = "";
                foreach (Component comp in pc.Components)
                {
                    comps += comp.Manufacturer + " " + comp.Model + "\n";
                }

            }
            doc.Add(table);
        }
    }
    
}
