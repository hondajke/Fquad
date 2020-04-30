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
        string[] Values;
        int length = 0;
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
            var spec = db.Specs.Where(p => p.PeripheryId == peri.Id)
                .Select(p => new
                {
                    Id = p.Id,
                    Название = p.Name,
                    Значение = p.Value,
                });
            int j = 0;
            foreach (var s in spec) 
            {
                Spe.Rows.Add();
                Spe.Rows[Spe.Rows.Count - 2].Cells[0].Value = s.Название;
                Spe.Rows[Spe.Rows.Count - 2].Cells[1].Value = s.Значение;
                Spe.Rows[Spe.Rows.Count - 1].Cells[0].Value = "";
                Spe.Rows[Spe.Rows.Count - 1].Cells[1].Value = "";
            }
            Console.WriteLine(j);
            
            //Spe.Rows.Add();
            //Spe.DataSource = spec.ToList();
            //Spe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //Spe.Rows.Insert(Spe.Rows.);
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


                changedPeri.Type = (PeripheryType)type.SelectedValue;
                changedPeri.Model = Model.Text;
                if (addedNewOne == true) changedPeri.ManufacturerId = tempManId;
                else changedPeri.ManufacturerId = (int)manufac.SelectedValue;
                //SpecId = (int)Spe.SelectedValue,
                changedPeri.EmployeeId = (int)comboBox1.SelectedValue;
                db.Entry(changedPeri).State = System.Data.Entity.EntityState.Modified;
                int temp = changedPeri.Id;
                db.SaveChanges();
                for (int i = 0; i < Spe.Rows.Count - 1; i++) 
                {
                    if (checkSpecsExistence2(Spe.Rows[i].Cells[0].Value.ToString(), changedPeri.Id))
                    {
                        var result = db.Specs.AsEnumerable()
                            .Single(p => (p.PeripheryId == changedPeri.Id) && (p.Name == Spe.Rows[i].Cells[0].Value.ToString()));
                        result.Value = Spe.Rows[i].Cells[1].Value.ToString();
                        db.SaveChanges();
                    }else if (!checkSpecsExistence(Spe.Rows[i].Cells[0].Value.ToString(), Spe.Rows[i].Cells[1].Value.ToString(), changedPeri.Id))
                    {
                        Specs spec = new Specs();
                        //spec.PeripheryId = example.Id;
                        spec.PeripheryId = changedPeri.Id;
                        spec.Name = Spe.Rows[i].Cells[0].Value.ToString();
                        spec.Value = Spe.Rows[i].Cells[1].Value.ToString();
                        db.Specs.Add(spec);
                        db.SaveChanges();
                    }
                }
                /*for (int i = 0; i < length - 1; i = i + 2)
                {
                    if (checkSpecsExistence2(Values[i], changedPeri.Id))
                    {
                        var result = db.Specs.AsEnumerable()
                            .Single(p => (p.PeripheryId == changedPeri.Id) && (p.Name == Values[i].ToString()));
                        result.Value = Values[i + 1];
                        db.SaveChanges();
                    }
                    else if (!checkSpecsExistence(Values[j], Values[j + 1], changedPeri.Id))
                    {
                        Specs spec = new Specs();
                        //spec.PeripheryId = example.Id;
                        for (int j = 0; j < Spe.Rows.Count - 1; j++)
                        {
                            spec.PeripheryId = changedPeri.Id;
                            spec.Name = Spe.Rows[j].Cells[0].Value.ToString();
                            spec.Value = Spe.Rows[j].Cells[1].Value.ToString();
                            db.Specs.Add(spec);
                            db.SaveChanges();
                        }
                    }
                }*/
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SplitSpecs()
        {
            Values = Spe.Text.Split(new Char[] { '@', '.', '\n', '-', ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            length = Values.Length;
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
        private bool checkSpecsExistence2(string _name, int _PeripheryId)
        {
            foreach (Specs spe in db.Specs.ToList())
            {
                if (spe.Name == _name && spe.PeripheryId == _PeripheryId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
