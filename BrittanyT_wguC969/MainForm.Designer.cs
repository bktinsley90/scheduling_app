namespace BrittanyT_wguC969
{
    partial class MainForm
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
            this.ApptGridView = new System.Windows.Forms.DataGridView();
            this.CustomerGridView = new System.Windows.Forms.DataGridView();
            this.apptLabel = new System.Windows.Forms.Label();
            this.CustomersLabel = new System.Windows.Forms.Label();
            this.UpdateApptBtn = new System.Windows.Forms.Button();
            this.DeleteApptBtn = new System.Windows.Forms.Button();
            this.AddApptBtn = new System.Windows.Forms.Button();
            this.AddCustomerBtn = new System.Windows.Forms.Button();
            this.UpdateCustomerBtn = new System.Windows.Forms.Button();
            this.DeleteCustomerBtn = new System.Windows.Forms.Button();
            this.LogOutBtn = new System.Windows.Forms.Button();
            this.Reports = new System.Windows.Forms.Button();
            this.CurrWeekBtn = new System.Windows.Forms.RadioButton();
            this.CurrMonthBtn = new System.Windows.Forms.RadioButton();
            this.AllApptsBtn = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.ApptGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ApptGridView
            // 
            this.ApptGridView.AllowUserToAddRows = false;
            this.ApptGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ApptGridView.Location = new System.Drawing.Point(374, 37);
            this.ApptGridView.Name = "ApptGridView";
            this.ApptGridView.ReadOnly = true;
            this.ApptGridView.Size = new System.Drawing.Size(630, 346);
            this.ApptGridView.TabIndex = 0;
            // 
            // CustomerGridView
            // 
            this.CustomerGridView.AllowUserToAddRows = false;
            this.CustomerGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomerGridView.Location = new System.Drawing.Point(28, 37);
            this.CustomerGridView.Name = "CustomerGridView";
            this.CustomerGridView.ReadOnly = true;
            this.CustomerGridView.Size = new System.Drawing.Size(318, 346);
            this.CustomerGridView.TabIndex = 1;
            // 
            // apptLabel
            // 
            this.apptLabel.AutoSize = true;
            this.apptLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.apptLabel.Location = new System.Drawing.Point(370, 9);
            this.apptLabel.Name = "apptLabel";
            this.apptLabel.Size = new System.Drawing.Size(138, 24);
            this.apptLabel.TabIndex = 2;
            this.apptLabel.Text = "Appointments";
            // 
            // CustomersLabel
            // 
            this.CustomersLabel.AutoSize = true;
            this.CustomersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomersLabel.Location = new System.Drawing.Point(24, 9);
            this.CustomersLabel.Name = "CustomersLabel";
            this.CustomersLabel.Size = new System.Drawing.Size(109, 24);
            this.CustomersLabel.TabIndex = 3;
            this.CustomersLabel.Text = "Customers";
            // 
            // UpdateApptBtn
            // 
            this.UpdateApptBtn.Location = new System.Drawing.Point(455, 389);
            this.UpdateApptBtn.Name = "UpdateApptBtn";
            this.UpdateApptBtn.Size = new System.Drawing.Size(75, 23);
            this.UpdateApptBtn.TabIndex = 4;
            this.UpdateApptBtn.Text = "Update";
            this.UpdateApptBtn.UseVisualStyleBackColor = true;
            // 
            // DeleteApptBtn
            // 
            this.DeleteApptBtn.Location = new System.Drawing.Point(536, 389);
            this.DeleteApptBtn.Name = "DeleteApptBtn";
            this.DeleteApptBtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteApptBtn.TabIndex = 5;
            this.DeleteApptBtn.Text = "Delete";
            this.DeleteApptBtn.UseVisualStyleBackColor = true;
            // 
            // AddApptBtn
            // 
            this.AddApptBtn.Location = new System.Drawing.Point(374, 389);
            this.AddApptBtn.Name = "AddApptBtn";
            this.AddApptBtn.Size = new System.Drawing.Size(75, 23);
            this.AddApptBtn.TabIndex = 6;
            this.AddApptBtn.Text = "Add";
            this.AddApptBtn.UseVisualStyleBackColor = true;
            this.AddApptBtn.Click += new System.EventHandler(this.AddApptBtn_Click);
            // 
            // AddCustomerBtn
            // 
            this.AddCustomerBtn.Location = new System.Drawing.Point(28, 389);
            this.AddCustomerBtn.Name = "AddCustomerBtn";
            this.AddCustomerBtn.Size = new System.Drawing.Size(75, 23);
            this.AddCustomerBtn.TabIndex = 7;
            this.AddCustomerBtn.Text = "Add";
            this.AddCustomerBtn.UseVisualStyleBackColor = true;
            this.AddCustomerBtn.Click += new System.EventHandler(this.AddCustomerBtn_Click);
            // 
            // UpdateCustomerBtn
            // 
            this.UpdateCustomerBtn.Location = new System.Drawing.Point(109, 389);
            this.UpdateCustomerBtn.Name = "UpdateCustomerBtn";
            this.UpdateCustomerBtn.Size = new System.Drawing.Size(75, 23);
            this.UpdateCustomerBtn.TabIndex = 8;
            this.UpdateCustomerBtn.Text = "Update";
            this.UpdateCustomerBtn.UseVisualStyleBackColor = true;
            this.UpdateCustomerBtn.Click += new System.EventHandler(this.UpdateCustomerBtn_Click);
            // 
            // DeleteCustomerBtn
            // 
            this.DeleteCustomerBtn.Location = new System.Drawing.Point(190, 389);
            this.DeleteCustomerBtn.Name = "DeleteCustomerBtn";
            this.DeleteCustomerBtn.Size = new System.Drawing.Size(75, 23);
            this.DeleteCustomerBtn.TabIndex = 9;
            this.DeleteCustomerBtn.Text = "Delete";
            this.DeleteCustomerBtn.UseVisualStyleBackColor = true;
            this.DeleteCustomerBtn.Click += new System.EventHandler(this.DeleteCustomerBtn_Click);
            // 
            // LogOutBtn
            // 
            this.LogOutBtn.Location = new System.Drawing.Point(929, 414);
            this.LogOutBtn.Name = "LogOutBtn";
            this.LogOutBtn.Size = new System.Drawing.Size(75, 23);
            this.LogOutBtn.TabIndex = 10;
            this.LogOutBtn.Text = "LogOut";
            this.LogOutBtn.UseVisualStyleBackColor = true;
            this.LogOutBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // Reports
            // 
            this.Reports.Location = new System.Drawing.Point(848, 414);
            this.Reports.Name = "Reports";
            this.Reports.Size = new System.Drawing.Size(75, 23);
            this.Reports.TabIndex = 11;
            this.Reports.Text = "Reports";
            this.Reports.UseVisualStyleBackColor = true;
            // 
            // CurrWeekBtn
            // 
            this.CurrWeekBtn.AutoSize = true;
            this.CurrWeekBtn.Location = new System.Drawing.Point(707, 12);
            this.CurrWeekBtn.Name = "CurrWeekBtn";
            this.CurrWeekBtn.Size = new System.Drawing.Size(91, 17);
            this.CurrWeekBtn.TabIndex = 12;
            this.CurrWeekBtn.TabStop = true;
            this.CurrWeekBtn.Text = "Current Week";
            this.CurrWeekBtn.UseVisualStyleBackColor = true;
            // 
            // CurrMonthBtn
            // 
            this.CurrMonthBtn.AutoSize = true;
            this.CurrMonthBtn.Location = new System.Drawing.Point(809, 12);
            this.CurrMonthBtn.Name = "CurrMonthBtn";
            this.CurrMonthBtn.Size = new System.Drawing.Size(92, 17);
            this.CurrMonthBtn.TabIndex = 13;
            this.CurrMonthBtn.TabStop = true;
            this.CurrMonthBtn.Text = "Current Month";
            this.CurrMonthBtn.UseVisualStyleBackColor = true;
            // 
            // AllApptsBtn
            // 
            this.AllApptsBtn.AutoSize = true;
            this.AllApptsBtn.Location = new System.Drawing.Point(909, 12);
            this.AllApptsBtn.Name = "AllApptsBtn";
            this.AllApptsBtn.Size = new System.Drawing.Size(103, 17);
            this.AllApptsBtn.TabIndex = 14;
            this.AllApptsBtn.TabStop = true;
            this.AllApptsBtn.Text = "All Appointments";
            this.AllApptsBtn.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 454);
            this.Controls.Add(this.AllApptsBtn);
            this.Controls.Add(this.CurrMonthBtn);
            this.Controls.Add(this.CurrWeekBtn);
            this.Controls.Add(this.Reports);
            this.Controls.Add(this.LogOutBtn);
            this.Controls.Add(this.DeleteCustomerBtn);
            this.Controls.Add(this.UpdateCustomerBtn);
            this.Controls.Add(this.AddCustomerBtn);
            this.Controls.Add(this.AddApptBtn);
            this.Controls.Add(this.DeleteApptBtn);
            this.Controls.Add(this.UpdateApptBtn);
            this.Controls.Add(this.CustomersLabel);
            this.Controls.Add(this.apptLabel);
            this.Controls.Add(this.CustomerGridView);
            this.Controls.Add(this.ApptGridView);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.ApptGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ApptGridView;
        private System.Windows.Forms.DataGridView CustomerGridView;
        private System.Windows.Forms.Label apptLabel;
        private System.Windows.Forms.Label CustomersLabel;
        private System.Windows.Forms.Button UpdateApptBtn;
        private System.Windows.Forms.Button DeleteApptBtn;
        private System.Windows.Forms.Button AddApptBtn;
        private System.Windows.Forms.Button AddCustomerBtn;
        private System.Windows.Forms.Button UpdateCustomerBtn;
        private System.Windows.Forms.Button DeleteCustomerBtn;
        private System.Windows.Forms.Button LogOutBtn;
        private System.Windows.Forms.Button Reports;
        private System.Windows.Forms.RadioButton CurrWeekBtn;
        private System.Windows.Forms.RadioButton CurrMonthBtn;
        private System.Windows.Forms.RadioButton AllApptsBtn;
    }
}