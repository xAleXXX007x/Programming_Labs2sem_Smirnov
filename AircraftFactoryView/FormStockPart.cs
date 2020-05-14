using AircraftFactoryBusinessLogic;
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
    public partial class FormStockPart : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IPartLogic logic;

        private readonly IStockLogic stockLogic;

        private readonly MainLogic mainLogic;

        public FormStockPart(IPartLogic logic, IStockLogic stockLogic, MainLogic mainLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.stockLogic = stockLogic;
            this.mainLogic = mainLogic;
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

                List<StockViewModel> stockList = stockLogic.GetList();

                if (stockList != null)
                {
                    comboBoxStocks.DisplayMember = "StockName";
                    comboBoxStocks.ValueMember = "Id";
                    comboBoxStocks.DataSource = stockList;
                    comboBoxStocks.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (comboBoxStocks.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            try
            {
                StockViewModel stock = stockLogic.GetElement((int)comboBoxStocks.SelectedValue);
                List<StockPartViewModel> stockParts = stock.StockParts;
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

                int maxId = -1;

                foreach (PartViewModel part in logic.GetList())
                {
                    if (part.Id > maxId)
                    {
                        maxId = part.Id;
                    }
                }

                mainLogic.RefillStock(new StockBindingModel
                {
                    Id = stock.Id,
                    StockName = stock.StockName,
                    StockParts = stockPartBM
                }, new StockPartBindingModel
                {
                    Id = ++maxId,
                    PartId = Convert.ToInt32(comboBoxPart.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    StockId = stock.Id
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
