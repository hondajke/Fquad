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
        //drow["Id"] = p.Id;
        string processor = "";
        string mboard = "";
        string video = "";
        string ram = "";
        string hdd = "";
        string ssd = "";
        string sound = "";
        string drive = "";
        string other = "";

        private void FormComputerAdd_Load_1(object sender, EventArgs e)
        {
            db = new ParkDBEntities();
            loadCompStatus();
            loadEmployees();
            loadComponents();
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
            //dataGridView1.DataSource = result; 
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
            var empQuery = from emp in db.Employees
                           join dep in db.Departments on emp.DepartmentId equals dep.Id
                           orderby emp.Name

                           select new
                           {
                               sad = dep.Name + " " + emp.Surname + " " + emp.Name,
                               gov = emp.Id
                           };
            comboBox7.DataSource = empQuery.ToList();
            comboBox7.DisplayMember = "sad";
            comboBox7.ValueMember = "gov";

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
            DataTable dt = new DataTable();
            //dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Процессор", typeof(string));
            dt.Columns.Add("Материнская плата", typeof(string));
            dt.Columns.Add("Видеокарта", typeof(string));
            dt.Columns.Add("Оперативная память", typeof(string));
            dt.Columns.Add("Жесткий диск", typeof(string));
            dt.Columns.Add("Твердотельный накопитель", typeof(string));
            dt.Columns.Add("Аудиокарта", typeof(string));
            dt.Columns.Add("Привод", typeof(string));
            dt.Columns.Add("Другое", typeof(string));

            DataRow drow;
            drow = dt.NewRow();
            //drow["Id"] = p.Id;
            drow["Процессор"] = processor;
            drow["Материнская плата"] = mboard;
            drow["Видеокарта"] = video;
            drow["Оперативная память"] = ram;
            drow["Жесткий диск"] = hdd;
            drow["Твердотельный накопитель"] = ssd;
            drow["Аудиокарта"] = sound;
            drow["Привод"] = drive;
            drow["Другое"] = other;
            dt.Rows.Add(drow);

            dataGridView2.DataSource = dt;
            dataGridView2.Refresh();

        }

        public void refreshList2(ComponentType comp)
        {
            switch (comp)
            {
                case ComponentType.processor:
                    break;
                case ComponentType.mboard:
                    break;
                case ComponentType.video:
                    break;
                case ComponentType.ram:
                    break;
                case ComponentType.hdd:
                    break;
                case ComponentType.ssd:
                    break;
                case ComponentType.sound:
                    break;
                case ComponentType.drive:
                    break;
                case ComponentType.other:
                    break;
                default:
                    break;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var filter = result;
            //filter = filter.Where(x => x.Тип.ToString() == comboBox1.SelectedValue.ToString());
            //dataGridView1.DataSource = filter.ToList();
        }    
        
       
    }
}
