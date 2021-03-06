﻿using System;
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
        List<DataGridViewCell> searchCells;
        int searchCellNum = 0;
        int gridHeader = 0;

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

        private void applyDataGridViewStyles(DataGridView date) 
        {
            //date.Columns[0].Visible = false;
            date.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            date.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void groupBoxView(DataGridView grid) 
        {
            groupBox1.Controls.Clear();
            int count = grid.Columns.Count;
            CheckBox[] checks = new CheckBox[count];
            for (int i = 0; i < count; i++) 
            {
                gridHeader = i;
                checks[i] = new CheckBox();
                checks[i].Location = new Point(4, 19 + i * checks[i].Size.Height);
                checks[i].Text = grid.Columns[i].HeaderText;
                checks[i].Checked = true;
                checks[i].CheckedChanged += CheckBox_CheckedChanged;
                groupBox1.Controls.Add(checks[i]);
            }
            Button btn = new Button();
            btn.Text = "Скрыть";
            btn.Location = new Point(4, 19 + count * btn.Size.Height);
            btn.Click += viewButton_Click;
            groupBox1.Controls.Add(btn);
        }

        private void CheckBox_CheckedChanged(Object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            DataGridView dgv = activeGrid();
            foreach (DataGridViewColumn clm in dgv.Columns)
            {
                if (check.Checked == false && clm.HeaderText == check.Text)
                {
                    dgv.Columns[clm.Index].Visible = false;
                }
                else
                {
                    dgv.Columns[clm.Index].Visible = true;
                }
            }
        }

        private void viewButton_Click(Object sender, EventArgs e) 
        {
            Button btn = (Button)sender;
            if (btn.Text == "Скрыть")
            {
                for (int i = 0; i < groupBox1.Controls.Count; i++)
                {
                    if (groupBox1.Controls[i] is CheckBox)
                    {
                        ((CheckBox)groupBox1.Controls[i]).Visible = false;
                    }
                }
                btn.Location = new Point(4, 19);
                btn.Text = "Показать";
                groupBox1.Controls.Add(btn);
            }
            else 
            {
                for (int i = 0; i < groupBox1.Controls.Count; i++)
                {
                    if (groupBox1.Controls[i] is CheckBox)
                    {
                        ((CheckBox)groupBox1.Controls[i]).Visible = true;
                    }
                }
                btn.Location = new Point(4, 19 + (groupBox1.Controls.Count - 1) * btn.Size.Height);
                btn.Text = "Скрыть";
                groupBox1.Controls.Add(btn);
            }
        }
        public void RefreshEmployeeGrid()
        {
            var result = from employee in db.Employees
                         join department in db.Departments on employee.DepartmentId equals department.Id
                         select new
                         {
                             ID = employee.Id,
                             ФИО = employee.Surname + " " + employee.Name + " " + employee.Patronymic_Name,
                             /*Имя = employee.Name,
                             Отчество = employee.Patronymic_Name,*/
                             ID_отдела = employee.DepartmentId,
                             Отдел = department.Name
                         };
            dataGridView4.DataSource = result.ToList();
            applyDataGridViewStyles(dataGridView4);
            groupBoxView(dataGridView4);
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
                             Характеристики = specs.Name + "−" + specs.Value,
                             IDСотрудника = empl.Id,
                             Сотрудник = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name
                         };
            dataGridView2.DataSource = result.ToList();
            applyDataGridViewStyles(dataGridView2);
            groupBoxView(dataGridView2);
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
            applyDataGridViewStyles(dataGridView3);
            groupBoxView(dataGridView3);
        }
        public void RefreshComputersGrid()
        {
            var result = from pc in db.Computers
                         join empl in db.Employees on pc.EmployeeId equals empl.Id
                         join comps in db.Components on pc.Id equals comps.Id
                         select new
                         {
                             ID = pc.Id,
                             Статус = pc.Status,
                             Сотрудник = empl.Surname + " " + empl.Name + " " + empl.Patronymic_Name,
                             Процессор = comps.Model,
                             Материнская_плата = comps.Model,
                             //Видеокарта 
                             //Оперативная память
                             //Жесткий диск
                             //Твердотельный накопитель
                             //Аудиокарта
                             //Привод
                             //Другое
                         };
            dataGridView1.DataSource = result.ToList();
            applyDataGridViewStyles(dataGridView1);
            groupBoxView(dataGridView1);
        }

        private void deletePeriphery() 
        {
            try
            {
                if (dataGridView2.SelectedCells.Count > 0)
                {
                    int id = 0;
                    int rowId = Convert.ToInt32(dataGridView2.SelectedCells[0].RowIndex.ToString());
                    bool converted = Int32.TryParse(dataGridView2[0,rowId].Value.ToString(), out id);
                    if (converted == false)
                        return;
                    Periphery peri = db.Peripheries
                        .Where(p => p.Id == id)
                        .FirstOrDefault();
                    Specs spec = db.Specs
                            .Where(p => p.PeripheryId == id)
                            .FirstOrDefault();
                    db.Specs.Remove(spec);
                    db.Peripheries.Remove(peri);
                    db.SaveChanges();
                    RefreshPeripheryGrid();
                }
                else
                {
                    MessageBox.Show("Сначала выберите");
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteComponent() 
        {
            try
            {
                if (dataGridView3.SelectedCells.Count > 0)
                {
                    int id = 0;
                    int rowId = Convert.ToInt32(dataGridView3.SelectedCells[0].RowIndex.ToString());
                    bool converted = Int32.TryParse(dataGridView3[0, rowId].Value.ToString(), out id);
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
                if (dataGridView4.SelectedCells.Count > 0)
                {
                    int id = 0;
                    int rowId = Convert.ToInt32(dataGridView4.SelectedCells[0].RowIndex.ToString());
                    bool converted = Int32.TryParse(dataGridView4[0, rowId].Value.ToString(), out id);
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
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int id = 0;
                    int rowId = Convert.ToInt32(dataGridView1.SelectedCells[0].RowIndex.ToString());
                    bool converted = Int32.TryParse(dataGridView1[0, rowId].Value.ToString(), out id);
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
            if (dataGridView2.SelectedCells.Count > 0)
            {
                int id = 0;
                int rowId = Convert.ToInt32(dataGridView2.SelectedCells[0].RowIndex.ToString());
                bool converted = Int32.TryParse(dataGridView2[0, rowId].Value.ToString(), out id);
                if (converted == false)
                    return;

                Periphery peri = db.Peripheries
                        .Where(p => p.Id == id)
                        .FirstOrDefault();

                FormPeripheryEdit Edit = new FormPeripheryEdit(peri);
                Edit.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Сначала выберите");
            }
        }

        private void editComponent() 
        {
            if (dataGridView3.SelectedCells.Count > 0)
            {
                int id = 0;
                int rowId = Convert.ToInt32(dataGridView3.SelectedCells[0].RowIndex.ToString());
                bool converted = Int32.TryParse(dataGridView3[0, rowId].Value.ToString(), out id);
                if (converted == false)
                    return;

                FormComponentEdit Edit = new FormComponentEdit();
                Edit.ShowDialog();
            }
        }

        private void editEmployee() 
        {
            if (dataGridView4.SelectedCells.Count > 0)
            {
                int id = 0;
                int rowId = Convert.ToInt32(dataGridView4.SelectedCells[0].RowIndex.ToString());
                bool converted = Int32.TryParse(dataGridView4[0, rowId].Value.ToString(), out id);
                if (converted == false)
                    return;

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
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int id = 0;
                int rowId = Convert.ToInt32(dataGridView1.SelectedCells[0].RowIndex.ToString());
                bool converted = Int32.TryParse(dataGridView1[0, rowId].Value.ToString(), out id);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchCells = new List<DataGridViewCell>();
            searchCellNum = 0;
            hideshowFilterButtons();
        }

        private void пКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage0"];
            computerTabShow();
            hideshowFilterButtons();
            пКToolStripMenuItem.Owner.Hide();
        }

        private void периферияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
            peripheryTabShow();
            hideshowFilterButtons();
            периферияToolStripMenuItem.Owner.Hide();
        }

        private void комплектующиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
            componentsTabShow();
            hideshowFilterButtons();
            комплектующиеToolStripMenuItem.Owner.Hide();
        }

        private void сотрудникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
            employeeTabShow();
            hideshowFilterButtons();
            сотрудникиToolStripMenuItem.Owner.Hide();
        }

        private void отчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage4"];
            hideshowFilterButtons();
            отчетыToolStripMenuItem.Owner.Hide();
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
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Тип")
            {
                cellFormattingType(sender, e);
            }
            if (dataGridView2.Columns[e.ColumnIndex].Name == "Характеристики")
            {
                cellFormattingSpecs(sender, e);
            }
        }


        private void dataGridView3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView3.Columns[e.ColumnIndex].Name == "Тип")
            {
                cellFormattingType(sender, e);
            }
            if (dataGridView3.Columns[e.ColumnIndex].Name == "Характеристики")
            {
                cellFormattingSpecs(sender, e);
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
                returnAddForm();
            if (e.Control == true && e.KeyCode == Keys.D) 
                deleteHotkey();
            if (e.Control == true && e.KeyCode == Keys.E) 
                editHotkey();
            if (e.Control == true && e.KeyCode == Keys.M) 
                returnAddOther();
            if (e.Control == true && e.KeyCode == Keys.O && tabControl1.SelectedIndex == 4) 
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "Pdf Files|*.pdf";
                DialogResult dial = openFileDialog1.ShowDialog();
                if (dial == DialogResult.OK)
                {
                    reportFile = openFileDialog1.FileName;
                }
            }
            if (e.Control == true && e.KeyCode == Keys.S && tabControl1.SelectedIndex == 4) 
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
            if (e.Control == true && e.KeyCode == Keys.J && tabControl1.SelectedIndex == 0) 
            {
                MaintenanceLogForm maint = new MaintenanceLogForm();
                maint.ShowDialog();
            }
            if (e.Control == true && e.KeyCode == Keys.F) 
            {
                Filter fil = new Filter();
                fil.ShowDialog();
            }
            if (e.Control == true && e.KeyCode == Keys.F && e.Alt == true) 
            {
                RefreshActiveGrid();
            }
        }

        private Form returnAddForm() 
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    FormComputerAdd dial = new FormComputerAdd();
                    dial.ShowDialog();
                    return dial;
                case 1:
                    FormPeripheryAdd dial1 = new FormPeripheryAdd();
                    dial1.ShowDialog();
                    return dial1;
                case 2:
                    FormComponentAdd dial2 = new FormComponentAdd();
                    dial2.ShowDialog();
                    return dial2;
                case 3:
                    FormEmployeeAdd dial3 = new FormEmployeeAdd();
                    dial3.ShowDialog();
                    return dial3;
                case 4:
                    FormReportAdd dial4 = new FormReportAdd();
                    dial4.ShowDialog();
                    return dial4;
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

        //Reports

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
                webBrowser1.Navigate(reportFile);
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

        private void hideshowFilterButtons() 
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage4"])
            {
                button17.Visible = false;
                button15.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                button13.Visible = false;
                button14.Visible = false;
                groupBox1.Visible = false;
            }
            else 
            {
                button17.Visible = true;
                button15.Visible = true;
                label6.Visible = true;
                textBox2.Visible = true;
                button13.Visible = true;
                button14.Visible = true;
                groupBox1.Visible = true;
            }
        }

        private void button20_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + A", button20, 10000);
        }

        private void button21_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + O", button21, 10000);
        }

        private void button22_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + S", button22, 10000);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            MaintenanceLogForm maint = new MaintenanceLogForm();
            maint.ShowDialog();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            intoRepair();
        }

        private void intoRepair()
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
                    AddMaintenanceNote maint = new AddMaintenanceNote(pc);
                    maint.ShowDialog();
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

        private void button23_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + J", button23, 10000);
        }

        private void button15_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + F", button15, 10000);
        }

        private void button17_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Ctrl + Alt + F", button17, 10000);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filter fil = new Filter();
            fil.ShowDialog();
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshActiveGrid();
        }

        //Cell format

        public void cellFormattingSpecs(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string[] specs = e.Value.ToString().Split('−');
            int maxNumOfSpecs = specs[0].Count(x => x == '|');
            if (specs[1].Count(x => x == '|') > maxNumOfSpecs)
            {
                maxNumOfSpecs = specs[1].Count(x => x == '|');
            }
            string specstring = "";
            string[] specNames = specs[0].Split('|');
            string[] specValues = specs[1].Split('|');
            for (int i = 0; i < maxNumOfSpecs; i++)
            {
                if (i == maxNumOfSpecs - 1) specstring += specNames[i] + " − " + specValues[i];
                else specstring += specNames[i] + " − " + specValues[i] + "\n";
            }
            e.Value = specstring;
        }

        public void cellFormattingType(object sender, DataGridViewCellFormattingEventArgs e)
        {
            ComponentType enumValue = (ComponentType)e.Value;
            GetTypes a = new GetTypes();
            string enumstring = a.GetDescription(enumValue);
            e.Value = enumstring;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}

