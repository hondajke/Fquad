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


        private void loadModels()
        {
            Model.DataSource = db.Peripheries.ToList();
            Model.DisplayMember = "model";
            Model.ValueMember = "Id";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (db = new ParkDBEntities())
            {
                int tempManId = 0;
                bool addedNewOne = false;
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
                    loadManufacturers();
                    tempManId = man.Id;
                    addedNewOne = true;
                }
                example.Type = (PeripheryType)type.SelectedValue;
                example.Model = Model.Text;
                if (addedNewOne == true) example.ManufacturerId = tempManId;
                else example.ManufacturerId = (int)manufac.SelectedValue;
                example.EmployeeId = (int)comboBox1.SelectedValue;
                db.Peripheries.Attach(example);
                db.Peripheries.Add(example);
                int temp = example.Id;
                db.SaveChanges();
                Specs spec = new Specs();
                string specnames = "";
                string specvalues = "";
                for (int i = 0; i < Spe.Rows.Count - 1; i++)
                {
                    specnames += Spe.Rows[i].Cells[0].Value.ToString() + "|";
                    specvalues += Spe.Rows[i].Cells[1].Value.ToString() + "|";
                }
                spec.PeripheryId = example.Id;
                spec.Name = specnames;
                spec.Value = specvalues;
                db.Specs.Add(spec);
                db.SaveChanges();
                this.Close();
            }
        }

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
    }
}
