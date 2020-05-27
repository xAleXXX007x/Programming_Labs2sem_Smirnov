using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.BusinessLogics;
using Microsoft.Reporting.WinForms;
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
    public partial class FormReportStockPartsPdf : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ReportLogic logic;

        public FormReportStockPartsPdf(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormReportAircrafts_Load(object sender, EventArgs e)
        {
            this.reportViewerStockParts.RefreshReport();
            try
            {
                var data = logic.GetStockParts();
                ReportDataSource source = new ReportDataSource("DataSetStockParts", data);
                reportViewerStockParts.LocalReport.DataSources.Add(source);
                reportViewerStockParts.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveStockPartsToPdfFile(new ReportBindingModel
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
