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
    public partial class FormComputerEdit : Form
    {
        ParkDBEntities db;
       
        public FormComputerEdit()
        {
            InitializeComponent();
        }

        private void FormComputerEdit_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            loadCompStatus();
            loadEmployees();
            loadComponents();
            int index = Program.mf.dataGridView1.SelectedRows[0].Index;
            refreshList();
            int id = 0;
            bool converted = Int32.TryParse(Program.mf.dataGridView1[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            Computer pc = db.Computers
                   .Where(p => p.Id == id)
                   .FirstOrDefault();
            Employee emplo = db.Employees
                   .Where(p => p.Id == pc.EmployeeId)
                   .FirstOrDefault();
           

            comboBox7.SelectedValue = emplo.Id;
            comboBox8.SelectedValue = pc.Status;

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (db = new ParkDBEntities())
            {
                int index = Program.mf.dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(Program.mf.dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Computer pc = db.Computers
                        .Where(p => p.Id == id)
                        .FirstOrDefault();
                //Employee emplo = db.Employees
                //       .Where(p => p.Id == pc.EmployeeId)
                //       .FirstOrDefault();

                pc.Status = (ComputerStatus)comboBox8.SelectedValue;
                pc.EmployeeId = (int)comboBox7.SelectedValue;
                //db.Computers.Add(pc);
                db.SaveChanges();
                Close();
                Program.mf.RefreshComputersGrid();
            }
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
