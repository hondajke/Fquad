using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Root.Reports;

namespace solpr
{
    public partial class FormReportAdd : Form
    {
        Report report;

        public FormReportAdd()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            double height = 30;
            report = new Report(new PdfFormatter());
            FontDef fd = new FontDef(report, "Helvetica");
            Page page = new Page(report);
            FontProp fph = new FontPropMM(fd, 5);
            FontProp fp = new FontPropMM(fd, 1.9);
            page.AddCB_MM(14, new RepString(fp, "Report"));
            page.AddCB_MM(20, new RepString(fp, "xdddd"));
            if (radioButton1.Checked)
            {
                
                if (checkedListBox1.CheckedIndices.Contains(0))
                {
                    page.AddCB_MM(height, new RepString(fp, checkedListBox1.Items[0].ToString()));
                    height += 5;

                    TableLayoutManager tlm = new TableLayoutManager(fp);
                    TlmColumn col;
                    col = new TlmColumnMM(tlm, "ID", 13);
                    col = new TlmColumnMM(tlm, "Статус", 40);
                    col = new TlmColumnMM(tlm, "Сотрудник", 60);/*
                    foreach (DataGridViewRow row in Program.mf.dataGridView1.Rows)
                    {
                        
                        page.AddCB_MM(height, new RepString(fp, row.Cells[0].Value + " " + row.Cells[1].Value + " " + row.Cells[2].Value));
                        height += 3;
                    }*/
                    height += 6;
                    tlm.Close();
                }
                if (checkedListBox1.CheckedIndices.Contains(1))
                {
                    page.AddCB_MM(height, new RepString(fp, checkedListBox1.Items[1].ToString()));
                    height += 5;

                    TableLayoutManager tlm = new TableLayoutManager(fp);
                    TlmColumn col;
                    col = new TlmColumnMM(tlm, "ID", 13);
                    col = new TlmColumnMM(tlm, "Тип", 40);
                    col = new TlmColumnMM(tlm, "Модель", 60);
                    col = new TlmColumnMM(tlm, "Производитель", 60);

                    foreach (DataGridViewRow row in Program.mf.dataGridView2.Rows)
                    {
                        tlm.NewRow();
                        tlm.Add(0, new RepString(fp, row.Cells[0].ToString()));
                        tlm.Add(1, new RepString(fp, row.Cells[1].ToString()));
                        tlm.Add(2, new RepString(fp, row.Cells[2].ToString()));
                        tlm.Add(3, new RepString(fp, row.Cells[3].ToString()));
                    }
                    height += 6;
                    tlm.Close();
                }
                if (checkedListBox1.CheckedIndices.Contains(2))
                {
                    page.AddCB_MM(height, new RepString(fp, checkedListBox1.Items[2].ToString()));
                    height += 5;

                    TableLayoutManager tlm = new TableLayoutManager(fp);
                    TlmColumn col;
                    col = new TlmColumnMM(tlm, "ID", 13);
                    col = new TlmColumnMM(tlm, "Тип", 40);
                    col = new TlmColumnMM(tlm, "Модель", 60);
                    col = new TlmColumnMM(tlm, "Производитель", 60);

                    foreach (DataGridViewRow row in Program.mf.dataGridView3.Rows)
                    {
                        page.AddCB_MM(height, new RepString(fp, row.Cells[0].Value + " " + row.Cells[1].Value + " " + row.Cells[2].Value));
                        height += 3;
                    }
                    height += 6;
                    tlm.Close();
                }
            }

            if (radioButton2.Checked)
            {/*
                if (checkedListBox2.CheckedIndices.Contains(0))
                {
                    
                }
                if (checkedListBox2.CheckedIndices.Contains(1))
                {
                    
                }
                if (checkedListBox2.CheckedIndices.Contains(2))
                {
                    
                }*/
            }

            report.Save("asd.pdf");
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
    }
    
}
