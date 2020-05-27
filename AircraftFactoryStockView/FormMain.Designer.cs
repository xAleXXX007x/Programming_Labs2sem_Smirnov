namespace AircraftFactoryStockView
{
    partial class FormMain
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
            this.dataGridViewStocks = new System.Windows.Forms.DataGridView();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRefill = new System.Windows.Forms.Button();
            this.dataGridViewParts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParts)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewStocks
            // 
            this.dataGridViewStocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStocks.Location = new System.Drawing.Point(12, 12);
            this.dataGridViewStocks.Name = "dataGridViewStocks";
            this.dataGridViewStocks.Size = new System.Drawing.Size(613, 426);
            this.dataGridViewStocks.TabIndex = 0;
            this.dataGridViewStocks.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStocks_CellClick);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(631, 82);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(157, 23);
            this.buttonCreate.TabIndex = 1;
            this.buttonCreate.Text = "Создать склад";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(631, 111);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(157, 23);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Редактировать склад";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(631, 140);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(157, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Удалить склад";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRefill
            // 
            this.buttonRefill.Location = new System.Drawing.Point(631, 12);
            this.buttonRefill.Name = "buttonRefill";
            this.buttonRefill.Size = new System.Drawing.Size(157, 23);
            this.buttonRefill.TabIndex = 4;
            this.buttonRefill.Text = "Пополнить склад";
            this.buttonRefill.UseVisualStyleBackColor = true;
            this.buttonRefill.Click += new System.EventHandler(this.buttonRefill_Click);
            // 
            // dataGridViewParts
            // 
            this.dataGridViewParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewParts.Location = new System.Drawing.Point(631, 185);
            this.dataGridViewParts.Name = "dataGridViewParts";
            this.dataGridViewParts.Size = new System.Drawing.Size(157, 253);
            this.dataGridViewParts.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(631, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Запчасти на складе";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(631, 41);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(157, 23);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Обновить список";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // FormMain1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewParts);
            this.Controls.Add(this.buttonRefill);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.dataGridViewStocks);
            this.Name = "FormMain1";
            this.Text = "Склады";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewParts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewStocks;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRefill;
        private System.Windows.Forms.DataGridView dataGridViewParts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRefresh;
    }
}