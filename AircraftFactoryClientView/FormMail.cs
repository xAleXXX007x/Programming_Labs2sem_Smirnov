using AircraftFactoryBusinessLogic.ViewModels;
using AircraftFactoryClientView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AircraftFactoryClientView
{
    public partial class FormMail : Form
    {
        int curPage = 0;
        int perPage = 4;
        bool blocked = false;

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
                var list = (APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getmessagespage?clientId={Program.Client.Id}&skip={curPage * perPage}&take={perPage}"));

                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                }

                blocked = list.Count < perPage;
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

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (blocked)
            {
                return;
            }

            curPage++;
            LoadData();
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            curPage = Math.Max(0, curPage - 1);
            LoadData();
        }
    }
}
