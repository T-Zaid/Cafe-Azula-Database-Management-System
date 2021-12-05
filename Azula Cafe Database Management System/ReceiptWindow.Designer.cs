
namespace Azula_Cafe_Database_Management_System
{
    partial class ReceiptWindow
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SerialNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SeatNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustID_Label = new System.Windows.Forms.Label();
            this.StartTime_Label = new System.Windows.Forms.Label();
            this.BookingTime_Label = new System.Windows.Forms.Label();
            this.EndTime_Label = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.PrintButton = new System.Windows.Forms.Button();
            this.AmountPaidColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.TotalAmountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(132, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Booking Receipt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(52, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Customer ID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(260, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Start Time:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(263, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "End Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(109, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(259, 55);
            this.label5.TabIndex = 4;
            this.label5.Text = "Cafe Azula";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(33, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Date of Booking:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNo,
            this.SeatNum,
            this.AmountPaidColumn});
            this.dataGridView1.Location = new System.Drawing.Point(64, 196);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(343, 150);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // SerialNo
            // 
            this.SerialNo.HeaderText = "Serial#";
            this.SerialNo.Name = "SerialNo";
            // 
            // SeatNum
            // 
            this.SeatNum.HeaderText = "Seat#";
            this.SeatNum.Name = "SeatNum";
            // 
            // CustID_Label
            // 
            this.CustID_Label.AutoSize = true;
            this.CustID_Label.ForeColor = System.Drawing.Color.DodgerBlue;
            this.CustID_Label.Location = new System.Drawing.Point(126, 108);
            this.CustID_Label.Name = "CustID_Label";
            this.CustID_Label.Size = new System.Drawing.Size(35, 13);
            this.CustID_Label.TabIndex = 7;
            this.CustID_Label.Text = "label7";
            // 
            // StartTime_Label
            // 
            this.StartTime_Label.AutoSize = true;
            this.StartTime_Label.ForeColor = System.Drawing.Color.DodgerBlue;
            this.StartTime_Label.Location = new System.Drawing.Point(324, 108);
            this.StartTime_Label.Name = "StartTime_Label";
            this.StartTime_Label.Size = new System.Drawing.Size(35, 13);
            this.StartTime_Label.TabIndex = 8;
            this.StartTime_Label.Text = "label7";
            // 
            // BookingTime_Label
            // 
            this.BookingTime_Label.AutoSize = true;
            this.BookingTime_Label.ForeColor = System.Drawing.Color.DodgerBlue;
            this.BookingTime_Label.Location = new System.Drawing.Point(126, 135);
            this.BookingTime_Label.Name = "BookingTime_Label";
            this.BookingTime_Label.Size = new System.Drawing.Size(35, 13);
            this.BookingTime_Label.TabIndex = 9;
            this.BookingTime_Label.Text = "label7";
            // 
            // EndTime_Label
            // 
            this.EndTime_Label.AutoSize = true;
            this.EndTime_Label.ForeColor = System.Drawing.Color.DodgerBlue;
            this.EndTime_Label.Location = new System.Drawing.Point(324, 135);
            this.EndTime_Label.Name = "EndTime_Label";
            this.EndTime_Label.Size = new System.Drawing.Size(35, 13);
            this.EndTime_Label.TabIndex = 10;
            this.EndTime_Label.Text = "label7";
            this.EndTime_Label.Click += new System.EventHandler(this.label7_Click);
            // 
            // OKButton
            // 
            this.OKButton.ForeColor = System.Drawing.Color.Black;
            this.OKButton.Location = new System.Drawing.Point(138, 352);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 11;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // PrintButton
            // 
            this.PrintButton.ForeColor = System.Drawing.Color.Black;
            this.PrintButton.Location = new System.Drawing.Point(254, 352);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(75, 23);
            this.PrintButton.TabIndex = 12;
            this.PrintButton.Text = "Print";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // AmountPaidColumn
            // 
            this.AmountPaidColumn.HeaderText = "Amount Paid";
            this.AmountPaidColumn.Name = "AmountPaidColumn";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(172, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Total Amount:";
            // 
            // TotalAmountLabel
            // 
            this.TotalAmountLabel.AutoSize = true;
            this.TotalAmountLabel.Location = new System.Drawing.Point(251, 168);
            this.TotalAmountLabel.Name = "TotalAmountLabel";
            this.TotalAmountLabel.Size = new System.Drawing.Size(35, 13);
            this.TotalAmountLabel.TabIndex = 14;
            this.TotalAmountLabel.Text = "label8";
            // 
            // ReceiptWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(470, 384);
            this.Controls.Add(this.TotalAmountLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.EndTime_Label);
            this.Controls.Add(this.BookingTime_Label);
            this.Controls.Add(this.StartTime_Label);
            this.Controls.Add(this.CustID_Label);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.DodgerBlue;
            this.Name = "ReceiptWindow";
            this.Text = "Booking Receipt";
            this.Load += new System.EventHandler(this.ReceiptWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeatNum;
        private System.Windows.Forms.Label CustID_Label;
        private System.Windows.Forms.Label StartTime_Label;
        private System.Windows.Forms.Label BookingTime_Label;
        private System.Windows.Forms.Label EndTime_Label;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountPaidColumn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label TotalAmountLabel;
    }
}