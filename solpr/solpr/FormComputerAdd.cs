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
    public partial class FormComputerAdd : Form
    {
        ParkDBEntities db;
        public FormComputerAdd()
        {
            InitializeComponent();
        }

        private void FormComputerAdd_Load_1(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            loadCompStatus();
            loadEmployees();
            loadComponents();
            refreshList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Computer pc = new Computer();
            pc.Status = (ComputerStatus)comboBox8.SelectedValue;
            pc.EmployeeId = (int)comboBox7.SelectedValue;
            db.Computers.Add(pc);
            db.SaveChanges();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadCompStatus()
        {
            comboBox8.DataSource = Enum.GetValues(typeof(ComputerStatus))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            comboBox8.DisplayMember = "Description";
            comboBox8.ValueMember = "value";
        }
        private void loadEmployees()
        {
            comboBox7.DataSource = db.Employees.ToList();
            comboBox7.DisplayMember = "Surname";
            comboBox7.ValueMember = "Id";
        }

        public void loadComponents()
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

        public void refreshList()
        {
            dataGridView2.DataSource = dataGridView1.DataSource;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                //row.re
            }
        }

        public void refreshList3()
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Тип", typeof(ComponentType));
            dt.Columns.Add("Модель", typeof(string));
            dt.Columns.Add("Видеокарта", typeof(string));
            dt.Columns.Add("Оперативная память", typeof(string));
            dt.Columns.Add("Жесткий диск", typeof(string));
            dt.Columns.Add("Твердотельный накопитель", typeof(string));
            dt.Columns.Add("Аудиокарта", typeof(string));
            dt.Columns.Add("Привод", typeof(string));
            dt.Columns.Add("Другое", typeof(string));        
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var result = from comps in db.Components
                         join manufac in db.Manufacturers on comps.ManufacturerId equals manufac.Id
                         join specs in db.Specs on comps.Id equals specs.ComponentId
                         select new
                         {
                             ID = comps.Id,
                             Тип = comps.Type,
                             Модель = comps.Model,
                             Производитель = manufac.Name,
                             Характеристики = specs.Name + "−" + specs.Value
                         };
            var componentsFilter = result.AsEnumerable();
            componentsFilter = componentsFilter.Where(x => x.Тип.ToString() == comboBox1.SelectedValue.ToString());
            dataGridView1.DataSource = componentsFilter.ToList();
            dataGridView1.Refresh();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView2.Rows.Add(dataGridView1.SelectedRows[0]);
                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                dataGridView1.Refresh();
                dataGridView2.Refresh();
            }
        }
    }
}
