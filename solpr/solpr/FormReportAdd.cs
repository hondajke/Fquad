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

        string dateFormat = "dd.MM.yyyy";

        string tempName = "";
        List<string> tempFiles;
        bool tempCreated = false;

        iText.Kernel.Colors.Color headerBg = new DeviceRgb(235, 235, 235);
        string fontpathV = "C:/Windows/Fonts/Verdana.ttf";

        public FormReportAdd()
        {
            InitializeComponent();
            tempFiles = new List<string>();
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
        }

        private void createPdf(bool temp)
        {
            db = new ParkDBEntities();

            FileInfo file;

            if (temp)
            {
                tempCreated = true;
                tempName = "reports/" + Guid.NewGuid().ToString() + ".pdf";
                file = new FileInfo(tempName);
                if (!file.Directory.Exists) file.Directory.Create();
                popPdf(tempName);
                webBrowser1.Navigate(AppDomain.CurrentDomain.BaseDirectory + tempName);
                tempFiles.Add(tempName);
            }
            else
            {
                saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "\reports";
                saveFileDialog1.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog1.DefaultExt = "pdf";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show(saveFileDialog1.FileName);
                    file = new FileInfo(saveFileDialog1.FileName);

                    popPdf(saveFileDialog1.FileName);
                    webBrowser1.Navigate(saveFileDialog1.FileName);
                }
                else
                {
                    return;
                }
            }
            
        }

        private void popPdf(string Name)
        {
            PdfWriter wr = new PdfWriter(Name);
            PdfDocument pdf = new PdfDocument(wr);
            Document doc = new Document(pdf, PageSize.A4);
            doc.SetMargins(25, 25, 25, 25);
            PdfFont font = PdfFontFactory.CreateFont(fontpathV, "Identity-H", true);

            Paragraph header = new Paragraph(string.Format("Отчет от {0}", DateTime.Today.ToString(dateFormat))).SetFont(font);
            header.SetTextAlignment(TextAlignment.CENTER);

            doc.Add(header);

            if (radioButton1.Checked)
            {

                if (checkedListBox1.CheckedIndices.Contains(0))
                {
                    doc.Add(new Paragraph("ПК").SetFont(font));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 4 }));

                    var pcs = from pc in db.Computers
                              select new
                              {
                                  ComputerId = pc.Id,
                                  Status = pc.Status,
                                  pc.Employee,
                                  pc.Components
                              };

                    addNewHeaderCell("ID", font, table);
                    addNewHeaderCell("Статус", font, table);
                    addNewHeaderCell("Комплектующие", font, table);
                    addNewHeaderCell("Сотрудник", font, table);

                    foreach (var pc in pcs)
                    {
                        table.AddCell(pc.ComputerId.ToString()).SetFont(font);
                        table.AddCell(pc.Status.ToString()).SetFont(font);
                        string comps = "";
                        if (pc.Components != null)
                        {
                            foreach (Component cc in pc.Components)
                            {
                                comps += cc.Manufacturer + " " + cc.Model + "\n";
                            }
                        }
                        table.AddCell(comps).SetFont(font);
                        table.AddCell(pc.Employee.Surname.ToString() + " " + pc.Employee.Name.ToString() + " " + pc.Employee.Patronymic_Name.ToString()).SetFont(font);
                    }
                    doc.Add(table);
                }
                if (checkedListBox1.CheckedIndices.Contains(1))
                {
                    doc.Add(new Paragraph("Периферия").SetFont(font));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 4, 6, 4 }));

                    var pers = from per in db.Peripheries
                               join specs in db.Specs on per.Id equals specs.PeripheryId
                               select new
                              {
                                  PerId = per.Id,
                                  per.Type,
                                  per.Model,
                                  per.Manufacturer,
                                  Specs = specs.Name + "−" + specs.Value,
                                  per.Employee
                              };

                    addNewHeaderCell("ID", font, table);
                    addNewHeaderCell("Тип", font, table);
                    addNewHeaderCell("Модель", font, table);
                    addNewHeaderCell("Производитель", font, table);
                    addNewHeaderCell("Характеристики", font, table);
                    addNewHeaderCell("Сотрудник", font, table);

                    foreach (var per in pers)
                    {
                        table.AddCell(per.PerId.ToString()).SetFont(font);
                        table.AddCell(cellFormattingType(per.Type)).SetFont(font);
                        table.AddCell(per.Model.ToString()).SetFont(font);
                        table.AddCell(per.Manufacturer.Name.ToString()).SetFont(font);
                        table.AddCell(cellFormattingSpecs(per.Specs)).SetFont(font);
                        table.AddCell(per.Employee.Surname.ToString() + " " + per.Employee.Name.ToString() + " " + per.Employee.Patronymic_Name.ToString()).SetFont(font);
                    }
                    doc.Add(table);
                }
                if (checkedListBox1.CheckedIndices.Contains(2))
                {
                    doc.Add(new Paragraph("Комплектующие").SetFont(font));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 4, 6 }));

                    var comps = from comp in db.Components
                               join specs in db.Specs on comp.Id equals specs.ComponentId
                               select new
                               {
                                   CompId = comp.Id,
                                   comp.Type,
                                   comp.Model,
                                   comp.Manufacturer,
                                   Specs = specs.Name + "−" + specs.Value
                               };

                    addNewHeaderCell("ID", font, table);
                    addNewHeaderCell("Тип", font, table);
                    addNewHeaderCell("Модель", font, table);
                    addNewHeaderCell("Производитель", font, table);
                    addNewHeaderCell("Характеристики", font, table);

                    foreach (var comp in comps)
                    {
                        table.AddCell(comp.CompId.ToString()).SetFont(font);
                        table.AddCell(cellFormattingType(comp.Type)).SetFont(font);
                        table.AddCell(comp.Model.ToString()).SetFont(font);
                        table.AddCell(comp.Manufacturer.Name.ToString()).SetFont(font);
                        table.AddCell(cellFormattingSpecs(comp.Specs)).SetFont(font);
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
            wr.Dispose();
            pdf.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createPdf(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(new Uri("about:blank"));
            createPdf(true);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                dateTimePicker1.Value = dateTimePicker2.Value;
            }
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
                      from main in db.Maintenance
                      where pc.Id == main.ComputerId
                      select new
                      {
                          MaintenanceId = main.Id,
                          ComputerId = pc.Id,
                          Status = pc.Status,
                          pc.Employee,
                          main.RepairStart,
                          main.RepairFinish,
                          main.Description,
                          pc.Components
                      };
            switch (status)
            {
                case 1:
                    doc.Add(new Paragraph("Отремонтированные ПК").SetFont(font));
                    pcs = pcs.Where(x => x.Status == ComputerStatus.ok);
                    if (checkBox1.Checked)
                    {
                        pcs.Where(x => x.RepairStart.Date >= dateTimePicker1.Value.Date);
                    }
                    if (checkBox2.Checked)
                    {
                        pcs.Where(x => x.RepairFinish.Value.Date <= dateTimePicker2.Value.Date);
                    }
                    break;
                case 2:
                    doc.Add(new Paragraph("ПК в ремонте").SetFont(font));
                    pcs = pcs.Where(x => x.Status == ComputerStatus.under_repair);
                    if (checkBox1.Checked)
                    {
                        pcs.Where(x => x.RepairStart >= dateTimePicker1.Value);
                    }
                    if (checkBox2.Checked)
                    {
                        pcs.Where(x => x.RepairFinish <= dateTimePicker2.Value);
                    }
                    break;
                case 3:
                    doc.Add(new Paragraph("Списанные ПК").SetFont(font));
                    pcs = pcs.Where(x => x.Status == ComputerStatus.scrapped);
                    if (checkBox1.Checked)
                    {
                        pcs.Where(x => x.RepairStart >= dateTimePicker1.Value);
                    }
                    if (checkBox2.Checked)
                    {
                        pcs.Where(x => x.RepairFinish <= dateTimePicker2.Value);
                    }
                    break;
            }
            
            Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 3, 4, 4, 4, 4 }));

            addNewHeaderCell("ID ремонта", font, table);
            addNewHeaderCell("ID компьютера", font, table);
            addNewHeaderCell("Сотрудник", font, table);
            if (status == 1 || status == 2) {
                addNewHeaderCell("Начало ремонта", font, table);
            }
            if (status == 1)
            {
                addNewHeaderCell("Конец ремонта", font, table);
            }
            addNewHeaderCell("Комплектующие", font, table);
            addNewHeaderCell("Описание", font, table);

            
            foreach (var pc in pcs)
            {
                table.AddCell(pc.MaintenanceId.ToString()).SetFont(font);
                table.AddCell(pc.ComputerId.ToString()).SetFont(font);
                table.AddCell(pc.Employee.Surname.ToString() + " " + pc.Employee.Name.ToString() + " " + pc.Employee.Patronymic_Name.ToString()).SetFont(font);
                if (status == 1 || status == 2)
                {
                    table.AddCell(pc.RepairStart.ToString(dateFormat)).SetFont(font);
                }
                if (status == 1)
                {
                    table.AddCell(Convert.ToDateTime(pc.RepairFinish).ToString(dateFormat)).SetFont(font);
                }
                string comps = "";
                if (pc.Components != null)
                {
                    foreach (Component cc in pc.Components)
                    {
                        comps += cc.Manufacturer + " " + cc.Model + "\n";
                    }
                }
                table.AddCell(comps).SetFont(font);
                table.AddCell(pc.Description.ToString()).SetFont(font);
                
                
                
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

        private void addNewHeaderCell(string cellName, PdfFont font, Table table)
        {
            Cell cell = new Cell();
            cell.SetBackgroundColor(headerBg);
            cell.Add(new Paragraph(cellName).SetFont(font));
            table.AddHeaderCell(cell);
        }

        private string cellFormattingType(ComponentType type)
        {
            GetTypes a = new GetTypes();
            return a.GetDescription(type);
        }

        private string cellFormattingType(PeripheryType type)
        {

            GetTypes a = new GetTypes();
            return a.GetDescription(type);
        }

        private string cellFormattingSpecs(string spec)
        {
            string[] specs = spec.Split('−');
            int maxNumOfSpecs = specs[0].Count(x => x == '|');
            if (specs[1].Count(x => x == '|') > maxNumOfSpecs)
            {
                maxNumOfSpecs = specs[1].Count(x => x == '|');
            }
            string specstring = "";
            string[] specNames = specs[0].Split('|');
            string[] specValues = specs[1].Split('|');
            for (int i = 0; i < maxNumOfSpecs; i++)
            {
                if (i == maxNumOfSpecs - 1) specstring += specNames[i] + " − " + specValues[i];
                else specstring += specNames[i] + " − " + specValues[i] + "\n";
            }
            return specstring;
        }

        private void FormReportAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tempCreated)
            {
                webBrowser1.Dispose();
                foreach(string filename in tempFiles)
                {
                    File.Delete(filename);
                }
            }
        }
    }
    
}
