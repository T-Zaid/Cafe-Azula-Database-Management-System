namespace Azula_Cafe_Database_Management_System
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.LoginPage = new System.Windows.Forms.TabPage();
            this.Register = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AccountPassword = new System.Windows.Forms.TextBox();
            this.AccountName = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CustomerPage = new System.Windows.Forms.TabPage();
            this.ViewEventsButton = new System.Windows.Forms.Button();
            this.CancelSeatBookingButton = new System.Windows.Forms.Button();
            this.ViewBookingHistoryButton = new System.Windows.Forms.Button();
            this.BookSeatsButton = new System.Windows.Forms.Button();
            this.CustomerNameLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BookSeatsPage = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.ContinueBookSeatsButton = new System.Windows.Forms.Button();
            this.BackFromBookSeatsButton = new System.Windows.Forms.Button();
            this.NumHoursDropDown = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.StartTimeSelect = new System.Windows.Forms.DateTimePicker();
            this.NumSeatsDropDown = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.StartingTimePicker = new System.Windows.Forms.DateTimePicker();
            this.BookSeatsPage2 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.LoginPage.SuspendLayout();
            this.CustomerPage.SuspendLayout();
            this.BookSeatsPage.SuspendLayout();
            this.BookSeatsPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.LoginPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.CustomerPage);
            this.tabControl1.Controls.Add(this.BookSeatsPage);
            this.tabControl1.Controls.Add(this.BookSeatsPage2);
            this.tabControl1.Location = new System.Drawing.Point(-7, -5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(814, 468);
            this.tabControl1.TabIndex = 0;
            // 
            // LoginPage
            // 
            this.LoginPage.Controls.Add(this.Register);
            this.LoginPage.Controls.Add(this.label4);
            this.LoginPage.Controls.Add(this.LoginButton);
            this.LoginPage.Controls.Add(this.label3);
            this.LoginPage.Controls.Add(this.label2);
            this.LoginPage.Controls.Add(this.label1);
            this.LoginPage.Controls.Add(this.AccountPassword);
            this.LoginPage.Controls.Add(this.AccountName);
            this.LoginPage.Location = new System.Drawing.Point(4, 22);
            this.LoginPage.Name = "LoginPage";
            this.LoginPage.Padding = new System.Windows.Forms.Padding(3);
            this.LoginPage.Size = new System.Drawing.Size(806, 442);
            this.LoginPage.TabIndex = 0;
            this.LoginPage.Text = "Login Page";
            this.LoginPage.UseVisualStyleBackColor = true;
            // 
            // Register
            // 
            this.Register.Location = new System.Drawing.Point(359, 334);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(75, 23);
            this.Register.TabIndex = 7;
            this.Register.Text = "Register";
            this.Register.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(356, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Not a user ?";
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(359, 275);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 23);
            this.LoginButton.TabIndex = 5;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(222, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enter Password :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(219, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Enter Username :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(178, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(411, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "Azula Cafe Management System";
            // 
            // AccountPassword
            // 
            this.AccountPassword.Location = new System.Drawing.Point(337, 214);
            this.AccountPassword.Name = "AccountPassword";
            this.AccountPassword.PasswordChar = '*';
            this.AccountPassword.Size = new System.Drawing.Size(123, 20);
            this.AccountPassword.TabIndex = 1;
            // 
            // AccountName
            // 
            this.AccountName.Location = new System.Drawing.Point(336, 154);
            this.AccountName.Name = "AccountName";
            this.AccountName.Size = new System.Drawing.Size(124, 20);
            this.AccountName.TabIndex = 0;
            this.AccountName.TextChanged += new System.EventHandler(this.AccountName_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(806, 442);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Welcome";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CustomerPage
            // 
            this.CustomerPage.Controls.Add(this.ViewEventsButton);
            this.CustomerPage.Controls.Add(this.CancelSeatBookingButton);
            this.CustomerPage.Controls.Add(this.ViewBookingHistoryButton);
            this.CustomerPage.Controls.Add(this.BookSeatsButton);
            this.CustomerPage.Controls.Add(this.CustomerNameLabel);
            this.CustomerPage.Controls.Add(this.label5);
            this.CustomerPage.Location = new System.Drawing.Point(4, 22);
            this.CustomerPage.Name = "CustomerPage";
            this.CustomerPage.Size = new System.Drawing.Size(806, 442);
            this.CustomerPage.TabIndex = 2;
            this.CustomerPage.Text = "Customer";
            this.CustomerPage.UseVisualStyleBackColor = true;
            this.CustomerPage.Click += new System.EventHandler(this.CustomerPage_Click);
            // 
            // ViewEventsButton
            // 
            this.ViewEventsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewEventsButton.Location = new System.Drawing.Point(419, 281);
            this.ViewEventsButton.Name = "ViewEventsButton";
            this.ViewEventsButton.Size = new System.Drawing.Size(276, 118);
            this.ViewEventsButton.TabIndex = 5;
            this.ViewEventsButton.Text = "View Events";
            this.ViewEventsButton.UseVisualStyleBackColor = true;
            // 
            // CancelSeatBookingButton
            // 
            this.CancelSeatBookingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelSeatBookingButton.Location = new System.Drawing.Point(114, 281);
            this.CancelSeatBookingButton.Name = "CancelSeatBookingButton";
            this.CancelSeatBookingButton.Size = new System.Drawing.Size(276, 118);
            this.CancelSeatBookingButton.TabIndex = 4;
            this.CancelSeatBookingButton.Text = "Cancel Seat Booking";
            this.CancelSeatBookingButton.UseVisualStyleBackColor = true;
            // 
            // ViewBookingHistoryButton
            // 
            this.ViewBookingHistoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewBookingHistoryButton.Location = new System.Drawing.Point(419, 141);
            this.ViewBookingHistoryButton.Name = "ViewBookingHistoryButton";
            this.ViewBookingHistoryButton.Size = new System.Drawing.Size(276, 118);
            this.ViewBookingHistoryButton.TabIndex = 3;
            this.ViewBookingHistoryButton.Text = "Booking History";
            this.ViewBookingHistoryButton.UseVisualStyleBackColor = true;
            // 
            // BookSeatsButton
            // 
            this.BookSeatsButton.BackgroundImage = global::Azula_Cafe_Database_Management_System.Properties.Resources.bookSeatButton;
            this.BookSeatsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BookSeatsButton.Location = new System.Drawing.Point(114, 141);
            this.BookSeatsButton.Name = "BookSeatsButton";
            this.BookSeatsButton.Size = new System.Drawing.Size(276, 118);
            this.BookSeatsButton.TabIndex = 2;
            this.BookSeatsButton.Text = "Book Seats";
            this.BookSeatsButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BookSeatsButton.UseVisualStyleBackColor = true;
            this.BookSeatsButton.Click += new System.EventHandler(this.BookSeatsButton_Click);
            // 
            // CustomerNameLabel
            // 
            this.CustomerNameLabel.AutoSize = true;
            this.CustomerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerNameLabel.Location = new System.Drawing.Point(330, 39);
            this.CustomerNameLabel.Name = "CustomerNameLabel";
            this.CustomerNameLabel.Size = new System.Drawing.Size(124, 20);
            this.CustomerNameLabel.TabIndex = 1;
            this.CustomerNameLabel.Text = "Cusomter Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(196, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(400, 39);
            this.label5.TabIndex = 0;
            this.label5.Text = "Welcome to Cafe Azula";
            // 
            // BookSeatsPage
            // 
            this.BookSeatsPage.Controls.Add(this.StartingTimePicker);
            this.BookSeatsPage.Controls.Add(this.label11);
            this.BookSeatsPage.Controls.Add(this.ContinueBookSeatsButton);
            this.BookSeatsPage.Controls.Add(this.BackFromBookSeatsButton);
            this.BookSeatsPage.Controls.Add(this.NumHoursDropDown);
            this.BookSeatsPage.Controls.Add(this.label10);
            this.BookSeatsPage.Controls.Add(this.label9);
            this.BookSeatsPage.Controls.Add(this.StartTimeSelect);
            this.BookSeatsPage.Controls.Add(this.NumSeatsDropDown);
            this.BookSeatsPage.Controls.Add(this.label8);
            this.BookSeatsPage.Controls.Add(this.label7);
            this.BookSeatsPage.Controls.Add(this.label6);
            this.BookSeatsPage.Location = new System.Drawing.Point(4, 22);
            this.BookSeatsPage.Name = "BookSeatsPage";
            this.BookSeatsPage.Size = new System.Drawing.Size(806, 442);
            this.BookSeatsPage.TabIndex = 3;
            this.BookSeatsPage.Text = "Book Seats";
            this.BookSeatsPage.UseVisualStyleBackColor = true;
            this.BookSeatsPage.Click += new System.EventHandler(this.BookSeatsPage_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(364, 301);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "(Default is set to Current Time)";
            // 
            // ContinueBookSeatsButton
            // 
            this.ContinueBookSeatsButton.Location = new System.Drawing.Point(307, 350);
            this.ContinueBookSeatsButton.Name = "ContinueBookSeatsButton";
            this.ContinueBookSeatsButton.Size = new System.Drawing.Size(75, 23);
            this.ContinueBookSeatsButton.TabIndex = 9;
            this.ContinueBookSeatsButton.Text = "Continue";
            this.ContinueBookSeatsButton.UseVisualStyleBackColor = true;
            this.ContinueBookSeatsButton.Click += new System.EventHandler(this.ContinueBookSeatsButton_Click);
            // 
            // BackFromBookSeatsButton
            // 
            this.BackFromBookSeatsButton.Location = new System.Drawing.Point(429, 350);
            this.BackFromBookSeatsButton.Name = "BackFromBookSeatsButton";
            this.BackFromBookSeatsButton.Size = new System.Drawing.Size(75, 23);
            this.BackFromBookSeatsButton.TabIndex = 8;
            this.BackFromBookSeatsButton.Text = "Back";
            this.BackFromBookSeatsButton.UseVisualStyleBackColor = true;
            // 
            // NumHoursDropDown
            // 
            this.NumHoursDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NumHoursDropDown.FormattingEnabled = true;
            this.NumHoursDropDown.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.NumHoursDropDown.Location = new System.Drawing.Point(339, 206);
            this.NumHoursDropDown.Name = "NumHoursDropDown";
            this.NumHoursDropDown.Size = new System.Drawing.Size(36, 21);
            this.NumHoursDropDown.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(243, 209);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Number of Hours:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(243, 278);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Starting Time:";
            // 
            // StartTimeSelect
            // 
            this.StartTimeSelect.Location = new System.Drawing.Point(321, 278);
            this.StartTimeSelect.Name = "StartTimeSelect";
            this.StartTimeSelect.Size = new System.Drawing.Size(146, 20);
            this.StartTimeSelect.TabIndex = 4;
            // 
            // NumSeatsDropDown
            // 
            this.NumSeatsDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NumSeatsDropDown.FormattingEnabled = true;
            this.NumSeatsDropDown.Location = new System.Drawing.Point(338, 136);
            this.NumSeatsDropDown.Name = "NumSeatsDropDown";
            this.NumSeatsDropDown.Size = new System.Drawing.Size(36, 21);
            this.NumSeatsDropDown.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(243, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Number of Seats:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(265, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(280, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Fill all the fields then click continue to proceed.";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(265, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 55);
            this.label6.TabIndex = 0;
            this.label6.Text = "Book Seats";
            // 
            // StartingTimePicker
            // 
            this.StartingTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.StartingTimePicker.Location = new System.Drawing.Point(473, 278);
            this.StartingTimePicker.Name = "StartingTimePicker";
            this.StartingTimePicker.Size = new System.Drawing.Size(92, 20);
            this.StartingTimePicker.TabIndex = 11;
            // 
            // BookSeatsPage2
            // 
            this.BookSeatsPage2.Controls.Add(this.label12);
            this.BookSeatsPage2.Location = new System.Drawing.Point(4, 22);
            this.BookSeatsPage2.Name = "BookSeatsPage2";
            this.BookSeatsPage2.Size = new System.Drawing.Size(806, 442);
            this.BookSeatsPage2.TabIndex = 4;
            this.BookSeatsPage2.Text = "Book Seats 2";
            this.BookSeatsPage2.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(295, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(224, 39);
            this.label12.TabIndex = 0;
            this.label12.Text = "Select Seats";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.LoginPage.ResumeLayout(false);
            this.LoginPage.PerformLayout();
            this.CustomerPage.ResumeLayout(false);
            this.CustomerPage.PerformLayout();
            this.BookSeatsPage.ResumeLayout(false);
            this.BookSeatsPage.PerformLayout();
            this.BookSeatsPage2.ResumeLayout(false);
            this.BookSeatsPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage LoginPage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Register;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AccountPassword;
        private System.Windows.Forms.TextBox AccountName;
        private System.Windows.Forms.TabPage CustomerPage;
        private System.Windows.Forms.Label CustomerNameLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button CancelSeatBookingButton;
        private System.Windows.Forms.Button ViewBookingHistoryButton;
        private System.Windows.Forms.Button BookSeatsButton;
        private System.Windows.Forms.Button ViewEventsButton;
        private System.Windows.Forms.TabPage BookSeatsPage;
        private System.Windows.Forms.ComboBox NumSeatsDropDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker StartTimeSelect;
        private System.Windows.Forms.Button ContinueBookSeatsButton;
        private System.Windows.Forms.Button BackFromBookSeatsButton;
        private System.Windows.Forms.ComboBox NumHoursDropDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker StartingTimePicker;
        private System.Windows.Forms.TabPage BookSeatsPage2;
        private System.Windows.Forms.Label label12;
    }
}

