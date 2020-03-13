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
    public partial class Form1 : Form
    {
        ParkDBEntities db;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            //dataGridView3.DataSource = 
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            dataGridView4.DataSource = db.Employees.ToList();
        }

        

    }
}
