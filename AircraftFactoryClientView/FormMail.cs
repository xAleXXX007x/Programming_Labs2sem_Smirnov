using AircraftFactoryBusinessLogic.ViewModels;
using AircraftFactoryClientView;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AircraftFactoryClientView
{
    public partial class FormMail : Form
    {
        public FormMail()
        {
            InitializeComponent();
        }

        private void FormParts_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getmessages?clientId={Program.Client.Id}");

                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[5].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
