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
            Values = Spe.Text.Split(new Char[] { '@', '.', '\n', '-' , ',', ';',' '}, StringSplitOptions.RemoveEmptyEntries);
            length = Values.Length;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            using (db = new ParkDBEntities())
            {
                Periphery example = new Periphery();
                if (!checkManufacturerExistence(manufac.Text))
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
                


                example.Type = (PeripheryType)type.SelectedValue;
                example.Model = Model.Text;
                example.ManufacturerId = (int)manufac.SelectedValue;
                //SpecId = (int)Spe.SelectedValue,

                example.EmployeeId = (int)comboBox1.SelectedValue;
                db.Peripheries.Attach(example);
                db.Peripheries.Add(example);
                int temp = example.Id;
                db.SaveChanges();
                SplitSpecs();
                for (int i = 0; i < length - 1; i = i + 2)
                {
                    if (!checkSpecsExistence(Values[i], Values[i + 1], example.Id))
                    {
                        Specs spe = new Specs
                        {
                            Name = Values[i],
                            Value = Values[i + 1],
                            PeripheryId = example.Id,
                        };
                        db.Specs.Attach(spe);
                        db.Specs.Add(spe);
                        db.SaveChanges();

                    }
                }
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

        private bool checkSpecsExistence(string _name, string _value, int _PeripheryId)
        {
            foreach (Specs spe in db.Specs.ToList())
            {
                if (spe.Name == _name && spe.Value == _value && spe.PeripheryId == _PeripheryId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
