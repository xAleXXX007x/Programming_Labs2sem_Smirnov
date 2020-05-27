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

namespace AircraftFactoryStockView
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridViewStocks.DataSource = APIClient.GetRequest<List<StockViewModel>>($"api/stock/getstocks");
                dataGridViewStocks.Columns[0].Visible = false;
                dataGridViewStocks.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStockParts()
        {
            if (dataGridViewStocks.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewStocks.SelectedRows[0].Cells[0].Value);
                var stock = APIClient.GetRequest<StockViewModel>($"api/stock/getstock?id={id}");

                if (stock != null)
                {
                    dataGridViewParts.DataSource = stock.StockParts;
                    dataGridViewParts.Columns[0].Visible = false;
                    dataGridViewParts.Columns[1].Visible = false;
                    dataGridViewParts.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewStocks.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridViewStocks.SelectedRows[0].Cells[0].Value);

                try
                {
                    APIClient.PostRequest("api/stock/deletestock", new StockBindingModel { Id = id });
                    MessageBox.Show("Склад удалён", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewStocks.SelectedRows.Count == 1)
            {
                var form = new FormStock();

                form.Id = Convert.ToInt32(dataGridViewStocks.SelectedRows[0].Cells[0].Value);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var form = new FormStock();

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonRefill_Click(object sender, EventArgs e)
        {
            if (dataGridViewStocks.SelectedRows.Count == 1)
            {
                var form = new FormStockPart();

                form.StockId = Convert.ToInt32(dataGridViewStocks.SelectedRows[0].Cells[0].Value);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    LoadStockParts();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridViewStocks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadStockParts();
        }
    }
}
