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
            dataGridView3.DataSource = db.Components.ToList();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            RefreshPeripheryGrid();
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            dataGridView4.DataSource = db.Employees.ToList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage0_Enter(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Computers.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormComponentAdd dial = new FormComponentAdd();
            dial.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormPeripheryAdd dial = new FormPeripheryAdd();
            dial.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormEmployeeAdd dial = new FormEmployeeAdd();
            dial.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormComputerAdd dial = new FormComputerAdd();
            dial.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    int index = dataGridView2.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    Periphery peri = db.Peripheries
                        .Where(p => p.Id == id)
                        .FirstOrDefault();

                    db.Peripheries.Remove(peri);
                    db.SaveChanges();
                    RefreshPeripheryGrid();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RefreshPeripheryGrid() 
        {
            var result = from periphery in db.Peripheries
                         join manufac in db.Manufacturers on periphery.ManufacturerId equals manufac.Id
                         join spec in db.Specs on periphery.SpecId equals spec.Id
                         join empl in db.Employees on periphery.EmployeeId equals empl.Id
                         select new
                         {
                             Айди = periphery.Id,
                             Тип = periphery.Type,
                             Модель = periphery.Model,
                             Производитель = manufac.Name,
                             Характеристика = spec.Name + " - " + spec.Value,
                             Сотрудник = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name
                         };
            dataGridView2.DataSource = result.ToList();
        }
    }
}
