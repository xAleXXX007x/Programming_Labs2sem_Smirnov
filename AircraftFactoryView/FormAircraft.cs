using AircraftFactoryBusinessLogic.BindingModels;
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
    public partial class FormAircraft : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IAircraftLogic logic;

        private int? id;

        private List<AircraftPartViewModel> aircraftParts;

        public FormAircraft(IAircraftLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormAircraft_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    AircraftViewModel view = logic.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.AircraftName;
                        textBoxPrice.Text = view.Price.ToString();
                        aircraftParts = view.AircraftParts;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                aircraftParts = new List<AircraftPartViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (aircraftParts != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = aircraftParts;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAircraftPart>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.ModelView != null)
                {
                    if (id.HasValue)
                    {
                        form.ModelView.AircraftId = id.Value;
                    }

                    aircraftParts.Add(form.ModelView);
                }

                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormAircraftPart>();
                form.ModelView = aircraftParts[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    aircraftParts[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.ModelView;
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        aircraftParts.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (aircraftParts == null || aircraftParts.Count == 0)
            {
                MessageBox.Show("Заполните запчасти", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                List<AircraftPartBindingModel> aircraftPartBM = new List<AircraftPartBindingModel>();

                for (int i = 0; i < aircraftParts.Count; ++i)
                {
                    aircraftPartBM.Add(new AircraftPartBindingModel
                    {
                        Id = aircraftParts[i].Id,
                        AircraftId = aircraftParts[i].AircraftId,
                        PartId = aircraftParts[i].PartId,
                        Count = aircraftParts[i].Count
                    });
                }

                if (id.HasValue)
                {
                    logic.UpdElement(new AircraftBindingModel
                    {
                        Id = id.Value,
                        AircraftName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        AircraftParts = aircraftPartBM
                    });
                }
                else
                {
                    logic.AddElement(new AircraftBindingModel
                    {
                        AircraftName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        AircraftParts = aircraftPartBM
                    });
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
