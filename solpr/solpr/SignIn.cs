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
        Timer myTimer = new Timer();
        int timeLeft = 10;
        int attemptLeft = 3;
        public SignIn()
        {
            InitializeComponent();
            pass.KeyDown += TextBoxKeyDown;
            login.KeyDown += TextBoxKeyDown;
            
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
                    attemptLeft--;
                    if (attemptLeft == 0)
                    {
                        this.Enabled = false;
                        myTimer.Interval = 1000;
                        myTimer.Enabled = true;

                        //Set the event handler for the timer, named "myTimer_Tick"
                        myTimer.Tick += myTimer_Tick;

                        //Start the timer as soon as the form is loaded
                        myTimer.Start();

                        //Show the time set in the "timeLeft" variable
                        label4.Text = timeLeft.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Найдена ошибка:" + ex.Message);
            }
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                checkIn.PerformClick();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
        private void myTimer_Tick(object sender, EventArgs e)
        {
            //perform these actions at the interval set in the properties.
            label4.Text = timeLeft.ToString();
            timeLeft -= 1;

            if (timeLeft < 0)
            {
                myTimer.Stop();
                this.Enabled = true;
                attemptLeft = 3;
                timeLeft = 10;
                label4.Text = "";
            }
        }

        private void login_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
