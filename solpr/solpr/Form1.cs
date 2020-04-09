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
            RefreshComponentsGrid();
            List<string> colnames = new List<string>();
            foreach (DataGridViewColumn col in dataGridView3.Columns)
            {
                colnames.Add(col.Name);
            }
            comboBox1.DataSource = colnames;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            RefreshPeripheryGrid();
            dataGridView2.AutoGenerateColumns = false;
            List<string> colnames = new List<string>();
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                colnames.Add(col.Name);
            }
            comboBox1.DataSource = colnames;
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            RefreshEmployeeGrid();
            List<string> colnames = new List<string>();
            foreach (DataGridViewColumn col in dataGridView4.Columns)
            {
                colnames.Add(col.Name);
            }
            comboBox1.DataSource = colnames;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage0_Enter(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Computers.ToList();
            List<string> colnames = new List<string>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                colnames.Add(col.Name);
            }
            comboBox1.DataSource = colnames;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormComponentAdd dial = new FormComponentAdd();
            dial.ShowDialog();
            RefreshComponentsGrid();
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
                    System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@param", id);
                    int numberOfRowDeleted = db.Database.ExecuteSqlCommand("DELETE FROM dbo.Specs WHERE PeripheryId=@param",param);
                    db.SaveChanges();
                    
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
        public void RefreshPeripheryGrid() 
        {
            var result = from periphery in db.Peripheries
                         join manufac in db.Manufacturers on periphery.ManufacturerId equals manufac.Id
                         join specs in db.Specs on periphery.Id equals specs.PeripheryId
                         join empl in db.Employees on periphery.EmployeeId equals empl.Id
                         select new
                         {
                             Айди = periphery.Id,
                             Тип = periphery.Type,
                             Модель = periphery.Model,
                             Производитель = manufac.Name,
                             Характеристики = specs.Name + " - " + specs.Value,
                             Сотрудник = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name
                         };
            dataGridView2.DataSource = result.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.SelectedRows.Count > 0)
                {
                    int index = dataGridView4.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView4[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    Employee emplo = db.Employees
                        .Where(p => p.Id == id)
                        .FirstOrDefault();

                    db.Employees.Remove(emplo);
                    db.SaveChanges();
                    RefreshEmployeeGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RefreshEmployeeGrid()
        {
            var result = from employee in db.Employees
                         join department in db.Departments on employee.DepartmentId equals department.Id
                         select new
                         {
                             ID = employee.Id,
                             Фамилия = employee.Surname,
                             Имя = employee.Name,
                             Отчество = employee.Patronymic_Name,
                             ID_отдела = employee.DepartmentId,
                             Отдел = department.Name
                         };
            dataGridView4.DataSource = result.ToList();
        }

        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dataGridView2[column, row];
            DataGridViewCell cell2 = dataGridView2[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }
        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dataGridView2.AdvancedCellBorderStyle.Top;
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormDepartmentAdd dial = new FormDepartmentAdd();
            dial.ShowDialog();
        }


        private void button8_Click(object sender, EventArgs e)
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

                FormPeripheryEdit Edit = new FormPeripheryEdit(peri);
                Edit.ShowDialog();

                /*peri.Age = (int)Edit.numericUpDown1.Value;
                peri.Name = Edit.textBox1.Text;
                peri.Position = Edit.comboBox1.SelectedItem.ToString();
                peri.Team = (Team)Edit.comboBox2.SelectedItem;

                db.Entry(peri).State = EntityState.Modified;
                db.SaveChanges();*/

                //MessageBox.Show("Объект обновлен");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int index = dataGridView4.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView4[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                /* Employee emplo = db.Employees
                         .Where(p => p.Id == id)
                         .FirstOrDefault();*/

                FormEmployeeEdit Edit = new FormEmployeeEdit();
                Edit.ShowDialog();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.SelectedRows.Count > 0)
                {
                    int index = dataGridView3.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView3[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    Component comp = db.Components
                        .Where(p => p.Id == id)
                        .FirstOrDefault();

                    db.Components.Remove(comp);
                    db.SaveChanges();
                    RefreshComponentsGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RefreshComponentsGrid()
        {
            var result = from comps in db.Components
                         join manufac in db.Manufacturers on comps.ManufacturerId equals manufac.Id
                         select new
                         {
                             ID = comps.Id,
                             Тип = comps.Type,
                             Производитель = manufac.Name,
                             //Характеристики = specs.Name + " - " + specs.Value
                         };
            dataGridView3.DataSource = result.ToList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RefreshPeripheryGrid();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int index = dataGridView3.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView3[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                
                FormComponentEdit Edit = new FormComponentEdit();
                Edit.ShowDialog();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
