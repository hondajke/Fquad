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
    public partial class FormManufacturerAdd : Form
    {
        ParkDBEntities db;

        public FormManufacturerAdd()
        {
            InitializeComponent();
        }

        private void FormManufacturerAdd_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (db = new ParkDBEntities())
            {
                Manufacturer man = new Manufacturer();
                man.Name = textBox1.Text;
                db.Manufacturers.Add(man);
                db.SaveChanges();
                Close();
                Program.mf.RefreshComponentsGrid();
            }
        }

    }
}
