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
    public partial class FormEmployeeAdd : Form
    {
        ParkDBEntities db;

        public FormEmployeeAdd()
        {
            InitializeComponent();
        }

        private void FormEmployeeAdd_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            loadDepartments();
        }

        private void loadDepartments()
        {
            var depQuery = from dep in db.Departments
                             orderby dep.Name
                             select new
                             {
                                 dep.Name,
                                 dep.Id
                             };
            comboBox1.DataSource = depQuery.ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (db = new ParkDBEntities())
            {
                Employee emplo = new Employee();
                emplo.Surname = textBox1.Text;
                emplo.Name = textBox2.Text;
                emplo.Patronymic_Name = textBox3.Text;
                emplo.DepartmentId = (int)comboBox1.SelectedValue;
                db.Employees.Add(emplo);
                db.SaveChanges();
                Close();
            }
        }

    }
}
