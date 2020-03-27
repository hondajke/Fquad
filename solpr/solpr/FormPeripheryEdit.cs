﻿using System;
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
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@name", peri.ManufacturerId);
            var manuf = db.Database.SqlQuery<Manufacturer>("SELECT * FROM dbo.Manufacturers WHERE Id LIKE @name",param);
            manufac.DataSource = manuf.ToList();
            manufac.DisplayMember = "Name";
            manufac.ValueMember = "Id";
            Model.Text = peri.Model;
            System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@name", peri.Id);
            var spe = db.Database.SqlQuery<Specs>("SELECT * FROM dbo.Specs WHERE PeripheryId LIKE @name", param1);
            //Spe.DataSource = spe.ToList();
            string display = "";
            foreach(var s in spe)
                display += s.Name + " - " + s.Value + ";";
            Spe.Text = display;
            System.Data.SqlClient.SqlParameter param2 = new System.Data.SqlClient.SqlParameter("@name", peri.EmployeeId);
            var empl = db.Database.SqlQuery<Employee>("SELECT Id, Surname + ' ' + Name + ' ' + Patronymic_Name as Name, Patronymic_Name, Surname, DepartmentId FROM dbo.Employees WHERE Id LIKE @name", param2);
            comboBox1.DataSource = empl.ToList();
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
                    this.Refresh();
                }


                changedPeri.Type = (PeripheryType)type.SelectedValue;
                changedPeri.Model = Model.Text;
                changedPeri.ManufacturerId = (int)manufac.SelectedValue;
                //SpecId = (int)Spe.SelectedValue,

                changedPeri.EmployeeId = (int)comboBox1.SelectedValue;
                db.Entry(changedPeri).State = System.Data.Entity.EntityState.Modified;
                int temp = changedPeri.Id;
                db.SaveChanges();
                SplitSpecs();
                for (int i = 0; i < length - 1; i = i + 2)
                {
                    if (checkSpecsExistence2(Values[i], changedPeri.Id))
                    {
                        System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@param1", Values[i+1]);
                        System.Data.SqlClient.SqlParameter param2 = new System.Data.SqlClient.SqlParameter("@param2", changedPeri.Id);
                        System.Data.SqlClient.SqlParameter param3 = new System.Data.SqlClient.SqlParameter("@param3", Values[i]);
                        int numberOfRowUpdated = db.Database.ExecuteSqlCommand("UPDATE dbo.Specs SET Value=@param1 WHERE PeripheryId=@param2 and Name=@param3",param1,param2,param3);
                        db.SaveChanges();
                    }
                    else if (!checkSpecsExistence(Values[i], Values[i + 1], changedPeri.Id))
                    {
                        Specs spe = new Specs
                        {
                            Name = Values[i],
                            Value = Values[i + 1],
                            PeripheryId = changedPeri.Id,
                        };
                        db.Specs.Attach(spe);
                        db.Specs.Add(spe);
                        db.SaveChanges();
                    }
                }
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