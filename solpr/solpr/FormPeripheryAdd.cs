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
        public FormPeripheryAdd()
        {
            InitializeComponent();
        }

        private void FormPeripheryAdd_Load(object sender, EventArgs e)
        {
            
        }

        private void loadPeripheryTypes() 
        {
            comboBox1.Items.Add("Клавиатура");
            comboBox1.Items.Add("Мышь");
            comboBox1.Items.Add("Монитор");
            comboBox1.Items.Add("Принтер");
            comboBox1.Items.Add("Веб-камера");
            comboBox1.Items.Add("Другое");
        }

        private void loadManufacturers() 
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
