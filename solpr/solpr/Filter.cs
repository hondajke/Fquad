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
            loadModels();
            loadTypes();
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            checkBox4.Enabled = true;
            checkBox5.Enabled = true;
            switch (Program.mf.dataGridNumber)
            {
                case 1:
                    comboBox1.Enabled = false;
                    break;
                case 2:
                    checkBox1.Enabled = false;
                    break;
                case 3:
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                    break;
                case 4:
                    checkBox2.Enabled = false;
                    checkBox3.Enabled = false;
                    checkBox4.Enabled = false;
                    checkBox5.Enabled = false;
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

        private void loadModels()
        {
            var mdlQuery = from mdl in db.Components
                            orderby mdl.Model
                            select new
                            {
                                Name = mdl.Model,
                                mdl.Id
                            };
            var pmdlQuery = from pmdl in db.Peripheries
                           orderby pmdl.Model
                           select new
                           {
                               Name = pmdl.Model,
                               pmdl.Id
                           };
            switch(Program.mf.dataGridNumber)
            {
                case 2:
                    comboBox4.DataSource = pmdlQuery.ToList();
                    comboBox4.DisplayMember = "Name";
                    comboBox4.ValueMember = "Id";
                    break;
                case 3:
                    comboBox4.DataSource = mdlQuery.ToList();
                    comboBox4.DisplayMember = "Name";
                    comboBox4.ValueMember = "Id";
                    break;
            }
        }

        private void loadTypes()
        {
            switch(Program.mf.dataGridNumber)
            {
                case 2:
                    comboBox5.DataSource = Enum.GetValues(typeof(PeripheryType))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
                    comboBox5.DisplayMember = "Description";
                    comboBox5.ValueMember = "value";
                    break;
                case 3:
                    comboBox5.DataSource = Enum.GetValues(typeof(ComponentType))
                 .Cast<Enum>()
                 .Select(value => new
                 {
                     (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                     value
                 })
                 .OrderBy(item => item.value)
                 .ToList();
                     comboBox5.DisplayMember = "Description";
                     comboBox5.ValueMember = "value";
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (Program.mf.dataGridNumber)
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
                    if (comboBox3.Enabled == true && comboBox4.Enabled == true && comboBox5.Enabled == false && comboBox2.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result2.Where(x => x.Производитель == comboBox3.Text && x.Модель == comboBox4.Text).ToList();
                    }
                    if (comboBox3.Enabled == true && comboBox4.Enabled == false && comboBox5.Enabled == false && comboBox2.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result2.Where(x => x.Производитель == comboBox3.Text).ToList();
                    }
                    if (comboBox3.Enabled == false && comboBox4.Enabled == true && comboBox5.Enabled == false && comboBox2.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result2.Where(x => x.Модель == comboBox4.Text).ToList();
                    }
                    if (comboBox4.Enabled == false && comboBox5.Enabled == true && comboBox3.Enabled == false && comboBox2.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result2.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString()).ToList();
                    }
                    if (comboBox4.Enabled == true && comboBox5.Enabled == true && comboBox3.Enabled == true && comboBox2.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result2.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString() && x.Модель == comboBox4.Text && x.Производитель == comboBox3.Text).ToList();
                    }
                    if (comboBox4.Enabled == true && comboBox5.Enabled == true && comboBox3.Enabled == false && comboBox2.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result2.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString() && x.Модель == comboBox4.Text).ToList();
                    }
                    if (comboBox4.Enabled == false && comboBox5.Enabled == true && comboBox3.Enabled == true && comboBox2.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result2.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString() && x.Производитель == comboBox3.Text).ToList();
                    }
                    if (comboBox4.Enabled == false && comboBox5.Enabled == false && comboBox3.Enabled == false && comboBox2.Enabled == true)
                    {
                        Program.mf.dataGridView3.DataSource = result2.Where(x => x.Сотрудник == comboBox2.Text).ToList();
                    }
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
                    if (comboBox3.Enabled == true && comboBox4.Enabled == true && comboBox5.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result3.Where(x => x.Производитель == comboBox3.Text && x.Модель == comboBox4.Text).ToList();
                    }
                    if (comboBox3.Enabled == true && comboBox4.Enabled == false && comboBox5.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result3.Where(x => x.Производитель == comboBox3.Text).ToList();
                    }
                    if (comboBox3.Enabled == false && comboBox4.Enabled == true && comboBox5.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result3.Where(x => x.Модель == comboBox4.Text).ToList();
                    }
                    if (comboBox4.Enabled == false && comboBox5.Enabled == true && comboBox3.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result3.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString()).ToList();
                    }
                    if (comboBox4.Enabled == true && comboBox5.Enabled == true && comboBox3.Enabled == true)
                    {
                        Program.mf.dataGridView3.DataSource = result3.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString() && x.Модель == comboBox4.Text && x.Производитель == comboBox3.Text).ToList();
                    }
                    if (comboBox4.Enabled == true && comboBox5.Enabled == true && comboBox3.Enabled == false)
                    {
                        Program.mf.dataGridView3.DataSource = result3.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString() && x.Модель == comboBox4.Text).ToList();
                    }
                    if (comboBox4.Enabled == false && comboBox5.Enabled == true && comboBox3.Enabled == true)
                    {
                        Program.mf.dataGridView3.DataSource = result3.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString() && x.Производитель == comboBox3.Text).ToList();
                    }
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

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
            }
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                comboBox3.Enabled = true;
            }
            else
            {
                comboBox3.Enabled = false;
            }
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                comboBox4.Enabled = true;
            }
            else
            {
                comboBox4.Enabled = false;
            }
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                comboBox5.Enabled = true;
            }
            else
            {
                comboBox5.Enabled = false;
            }
        }
    }
}
