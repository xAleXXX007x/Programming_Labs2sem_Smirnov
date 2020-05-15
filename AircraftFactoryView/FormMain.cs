using AircraftFactoryBusinessLogic;
using AircraftFactoryBusinessLogic.BindingModels;
using AircraftFactoryBusinessLogic.BusinessLogics;
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
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly MainLogic logic;

        private readonly IOrderLogic orderLogic;

        private readonly ReportLogic report;

        private readonly WorkModeling work;

        public FormMain(MainLogic logic, IOrderLogic orderLogic, ReportLogic report, WorkModeling work)
        {
            InitializeComponent();
            this.logic = logic;
            this.orderLogic = orderLogic;
            this.report = report;
            this.work = work;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView.DataSource = null;
                dataGridView.DataSource = orderLogic.Read(null);
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[1].Visible = false;
                dataGridView.Columns[2].Visible = false;
                dataGridView.Columns[3].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void запчастиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormParts>();
            form.ShowDialog();
        }

        private void самолетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAircrafts>();
            form.ShowDialog();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCreateOrder>();
            form.ShowDialog();
            LoadData();
        }

        private void buttonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    logic.PayOrder(new ChangeStatusBindingModel { OrderId = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void AircraftsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    report.SaveAircraftsToWordFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DayOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportDayOrders>();
            form.ShowDialog();
        }

        private void AircraftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportAircrafts>();
            form.ShowDialog();
        }

        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormClients>();
            form.ShowDialog();
        }

        private void запускРаботToolStripMenuItem_Click(object sender, EventArgs e)
        {
            work.DoWork();
        }

        private void исполнителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormImplementers>();
            form.ShowDialog();
        }

        private void почтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormMail>();
            form.ShowDialog();
        }

        private void пополнитьСкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStockPart>();
            form.ShowDialog();
        }

        private void складыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStocks>();
            form.ShowDialog();
        }

        private void списокСкладовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    report.SaveStocksToWordFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void списокЗапчастейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportStockParts>();
            form.ShowDialog();
        }

        private void списокЗапчастейНаСкладахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReportStockPartsPdf>();
            form.ShowDialog();
        }
    }
}
