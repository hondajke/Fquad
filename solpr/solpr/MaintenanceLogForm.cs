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
using System.Linq.Expressions;

namespace solpr
{
    public partial class MaintenanceLogForm : Form
    {
        ParkDBEntities db;
        int searchCellNum = 0;
        bool dtpickerFl_1 = false;
        bool dtpickerFl_2 = false;

        List<DataGridViewCell> searchCells;

        public MaintenanceLogForm()
        {
            InitializeComponent();
            db = new ParkDBEntities();
            dtpickerFl_1 = false;
            dtpickerFl_2 = false;

            RefreshMainLogGrid();

        }

        public Expression<Func<TElement, bool>> IsSameDate<TElement>(Expression<Func<TElement, DateTime>> valueSelector, DateTime value)
        {
            ParameterExpression p = valueSelector.Parameters.Single();

            var antes = Expression.GreaterThanOrEqual(valueSelector.Body, Expression.Constant(value.Date, typeof(DateTime)));

            var despues = Expression.LessThan(valueSelector.Body, Expression.Constant(value.AddDays(1).Date, typeof(DateTime)));

            Expression body = Expression.And(antes, despues);

            return Expression.Lambda<Func<TElement, bool>>(body, p);
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
                             Дата_начала_ремонта = maint.RepairStart.Day.ToString()+ "." + maint.RepairStart.Month.ToString() + "." + maint.RepairStart.Year.ToString(),
                             Дата_окончания_ремонта = maint.RepairFinish.Value.Day.ToString() + "." + maint.RepairFinish.Value.Month.ToString() + "." + maint.RepairFinish.Value.Year.ToString()
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dtpickerFl_1 = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dtpickerFl_2 = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = from maint in db.Maintenance
                         join pc in db.Computers on maint.ComputerId equals pc.Id
                         select new
                         {
                             ID = maint.Id,
                             PC_ID = pc.Id,
                             Причина = maint.Description,
                             Дата_начала_ремонта = maint.RepairStart,
                             Дата_окончания_ремонта = maint.RepairFinish
                         };

            if (dateTimePicker1.Value.Date < dateTimePicker2.Value.Date)
            {
                var filteredNotes = result.Where(x => x.Дата_начала_ремонта <= dateTimePicker1.Value && x.Дата_окончания_ремонта >= dateTimePicker2.Value);

            }
            else
            {
                MessageBox.Show("Начало отрезка не должно быть больше конца");
            }
        }

    }
}
