using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.BusinessLogics;
using AircraftFactoryDatabaseImplement.Implements;
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
    public partial class FormReportStockParts : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic logic;

        private readonly StockLogic stockLogic;

        public FormReportStockParts(ReportLogic logic, StockLogic stockLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.stockLogic = stockLogic;
        }

        private void FormReportDayOrders_Load(object sender, EventArgs e)
        {
            Rebuild();
        }

        private void Rebuild()
        {
            try
            {
                var stocks = stockLogic.GetList();

                if (stocks != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var stock in stocks)
                    {
                        int sum = 0;

                        dataGridView.Rows.Add(new object[] { stock.StockName, "", "" });

                        foreach (var stockPart in stock.StockParts)
                        {
                            dataGridView.Rows.Add(new object[] { "", stockPart.PartName, stockPart.Count });
                            sum += stockPart.Count;
                        }

                        dataGridView.Rows.Add(new object[] { "", "Итого:", sum });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveStockPartsToExcel(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
