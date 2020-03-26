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
    public partial class FormPeripheryAdd : Form
    {
        ParkDBEntities db;
        string[] Values;
        int length = 0;
        public FormPeripheryAdd()
        {
            InitializeComponent();
        }

        private void FormPeripheryAdd_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            loadPeripheryTypes();
            loadManufacturers();
            loadSpecs();
            loadModels();
            loadEmployee();
        }

        private void loadEmployee()
        {
            comboBox1.DataSource = db.Employees.ToList();
            comboBox1.DisplayMember = "Surname";
            comboBox1.ValueMember = "Id";
        }

        private void loadPeripheryTypes() 
        {
            type.DataSource = Enum.GetValues(typeof(PeripheryType))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            type.DisplayMember = "Description";
            type.ValueMember = "value";

        }

        private void loadManufacturers() 
        {
            manufac.DataSource = db.Manufacturers.ToList();
            manufac.DisplayMember = "Name";
            manufac.ValueMember = "Id";  
        }

        private void loadSpecs() 
        {
            var spe = db.Specs.Select(p => new
            {
                Id = p.Id,
                Name = p.Name + p.Value,
            });
            Spe.DataSource = spe.ToList();
            Spe.DisplayMember = "Name";
            Spe.ValueMember = "Id";
        }

        private void loadModels()
        {
            Model.DataSource = db.Peripheries.ToList();
            Model.DisplayMember = "model";
            Model.ValueMember = "Id";
        }
        private void SplitSpecs() 
        {
            Values = Spe.Text.Split(new Char[] { '@', '.', '\n', '-' , ',' }, StringSplitOptions.RemoveEmptyEntries);
            length = Values.Length;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            using (db = new ParkDBEntities())
            {
                if (!checkManufacturerExistence(manufac.SelectedItem.ToString()))
                {
                    Manufacturer man = new Manufacturer
                    {
                        Name = manufac.Text
                    };
                    db.Manufacturers.Attach(man);
                db.Manufacturers.Add(man);
                db.SaveChanges();
                this.Refresh();
                }
                SplitSpecs();
                string _name = "";
                string _values = "";
                for (int i = 0; i < length - 1; i = i + 2) 
                { 
                    _name = _name + Values[i] + " ";
                    _values = _values + Values[i + 1] + ";";
                }

                    if (!checkSpecsExistence(_name, _values))
                    {
                        Specs spe = new Specs
                        {
                            Name = _name,
                            Value = _values
                        };
                        db.Specs.Attach(spe);
                        db.Specs.Add(spe);
                        db.SaveChanges();

                    }
                Periphery example = new Periphery()
                {
                    Type = (PeripheryType)type.SelectedValue,
                    Model = Model.Text,
                    ManufacturerId = (int)manufac.SelectedValue,
<<<<<<< HEAD
                    //SpecId = (int)Spe.SelectedValue,
=======
>>>>>>> 4a55da9931de7feced28a7213cea22763b0c2f8c
                    EmployeeId = (int)comboBox1.SelectedValue,
                };
                db.Peripheries.Attach(example);
                db.Peripheries.Add(example);
                db.SaveChanges();
                }
                this.Close();
            }
            /*catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool checkManufacturerExistence(string newMan)
        {
            foreach (Manufacturer man in db.Manufacturers.ToList())
            {
                if (man.Name == newMan)
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkSpecsExistence(string _name, string _value)
        {
            foreach (Specs spe in db.Specs.ToList())
            {
                if (spe.Name == _name && spe.Value == _value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
