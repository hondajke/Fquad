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
            loadStatus();
            switch (Program.mf.dataGridNumber)
            {
                case 1:
                    checkBox1.Enabled = false;
                    checkBox3.Enabled = false;
                    checkBox4.Enabled = false;
                    checkBox5.Enabled = false;
                    break;
                case 2:
                    checkBox1.Enabled = false;
                    checkBox6.Enabled = false;
                    break;
                case 3:
                    checkBox1.Enabled = false;
                    checkBox2.Enabled = false;
                    checkBox6.Enabled = false;
                    break;
                case 4:
                    checkBox2.Enabled = false;
                    checkBox3.Enabled = false;
                    checkBox4.Enabled = false;
                    checkBox5.Enabled = false;
                    checkBox6.Enabled = false;
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
                            };
            var pmdlQuery = from pmdl in db.Peripheries
                           orderby pmdl.Model
                           select new
                           {
                               Name = pmdl.Model,
                           };
            switch(Program.mf.dataGridNumber)
            {
                case 2:
                    comboBox4.DataSource = pmdlQuery.Distinct().ToList();
                    comboBox4.DisplayMember = "Name";
                    break;
                case 3:
                    comboBox4.DataSource = mdlQuery.Distinct().ToList();
                    comboBox4.DisplayMember = "Name";
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

        private void loadStatus()
        {
            comboBox6.DataSource = Enum.GetValues(typeof(ComputerStatus))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            comboBox6.DisplayMember = "Description";
            comboBox6.ValueMember = "value";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool flag = false;
            switch (Program.mf.dataGridNumber)
            {
                case 1:
                    var result1 = from pc in db.Computers
                                 join empl in db.Employees on pc.EmployeeId equals empl.Id
                                 select new
                                 {
                                     ID = pc.Id,
                                     Статус = pc.Status,
                                     Сотрудник = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name
                                 };
                    var computersFilter = result1;

                    if (comboBox2.Enabled == true)
                    {
                        computersFilter = computersFilter.Where(x => x.Сотрудник == comboBox2.Text);
                        flag = true;
                    }
                    if (comboBox6.Enabled == true)
                    {
                        computersFilter = computersFilter.Where(x => x.Статус.ToString() == comboBox6.SelectedValue.ToString());
                        flag = true;
                    }

                    if (flag == true)
                    {
                        Program.mf.dataGridView1.DataSource = computersFilter.ToList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Сначала выберите критерий фильтрации");
                    }
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
                    var peripheryResult = result2;
                    flag = false;

                    if (comboBox2.Enabled == true)
                    {
                        peripheryResult = result2.Where(x => x.Сотрудник == comboBox2.Text);
                        flag = true;
                    }
                    if (comboBox3.Enabled == true)
                    {
                        peripheryResult = peripheryResult.Where(x => x.Производитель == comboBox3.Text);
                        flag = true;
                    }
                    if (comboBox4.Enabled == true)
                    {
                        peripheryResult = peripheryResult.Where(x => x.Модель == comboBox4.Text);
                        flag = true;
                    }
                    if (comboBox5.Enabled == true)
                    {
                        peripheryResult = peripheryResult.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString());
                        flag = true;
                    }

                    if (flag == true)
                    {
                        Program.mf.dataGridView2.DataSource = peripheryResult.ToList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Сначала выберите критерий фильтрации");
                    }

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
                    var componentsFilter = result3;
                   
                    if (comboBox3.Enabled == true)
                    {
                        componentsFilter = componentsFilter.Where(x => x.Производитель == comboBox3.Text);
                        flag = true;
                    }
                    if (comboBox4.Enabled == true)
                    {
                        componentsFilter = componentsFilter.Where(x => x.Модель == comboBox4.Text);
                        flag = true;
                    }
                    if (comboBox5.Enabled == true)
                    {
                        componentsFilter = componentsFilter.Where(x => x.Тип.ToString() == comboBox5.SelectedValue.ToString());
                        flag = true;
                    }

                    if (flag == true)
                    {
                        Program.mf.dataGridView3.DataSource = componentsFilter.ToList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Сначала выберите критерий фильтрации");
                    }

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
                    if (comboBox1.Enabled == true)
                    {
                        Program.mf.dataGridView4.DataSource = result4.Where(x => x.Отдел == comboBox1.Text).ToList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Сначала выберите критерий фильтрации");
                    }

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

        private void checkBox6_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                comboBox6.Enabled = true;
            }
            else
            {
                comboBox6.Enabled = false;
            }
        }
    }
}
