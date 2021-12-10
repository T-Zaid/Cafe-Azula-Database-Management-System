using System;
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
        int customerID_Form1, staffID_From1, staff_accessibility;
        List<int> selectedSeats;
        List<string> EventImageLocations;
        string previousPageFlag;
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
                int[] flag = new int[2];
                flag = log.checkaccount(AccountName.Text.ToString(), AccountPassword.Text.ToString());
                if (flag[0] == 1)
                {
                    staffID_From1 = flag[1];
                    string newQuery = "SELECT StaffName, Position FROM Staff WHERE StaffID = " + staffID_From1.ToString();
                    SqlCommand cmd = new SqlCommand(newQuery, cnn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    StaffLabelName.Text = reader["StaffName"].ToString();
                    staff_accessibility = log.positions.FindIndex(x => x == (reader["Position"].ToString()));
                    reader.Close();
                    cmd.Dispose();
                    tabControl1.SelectedTab = StaffPage;
                }
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
            NumHoursDropDown.Items.Clear();
            string[] hourRange = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            NumHoursDropDown.Items.AddRange(hourRange);

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
                int success = log.CustomerAccountCreate(CustName.Text.ToString(), CustPhone.Text.ToString(), CustUsername.Text.ToString(), CustPassword.Text.ToString());
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

            previousPageFlag = "BookSeatsPage";
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
            if (previousPageFlag == "BookSeatsPage")
                tabControl1.SelectedTab = BookSeatsPage;
            else if (previousPageFlag == "BookEventsPage")
                tabControl1.SelectedTab = BookEventsPage;
            else
                tabControl1.SelectedTab = CustomerPage;
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

        private void StaffAccReg_Click(object sender, EventArgs e)
        {

        }

        private void StaffRegisterAccount_Click(object sender, EventArgs e)
        {
            Login log = new Login(cnn);
            if (StaffName.Text.Length > 0 && StaffPhone.Text.Length > 0 && StaffPassword.Text.Length > 0 && StaffUsername.Text.Length > 0 && StaffSalary.Text.Length > 0 && StaffPosition.Text.Length > 0)
            {
                int success = log.StaffAccountCreate(StaffName.Text.ToString(), StaffUsername.Text.ToString(), StaffPassword.Text.ToString(), StaffPhone.Text.ToString(), StaffPosition.Text.ToString(), StaffSupervisor.Text.ToString(), Convert.ToInt32(StaffSalary.Text));
                if (success == 1)
                {
                    MessageBox.Show("Welcome Onboard Captain !!", "Creation Successfull");
                    tabControl1.SelectedTab = StaffPage;
                }
                else
                    MessageBox.Show("The Username is already in use. Try Another One!\nThe world is small, but the combinations are immense", "Account exists with this Username", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            else
                MessageBox.Show("Field(s) Empty", "Filling all the fields are mandatory", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void StaffSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void StaffPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void AddGameButton_Click(object sender, EventArgs e)
        {
            CafeFeatures feats = new CafeFeatures(cnn);
            if (GameName.Text.Length > 0)
            {
                int success = feats.InsertGame(GameName.Text.ToString(), GameGenre.Text.ToString(), GameDesc.Text.ToString(), Convert.ToInt32(PopularityUpdown.Value));
                if (success == 1)
                {
                    MessageBox.Show("Game '" + GameName.Text.ToString() + "' has been successfully added", "Game Added", MessageBoxButtons.OK);
                    tabControl1.SelectedTab = StaffPage;
                }
                else
                    MessageBox.Show("This Game is already in database");
            }
            else
                MessageBox.Show("Game Name Field is Empty", "Entering the Game name is mandatory !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CreateNewGame_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Game_Add;
        }

        private void RAM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void Netspeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar!='.')
                e.Handled = true;
        }

        private void AddComputerButton_Click(object sender, EventArgs e)
        {
            CafeFeatures feats = new CafeFeatures(cnn);
            if(CPUname.Text.Length > 0 && GPUname.Text.Length > 0 && RAM.Text.Length > 0 && Netspeed.Text.Length > 0)
            {
                int success = feats.InsertComputers(GPUname.Text.ToString(), CPUname.Text.ToString(), Convert.ToInt32(RAM.Text), (float)Convert.ToDouble(Netspeed.Text));
                if (success == 1)
                {
                    MessageBox.Show("Computer Added to Cafe", "Operation Successfull");
                    tabControl1.SelectedTab = StaffPage;
                }
                else
                    MessageBox.Show("Computer with this specs are already in the database", "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Filling all the fields is Mandatory", "Field(s) Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CreateNewComputer_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Computer_Add;
        }

        private void AddLeaderBoardButton_Click(object sender, EventArgs e)
        {
            CafeFeatures feats = new CafeFeatures(cnn);
            if(Gamedropdown.Text.Length > 0 && CustNameDropDown.Text.Length > 0 && Rank_ig.Text.Length > 0)
            {
                int success = feats.InsertinLeaderboard(Gamedropdown.Text.ToString(), CustNameDropDown.Text.ToString(), Convert.ToInt32(Rank_ig.Text));
                if(success == 1)
                {
                    MessageBox.Show("Congratulations on getting a position !", "LeaderBoard Added", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                    tabControl1.SelectedTab = StaffPage;
                }
                else
                {
                    MessageBox.Show("This record has already been stored", "Duplicate Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Entering all the fields are mandatory", "Field(s) Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LeaderboardOperations_Click(object sender, EventArgs e)
        {
            string Query = "select GameName from Games", Query1 = "select CustName from Customers";
            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            Gamedropdown.Items.Clear();
            while(reader.Read())
            {
                Gamedropdown.Items.Add(reader["GameName"]);
            }

            cmd = new SqlCommand(Query1, cnn);
            reader = cmd.ExecuteReader();
            CustNameDropDown.Items.Clear();
            while(reader.Read())
            {
                CustNameDropDown.Items.Add(reader["CustName"]);
            }

            reader.Close();
            tabControl1.SelectedTab = LeaderBoard_Add;
        }

        private void MaxParticipants_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void AddEventButton_Click(object sender, EventArgs e)
        {
            CafeFeatures feats = new CafeFeatures(cnn);
            if(EventName.Text.Length > 0 && MaxParticipants.Text.Length > 0 && EventDuration.Text.Length > 0)
            {
                DateTime start = new DateTime(EventStartDate.Value.Year, EventStartDate.Value.Month, EventStartDate.Value.Day, EventStartTime.Value.Hour, EventStartTime.Value.Minute, EventStartTime.Value.Second);
                DateTime end = start.AddHours(Convert.ToInt32(EventDuration.Text));

                if (start < DateTime.Now)
                {
                    MessageBox.Show("Please enter a time after current time.", "Input Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int success = feats.InsertEvent(EventName.Text.ToString(), start, end, GameDropEvent.Text.ToString(), Convert.ToInt32(MaxParticipants.Text), ImageLocation.Text.ToString());
                if (success == 1)
                    MessageBox.Show("Event successfully added to Cafe Azula", "Event Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("This event is already registered !!", "Event Duplicated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files| *.jpg;*.jpeg;*.png";
            dialog.InitialDirectory = @"E:\Azula Cafe Database Management System";
            dialog.Title = "Select the Image File for Event";

            if (dialog.ShowDialog() == DialogResult.OK)
                ImageLocation.Text = dialog.FileName;
        }

        private void CreateNewEvent_Click(object sender, EventArgs e)
        {
            string Query = "select GameName from Games";
            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            GameDropEvent.Items.Clear();
            while (reader.Read())
            {
                GameDropEvent.Items.Add(reader["GameName"]);
            }

            reader.Close();
            cmd.Dispose();
            tabControl1.SelectedTab = Event_Add;
        }

        private void ViewEventsButton_Click(object sender, EventArgs e)
        {
            EventViewerTable.Rows.Clear();
            EventImageLocations = new List<string>();
            string newQuery = "SELECT * FROM Events AS E LEFT OUTER JOIN Games AS G ON E.GameID = G.GameID WHERE Start_Time > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                string[] row = { reader["EventName"].ToString(), reader["GameName"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Max_Participants"].ToString() };
                EventViewerTable.Rows.Add(row);
                EventImageLocations.Add(reader["Poster_link"].ToString());
            }

            reader.Close();
            cmd.Dispose();

            if(EventViewerTable.RowCount != 0)
            {
                Bitmap Poster = new Bitmap(EventImageLocations[0]);

                EventPosterImage.SizeMode = PictureBoxSizeMode.StretchImage;
                EventPosterImage.Image = (Image)Poster;
            }    

            tabControl1.SelectedTab = BookEventsPage;
        }

        private void EventViewerTable_CurrentCellChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == BookEventsPage && EventViewerTable.RowCount != 0)
            {
                Bitmap Poster = new Bitmap(EventImageLocations[EventViewerTable.CurrentRow.Index]);

                EventPosterImage.SizeMode = PictureBoxSizeMode.StretchImage;
                EventPosterImage.Image = (Image)Poster;
            }
        }

        private void EventBackButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = CustomerPage;
        }

        private void EventBookButton_Click(object sender, EventArgs e)
        {
            List<int> availableSeatNums = new List<int>();
            TicketingSystem seatCheck = new TicketingSystem(cnn);
            DateTime start = Convert.ToDateTime(EventViewerTable.CurrentRow.Cells[2].Value.ToString());
            DateTime end = Convert.ToDateTime(EventViewerTable.CurrentRow.Cells[3].Value.ToString());

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

            if (availableSeatNums.Count() < Convert.ToInt32(EventViewerTable.CurrentRow.Cells[4].Value))
            {
                MessageBox.Show("Not enough seats remain for selected event.", "Seats Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NumSeatsDropDown.Items.Clear();
            NumSeatsDropDown.Items.Add(Convert.ToInt32(EventViewerTable.CurrentRow.Cells[4].Value));
            NumSeatsDropDown.SelectedIndex = 0;
            int hours = end.Hour - start.Hour;
            NumHoursDropDown.Items.Clear();
            NumHoursDropDown.Items.Add(hours.ToString());
            NumHoursDropDown.SelectedIndex = 0;
            StartTimeSelect.Value = start;
            StartingTimePicker.Value = start;
            previousPageFlag = "BookEventsPage";
            SeatPicker.Items.Clear();
            for (int i = 0; i < availableSeatNums.Count(); i++)
            {
                SeatPicker.Items.Add(availableSeatNums[i]);
            }
            selectedSeats = new List<int>();

            SeatBookingPage2();
        }

        private void StaffCreateAccount_Click(object sender, EventArgs e)
        {
            string Query = "select StaffName from Staff";
            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Dispose();

            StaffSupervisor.Items.Clear();
            while(reader.Read())
            {
                StaffSupervisor.Items.Add(reader["StaffName"]);
            }
            StaffSupervisor.SelectedIndex = 0;
            reader.Close();

            Login log = new Login(cnn);

            StaffPosition.Items.Clear();
            for(int i=0; i<log.positions.Count; i++)
            {
                StaffPosition.Items.Add(log.positions[i]);
            }
            StaffPosition.SelectedIndex = 0;

            //foreach(Control c in tabControl1.TabPages)
            //{
            //    if (c is TextBox)
            //        c.Text = "";
            //}

            tabControl1.SelectedTab = StaffAccReg;
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
