using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.BusinessLogics;
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
    public partial class FormReportDayOrders : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic logic;

        public FormReportDayOrders(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormReportDayOrders_Load(object sender, EventArgs e)
        {
            Rebuild();
        }

        private void Rebuild()
        {
            try
            {
                var date = dateTimePicker.Value.Date;
                var orders = logic.GetOrders(new ReportBindingModel
                {
                    DateFrom = date,
                    DateTo = date.AddHours(23D).AddMinutes(59).AddSeconds(59)
                });

                if (orders != null)
                {
                    decimal sum = 0;
                    dataGridView.Rows.Clear();
                    foreach (var elem in orders)
                    {
                        dataGridView.Rows.Add(new object[] { elem.DateCreate, elem.AircraftName, elem.Count, elem.Sum, elem.Status });
                        sum += elem.Sum;
                    }

                    dataGridView.Rows.Add(new object[] { });
                    dataGridView.Rows.Add(new object[] { "", "", "Итого", sum });
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
                        var date = dateTimePicker.Value.Date;
                        logic.SaveOrdersToExcel(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = date,
                            DateTo = date.AddHours(23D).AddMinutes(59).AddSeconds(59)
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

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            Rebuild();
        }
    }
}
