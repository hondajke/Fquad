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
        ToolTip toolTip1 = new ToolTip()
        {
            AutoPopDelay = 5000,
            InitialDelay = 500,
            ReshowDelay = 100,
            ShowAlways = true,
        };
        bool inButton = false;
        List<DataGridViewCell> searchCells;
        int searchCellNum = 0;

        private string reportFile = "";

        public int dataGridNumber = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            this.KeyPreview = true;
        }

        private void peripheryTabShow()
        {
            RefreshPeripheryGrid();
            dataGridView2.AutoGenerateColumns = false;
        }

        private void componentsTabShow()
        {
            RefreshComponentsGrid();
        }

        private void employeeTabShow() 
        {
            RefreshEmployeeGrid();
        }

        private void computerTabShow()
        {
            RefreshComputersGrid();
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
                         join specs in db.Specs on comps.Id equals specs.ComponentId
                         select new
                         {
                             ID = comps.Id,
                             Тип = comps.Type,
                             Модель = comps.Model,
                             Производитель = manufac.Name,
                             Характеристики = specs.Name + "−" + specs.Value
                         };
            
            dataGridView3.DataSource = result.ToList();
        }
        public void RefreshComputersGrid()
        {
            var result = from pc in db.Computers
                         join empl in db.Employees on pc.EmployeeId equals empl.Id
                         select new
                         {
                             ID = pc.Id,
                             Статус = pc.Status,
                             Сотрудник = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name
                         };
            dataGridView1.DataSource = result.ToList();
        }

        private void deletePeriphery() 
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
               /* System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@param", id);
                int numberOfRowDeleted = db.Database.ExecuteSqlCommand("DELETE FROM dbo.Specs WHERE PeripheryId=@param", param);
                db.SaveChanges();*/

                Periphery peri = db.Peripheries
                    .Where(p => p.Id == id)
                    .FirstOrDefault();

                db.Peripheries.Remove(peri);
                db.SaveChanges();
                RefreshPeripheryGrid();
            }
            else 
            {
                MessageBox.Show("Сначала выберите");
            }
        }

        private void deleteComponent() 
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
                    Specs spec = db.Specs
                        .Where(p => p.ComponentId == id)
                        .FirstOrDefault();
                    db.Components.Remove(comp);
                    db.Specs.Remove(spec);
                    db.SaveChanges();
                    RefreshComponentsGrid();
                }
                else 
                {
                    MessageBox.Show("Сначала выберите");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteEmployee() 
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
                else 
                {
                    MessageBox.Show("Сначала выберите");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteComputer()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int index = dataGridView1.SelectedRows[0].Index;
                    int id = 0;
                    bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    Computer pc = db.Computers
                        .Where(p => p.Id == id)
                        .FirstOrDefault();

                    db.Computers.Remove(pc);
                    db.SaveChanges();
                    RefreshComputersGrid();
                }
                else
                {
                    MessageBox.Show("Сначала выберите");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editPeriphery() 
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
            else 
            {
                MessageBox.Show("Сначала выберите");
            }
        }

        private void editComponent() 
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

        private void editEmployee() 
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
            else 
            {
                MessageBox.Show("Сначала выберите");
            }
        }

        private void editComputer()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;

                FormComputerEdit Edit = new FormComputerEdit();
                Edit.ShowDialog();
            }
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            componentsTabShow();
            dataGridNumber = 3;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            peripheryTabShow();
            dataGridNumber = 2;
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            employeeTabShow();
            dataGridNumber = 4;
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage0_Enter(object sender, EventArgs e)
        {
            computerTabShow();
            dataGridNumber = 1;
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
            RefreshPeripheryGrid();
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
            RefreshComputersGrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            deletePeriphery();
        }
        

        private void button6_Click(object sender, EventArgs e)
        {
            deleteEmployee();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            deleteComputer();
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
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Тип")
            {
                PeripheryType enumValue = (PeripheryType)e.Value;
                GetTypes a = new GetTypes();
                string enumstring = a.GetDescription(enumValue);
                e.Value = enumstring;
            }
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
            editPeriphery();
            RefreshPeripheryGrid();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            editEmployee();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            deleteComponent();
        }


        private void button12_Click(object sender, EventArgs e)
        {
            editComponent();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            editComputer();
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
                    RefreshComputersGrid();
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

  /*      private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterByDepartment();
        }*/

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
            {
                searchCells = new List<DataGridViewCell>();
                searchCellNum = 0;
            }

        private void пКToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /*tabControl1.SelectedTab = tabControl1.TabPages["tabPage0"];
            dataGridView1.DataSource = db.Computers.ToList();
            List<string> colnames = new List<string>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                colnames.Add(col.Name);
            }
            comboBox1.DataSource = colnames;*/
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

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage1"]) 
            {
                MessageBox.Show("Сначала перейдите во вкладку: Периферия");
            }
            else 
            {
                FormPeripheryAdd dial = new FormPeripheryAdd();
                dial.ShowDialog();
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage1"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Периферия");
            }
            else
            {
                deletePeriphery();
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage1"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Периферия");
            }
            else
            {
                editPeriphery();
            }
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage2"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Комплектующие");
            }
            else
            {
                FormComponentAdd dial = new FormComponentAdd();
                dial.ShowDialog();
                RefreshComponentsGrid();
            }
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage2"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Комплектующие");
            }
            else
            {
                deleteComponent();
            }
        }

        private void редактироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage2"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Комплектующие");
            }
            else
            {
                editComponent();
            }
        }

        private void добавитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage3"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Сотрудники");
            }
            else
            {
                FormEmployeeAdd dial = new FormEmployeeAdd();
                dial.ShowDialog();
            }
        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage3"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Сотрудники");
            }
            else
            {
                deleteEmployee();
            }
        }

        private void редактироватьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage3"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Сотрудники");
            }
            else
            {
                editEmployee();
            }
        }

        private void добавитьОтделToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage3"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: Сотрудники");
            }
            else
            {
                FormDepartmentAdd dial = new FormDepartmentAdd();
                dial.ShowDialog();
            }
        }

        private void добавитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage0"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: ПК");
            }
            else
            {
                FormComputerAdd dial = new FormComputerAdd();
                dial.ShowDialog();
                RefreshComputersGrid();
            }
        }

        private void удалитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage0"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: ПК");
            }
            else
            {
                deleteComputer();
            }
        }
    
        private void редактироватьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
             if (tabControl1.SelectedTab != tabControl1.TabPages["tabPage0"])
            {
                MessageBox.Show("Сначала перейдите во вкладку: ПК");
            }
            else
            {
                editComputer();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Filter fil = new Filter();
            fil.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            RefreshActiveGrid();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            FormManufacturerAdd dial = new FormManufacturerAdd();
            dial.ShowDialog();
        }
        
        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView3.Columns[e.ColumnIndex].Name == "Тип")
            {
                ComponentType enumValue = (ComponentType)e.Value;
                GetTypes a = new GetTypes();
                string enumstring = a.GetDescription(enumValue);
                e.Value = enumstring;
            }
            if (dataGridView3.Columns[e.ColumnIndex].Name == "Характеристики")
            {
                string[] specs = e.Value.ToString().Split('−');
                int maxNumOfSpecs = specs[0].Count(x => x == '|');
                if (specs[1].Count(x => x == '|') > maxNumOfSpecs) {
                    maxNumOfSpecs = specs[1].Count(x => x == '|');
                }
                string specstring = "";
                string[] specNames = specs[0].Split('|');
                string[] specValues = specs[1].Split('|');
                for (int i = 0; i < maxNumOfSpecs; i++)
                {
                    specstring += specNames[i] + " - " + specValues[i] + "\n";
                }
                e.Value = specstring;
            }
        }
        
        private void button19_Click(object sender, EventArgs e)
        {
            FormManufacturerAdd dial = new FormManufacturerAdd();
            dial.ShowDialog();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.A)
            {
                Form add = returnAddForm();
                add.ShowDialog();
            }
            if (e.Control == true && e.KeyCode == Keys.D) 
            {
                deleteHotkey();
            }
            if (e.Control == true && e.KeyCode == Keys.E) 
            {
                editHotkey();
            }
            if (e.Control == true && e.KeyCode == Keys.M) 
            {
                returnAddOther();
            }
        }

        private Form returnAddForm() 
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    FormComputerAdd dial = new FormComputerAdd();
                    RefreshComputersGrid();
                    return dial;
                case 1:
                    FormPeripheryAdd dial1 = new FormPeripheryAdd();
                    RefreshPeripheryGrid();
                    return dial1;
                case 2:
                    FormComponentAdd dial2 = new FormComponentAdd();
                    return dial2;
                case 3:
                    FormEmployeeAdd dial3 = new FormEmployeeAdd();
                    return dial3;
                default:
                    return null;
            }
        }

        private Form returnAddOther() 
        {
            switch (tabControl1.SelectedIndex) 
            {
                case 1:
                    FormManufacturerAdd dial = new FormManufacturerAdd();
                    dial.ShowDialog();
                    return dial;
                case 2:
                    FormManufacturerAdd dial1 = new FormManufacturerAdd();
                    dial1.ShowDialog();
                    return dial1;
                case 3:
                    FormDepartmentAdd dial2 = new FormDepartmentAdd();
                    dial2.ShowDialog();
                    return dial2;
                default:
                    return null;
            }
        }

        private void deleteHotkey() 
        {
            switch (tabControl1.SelectedIndex) 
            {
                case 0:
                    deleteComputer();
                    break;
                case 1:
                    deletePeriphery();
                    break;
                case 2:
                    deleteComponent();
                    break;
                case 3:
                    deleteEmployee();
                    break;
                default:
                    break;
            }
        }

        private void editHotkey() 
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    editComputer();
                    break;
                case 1:
                    editPeriphery();
                    break;
                case 2:
                    editComponent();
                    break;
                case 3:
                    editEmployee();
                    break;
                default:
                    break;
            }
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + A", button2, 10000);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            /*toolTip1.Hide(button2);
            inButton = false;*/
        }

        private void button20_Click(object sender, EventArgs e)
        {
            FormReportAdd dial = new FormReportAdd();
            dial.ShowDialog();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Pdf Files|*.pdf";
            DialogResult dial = openFileDialog1.ShowDialog();
            if (dial == DialogResult.OK)
            {
                reportFile = openFileDialog1.FileName;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (reportFile != "")
            {
                saveFileDialog1.Filter = "Pdf Files|*.pdf";
                DialogResult dial = saveFileDialog1.ShowDialog();
                if (dial == DialogResult.OK)
                {
                    
                }
            }
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + A", button4, 10000);
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + A", button1, 10000);
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + A", button3, 10000);
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + D", button11, 10000);
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + D", button5, 10000);
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + D", button10, 10000);
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + D", button6, 10000);
        }

        private void button16_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + E", button16, 10000);
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + E", button8, 10000);
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + E", button12, 10000);
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + E", button9, 10000);
        }

        private void button19_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + M", button19, 10000);
        }

        private void button18_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + M", button18, 10000);
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + M", button7, 10000);
        }
    }

}

