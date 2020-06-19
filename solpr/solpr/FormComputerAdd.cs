using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace solpr
{
    public partial class FormComputerAdd : Form
    {
        ParkDBEntities db;
        DataTable dataTable = new DataTable();
        bool check = true;
        private List<int> selectedComp = new List<int>();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Computer pc = new Computer();
            pc.Status = (ComputerStatus)comboBox8.SelectedValue;
            pc.EmployeeId = (int)comboBox7.SelectedValue;
            //for (int i = 0; i < dataGridView2.Rows.Count - 1; i++) {
            //    int W = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value);
            //    Component component = db.Components.Where(x => x.Id == W).FirstOrDefault();
            //    pc.Components.Add(component);
            //}
           
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
            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            //{
            //    dataTable.Columns.Add(column.Name, column.GetType());
            //}
            //DataRow newrow = dataTable.NewRow();
            if (check)
            {
                dataTable.Columns.Add("ID", typeof(int));
                dataTable.Columns.Add("Тип", typeof(string));
                dataTable.Columns.Add("Модель", typeof(string));
                dataTable.Columns.Add("Производитель", typeof(string));
                dataTable.Columns.Add("Характеристики", typeof(string));
                check = false;
            }

       
            dataTable.Rows.Add(dataGridView1.Rows[selectedComp[selectedComp.Count-1]].Cells[0].Value);

                for (int i = 1; i < dataGridView1.Rows[selectedComp[selectedComp.Count - 1]].Cells.Count; i++)
                {
                    dataTable.Rows[selectedComp.Count-1][i] = dataGridView1.Rows[selectedComp[selectedComp.Count - 1]].Cells[i].Value;
                }

            dataGridView2.DataSource = dataTable;
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
                //dataGridView2.Rows.Add(dataGridView1.SelectedRows[0]);
                //dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                //for (int i = 0; i < dataGridView2.Rows.Count; i++)
                //{
                //    dataGridView2.Rows[i].Visible = false;
                //}
                //if (iscolumn)
                //{
                //    foreach (DataGridViewColumn column in dataGridView1.Columns)
                //    {
                //        dataGridView2.Columns.Add(column);
                //    }
                //    //for (int j = 0; j < dataGridView1.Rows.Count; j++)
                //    //{
                //    //    dataGridView2.Rows.Add(dataGridView1.Rows[j].Clone() as DataGridViewRow);
                //    //}
                //    iscolumn = false;
                //}

                selectedComp.Add(dataGridView1.CurrentCell.RowIndex);
                refreshList();
                dataGridView1.Refresh();
                dataGridView2.Refresh();
            }
        }
    }
}