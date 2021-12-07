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
        List<int> selectedSeats;
        //SqlCommand cmd;
        //SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            umer_connectionString = @"Data Source=DESKTOP-L0E3C0D\SERWORK;Initial Catalog=AzulaDB;Integrated Security = True;MultipleActiveResultSets=true";
            connectionString = @"Data Source=ZAID-PC\SERWORK;Initial Catalog=AzulaDB;Integrated Security = True;MultipleActiveResultSets=true";
            cnn = new SqlConnection(umer_connectionString);
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
            selectedSeats = new List<int>();

            string newQuery = "SELECT count(*) as cnt FROM Seats";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int rows = Convert.ToInt32(reader["cnt"]);
            reader.Close();
            cmd.Dispose();

            NumSeatsDropDown.Items.Clear();
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
            SeatBookingPage1();
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
                    MessageBox.Show("Congratulations !! Account Created", "Creation Successfull");
                    tabControl1.SelectedTab = LoginPage;
                }
                else
                    MessageBox.Show("The Username is already in use. Try Another One!\nThe world is small, but the combinations are immense", "Account exists with this Username", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        private void SeatBookingPage1()
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

            if (availableSeatNums.Count() < Convert.ToInt32(NumSeatsDropDown.SelectedItem))
            {
                MessageBox.Show("Not enough seats available. Kindly reduce the number of seats or pick a different time.", "Seats Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // MessageBox.Show(availableSeatNums.Count().ToString() + availableSeatNums[0].ToString() + availableSeatNums[1].ToString() + availableSeatNums[2].ToString() + availableSeatNums[3].ToString(), "DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            SeatPicker.Items.Clear();
            for (int i = 0; i < availableSeatNums.Count(); i++)
            {
                SeatPicker.Items.Add(availableSeatNums[i]);
            }

            SeatBookingPage2();
        }

        private void SeatBookingPage2()
        {
            selectedSeats.Clear();
            ComputerInfoBookingLabel.Text = "N/A";
            SeatPicker.SelectedIndex = 0;
            SelectedSeatsLabel.Text = "";
            RemoveSeatButton.Enabled = false;
            AddSeatButton.Enabled = true;

            string newQuery = "SELECT CPU, GPU, RAM, NetSpeed FROM Computers AS C JOIN Seats AS S ON C.ComputerID = S.ComputerID WHERE SeatNo = " + SeatPicker.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ComputerInfoBookingLabel.Text = reader["CPU"].ToString() + "\n" + reader["GPU"].ToString() + "\n" + reader["RAM"] + "GB RAM\n" + reader["NetSpeed"].ToString() + "MB/s";
            }

            tabControl1.SelectedTab = BookSeatsPage2;
        }

        private void AddSeatButton_Click(object sender, EventArgs e)
        {
            if (selectedSeats.Contains(Convert.ToInt32(SeatPicker.SelectedItem)))
            {
                MessageBox.Show("Cannot add seat that is already selected.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedSeats.Add(Convert.ToInt32(SeatPicker.SelectedItem));
            UpdateSelectedSeatsLabel();

            if (selectedSeats.Count() == Convert.ToInt32(NumSeatsDropDown.SelectedItem))
            {
                AddSeatButton.Enabled = false;
            }

            if(selectedSeats.Count() > 0)
            {
                RemoveSeatButton.Enabled = true;
            }
        }

        private void RemoveSeatButton_Click(object sender, EventArgs e)
        {
            if (!selectedSeats.Contains(Convert.ToInt32(SeatPicker.SelectedItem)))
            {
                MessageBox.Show("Cannot remove seat that is not selected.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedSeats.Remove(Convert.ToInt32(SeatPicker.SelectedItem));
            UpdateSelectedSeatsLabel();

            if (selectedSeats.Count() < Convert.ToInt32(NumSeatsDropDown.SelectedItem))
            {
                AddSeatButton.Enabled = true;
            }

            if(selectedSeats.Count() == 0)
            {
                RemoveSeatButton.Enabled = false;
            }
        }

        private void UpdateSelectedSeatsLabel()
        {
            SelectedSeatsLabel.Text = "";

            for(int i = 0; i < selectedSeats.Count(); i++)
            {
                if (i != 0)
                    SelectedSeatsLabel.Text += ", ";
                SelectedSeatsLabel.Text += selectedSeats[i];
            }
        }

        private void BackFromBookSeatsButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = CustomerPage;
        }

        private void AutoSelectSeatsButton_Click(object sender, EventArgs e)
        {
            AddSeatButton.Enabled = false;
            RemoveSeatButton.Enabled = true;

            selectedSeats.Clear();
            for(int i = 0; i < Convert.ToInt32(NumSeatsDropDown.SelectedItem); i++)
            {
                selectedSeats.Add(Convert.ToInt32(SeatPicker.Items[i]));
            }

            SelectedSeatsLabel.Text = "";
            UpdateSelectedSeatsLabel();
        }

        private void BackButton2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = BookSeatsPage;
        }

        private void ContinueButton2_Click(object sender, EventArgs e)
        {
            if (selectedSeats.Count() < Convert.ToInt32(NumSeatsDropDown.SelectedItem))
            {
                MessageBox.Show("Please select " + NumSeatsDropDown.SelectedItem + " seats to proceed.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TicketingSystem Book = new TicketingSystem(cnn);
            DateTime start = new DateTime(StartTimeSelect.Value.Year, StartTimeSelect.Value.Month, StartTimeSelect.Value.Day, StartingTimePicker.Value.Hour, StartingTimePicker.Value.Minute, StartingTimePicker.Value.Second);
            DateTime end = start.AddHours(Convert.ToInt32(NumHoursDropDown.SelectedItem));

            Book.BookMultipleSeats(selectedSeats.Count(), selectedSeats, start, end, customerID_Form1);

            tabControl1.SelectedTab = CustomerPage;
        }

        private void CancelSeatBookingButton_Click(object sender, EventArgs e)
        {
            CancelBookingPageCall();
        }

        private void CancelBookingPageCall()
        {
            CancelBookingTable.Rows.Clear();
            SearchCancelSeatDropdown.Items.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString() + " AND Start_Time > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                CancelBookingTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();

            SearchSeatDropDown.Items.Clear();

            newQuery = "SELECT SeatNo FROM Seats";
            cmd = new SqlCommand(newQuery, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SearchCancelSeatDropdown.Items.Add(reader["SeatNo"]);
            }

            reader.Close();
            cmd.Dispose();

            tabControl1.SelectedTab = CancelSeatPage;
        }

        private void CancelBookingBackButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = CustomerPage;
        }

        private void CancelBookingButton_Click(object sender, EventArgs e)
        {
            if(CancelBookingTable.RowCount != 0)
            {
                var yesNo = MessageBox.Show("Are you sure you want to cancel this seat booking?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (yesNo == DialogResult.Yes)
                {
                    string newQuery = "DELETE FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString() +
                                        " AND SeatNo = " + CancelBookingTable.CurrentRow.Cells[0].Value.ToString() +
                                        " AND Start_Time = '" + Convert.ToDateTime(CancelBookingTable.CurrentRow.Cells[2].Value).ToString("yyyy-MM-dd HH:mm:ss") +
                                        "' AND End_Time = '" + Convert.ToDateTime(CancelBookingTable.CurrentRow.Cells[3].Value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    SqlCommand cmd = new SqlCommand(newQuery, cnn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Booking successfully cancelled.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CancelBookingPageCall();
                }
            }
        }

        private void ViewBookingHistoryButton_Click(object sender, EventArgs e)
        {
            BookingHistoryStatementLabel.Text = "Following is the list of all bookings.";
            BookingHistoryTable.Rows.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString();
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                BookingHistoryTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();

            SearchSeatDropDown.Items.Clear();

            newQuery = "SELECT SeatNo FROM Seats";
            cmd = new SqlCommand(newQuery, cnn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SearchSeatDropDown.Items.Add(reader["SeatNo"]);
            }

            reader.Close();
            cmd.Dispose();

            tabControl1.SelectedTab = ViewBookingsPage;
        }

        private void CustomerActiveBookingsButton_Click(object sender, EventArgs e)
        {
            BookingHistoryStatementLabel.Text = "Following is the list of currently active bookings.";
            BookingHistoryTable.Rows.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString() + " AND End_Time > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                BookingHistoryTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void CustomerPastBookingsButton_Click(object sender, EventArgs e)
        {
            BookingHistoryStatementLabel.Text = "Following is the list of past bookings.";
            BookingHistoryTable.Rows.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString() + " AND End_Time < '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                BookingHistoryTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void CustomerAllBookingsButton_Click(object sender, EventArgs e)
        {
            BookingHistoryStatementLabel.Text = "Following is the list of all bookings.";
            BookingHistoryTable.Rows.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString();
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                BookingHistoryTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void BookingHistoryBackButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = CustomerPage;
        }

        private void SearchDateHistory_ValueChanged(object sender, EventArgs e)
        {
            BookingHistoryTable.Rows.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString() + " AND Date_of_Booking = '" + SearchDateHistory.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                BookingHistoryTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void SearchSeatDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            BookingHistoryTable.Rows.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString() + " AND SeatNo = " + SearchSeatDropDown.SelectedItem;
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                BookingHistoryTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void SearchCancelSeatDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            CancelBookingTable.Rows.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString() + " AND Start_Time > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND SeatNo = " + SearchCancelSeatDropdown.SelectedItem;
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                CancelBookingTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void SearchCancelDateDropDown_ValueChanged(object sender, EventArgs e)
        {
            CancelBookingTable.Rows.Clear();

            string newQuery = "SELECT * FROM Bookings WHERE CustomerID = " + customerID_Form1.ToString() + " AND Start_Time > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Date_of_Booking = '" + SearchCancelDateDropDown.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["SeatNo"].ToString(), reader["Date_of_Booking"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Amount_Paid"].ToString() };
                CancelBookingTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void NumSeatsDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newQuery = "SELECT CPU, GPU, RAM, NetSpeed FROM Computers as C JOIN Seats as S ON C.ComputerID = S.ComputerID WHERE SeatNo = " + NumSeatsDropDown.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ComputerInfoBookingLabel.Text = reader["CPU"].ToString() + "\n" + reader["GPU"].ToString() + "\n" + reader["RAM"].ToString() + "GB RAM\n" + reader["NetSpeed"].ToString() + "MB/s";
            }
            else
            {
                ComputerInfoBookingLabel.Text = "N/A";
            }
        }

        private void SeatPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComputerInfoBookingLabel.Text = "N/A";

            string newQuery = "SELECT CPU, GPU, RAM, NetSpeed FROM Computers AS C JOIN Seats AS S ON C.ComputerID = S.ComputerID WHERE SeatNo = " + SeatPicker.SelectedItem.ToString();
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                ComputerInfoBookingLabel.Text = reader["CPU"].ToString() + "\n" + reader["GPU"].ToString() + "\n" + reader["RAM"] + "GB RAM\n" + reader["NetSpeed"].ToString() + "MB/s";
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
