﻿namespace BrittanyT_wguC969
{
    partial class ReportForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.BackBtn = new System.Windows.Forms.Button();
            this.ScheduleGridView = new System.Windows.Forms.DataGridView();
            this.UsernameSelect = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ApptTypesGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CustomerAppointmentsGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApptTypesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerAppointmentsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reports";
            // 
            // BackBtn
            // 
            this.BackBtn.Location = new System.Drawing.Point(785, 510);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(75, 23);
            this.BackBtn.TabIndex = 1;
            this.BackBtn.Text = "Back";
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // ScheduleGridView
            // 
            this.ScheduleGridView.AllowUserToAddRows = false;
            this.ScheduleGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ScheduleGridView.Location = new System.Drawing.Point(31, 80);
            this.ScheduleGridView.Name = "ScheduleGridView";
            this.ScheduleGridView.ReadOnly = true;
            this.ScheduleGridView.Size = new System.Drawing.Size(829, 166);
            this.ScheduleGridView.TabIndex = 3;
            // 
            // UsernameSelect
            // 
            this.UsernameSelect.FormattingEnabled = true;
            this.UsernameSelect.Location = new System.Drawing.Point(739, 53);
            this.UsernameSelect.Name = "UsernameSelect";
            this.UsernameSelect.Size = new System.Drawing.Size(121, 21);
            this.UsernameSelect.TabIndex = 4;
            this.UsernameSelect.SelectedIndexChanged += new System.EventHandler(this.UsernameSelect_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(593, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Consultant:";
            // 
            // ApptTypesGridView
            // 
            this.ApptTypesGridView.AllowUserToAddRows = false;
            this.ApptTypesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ApptTypesGridView.Location = new System.Drawing.Point(31, 296);
            this.ApptTypesGridView.Name = "ApptTypesGridView";
            this.ApptTypesGridView.ReadOnly = true;
            this.ApptTypesGridView.Size = new System.Drawing.Size(305, 202);
            this.ApptTypesGridView.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Appointments By Types";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Schedules By User";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(565, 277);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Appointments By Customer";
            // 
            // CustomerAppointmentsGridView
            // 
            this.CustomerAppointmentsGridView.AllowUserToAddRows = false;
            this.CustomerAppointmentsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CustomerAppointmentsGridView.Location = new System.Drawing.Point(568, 297);
            this.CustomerAppointmentsGridView.Name = "CustomerAppointmentsGridView";
            this.CustomerAppointmentsGridView.ReadOnly = true;
            this.CustomerAppointmentsGridView.Size = new System.Drawing.Size(292, 201);
            this.CustomerAppointmentsGridView.TabIndex = 10;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 545);
            this.Controls.Add(this.CustomerAppointmentsGridView);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ApptTypesGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UsernameSelect);
            this.Controls.Add(this.ScheduleGridView);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.label1);
            this.Name = "ReportForm";
            this.Text = "ReportForm";
            ((System.ComponentModel.ISupportInitialize)(this.ScheduleGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ApptTypesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerAppointmentsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BackBtn;
        private System.Windows.Forms.DataGridView ScheduleGridView;
        private System.Windows.Forms.ComboBox UsernameSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView ApptTypesGridView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView CustomerAppointmentsGridView;
    }
}