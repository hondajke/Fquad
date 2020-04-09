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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkIn_Click(object sender, EventArgs e)
        {
            try 
            {
                if ((login.Text == "admin") && (pass.Text == "123"))
                {
                    Program.st.Hide();
                    Program.mf.ShowDialog();
                }
                else 
                {
                    MessageBox.Show("Данные введены неправильно");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Найдена ошибка:" + ex.Message);
            }
        }
    }
}
