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
    public partial class FormStock : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IStockLogic logic;

        private int? id;

        private List<StockPartViewModel> stockParts;

        public FormStock(IStockLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StockViewModel view = logic.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.StockName;
                        stockParts = view.StockParts;
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
                stockParts = new List<StockPartViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (stockParts != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = stockParts;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            try
            {
                List<StockPartBindingModel> stockPartBM = new List<StockPartBindingModel>();

                for (int i = 0; i < stockParts.Count; ++i)
                {
                    stockPartBM.Add(new StockPartBindingModel
                    {
                        Id = stockParts[i].Id,
                        StockId = stockParts[i].StockId,
                        PartId = stockParts[i].PartId,
                        Count = stockParts[i].Count
                    });
                }

                if (id.HasValue)
                {
                    logic.UpdElement(new StockBindingModel
                    {
                        Id = id.Value,
                        StockName = textBoxName.Text,
                        StockParts = stockPartBM
                    });
                }
                else
                {
                    logic.AddElement(new StockBindingModel
                    {
                        StockName = textBoxName.Text,
                        StockParts = stockPartBM
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
