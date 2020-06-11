using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AircraftFactoryView
{
    public partial class FormMail : Form
    {
        int curPage = 0;
        int perPage = 4;
        bool blocked = false;

        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMessageInfoLogic logic;

        public FormMail(IMessageInfoLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormParts_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null).Skip(curPage * perPage).Take(perPage).ToList();

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
