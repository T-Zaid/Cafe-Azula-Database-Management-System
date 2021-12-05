﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Azula_Cafe_Database_Management_System
{
    public partial class Form1 : Form
    {
        string connectionString, umer_connectionString;
        SqlConnection cnn;
        int customerID_Form1, staffID_From1;
        //SqlCommand cmd;
        //SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            umer_connectionString = @"Data Source=DESKTOP-L0E3C0D\SERWORK;Initial Catalog=AzulaDB;Integrated Security = True;MultipleActiveResultSets=true";
            connectionString = @"Data Source=ZAID-PC\SERWORK;Initial Catalog=AzulaDB;Integrated Security = True;MultipleActiveResultSets=true";
            cnn = new SqlConnection(connectionString);
            //cnn = new SqlConnection(umer_connectionString);
            cnn.Open();
        }

        private void AccountName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            //Login log = new Login(connectionString);
            Login log = new Login(cnn);
            if (AccountName.Text.Length > 0 && AccountPassword.Text.Length > 0)
            {
                //string sql = "select * from Accounts where Username = '" + AccountName.Text.ToString() + "' and AccPassword = '" + AccountPassword.Text.ToString() + "'";
                //cmd = new SqlCommand(sql, cnn);
                //reader = cmd.ExecuteReader();
                //if (reader.Read()) //if the account exists
                //{
                //        int AccNo = Convert.ToInt32(reader["AccountNo"]);
                //        reader.Close();
                //        cmd.Dispose();
                //        tabControl1.SelectedTab = tabPage2;
                //}
                //else
                //{
                //    reader.Close();
                //    cmd.Dispose();
                //    MessageBox.Show("Invalid Credentials");
                //}
                //Login log = new Login(connectionString);
                int[] flag = new int[2];
                flag = log.checkaccount(AccountName.Text.ToString(), AccountPassword.Text.ToString());
                if (flag[0] == 1)
                    MessageBox.Show("Welcome Staff");
                //tabControl1.SelectedTab = CustAccReg;
                else if (flag[0] == 0)
                {
                    customerID_Form1 = flag[1];
                    string newQuery = "SELECT CustName FROM Customers WHERE CustomerID = " + customerID_Form1.ToString();
                    SqlCommand cmd = new SqlCommand(newQuery, cnn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    CustomerNameLabel.Text = reader["CustName"].ToString();
                    reader.Close();
                    cmd.Dispose();
                    tabControl1.SelectedTab = CustomerPage;
                }
                else
                    MessageBox.Show("Invalid Credentials");
            }
        }

        private void CustomerPage_Click(object sender, EventArgs e)
        {

        }

        private void BookSeatsButton_Click(object sender, EventArgs e)
        {
            string newQuery = "SELECT count(*) as cnt FROM Seats";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int rows = Convert.ToInt32(reader["cnt"]);
            reader.Close();
            cmd.Dispose();

            for(int i = 0; i < rows && i < 10; i++)
            {
                NumSeatsDropDown.Items.Add(i + 1);
            }

            NumSeatsDropDown.SelectedIndex = 0;
            NumHoursDropDown.SelectedIndex = 0;
            StartTimeSelect.Value = DateTime.Now;
            StartingTimePicker.Value = DateTime.Now.AddMinutes(5);
            tabControl1.SelectedTab = BookSeatsPage;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void BookSeatsPage_Click(object sender, EventArgs e)
        {

        }

        private void ContinueBookSeatsButton_Click(object sender, EventArgs e)
        {
            DateTime start = new DateTime(StartTimeSelect.Value.Year, StartTimeSelect.Value.Month, StartTimeSelect.Value.Day, StartingTimePicker.Value.Hour, StartingTimePicker.Value.Minute, StartingTimePicker.Value.Second);
            DateTime end = start.AddHours(Convert.ToInt32(NumHoursDropDown.SelectedItem));

            if (start < DateTime.Now)
            {
                MessageBox.Show("Please enter a time after current time.", "Input Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> availableSeatNums = new List<int>();
            TicketingSystem seatCheck = new TicketingSystem(cnn);

            string newQuery = "SELECT SeatNo FROM Seats";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int sNum = Convert.ToInt32(reader["SeatNo"]);

                if (seatCheck.CheckSeatAvailability(sNum, start, end))
                {
                    availableSeatNums.Add(sNum);
                }
            }

            reader.Close();
            cmd.Dispose();

            if(availableSeatNums.Count() < Convert.ToInt32(NumSeatsDropDown.SelectedItem))
            {
                MessageBox.Show("Not enough seats available. Kindly reduce the number of seats or pick a different time.", "Seats Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // MessageBox.Show(availableSeatNums.Count().ToString() + availableSeatNums[0].ToString() + availableSeatNums[1].ToString() + availableSeatNums[2].ToString() + availableSeatNums[3].ToString(), "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            tabControl1.SelectedTab = BookSeatsPage2;
        }

        private void Register_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = CustAccReg;
        }

        private void CustPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void CustCreateAccount_Click(object sender, EventArgs e)
        {
            Login log = new Login(cnn);
            if (CustName.Text.Length > 0 && CustPhone.Text.Length > 0 && CustPassword.Text.Length > 0 && CustUsername.Text.Length > 0)
            {
                int success = log.CustomerAccountCreate(CustName.Text, CustPhone.Text, CustUsername.Text, CustPassword.Text);
                if (success == 1)
                {
                    MessageBox.Show("Congratulations !! Account Created");
                    tabControl1.SelectedTab = LoginPage;
                }
                else
                    MessageBox.Show("The Username is already in use. Try Another One!\nThe world is small, but the combinations are immense");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cnn.Close();
        }
    }
}
