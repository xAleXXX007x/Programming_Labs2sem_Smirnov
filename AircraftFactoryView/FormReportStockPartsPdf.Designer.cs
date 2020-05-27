namespace AircraftFactoryView
{
    partial class FormReportStockPartsPdf
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
            this.buttonSave = new System.Windows.Forms.Button();
            this.reportViewerStockParts = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
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
            // reportViewerStockParts
            // 
            reportDataSource1.Name = "DataSetStockParts";
            this.reportViewerStockParts.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerStockParts.LocalReport.ReportEmbeddedResource = "AircraftFactoryView.ReportStockParts.rdlc";
            this.reportViewerStockParts.Location = new System.Drawing.Point(12, 41);
            this.reportViewerStockParts.Name = "reportViewerStockParts";
            this.reportViewerStockParts.ServerReport.BearerToken = null;
            this.reportViewerStockParts.Size = new System.Drawing.Size(693, 397);
            this.reportViewerStockParts.TabIndex = 1;
            // 
            // FormReportStockPartsPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 450);
            this.Controls.Add(this.reportViewerStockParts);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormReportStockPartsPdf";
            this.Text = "Список запчастей на складах";
            this.Load += new System.EventHandler(this.FormReportAircrafts_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerStockParts;
    }
}