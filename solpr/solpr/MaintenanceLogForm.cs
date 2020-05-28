using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace solpr
{
    public partial class MaintenanceLogForm : Form
    {
        ParkDBEntities db;
        int searchCellNum = 0;
        List<DataGridViewCell> searchCells;

        public MaintenanceLogForm()
        {
            InitializeComponent();
            db = new ParkDBEntities();

            RefreshMainLogGrid();

        }

        public void RefreshMainLogGrid()
        {
            var result = from maint in db.Maintenance
                         join pc in db.Computers on maint.ComputerId equals pc.Id
                         select new
                         {
                             ID = maint.Id,
                             PC_ID = pc.Id,
                             Причина = maint.Description,
                             Дата_начала_ремонта = maint.RepairStart.Day.ToString() + maint.RepairStart.Month.ToString() + maint.RepairStart.Year.ToString(),
                             Дата_окончания_ремонта = maint.RepairFinish.Value.Day.ToString() + maint.RepairFinish.Value.Month.ToString() + maint.RepairFinish.Value.Year.ToString()
                         };
            dataGridView1.DataSource = result.ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            searchCellNum = 0;
            searchCells = new List<DataGridViewCell>();
            RefreshMainLogGrid();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString().ToLower().Contains(textBox1.Text.ToLower()) && textBox1.Text != "")
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
            if (searchCells.Count != 0) dataGridView1.CurrentCell = searchCells[0];
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && searchCells.Count != 0)
            {
                if (searchCellNum > 0)
                {
                    searchCellNum--;
                }
                else
                {
                    searchCellNum = searchCells.Count - 1;
                }
                dataGridView1.CurrentCell = searchCells[searchCellNum];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && searchCells.Count != 0)
            {
                if (searchCellNum < searchCells.Count - 1)
                {
                    searchCellNum++;
                }
                else
                {
                    searchCellNum = 0;
                }
                dataGridView1.CurrentCell = searchCells[searchCellNum];
            }
        }
    }
}
