namespace AircraftFactoryView
{
    partial class FormReportStockParts
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
            this.buttonSaveExcel = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColumnStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSaveExcel
            // 
            this.buttonSaveExcel.Location = new System.Drawing.Point(12, 12);
            this.buttonSaveExcel.Name = "buttonSaveExcel";
            this.buttonSaveExcel.Size = new System.Drawing.Size(135, 23);
            this.buttonSaveExcel.TabIndex = 0;
            this.buttonSaveExcel.Text = "Сохранить в Excel";
            this.buttonSaveExcel.UseVisualStyleBackColor = true;
            this.buttonSaveExcel.Click += new System.EventHandler(this.buttonSaveExcel_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStock,
            this.ColumnPart,
            this.ColumnCount});
            this.dataGridView.Location = new System.Drawing.Point(12, 41);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(580, 428);
            this.dataGridView.TabIndex = 1;
            // 
            // ColumnStock
            // 
            this.ColumnStock.HeaderText = "Склад";
            this.ColumnStock.Name = "ColumnStock";
            // 
            // ColumnPart
            // 
            this.ColumnPart.HeaderText = "Запчасть";
            this.ColumnPart.Name = "ColumnPart";
            // 
            // ColumnCount
            // 
            this.ColumnCount.HeaderText = "Количество";
            this.ColumnCount.Name = "ColumnCount";
            // 
            // FormReportStockParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 481);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonSaveExcel);
            this.Name = "FormReportStockParts";
            this.Text = "Запчасти на складах";
            this.Load += new System.EventHandler(this.FormReportDayOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveExcel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPart;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
    }
}