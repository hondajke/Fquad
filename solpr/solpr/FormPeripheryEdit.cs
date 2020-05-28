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
    public partial class FormPeripheryEdit : Form
    {
        ParkDBEntities db;
        Periphery changedPeri;
        int tempPerId = 0;
        public FormPeripheryEdit()
        {
            InitializeComponent();
        }
        public FormPeripheryEdit(Periphery peri)
        {
            InitializeComponent();
            db = new ParkDBEntities();
            tempPerId = peri.Id;
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
            var manu = db.Manufacturers.Where(p => p.Id == peri.ManufacturerId);
            manufac.DataSource = manu.ToList();
            manufac.DisplayMember = "Name";
            manufac.ValueMember = "Id";
            Model.Text = peri.Model;
            Specs spec = db.Specs.Where(x => x.PeripheryId == peri.Id).FirstOrDefault();
            int specNum = countNumofSpecs(spec);
            string[] SpecNames = spec.Name.Split('|');
            string[] SpecValues = spec.Value.Split('|');

            for (int i = 0; i < specNum; i++)
            {
                Spe.Rows.Add();
                Spe.Rows[i].Cells[0].Value = SpecNames[i];
                Spe.Rows[i].Cells[1].Value = SpecValues[i];
            }
            var emplo = db.Employees
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Surname + " " + p.Name + " " + p.Patronymic_Name,
                });
            comboBox1.DataSource = emplo.ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (db = new ParkDBEntities())
            {
                int tempManId = 0;
                bool addedNewOne = false;
                changedPeri = db.Peripheries.Where(p => p.Id == tempPerId).FirstOrDefault();
                if (!checkManufacturerExistence(manufac.Text))
                {
                    Manufacturer man = new Manufacturer
                    {
                        Name = manufac.Text
                    };
                    db.Manufacturers.Attach(man);
                    db.Manufacturers.Add(man);
                    db.SaveChanges();
                    tempManId = man.Id;
                    addedNewOne = true;
                }
                Specs spec = db.Specs.Where(x => x.PeripheryId == tempPerId).FirstOrDefault();
                changedPeri.Type = (PeripheryType)type.SelectedValue;
                changedPeri.Model = Model.Text;
                if (addedNewOne == true) changedPeri.ManufacturerId = tempManId;
                else changedPeri.ManufacturerId = (int)manufac.SelectedValue;
                changedPeri.EmployeeId = (int)comboBox1.SelectedValue;
                db.Entry(changedPeri).State = System.Data.Entity.EntityState.Modified;
                int temp = changedPeri.Id;
                string specnames = "";
                string specvalues = "";
                for (int i = 0; i < Spe.Rows.Count - 1; i++)
                {
                    specnames += Spe.Rows[i].Cells[0].Value + "|";
                    specvalues += Spe.Rows[i].Cells[1].Value + "|";
                }
                spec.Name = specnames;
                spec.Value = specvalues;
                db.SaveChanges();
            }
            this.Close();
        }

        private int countNumofSpecs(Specs spec)
        {
            int names = 0;
            int values = 0;
            foreach (char c in spec.Name)
            {
                if (c == '|')
                {
                    names++;
                }
            }
            foreach (char c in spec.Value)
            {
                if (c == '|')
                {
                    values++;
                }
            }
            if (names >= values)
            {
                return names;
            }
            else
            {
                return values;
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
