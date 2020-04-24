namespace AircraftFactoryClientView
{
    partial class FormCreateOrder
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
            this.labelAircraft = new System.Windows.Forms.Label();
            this.comboBoxAircraft = new System.Windows.Forms.ComboBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.labelSum = new System.Windows.Forms.Label();
            this.textBoxSum = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // labelAircraft
            //
            this.labelAircraft.AutoSize = true;
            this.labelAircraft.Location = new System.Drawing.Point(12, 9);
            this.labelAircraft.Name = "labelAircraft";
            this.labelAircraft.Size = new System.Drawing.Size(54, 13);
            this.labelAircraft.TabIndex = 0;
            this.labelAircraft.Text = "Самолет:";
            //
            // comboBoxAircraft
            // 
            this.comboBoxAircraft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAircraft.FormattingEnabled = true;
            this.comboBoxAircraft.Location = new System.Drawing.Point(87, 6);
            this.comboBoxAircraft.Name = "comboBoxAircraft";
            this.comboBoxAircraft.Size = new System.Drawing.Size(217, 21);
            this.comboBoxAircraft.TabIndex = 1;
            this.comboBoxAircraft.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAircraft_SelectedIndexChanged);
            //
            // labelCount
            //
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(12, 36);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(69, 13);
            this.labelCount.TabIndex = 2;
            this.labelCount.Text = "Количество:";
            //
            // textBoxCount
            //
            this.textBoxCount.Location = new System.Drawing.Point(87, 33);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(217, 20);
            this.textBoxCount.TabIndex = 3;
            this.textBoxCount.TextChanged += new System.EventHandler(this.TextBoxCount_TextChanged);
            //
            // labelSum
            //
            this.labelSum.AutoSize = true;
            this.labelSum.Location = new System.Drawing.Point(12, 62);
            this.labelSum.Name = "labelSum";
            this.labelSum.Size = new System.Drawing.Size(44, 13);
            this.labelSum.TabIndex = 4;
            this.labelSum.Text = "Сумма:";
            //
            // textBoxSum
            //
            this.textBoxSum.Location = new System.Drawing.Point(87, 59);
            this.textBoxSum.Name = "textBoxSum";
            this.textBoxSum.ReadOnly = true;
            this.textBoxSum.Size = new System.Drawing.Size(217, 20);
            this.textBoxSum.TabIndex = 5;
            //
            // buttonSave
            //
            this.buttonSave.Location = new System.Drawing.Point(138, 85);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            //
            // FormCreateOrder
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 118);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxSum);
            this.Controls.Add(this.labelSum);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.comboBoxAircraft);
            this.Controls.Add(this.labelAircraft);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заказ";
            this.Load += new System.EventHandler(this.FormCreateOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        #endregion

        private System.Windows.Forms.Label labelAircraft;
        private System.Windows.Forms.ComboBox comboBoxAircraft;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Label labelSum;
        private System.Windows.Forms.TextBox textBoxSum;
        private System.Windows.Forms.Button buttonSave; 
    }
}