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
    public partial class FormEmployeeEdit : Form
    {
        ParkDBEntities db;


        public FormEmployeeEdit()
        {
            InitializeComponent();
            db = new ParkDBEntities();
            loadDepartments();
            int index = Program.mf.dataGridView4.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(Program.mf.dataGridView4[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            Employee emplo = db.Employees
                   .Where(p => p.Id == id)
                   .FirstOrDefault();
            textBox1.Text = emplo.Surname;
            textBox2.Text = emplo.Name;
            textBox3.Text = emplo.Patronymic_Name;
            comboBox1.SelectedValue = emplo.DepartmentId;
       
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (db = new ParkDBEntities())
            {
                int index = Program.mf.dataGridView4.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(Program.mf.dataGridView4[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Employee emplo = db.Employees
                       .Where(p => p.Id == id)
                       .FirstOrDefault();
                emplo.Surname = textBox1.Text;
                emplo.Name = textBox2.Text;
                emplo.Patronymic_Name = textBox3.Text;
                emplo.DepartmentId = (int)comboBox1.SelectedValue;
                db.SaveChanges();
                Close();
                Program.mf.RefreshEmployeeGrid();
            }

        }
    }
}
