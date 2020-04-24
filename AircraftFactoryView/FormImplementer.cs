using System;
using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.Interfaces;
using System.Windows.Forms;
using Unity;

namespace AircraftFactoryView
{
    public partial class FormImplementer : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IImplementerLogic logic;

        private int? id;

        public FormImplementer(IImplementerLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormPart_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new ImplementerBindingModel
                    {
                        Id = id
                    })[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.ImplementerFIO;
                        textBoxWorkingTime.Text = view.WorkingTime.ToString();
                        textBoxPauseTime.Text = view.PauseTime.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (string.IsNullOrEmpty(textBoxWorkingTime.Text))
            {
                MessageBox.Show("Заполните время работы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            if (string.IsNullOrEmpty(textBoxPauseTime.Text))
            {
                MessageBox.Show("Заполните время отдыха", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            try
            {
                logic.CreateOrUpdate(new ImplementerBindingModel
                {
                    Id = id,
                    ImplementerFIO = textBoxName.Text,
                    WorkingTime = Convert.ToInt32(textBoxWorkingTime.Text),
                    PauseTime = Convert.ToInt32(textBoxPauseTime.Text)
                });
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
