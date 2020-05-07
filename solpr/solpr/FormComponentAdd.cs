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

    public partial class FormComponentAdd : Form
    {
        ParkDBEntities db;

        public FormComponentAdd()
        {
            InitializeComponent();
        }
        
        private void FormComponentAdd_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            loadCompTypes();
            loadManufacturers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Component comp = new Component();
            Specs spec = new Specs();
            string specnames = "";
            string specvalues = "";
            comp.Type = (ComponentType)comboBox1.SelectedValue;
            comp.Model = textBox1.Text;
            if (checkManufacturerExistence(comboBox2.Text))
            {
                comp.ManufacturerId = (int)comboBox2.SelectedValue;
            }
            else
            {
                Manufacturer man = new Manufacturer();
                man.Name = comboBox2.Text;
                db.Manufacturers.Add(man);
                db.SaveChanges();
                comp.ManufacturerId = man.Id;
            }
            db.Components.Add(comp);
            db.SaveChanges();
            spec.ComponentId = comp.Id;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                specnames += dataGridView1.Rows[i].Cells[0].Value + "|";
                specvalues += dataGridView1.Rows[i].Cells[1].Value + "|";
            }
            spec.Name = specnames;
            spec.Value = specvalues;
            db.Specs.Add(spec);
            db.SaveChanges();

            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadCompTypes()
        {
            comboBox1.DataSource = Enum.GetValues(typeof(ComponentType))
                .Cast<Enum>()
                .Select(value => new
                    {
                        (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                            value
                    })
                .OrderBy(item => item.value)
                .ToList();
            comboBox1.DisplayMember = "Description";
            comboBox1.ValueMember = "Value";
        }

        private void loadManufacturers()
        {
            var manufQuery = from man in db.Manufacturers
                             orderby man.Name
                             select new
                             {
                                 man.Name, man.Id
                             };
            comboBox2.DataSource = manufQuery.ToList();
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Id";
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
        

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
