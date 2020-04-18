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

        List<DataGridViewCell> searchCells;
        int searchCellNum = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();

        }

        private void peripheryTabShow()
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

        private void componentsTabShow()
        {
            RefreshComponentsGrid();
            List<string> colnames = new List<string>();
            foreach (DataGridViewColumn col in dataGridView3.Columns)
            {
                colnames.Add(col.Name);
            }
            comboBox1.DataSource = colnames;
        }

        private void employeeTabShow() 
        {
            RefreshEmployeeGrid();
            var depQuery = from dep in db.Departments
                           orderby dep.Name
                           select new
                           {
                               dep.Name,
                               dep.Id
                           };
            comboBox1.DataSource = depQuery.ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
            comboBox1.Text = "По отделам";
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
        public void RefreshPeripheryGrid()
        {
            var result = from periphery in db.Peripheries
                         join manufac in db.Manufacturers on periphery.ManufacturerId equals manufac.Id
                         join specs in db.Specs on periphery.Id equals specs.PeripheryId
                         join empl in db.Employees on periphery.EmployeeId equals empl.Id
                         select new
                         {
                             ID = periphery.Id,
                             Тип = periphery.Type,
                             Модель = periphery.Model,
                             Производитель = manufac.Name,
                             Характеристики = specs.Name + " - " + specs.Value,
                             Сотрудник = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name
                         };
            dataGridView2.DataSource = result.ToList();
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
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            componentsTabShow();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            peripheryTabShow();
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            employeeTabShow();
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
                    int numberOfRowDeleted = db.Database.ExecuteSqlCommand("DELETE FROM dbo.Specs WHERE PeripheryId=@param", param);
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

        private void FilterDataView()
        {
            /*DataTable dt = new DataTable();
            DataView view = new DataView();

            view = dt.DefaultView;
            string filter = string.Format("CONVERT(" + dataGridView4.Columns[2].DataPropertyName + ", System.String)  LIKE '" + textBox1.Text + "*'");
            view.RowFilter = filter;
            dataGridView4.DataSource = view;*/


            //dt.DefaultView.RowFilter = string.Format(comboBox1.Text + " like '%{0}%'", textBox1.Text);
            //dataGridView4.Refresh();


            // (dataGridView4.DataSource as DataTable).DefaultView.RowFilter = string.Format(comboBox1.Text + " like '{0}%'", textBox1.Text);
            /* BindingSource bs = new BindingSource();
             bs.DataSource = dataGridView4.DataSource;
             bs.Filter = string.Format(comboBox1.Text + " like '" + textBox1.Text + "*'");
             dataGridView4.DataSource = bs;
             dataGridView4.Refresh();*/
            /*  BindingSource bs = new BindingSource();
              bs.DataSource = dataGridView4.DataSource;
              bs.Filter = string.Format(comboBox1.Text + " like '" + textBox1.Text + "*'");
              dataGridView4.DataSource = bs;
              dataGridView4.Refresh();*/
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            searchCellNum = 0;
            searchCells = new List<DataGridViewCell>();
            RefreshActiveGrid();
            foreach (DataGridViewRow row in activeGrid().Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString().ToLower().Contains(textBox2.Text.ToLower()) && textBox2.Text != "")
                    {
                        searchCells.Add(cell);
                        cell.Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        cell.Style.BackColor = Color.White;
                    }
                }
            }
            if (searchCells.Count != 0) activeGrid().CurrentCell = searchCells[0];
            activeGrid().Refresh();
        }

        private void RefreshActiveGrid()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:

                    break;
                case 1:
                    RefreshPeripheryGrid();
                    break;
                case 2:
                    RefreshComponentsGrid();
                    break;
                case 3:
                    RefreshEmployeeGrid();
                    break;
                default:
                    break;
            }
        }

        private DataGridView activeGrid()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    return dataGridView1;
                case 1:
                    return dataGridView2;
                case 2:
                    return dataGridView3;
                case 3:
                    return dataGridView4;
                default:
                    return dataGridView1;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && searchCells.Count != 0)
            {
                if (searchCellNum < searchCells.Count - 1)
                {
                    searchCellNum++;
                }
                else
                {
                    searchCellNum = 0;
                }
                activeGrid().CurrentCell = searchCells[searchCellNum];
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && searchCells.Count != 0)
            {
                if (searchCellNum > 0)
                {
                    searchCellNum--;
                }
                else
                {
                    searchCellNum = searchCells.Count - 1;
                }
                activeGrid().CurrentCell = searchCells[searchCellNum];
            }
        }


        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < activeGrid().Rows.Count - 1; i++)
            {
                activeGrid().Rows[i].Visible = false;
                for (int c = 0; c < activeGrid().Columns.Count; c++)
                {
                    if (activeGrid()[c, i].Value.ToString() == comboBox1.Text)
                    {
                        activeGrid().Rows[i].Visible = true;
                        break;
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
            {
                searchCells = new List<DataGridViewCell>();
                searchCellNum = 0;
            }

        private void пКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage0"];
            dataGridView1.DataSource = db.Computers.ToList();
            List<string> colnames = new List<string>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                colnames.Add(col.Name);
            }
            comboBox1.DataSource = colnames;
        }

        private void периферияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
            peripheryTabShow();
        }

        private void комплектующиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
            componentsTabShow();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
            employeeTabShow();
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage4"];
        }
    }
    }

