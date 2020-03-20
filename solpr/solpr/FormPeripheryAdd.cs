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
        int length;
        public FormPeripheryAdd()
        {
            InitializeComponent();
        }

        private void FormPeripheryAdd_Load(object sender, EventArgs e)
        {
            loadPeripheryTypes();
            loadManufacturers();
            loadSpecs();
            loadModels();
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
            db = new ParkDBEntities();
            manufac.DataSource = db.Manufacturers.ToList();
            manufac.DisplayMember = "Name";
            manufac.ValueMember = "Id";  
        }

        private void loadSpecs() 
        {
            db = new ParkDBEntities();
            Spe.DataSource = db.Specs.ToList();
            Spe.DisplayMember = "Name";
            Spe.ValueMember = "Id";
        }

        private void loadModels()
        {
            db = new ParkDBEntities();
            model.DataSource = db.Peripheries.ToList();
            model.DisplayMember = "model";
            model.ValueMember = "Id";
        }
        private void SplitSpecs() 
        {
            Values = Spe.Text.Split(new Char[] { '@', '.', '\n', '-' }, StringSplitOptions.RemoveEmptyEntries);
            length = Values.Length;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (db = new ParkDBEntities())
                {
                    if (!db.Manufacturers.ToList().Exists(x => x.Name == manufac.Text))
                    {
                        Manufacturer man = new Manufacturer
                        {
                            Name = manufac.Text
                        };
                    db.Manufacturers.Add(man);
                    db.SaveChanges();
                    }
                    SplitSpecs();
                    for (int i = 0; i < length - 1; i += 2)
                    {
                        if (!db.Specs.ToList().Exists(x => x.Name == Values[i]) && !db.Specs.ToList().Exists(y => y.Value == Values[i + 1]))
                        {
                            Specs spe = new Specs
                            {
                                Name = Values[i],
                                Value = Values[i + 1]
                            };
                            db.Specs.Add(spe);
                            db.SaveChanges();
                        }
                    }
                    Periphery example = new Periphery
                    {
                        Type = (PeripheryType)type.SelectedItem,
                        ManufacturerId = manufac.SelectedIndex,
                        Manufacturer = (Manufacturer)manufac.SelectedItem,
                        model = model.Text,
                        SpecId = Spe.SelectedIndex,
                        Specs = (Specs)Spe.SelectedItem,
                        EmployeeId = comboBox1.SelectedIndex,
                        Employee = (Employee)comboBox1.SelectedItem,
                        };
                    db.Peripheries.Add(example);
                    db.SaveChanges();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
