namespace AircraftFactoryView
{
    partial class FormReportAircrafts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportAircraftsViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonSave = new System.Windows.Forms.Button();
            this.reportViewerAircrafts = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.reportAircraftsViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportAircraftsViewModelBindingSource
            // 
            this.reportAircraftsViewModelBindingSource.DataSource = typeof(AircraftFactoryBusinessLogic.ViewModels.ReportAircraftsViewModel);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(122, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Сохранить в Pdf";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // reportViewerAircrafts
            // 
            reportDataSource1.Name = "DataSetAircrafts";
            this.reportViewerAircrafts.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerAircrafts.LocalReport.ReportEmbeddedResource = "AircraftFactoryView.ReportAircrafts.rdlc";
            this.reportViewerAircrafts.Location = new System.Drawing.Point(12, 41);
            this.reportViewerAircrafts.Name = "reportViewerAircrafts";
            this.reportViewerAircrafts.ServerReport.BearerToken = null;
            this.reportViewerAircrafts.Size = new System.Drawing.Size(693, 397);
            this.reportViewerAircrafts.TabIndex = 1;
            // 
            // FormReportAircrafts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 450);
            this.Controls.Add(this.reportViewerAircrafts);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormReportAircrafts";
            this.Text = "Список самолётов и их запчастей";
            this.Load += new System.EventHandler(this.FormReportAircrafts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reportAircraftsViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerAircrafts;
        private System.Windows.Forms.BindingSource reportAircraftsViewModelBindingSource;
    }
}