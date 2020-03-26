using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace solpr
{
    public partial class FormDepartmentAdd : Form
    {
        ParkDBEntities db;

        public FormDepartmentAdd()
        {
            InitializeComponent();
        }

        private void FormDepartmentAdd_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (db = new ParkDBEntities())
            {
                //yEEEE
                Department dep = new Department();
                dep.Name = textBox2.Text;
                db.Departments.Add(dep);
                db.SaveChanges();
                Close();
                Program.mf.RefreshEmployeeGrid();
            }
        }
    }
}
