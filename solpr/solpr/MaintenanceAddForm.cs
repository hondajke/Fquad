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
    public partial class MaintenanceAddForm : Form
    {
        ParkDBEntities db;
        private Computer pc;

        public MaintenanceAddForm()
        {
            InitializeComponent();
        }

        public MaintenanceAddForm(Computer pc)
        {
            InitializeComponent();
            this.pc = pc;
            db = new ParkDBEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Maintenance maint = new Maintenance();
            maint.ComputerId = pc.Id;
            maint.Description = textBox1.Text;
            maint.RepairStart = dateTimePicker1.Value;
            db.Maintenance.Add(maint);
            db.SaveChanges();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
