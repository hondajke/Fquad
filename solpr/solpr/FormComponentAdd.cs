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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
            comboBox1.ValueMember = "value";

        }

        private void loadManufacturers()
        {
            //comboBox2.DataSource = db.Manufacturers.
        }

    }
}
