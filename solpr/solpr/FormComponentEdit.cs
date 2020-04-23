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
    public partial class FormComponentEdit : Form
    {
        ParkDBEntities db;

        public FormComponentEdit()
        {
            InitializeComponent();

        }
        private void FormComponentEdit_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            loadCompTypes();
            loadManufacturers();
            int index = Program.mf.dataGridView3.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(Program.mf.dataGridView3[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            Component comp = db.Components
                   .Where(p => p.Id == id)
                   .FirstOrDefault();
            comboBox1.SelectedValue = comp.Type;
            textBox1.Text = comp.Model;
            comboBox2.SelectedValue = comp.ManufacturerId;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index = Program.mf.dataGridView3.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(Program.mf.dataGridView3[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            Component comp = db.Components
                   .Where(p => p.Id == id)
                   .FirstOrDefault();
            comp.Type = (ComponentType)comboBox1.SelectedValue;
            comp.Model = textBox1.Text;
            comp.ManufacturerId = (int)comboBox2.SelectedValue;
            db.SaveChanges();
            Close();
            Program.mf.RefreshComponentsGrid();
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
                                 man.Name,
                                 man.Id
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

        
    }
}
