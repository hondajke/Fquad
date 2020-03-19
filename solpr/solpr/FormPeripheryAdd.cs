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
            loadPeripheryTypes();
            loadManufacturers();
        }

        private void loadPeripheryTypes() 
        {
            comboBox1.DataSource = Enum.GetValues(typeof(PeripheryType))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            comboBox1.DisplayMember = "Description";
            comboBox1.ValueMember = "value";

        }

        private void loadManufacturers() 
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.ValueMember != null &&
                comboBox2.ValueMember != null && 
                comboBox3.ValueMember != null && 
                comboBox4.ValueMember != null && 
                textBox1.Text != null) { 

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
