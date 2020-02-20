using AircraftFactoryBusinessLogic.Interfaces;
using AircraftFactoryBusinessLogic.ViewModels;
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
    public partial class FormStockPart : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public StockPartViewModel ModelView { get; set; }

        private readonly IPartLogic logic;

        public FormStockPart(IPartLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormStockPart_Load(object sender, EventArgs e)
        {
            try
            {
                List<PartViewModel> list = logic.GetList();
                if (list != null)
                {
                    comboBoxPart.DisplayMember = "PartName";
                    comboBoxPart.ValueMember = "Id";
                    comboBoxPart.DataSource = list;
                    comboBoxPart.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ModelView != null)
            {
                comboBoxPart.Enabled = false;
                comboBoxPart.SelectedValue = ModelView.PartId;
                textBoxCount.Text = ModelView.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (comboBoxPart.SelectedValue == null)
            {
                MessageBox.Show("Выберите запчасть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            try
            {
                if (ModelView == null)
                {
                    ModelView = new StockPartViewModel
                    {
                        PartId = Convert.ToInt32(comboBoxPart.SelectedValue),
                        PartName = comboBoxPart.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    ModelView.Count = Convert.ToInt32(textBoxCount.Text);
                }

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
