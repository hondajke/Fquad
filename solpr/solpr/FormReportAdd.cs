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
        public FormReportAdd()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            double height = 30;
            Report report = new Report(new PdfFormatter());
            FontDef fd = new FontDef(report, "Helvetica");
            Page page = new Page(report);
            FontProp fph = new FontPropMM(fd, 5);
            FontProp fp = new FontPropMM(fd, 1.9);
            page.AddCB_MM(14, new RepString(fp, "Report"));
            page.AddCB_MM(20, new RepString(fp, "xdddd"));
            foreach(DataGridViewRow row in Program.mf.dataGridView3.Rows)
            {
                page.AddCB_MM(height, new RepString(fp, row.Cells[0].Value + " " + row.Cells[1].Value + " " + row.Cells[2].Value));
                height += 3;
            }
            report.Save("temp\\asd.pdf");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        
    }
}
