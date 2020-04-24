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
    public partial class FormComputerEdit : Form
    {
        ParkDBEntities db;

        public FormComputerEdit()
        {
            InitializeComponent();
        }

        private void FormComputerEdit_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            loadCompStatus();
            loadEmployees();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadCompStatus()
        {
            comboBox8.DataSource = Enum.GetValues(typeof(ComputerStatus))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            comboBox8.DisplayMember = "Description";
            comboBox8.ValueMember = "value";
        }
        private void loadEmployees()
        {
            var empQuery = from emp in db.Employees
                           join dep in db.Departments on emp.DepartmentId equals dep.Id
                           orderby emp.Name

                           select new
                           {
                               sad = dep.Name + " " + emp.Surname + " " + emp.Name,
                               gov = emp.Id
                           };
            comboBox7.DataSource = empQuery.ToList();
            comboBox7.DisplayMember = "sad";
            comboBox7.ValueMember = "gov";

        }
    }
}
