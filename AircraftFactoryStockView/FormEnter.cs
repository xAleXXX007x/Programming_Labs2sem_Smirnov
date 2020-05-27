using AircraftFactoryBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AircraftFactoryStockView
{
    public partial class FormEnter : Form
    {
        public FormEnter()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxEmail.Text) && !string.IsNullOrEmpty(textBoxPassword.Text))
            {
                try
                {
                    string login = ConfigurationManager.AppSettings["Login"];
                    string password = ConfigurationManager.AppSettings["Password"];

                    if (!textBoxEmail.Text.Equals(login) || !textBoxPassword.Text.Equals(password))
                    {
                        throw new Exception("Неверный логин или пароль");
                    }

                    Program.LoggedIn = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
