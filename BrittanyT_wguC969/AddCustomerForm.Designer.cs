namespace BrittanyT_wguC969
{
    partial class AddCustomerForm
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
            this.CustomerIdLabel = new System.Windows.Forms.Label();
            this.CustomerNameLabel = new System.Windows.Forms.Label();
            this.CustomerAddressLabel = new System.Windows.Forms.Label();
            this.CustomerPhoneLabel = new System.Windows.Forms.Label();
            this.CountryLabel = new System.Windows.Forms.Label();
            this.StateLabel = new System.Windows.Forms.Label();
            this.SaveCustomerBtn = new System.Windows.Forms.Button();
            this.CancelCustomerBtn = new System.Windows.Forms.Button();
            this.CustomerIdInput = new System.Windows.Forms.TextBox();
            this.CustomerNameInput = new System.Windows.Forms.TextBox();
            this.CustomerAddressInput = new System.Windows.Forms.TextBox();
            this.PhoneNumberInput = new System.Windows.Forms.TextBox();
            this.StateInput = new System.Windows.Forms.TextBox();
            this.PostalCodeInput = new System.Windows.Forms.TextBox();
            this.CountryInput = new System.Windows.Forms.TextBox();
            this.PostalCodeLabel = new System.Windows.Forms.Label();
            this.CityLabel = new System.Windows.Forms.Label();
            this.CityInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add Customer";
            // 
            // CustomerIdLabel
            // 
            this.CustomerIdLabel.AutoSize = true;
            this.CustomerIdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerIdLabel.Location = new System.Drawing.Point(168, 73);
            this.CustomerIdLabel.Name = "CustomerIdLabel";
            this.CustomerIdLabel.Size = new System.Drawing.Size(21, 17);
            this.CustomerIdLabel.TabIndex = 1;
            this.CustomerIdLabel.Text = "ID";
            // 
            // CustomerNameLabel
            // 
            this.CustomerNameLabel.AutoSize = true;
            this.CustomerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerNameLabel.Location = new System.Drawing.Point(144, 115);
            this.CustomerNameLabel.Name = "CustomerNameLabel";
            this.CustomerNameLabel.Size = new System.Drawing.Size(45, 17);
            this.CustomerNameLabel.TabIndex = 2;
            this.CustomerNameLabel.Text = "Name";
            // 
            // CustomerAddressLabel
            // 
            this.CustomerAddressLabel.AutoSize = true;
            this.CustomerAddressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerAddressLabel.Location = new System.Drawing.Point(129, 158);
            this.CustomerAddressLabel.Name = "CustomerAddressLabel";
            this.CustomerAddressLabel.Size = new System.Drawing.Size(60, 17);
            this.CustomerAddressLabel.TabIndex = 3;
            this.CustomerAddressLabel.Text = "Address";
            // 
            // CustomerPhoneLabel
            // 
            this.CustomerPhoneLabel.AutoSize = true;
            this.CustomerPhoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerPhoneLabel.Location = new System.Drawing.Point(90, 200);
            this.CustomerPhoneLabel.Name = "CustomerPhoneLabel";
            this.CustomerPhoneLabel.Size = new System.Drawing.Size(103, 17);
            this.CustomerPhoneLabel.TabIndex = 4;
            this.CustomerPhoneLabel.Text = "Phone Number";
            // 
            // CountryLabel
            // 
            this.CountryLabel.AutoSize = true;
            this.CountryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountryLabel.Location = new System.Drawing.Point(132, 341);
            this.CountryLabel.Name = "CountryLabel";
            this.CountryLabel.Size = new System.Drawing.Size(57, 17);
            this.CountryLabel.TabIndex = 5;
            this.CountryLabel.Text = "Country";
            // 
            // StateLabel
            // 
            this.StateLabel.AutoSize = true;
            this.StateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StateLabel.Location = new System.Drawing.Point(148, 268);
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(41, 17);
            this.StateLabel.TabIndex = 6;
            this.StateLabel.Text = "State";
            // 
            // SaveCustomerBtn
            // 
            this.SaveCustomerBtn.Location = new System.Drawing.Point(251, 373);
            this.SaveCustomerBtn.Name = "SaveCustomerBtn";
            this.SaveCustomerBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveCustomerBtn.TabIndex = 7;
            this.SaveCustomerBtn.Text = "Save";
            this.SaveCustomerBtn.UseVisualStyleBackColor = true;
            this.SaveCustomerBtn.Click += new System.EventHandler(this.SaveCustomerBtn_Click);
            // 
            // CancelCustomerBtn
            // 
            this.CancelCustomerBtn.Location = new System.Drawing.Point(357, 373);
            this.CancelCustomerBtn.Name = "CancelCustomerBtn";
            this.CancelCustomerBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelCustomerBtn.TabIndex = 8;
            this.CancelCustomerBtn.Text = "Cancel";
            this.CancelCustomerBtn.UseVisualStyleBackColor = true;
            this.CancelCustomerBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // CustomerIdInput
            // 
            this.CustomerIdInput.Enabled = false;
            this.CustomerIdInput.Location = new System.Drawing.Point(199, 70);
            this.CustomerIdInput.Name = "CustomerIdInput";
            this.CustomerIdInput.Size = new System.Drawing.Size(100, 20);
            this.CustomerIdInput.TabIndex = 9;
            // 
            // CustomerNameInput
            // 
            this.CustomerNameInput.Location = new System.Drawing.Point(199, 112);
            this.CustomerNameInput.Name = "CustomerNameInput";
            this.CustomerNameInput.Size = new System.Drawing.Size(100, 20);
            this.CustomerNameInput.TabIndex = 10;
            // 
            // CustomerAddressInput
            // 
            this.CustomerAddressInput.Location = new System.Drawing.Point(199, 155);
            this.CustomerAddressInput.Name = "CustomerAddressInput";
            this.CustomerAddressInput.Size = new System.Drawing.Size(100, 20);
            this.CustomerAddressInput.TabIndex = 11;
            // 
            // PhoneNumberInput
            // 
            this.PhoneNumberInput.Location = new System.Drawing.Point(199, 197);
            this.PhoneNumberInput.Name = "PhoneNumberInput";
            this.PhoneNumberInput.Size = new System.Drawing.Size(100, 20);
            this.PhoneNumberInput.TabIndex = 12;
            // 
            // StateInput
            // 
            this.StateInput.Location = new System.Drawing.Point(199, 268);
            this.StateInput.Name = "StateInput";
            this.StateInput.Size = new System.Drawing.Size(100, 20);
            this.StateInput.TabIndex = 13;
            // 
            // PostalCodeInput
            // 
            this.PostalCodeInput.Location = new System.Drawing.Point(199, 303);
            this.PostalCodeInput.Name = "PostalCodeInput";
            this.PostalCodeInput.Size = new System.Drawing.Size(100, 20);
            this.PostalCodeInput.TabIndex = 14;
            // 
            // CountryInput
            // 
            this.CountryInput.Location = new System.Drawing.Point(199, 338);
            this.CountryInput.Name = "CountryInput";
            this.CountryInput.Size = new System.Drawing.Size(100, 20);
            this.CountryInput.TabIndex = 15;
            // 
            // PostalCodeLabel
            // 
            this.PostalCodeLabel.AutoSize = true;
            this.PostalCodeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PostalCodeLabel.Location = new System.Drawing.Point(105, 303);
            this.PostalCodeLabel.Name = "PostalCodeLabel";
            this.PostalCodeLabel.Size = new System.Drawing.Size(84, 17);
            this.PostalCodeLabel.TabIndex = 16;
            this.PostalCodeLabel.Text = "Postal Code";
            // 
            // CityLabel
            // 
            this.CityLabel.AutoSize = true;
            this.CityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CityLabel.Location = new System.Drawing.Point(154, 236);
            this.CityLabel.Name = "CityLabel";
            this.CityLabel.Size = new System.Drawing.Size(31, 17);
            this.CityLabel.TabIndex = 17;
            this.CityLabel.Text = "City";
            // 
            // CityInput
            // 
            this.CityInput.Location = new System.Drawing.Point(199, 228);
            this.CityInput.Name = "CityInput";
            this.CityInput.Size = new System.Drawing.Size(100, 20);
            this.CityInput.TabIndex = 18;
            // 
            // AddCustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 421);
            this.Controls.Add(this.CityInput);
            this.Controls.Add(this.CityLabel);
            this.Controls.Add(this.PostalCodeLabel);
            this.Controls.Add(this.CountryInput);
            this.Controls.Add(this.PostalCodeInput);
            this.Controls.Add(this.StateInput);
            this.Controls.Add(this.PhoneNumberInput);
            this.Controls.Add(this.CustomerAddressInput);
            this.Controls.Add(this.CustomerNameInput);
            this.Controls.Add(this.CustomerIdInput);
            this.Controls.Add(this.CancelCustomerBtn);
            this.Controls.Add(this.SaveCustomerBtn);
            this.Controls.Add(this.StateLabel);
            this.Controls.Add(this.CountryLabel);
            this.Controls.Add(this.CustomerPhoneLabel);
            this.Controls.Add(this.CustomerAddressLabel);
            this.Controls.Add(this.CustomerNameLabel);
            this.Controls.Add(this.CustomerIdLabel);
            this.Controls.Add(this.label1);
            this.Name = "AddCustomerForm";
            this.Text = "AddCustomerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CustomerIdLabel;
        private System.Windows.Forms.Label CustomerNameLabel;
        private System.Windows.Forms.Label CustomerAddressLabel;
        private System.Windows.Forms.Label CustomerPhoneLabel;
        private System.Windows.Forms.Label CountryLabel;
        private System.Windows.Forms.Label StateLabel;
        private System.Windows.Forms.Button SaveCustomerBtn;
        private System.Windows.Forms.Button CancelCustomerBtn;
        private System.Windows.Forms.TextBox CustomerIdInput;
        private System.Windows.Forms.TextBox CustomerNameInput;
        private System.Windows.Forms.TextBox CustomerAddressInput;
        private System.Windows.Forms.TextBox PhoneNumberInput;
        private System.Windows.Forms.TextBox StateInput;
        private System.Windows.Forms.TextBox PostalCodeInput;
        private System.Windows.Forms.TextBox CountryInput;
        private System.Windows.Forms.Label PostalCodeLabel;
        private System.Windows.Forms.Label CityLabel;
        private System.Windows.Forms.TextBox CityInput;
    }
}