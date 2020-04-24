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
    public partial class Filter : Form
    {
        ParkDBEntities db;

        public Filter()
        {
            InitializeComponent();
            db = new ParkDBEntities();
            loadDepartments();
            loadEmployees();
            loadManufacturers();
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            switch (Program.mf.dataGridNumber)
            {
                case 1:
                    comboBox1.Enabled = false;
                    break;
                case 2:
                    comboBox1.Enabled = false;
                    break;
                case 3:
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                    break;
                case 4:
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                    comboBox4.Enabled = false;
                    break;
            }
        }

        private void loadManufacturers()
        {
            var manufQuery = from man in db.Manufacturers
                             orderby man.Name
                             select new
                             {
                                 man.Name,
                                 man.Id
                             };
            comboBox3.DataSource = manufQuery.ToList();
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "Id";
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

        private void loadEmployees()
        {
            var emplQuery = from empl in db.Employees
                           orderby empl.Surname
                           select new
                           {
                               Name = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name,
                               empl.Id
                           };
            comboBox2.DataSource = emplQuery.ToList();
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch(Program.mf.dataGridNumber)
            {
                case 1:
                    comboBox1.Enabled = false;
                    this.Close();
                    break;
                case 2:
                    var result2 = from periphery in db.Peripheries
                                 join manufac in db.Manufacturers on periphery.ManufacturerId equals manufac.Id
                                 join specs in db.Specs on periphery.Id equals specs.PeripheryId
                                 join empl in db.Employees on periphery.EmployeeId equals empl.Id
                                 select new
                                 {
                                     ID = periphery.Id,
                                     Тип = periphery.Type,
                                     Модель = periphery.Model,
                                     Производитель = manufac.Name,
                                     Характеристики = specs.Name + " - " + specs.Value,
                                     Сотрудник = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name
                                 };
                    Program.mf.dataGridView2.DataSource = result2.Where(x => x.Сотрудник == comboBox2.Text && x.Производитель == comboBox3.Text && x.Модель == comboBox4.Text).ToList();
                    this.Close();
                    break;
                case 3:
                    var result3 = from comps in db.Components
                                 join manufac in db.Manufacturers on comps.ManufacturerId equals manufac.Id
                                 select new
                                 {
                                     ID = comps.Id,
                                     Тип = comps.Type,
                                     Модель = comps.Model,
                                     Производитель = manufac.Name,
                                     //Характеристики = specs.Name + " - " + specs.Value
                                 };
                    Program.mf.dataGridView3.DataSource = result3.Where(x => x.Производитель == comboBox3.Text && x.Модель == comboBox4.Text).ToList();
                    this.Close();
                    break;
                case 4:
                    var result4 = from employee in db.Employees
                                 join department in db.Departments on employee.DepartmentId equals department.Id
                                 select new
                                 {
                                     ID = employee.Id,
                                     Фамилия = employee.Surname,
                                     Имя = employee.Name,
                                     Отчество = employee.Patronymic_Name,
                                     ID_отдела = employee.DepartmentId,
                                     Отдел = department.Name
                                 };
                    Program.mf.dataGridView4.DataSource = result4.Where(x => x.Отдел == comboBox1.Text).ToList();
                    this.Close();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
