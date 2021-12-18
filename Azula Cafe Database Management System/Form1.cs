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
    public partial class AzulaForm : Form
    {
        string connectionString, umer_connectionString;
        SqlConnection cnn;
        int customerID_Form1, staffID_From1, staff_accessibility;
        List<int> selectedSeats;
        List<string> EventImageLocations;
        List<int> trackid;
        List<int> trackcusid;
        string previousPageFlag;
        bool staffCollapse = true, LeaderCollapse = true, GameCollapse = true, EventCollapse = true, SeatCollapse = true, ComputerCollapse = true;
        //SqlCommand cmd;
        //SqlDataReader reader;
        public AzulaForm()
        {
            InitializeComponent();
            umer_connectionString = @"Data Source=DESKTOP-L0E3C0D\SERWORK;Initial Catalog=AzulaDB;Integrated Security = True;MultipleActiveResultSets=true";
            connectionString = @"Data Source=ZAID-PC\SERWORK;Initial Catalog=AzulaDB;Integrated Security = True;MultipleActiveResultSets=true";
            //cnn = new SqlConnection(connectionString);
            cnn = new SqlConnection(umer_connectionString);
            cnn.Open();
        }

        private void AccountName_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }
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
                    //StaffLabelName.Text = reader["StaffName"].ToString();
                    StaffNameRe.Text = reader["StaffName"].ToString();
                    Staff_Designation.Text = "Designation : " + reader["Position"].ToString();
                    staff_accessibility = log.positions.FindIndex(x => x == (reader["Position"].ToString()));
                    reader.Close();
                    cmd.Dispose();
                    //tabControl1.SelectedTab = StaffPage;

                    if (staff_accessibility == 3)
                    {
                        CreateStaffRe.Enabled = false;
                        CreateLeaderBoardRe.Enabled = false;
                        CreateEventRe.Enabled = false;
                        CreateSeatRe.Enabled = false;
                    }
                    if (staff_accessibility == 4)
                    {
                        CreateLeaderBoardRe.Enabled = false;
                        CreateEventRe.Enabled = false;
                        CreateSeatRe.Enabled = false;
                        CreateGameRe.Enabled = false;
                        CreateComputerRe.Enabled = false;
                    }
                    if (staff_accessibility == 5)
                    {
                        CreateStaffRe.Enabled = false;
                        CreateLeaderBoardRe.Enabled = false;
                        CreateSeatRe.Enabled = false;
                        CreateGameRe.Enabled = false;
                        CreateComputerRe.Enabled = false;
                    }
                    if (staff_accessibility == 6)
                    {
                        CreateStaffRe.Enabled = false;
                        CreateLeaderBoardRe.Enabled = false;
                        CreateEventRe.Enabled = false;
                        CreateGameRe.Enabled = false;
                        CreateComputerRe.Enabled = false;
                    }

                    tabControl1.SelectedTab = StaffPageReloaded;
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
                int success;
                if(StaffSupervisor.SelectedIndex != -1)
                    success = log.StaffAccountCreate(StaffName.Text.ToString(), StaffUsername.Text.ToString(), StaffPassword.Text.ToString(), StaffPhone.Text.ToString(), StaffPosition.Text.ToString(), trackid[StaffSupervisor.SelectedIndex], Convert.ToInt32(StaffSalary.Text));
                else
                    success = log.StaffAccountCreate(StaffName.Text.ToString(), StaffUsername.Text.ToString(), StaffPassword.Text.ToString(), StaffPhone.Text.ToString(), StaffPosition.Text.ToString(), 0, Convert.ToInt32(StaffSalary.Text));
                if (success == 1)
                {
                    MessageBox.Show("Welcome Onboard Captain !!", "Creation Successfull");
                    //tabControl1.SelectedTab = StaffPage;
                    tabControl1.SelectedTab = StaffPageReloaded;
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
                    //tabControl1.SelectedTab = StaffPage;
                    tabControl1.SelectedTab = StaffPageReloaded;
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
            ClearTextBoxes(this.Controls);
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
                    //tabControl1.SelectedTab = StaffPage;
                    tabControl1.SelectedTab = StaffPageReloaded;
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
            ClearTextBoxes(this.Controls);
        }

        private void AddLeaderBoardButton_Click(object sender, EventArgs e)
        {
            CafeFeatures feats = new CafeFeatures(cnn);
            if(Gamedropdown.Text.Length > 0 && CustNameDropDown.Text.Length > 0 && Rank_ig.Text.Length > 0)
            {
                int success = feats.InsertinLeaderboard(Gamedropdown.Text.ToString(), trackcusid[CustNameDropDown.SelectedIndex], Convert.ToInt32(Rank_ig.Text));
                if(success == 1)
                {
                    MessageBox.Show("Congratulations on getting a position !", "LeaderBoard Added", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                    //tabControl1.SelectedTab = StaffPage;
                    tabControl1.SelectedTab = StaffPageReloaded;
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
            ClearTextBoxes(this.Controls);
            string Query = "select GameName from Games", Query1 = "select CustomerID, CustName from Customers";
            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            Gamedropdown.Items.Clear();
            while(reader.Read())
            {
                Gamedropdown.Items.Add(reader["GameName"]);
            }

            trackcusid = new List<int>();
            cmd = new SqlCommand(Query1, cnn);
            reader = cmd.ExecuteReader();
            CustNameDropDown.Items.Clear();
            while(reader.Read())
            {
                trackcusid.Add(Convert.ToInt32(reader["CustomerID"].ToString()));
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
                {
                    MessageBox.Show("Event successfully added to Cafe Azula", "Event Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedTab = StaffPageReloaded;
                }
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
            ClearTextBoxes(this.Controls);
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
            TimeSpan hours = end - start;
            NumHoursDropDown.Items.Clear();
            NumHoursDropDown.Items.Add(hours.TotalHours.ToString());
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

        private void ViewLeaderboardButton_Click(object sender, EventArgs e)
        {
            ViewLeaderboard();
        }

        private void ViewLeaderboard()
        {
            ViewLeaderboardTable.Rows.Clear();
            SearchGameDropDown.Items.Clear();
            SearchNameDropDown.Items.Clear();
            SearchGamerTagDropDown.Items.Clear();
            SearchRankDropDown.Items.Clear();

            for (int i = 0; i < 10; i++)
            {
                SearchRankDropDown.Items.Add((i + 1).ToString());
            }

            string newQuery = "SELECT * FROM Leaderboard_Details ORDER BY Rank asc"; // Leaderboard_Details is a view
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                ViewLeaderboardTable.Rows.Add(row);

                if (!SearchGameDropDown.Items.Contains(reader["Game"].ToString()))
                {
                    SearchGameDropDown.Items.Add(reader["Game"].ToString());
                }

                if (!SearchGamerTagDropDown.Items.Contains(reader["Gamer Tag"].ToString()))
                {
                    SearchGamerTagDropDown.Items.Add(reader["Gamer Tag"].ToString());
                }

                if (!SearchNameDropDown.Items.Contains(reader["Name"].ToString()))
                {
                    SearchNameDropDown.Items.Add(reader["Name"].ToString());
                }
            }

            reader.Close();
            cmd.Dispose();

            tabControl1.SelectedTab = ViewLeaderboardPage;
        }

        private void StaffSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            StaffSupervisorID.Text = "StaffID : " + trackid[StaffSupervisor.SelectedIndex];
        }

        private void CustNameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "Select Username from Accounts, Customers where Accounts.AccountNo = Customers.AccountNo and Customers.CustomerID = " + trackcusid[CustNameDropDown.SelectedIndex];
            SqlCommand cmd = new SqlCommand(sql, cnn);
            ShowCustGamerTag.Text = "Gamer Tag : " + cmd.ExecuteScalar().ToString();
        }

        private void FromRegStaffTOStaffPage_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedTab = StaffPage;
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromAddGameTOStaffPage_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedTab = StaffPage;
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromAddCompsTOStaffPage_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedTab = StaffPage;
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromaddLeaderBoardTOStaffPage_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedTab = StaffPage;
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromaddEventTOStaffPage_Click(object sender, EventArgs e)
        {
            //tabControl1.SelectedTab = StaffPage;
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void ViewLeaderboardBackButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = CustomerPage;
        }

        private void SearchGameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SearchGameDropDown.SelectedIndex != -1)
            {
                ViewLeaderboardTable.Rows.Clear();
                SearchGamerTagDropDown.SelectedIndex = -1;
                SearchNameDropDown.SelectedIndex = -1;
                SearchRankDropDown.SelectedIndex = -1;

                string newQuery = "SELECT * FROM Leaderboard_Details WHERE Game like '" + SearchGameDropDown.SelectedItem.ToString() + "%' ORDER BY Rank asc";
                SqlCommand cmd = new SqlCommand(newQuery, cnn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                    ViewLeaderboardTable.Rows.Add(row);
                }

                reader.Close();
                cmd.Dispose();
            }
        }

        private void SearchRankDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SearchRankDropDown.SelectedIndex != -1)
            {
                ViewLeaderboardTable.Rows.Clear();
                SearchGamerTagDropDown.SelectedIndex = -1;
                SearchNameDropDown.SelectedIndex = -1;
                SearchGameDropDown.SelectedIndex = -1;

                string newQuery = "SELECT * FROM Leaderboard_Details WHERE Rank = " + SearchRankDropDown.SelectedItem.ToString() + " ORDER BY Rank asc";
                SqlCommand cmd = new SqlCommand(newQuery, cnn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                    ViewLeaderboardTable.Rows.Add(row);
                }

                reader.Close();
                cmd.Dispose();
            }
        }

        private void SearchGamerTagDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SearchGamerTagDropDown.SelectedIndex != -1)
            {
                ViewLeaderboardTable.Rows.Clear();
                SearchGameDropDown.SelectedIndex = -1;
                SearchNameDropDown.SelectedIndex = -1;
                SearchRankDropDown.SelectedIndex = -1;

                string newQuery = "SELECT * FROM Leaderboard_Details WHERE \"Gamer Tag\" like '" + SearchGamerTagDropDown.SelectedItem.ToString() + "%' ORDER BY Rank asc";
                SqlCommand cmd = new SqlCommand(newQuery, cnn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                    ViewLeaderboardTable.Rows.Add(row);
                }

                reader.Close();
                cmd.Dispose();
            }
        }

        private void SearchNameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SearchNameDropDown.SelectedIndex != -1)
            {
                ViewLeaderboardTable.Rows.Clear();
                SearchGameDropDown.SelectedIndex = -1;
                SearchGamerTagDropDown.SelectedIndex = -1;
                SearchRankDropDown.SelectedIndex = -1;

                string newQuery = "SELECT * FROM Leaderboard_Details WHERE Name like '" + SearchNameDropDown.SelectedItem.ToString() + "%'  ORDER BY Rank asc";
                SqlCommand cmd = new SqlCommand(newQuery, cnn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                    ViewLeaderboardTable.Rows.Add(row);
                }

                reader.Close();
                cmd.Dispose();
            }
        }

        private void LeaderboardResetButton_Click(object sender, EventArgs e)
        {
            ViewLeaderboard();
        }

        private void MyRanksButton_Click(object sender, EventArgs e)
        {
            ViewLeaderboardTable.Rows.Clear();
            SearchGameDropDown.SelectedIndex = -1;
            SearchGamerTagDropDown.SelectedIndex = -1;
            SearchRankDropDown.SelectedIndex = -1;
            SearchNameDropDown.SelectedIndex = -1;

            string newQuery = "SELECT * FROM Leaderboard_Details WHERE \"Gamer Tag\" = (SELECT CP.Username FROM Customer_Profile AS CP WHERE CP.CustomerID = " + customerID_Form1.ToString() + ") ORDER BY Rank asc";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                ViewLeaderboardTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();
        }

        private void CustomerProfileButton_Click(object sender, EventArgs e)
        {
            CustomerProfile();
        }

        private void CustomerProfile()
        {
            string newQuery = "SELECT * FROM Customer_Profile WHERE CustomerID = " + customerID_Form1.ToString();
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            ProfileNameLabel.Text = reader["CustName"].ToString();
            ProfilePhoneNumberLabel.Text = reader["PhoneNo"].ToString();
            ProfileUsernameLabel.Text = reader["Username"].ToString();

            reader.Close();
            cmd.Dispose();

            tabControl1.SelectedTab = ViewProfilePage;
        }

        private void EditNameButton_Click(object sender, EventArgs e)
        {
            CustomerChangeNameForm changeName = new CustomerChangeNameForm(cnn, customerID_Form1, "Name");
            changeName.ShowDialog();

            string newQuery = "SELECT CustName FROM Customers WHERE CustomerID = " + customerID_Form1.ToString();
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            CustomerNameLabel.Text = reader["CustName"].ToString();

            reader.Close();
            cmd.Dispose();

            CustomerProfile();
        }

        private void CreateStaffRe_Click(object sender, EventArgs e)
        {
            StaffDropTime.Start();
        }

        private void LeaderDropTIme_Tick(object sender, EventArgs e)
        {
            if (LeaderCollapse)
            {
                LeaderBoardPanel.Height += 20;
                if (LeaderBoardPanel.Size == LeaderBoardPanel.MaximumSize)
                {
                    LeaderDropTIme.Stop();
                    LeaderCollapse = false;
                }
            }
            else
            {
                LeaderBoardPanel.Height -= 20;
                if (LeaderBoardPanel.Size == LeaderBoardPanel.MinimumSize)
                {
                    LeaderDropTIme.Stop();
                    LeaderCollapse = true;
                }
            }
        }

        private void GameDropTime_Tick(object sender, EventArgs e)
        {
            if (GameCollapse)
            {
                GamePanel.Height += 20;
                if (GamePanel.Size == StaffPanel.MaximumSize)
                {
                    GameDropTime.Stop();
                    GameCollapse = false;
                }
            }
            else
            {
                GamePanel.Height -= 20;
                if (GamePanel.Size == GamePanel.MinimumSize)
                {
                    GameDropTime.Stop();
                    GameCollapse = true;
                }
            }
        }

        private void ComputerDropTime_Tick(object sender, EventArgs e)
        {
            if (ComputerCollapse)
            {
                ComputerPanel.Height += 20;
                if (ComputerPanel.Size == ComputerPanel.MaximumSize)
                {
                    ComputerDropTime.Stop();
                    ComputerCollapse = false;
                }
            }
            else
            {
                ComputerPanel.Height -= 20;
                if (ComputerPanel.Size == ComputerPanel.MinimumSize)
                {
                    ComputerDropTime.Stop();
                    ComputerCollapse = true;
                }
            }
        }

        private void SeatDropTime_Tick(object sender, EventArgs e)
        {
            if (SeatCollapse)
            {
                SeatPanel.Height += 20;
                if (SeatPanel.Size == SeatPanel.MaximumSize)
                {
                    SeatDropTime.Stop();
                    SeatCollapse = false;
                }
            }
            else
            {
                SeatPanel.Height -= 20;
                if (SeatPanel.Size == SeatPanel.MinimumSize)
                {
                    SeatDropTime.Stop();
                    SeatCollapse = true;
                }
            }
        }

        private void EventDropTimer_Tick(object sender, EventArgs e)
        {
            if (EventCollapse)
            {
                EventPanel.Height += 20;
                if (EventPanel.Size == EventPanel.MaximumSize)
                {
                    EventDropTimer.Stop();
                    EventCollapse = false;
                }
            }
            else
            {
                EventPanel.Height -= 20;
                if (EventPanel.Size == EventPanel.MinimumSize)
                {
                    EventDropTimer.Stop();
                    EventCollapse = true;
                }
            }
        }

        private void CreateLeaderBoardRe_Click(object sender, EventArgs e)
        {
            LeaderDropTIme.Start();
        }

        private void CreateGameRe_Click(object sender, EventArgs e)
        {
            GameDropTime.Start();
        }

        private void CreateComputerRe_Click(object sender, EventArgs e)
        {
            ComputerDropTime.Start();
        }

        private void CreateEventRe_Click(object sender, EventArgs e)
        {
            EventDropTimer.Start();
        }

        private void CreateSeatRe_Click(object sender, EventArgs e)
        {
            SeatDropTime.Start();
        }

        private void CreateStaffAdd_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
            string Query = "select StaffID, StaffName from Staff where Position in ('Chairman', 'CEO', 'Manager')";
            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Dispose();

            trackid = new List<int>();
            StaffSupervisor.Items.Clear();
            while (reader.Read())
            {
                trackid.Add(Convert.ToInt32(reader["StaffID"].ToString()));
                StaffSupervisor.Items.Add(reader["StaffName"]);
            }
            StaffSupervisor.SelectedIndex = -1;
            reader.Close();

            Login log = new Login(cnn);

            StaffPosition.Items.Clear();
            for (int i = 0; i < log.positions.Count; i++)
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

        private void CreateLeaderBoardAdd_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
            string Query = "select GameName from Games", Query1 = "select CustomerID, CustName from Customers";
            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            Gamedropdown.Items.Clear();
            while (reader.Read())
            {
                Gamedropdown.Items.Add(reader["GameName"]);
            }

            trackcusid = new List<int>();
            cmd = new SqlCommand(Query1, cnn);
            reader = cmd.ExecuteReader();
            CustNameDropDown.Items.Clear();
            while (reader.Read())
            {
                trackcusid.Add(Convert.ToInt32(reader["CustomerID"].ToString()));
                CustNameDropDown.Items.Add(reader["CustName"]);
            }

            reader.Close();
            tabControl1.SelectedTab = LeaderBoard_Add;
        }

        private void CreateGameAdd_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Game_Add;
            ClearTextBoxes(this.Controls);
        }

        private void CreateComputerAdd_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = Computer_Add;
            ClearTextBoxes(this.Controls);
        }

        private void CreateEventAdd_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateSeatAdd_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
            string Query = "select ComputerID from Computers";
            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            ComputerDropDownSeats.Items.Clear();
            while (reader.Read())
            {
                ComputerDropDownSeats.Items.Add(reader["ComputerID"]);
            }
            tabControl1.SelectedTab = Seats_Add;
            ComputerDropDownSeats.SelectedIndex = 0;
            PremiumStatusSeats.SelectedIndex = 0;
        }

        private void ComputerDropDownSeats_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select CPU, GPU, RAM, NetSpeed from Computers where ComputerID = " + Convert.ToInt32(ComputerDropDownSeats.Text.ToString());
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            InstalledCompInfo.Text = "CPU : " + reader["CPU"].ToString() + "\nGPU: " + reader["GPU"].ToString() + "\nRam : " + reader["RAM"].ToString() + "\nNet Speed: " + reader["NetSpeed"].ToString();
        }

        private void AddSeatsButton_Click(object sender, EventArgs e)
        {
            CafeFeatures feats = new CafeFeatures(cnn);
            int success = feats.InsertSeat(Convert.ToInt32(ComputerDropDownSeats.Text.ToString()), Convert.ToInt32(PremiumStatusSeats.Text.ToString()));
            if (success == 1)
            {
                MessageBox.Show("New Seat Added to Cafe Azula", "Seat Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.SelectedTab = StaffPageReloaded;
            }
        }

        private void CreateComputerDelete_Click(object sender, EventArgs e)
        {
            string sql = "select * from Computers";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            ComputerDeleteTable.Rows.Clear();

            while(reader.Read())
            {
                string[] row = { reader["ComputerID"].ToString(), reader["CPU"].ToString(), reader["GPU"].ToString(), reader["RAM"].ToString(), reader["NetSpeed"].ToString() };
                ComputerDeleteTable.Rows.Add(row);
            }
            reader.Close();
            cmd.Dispose();
            tabControl1.SelectedTab = Computer_Delete;
        }

        private void DeleteComputerRow_Click(object sender, EventArgs e)
        {
            if (ComputerDeleteTable.RowCount != 0)
            {
                var yesNo = MessageBox.Show("Are you sure you want to permanently delete this Computer from Cafe Azula Database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (yesNo == DialogResult.Yes)
                {
                    CafeFeatures feats = new CafeFeatures(cnn);
                    int success = feats.deleteComputer(Convert.ToInt32(CancelBookingTable.CurrentRow.Cells[0].Value.ToString()));

                    if (success == 1)
                    {
                        MessageBox.Show("Computer successfully Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //tabControl1.SelectedTab = StaffPageReloaded;    //A workaround to get the performClick function working
                        //CreateComputerDelete.PerformClick();
                        CreateComputerDelete_Click(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void CreateGameDelete_Click(object sender, EventArgs e)
        {
            string sql = "select * from Games";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            GameDeleteTable.Rows.Clear();

            while (reader.Read())
            {
                string[] row = { reader["GameName"].ToString(), reader["Genre"].ToString(), reader["GameDescription"].ToString(), reader["Popularity"].ToString() };
                GameDeleteTable.Rows.Add(row);
            }
            reader.Close();
            cmd.Dispose();
            tabControl1.SelectedTab = Game_Delete;
        }

        private void DeleteGameRow_Click(object sender, EventArgs e)
        {
            if (GameDeleteTable.RowCount != 0)
            {
                var yesNo = MessageBox.Show("Are you sure you want to permanently delete this Game from Cafe Azula Database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (yesNo == DialogResult.Yes)
                {
                    CafeFeatures feats = new CafeFeatures(cnn);
                    string newQuery = "Select GameID FROM Games WHERE GameName = '" + GameDeleteTable.CurrentRow.Cells[0].Value.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(newQuery, cnn);
                    int success = feats.deleteGame(Convert.ToInt32(cmd.ExecuteScalar()));
                    if (success == 1)
                    {
                        MessageBox.Show("Game successfully Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //tabControl1.SelectedTab = StaffPageReloaded;    //A workaround to get the performClick function working
                        //CreateComputerDelete.PerformClick();
                        CreateGameDelete_Click(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void CreateSeatDelete_Click(object sender, EventArgs e)
        {
            string sql = "Select * from Seats left join Computers on Seats.ComputerID = Computers.ComputerID";
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            SeatDeleteTable.Rows.Clear();
            while(reader.Read())
            {
                if (reader["ComputerID"].ToString().Length > 0)
                {
                    string[] row = {reader["Seatno"].ToString(), reader["CPU"].ToString(), reader["GPU"].ToString(), reader["RAM"].ToString(), reader["NetSpeed"].ToString(), reader["Premium_YES_NO"].ToString()};
                    SeatDeleteTable.Rows.Add(row);
                }
                else
                {
                    string[] row = { reader["Seatno"].ToString(), "NULL", "NULL", "NULL", "NULL", reader["Premium_YES_NO"].ToString() };
                    SeatDeleteTable.Rows.Add(row);
                }
            }
            reader.Close();
            cmd.Dispose();
            tabControl1.SelectedTab = Seats_Delete;
        }

        private void DeleteSeatRow_Click(object sender, EventArgs e)
        {
            if (SeatDeleteTable.RowCount != 0)
            {
                var yesNo = MessageBox.Show("Are you sure you want to permanently delete this Seat from Cafe Azula Database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (yesNo == DialogResult.Yes)
                {
                    CafeFeatures feats = new CafeFeatures(cnn);
                    int success = feats.DeleteSeat(Convert.ToInt32(SeatDeleteTable.CurrentRow.Cells[0].Value.ToString()));

                    if (success == 1)
                    {
                        MessageBox.Show("Seat successfully Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //tabControl1.SelectedTab = StaffPageReloaded;    //A workaround to get the performClick function working
                        //CreateComputerDelete.PerformClick();
                        CreateSeatDelete_Click(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void CreateLeaderBoardDelete_Click(object sender, EventArgs e)
        {
            DeleteGameDropDown.Items.Clear();
            DeleteGamerTagDropDown.Items.Clear();
            LeaderBoardDeleteTable.Rows.Clear();

            string sql = "SELECT * FROM Leaderboard_Details ORDER BY Rank asc"; // Leaderboard_Details is a view
            SqlCommand cmd = new SqlCommand(sql, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                LeaderBoardDeleteTable.Rows.Add(row);

                if (!DeleteGameDropDown.Items.Contains(reader["Game"].ToString()))
                {
                    DeleteGameDropDown.Items.Add(reader["Game"].ToString());
                }

                if (!DeleteGamerTagDropDown.Items.Contains(reader["Gamer Tag"].ToString()))
                {
                    DeleteGamerTagDropDown.Items.Add(reader["Gamer Tag"].ToString());
                }
            }

            reader.Close();
            cmd.Dispose();

            tabControl1.SelectedTab = LeaderBoard_Delete;
        }

        private void DeleteGameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DeleteGameDropDown.SelectedIndex != -1)
            {
                LeaderBoardDeleteTable.Rows.Clear();
                DeleteGamerTagDropDown.SelectedIndex = -1;

                string newQuery = "SELECT * FROM Leaderboard_Details WHERE Game like '" + DeleteGameDropDown.SelectedItem.ToString() + "%' ORDER BY Rank asc";
                SqlCommand cmd = new SqlCommand(newQuery, cnn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                    LeaderBoardDeleteTable.Rows.Add(row);
                }

                reader.Close();
                cmd.Dispose();
            }
        }

        private void DeleteGamerTagDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DeleteGamerTagDropDown.SelectedIndex != -1)
            {
                LeaderBoardDeleteTable.Rows.Clear();
                DeleteGameDropDown.SelectedIndex = -1;

                string newQuery = "SELECT * FROM Leaderboard_Details WHERE \"Gamer Tag\" like '" + DeleteGamerTagDropDown.SelectedItem.ToString() + "%' ORDER BY Rank asc";
                SqlCommand cmd = new SqlCommand(newQuery, cnn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string[] row = { reader["Rank"].ToString(), reader["Game"].ToString(), reader["Gamer Tag"].ToString(), reader["Name"].ToString() };
                    LeaderBoardDeleteTable.Rows.Add(row);
                }

                reader.Close();
                cmd.Dispose();
            }
        }

        private void DeleteLeaderRow_Click(object sender, EventArgs e)
        {
            if (LeaderBoardDeleteTable.RowCount != 0)
            {
                var yesNo = MessageBox.Show("Are you sure you want to permanently delete this Record from Cafe Azula LeaderBoard?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (yesNo == DialogResult.Yes)
                {
                    CafeFeatures feats = new CafeFeatures(cnn);
                    string newQuery = "Select GameID FROM Games WHERE GameName = '" + LeaderBoardDeleteTable.CurrentRow.Cells[1].Value.ToString() + "'";
                    String newQuery2 = "Select Customers.CustomerID from Accounts, Customers where Accounts.AccountNo = Customers.AccountNo and Accounts.Username = '" + LeaderBoardDeleteTable.CurrentRow.Cells[2].Value.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(newQuery, cnn);
                    SqlCommand cmd2 = new SqlCommand(newQuery2, cnn);
                    int success = feats.deletefromLeaderBoard(Convert.ToInt32(cmd.ExecuteScalar()), Convert.ToInt32(cmd2.ExecuteScalar()));
                    if (success == 1)
                    {
                        MessageBox.Show("LeaderBoard Record successfully Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //tabControl1.SelectedTab = StaffPageReloaded;    //A workaround to get the performClick function working
                        //CreateComputerDelete.PerformClick();
                        CreateLeaderBoardDelete_Click(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void CreateEventDelete_Click(object sender, EventArgs e)
        {
            DeleteEventTable.Rows.Clear();
            string newQuery = "SELECT * FROM Events AS E LEFT OUTER JOIN Games AS G ON E.GameID = G.GameID WHERE Start_Time > '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string[] row = { reader["EventID"].ToString(), reader["EventName"].ToString(), reader["GameName"].ToString(), reader["Start_Time"].ToString(), reader["End_Time"].ToString(), reader["Max_Participants"].ToString() };
                DeleteEventTable.Rows.Add(row);
            }

            reader.Close();
            cmd.Dispose();

            tabControl1.SelectedTab = Event_Delete;
        }

        private void DeleteEventRow_Click(object sender, EventArgs e)
        {
            if (DeleteEventTable.RowCount != 0)
            {
                var yesNo = MessageBox.Show("Are you sure you want to permanently delete this Event from Cafe Azula Events?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (yesNo == DialogResult.Yes)
                {
                    CafeFeatures feats = new CafeFeatures(cnn);
                    int success = feats.deleteEvent(Convert.ToInt32(DeleteEventTable.CurrentRow.Cells[0].Value.ToString()));
                    if (success == 1)
                    {
                        MessageBox.Show("Event successfully Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CreateEventDelete_Click(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void CreateStaffDelete_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
            string sql = "select Accounts.Username, Staff.StaffName, Staff.Salary, Staff.Position from Accounts, Staff where Accounts.AccountNo = Staff.AccountNo";
            SqlDataAdapter adapt = new SqlDataAdapter(sql, cnn);
            DataTable data = new DataTable();
            adapt.Fill(data);
            AccountTableView.DataSource = data;

            tabControl1.SelectedTab = Account_Delete;
        }

        private void TableSwitchButton_Click(object sender, EventArgs e)
        {
            if(TableSwitchButton.Text == "Show for Customer")
            {
                string sql = "select Accounts.Username, Customers.CustName, Customers.PhoneNo from Accounts, Customers where Accounts.AccountNo = Customers.AccountNo";
                SqlDataAdapter adapt = new SqlDataAdapter(sql, cnn);
                DataTable data = new DataTable();
                adapt.Fill(data);
                AccountTableView.DataSource = data;
                TableSwitchButton.Text = "Show for Staff";
            }
            else
            {
                string sql = "select Accounts.Username, Staff.StaffName, Staff.Salary, Staff.Position from Accounts, Staff where Accounts.AccountNo = Staff.AccountNo";
                SqlDataAdapter adapt = new SqlDataAdapter(sql, cnn);
                DataTable data = new DataTable();
                adapt.Fill(data);
                AccountTableView.DataSource = data;
                TableSwitchButton.Text = "Show for Customer";
            }
        }

        private void SearchAccName_TextChanged(object sender, EventArgs e)
        {
            if (TableSwitchButton.Text == "Show for Customer")
            {
                string sql = "select Accounts.Username, Staff.StaffName, Staff.Salary, Staff.Position from Accounts, Staff where Accounts.AccountNo = Staff.AccountNo and Accounts.Username like '" + SearchAccName.Text.ToString() + "%'";
                SqlDataAdapter adapt = new SqlDataAdapter(sql, cnn);
                DataTable data = new DataTable();
                adapt.Fill(data);
                AccountTableView.DataSource = data;
            }
            else
            {
                string sql = "select Accounts.Username, Customers.CustName, Customers.PhoneNo from Accounts, Customers where Accounts.AccountNo = Customers.AccountNo and Accounts.Username like '" + SearchAccName.Text.ToString() + "%'";
                SqlDataAdapter adapt = new SqlDataAdapter(sql, cnn);
                DataTable data = new DataTable();
                adapt.Fill(data);
                AccountTableView.DataSource = data;
            }
        }

        private void DeleteSelectedAccount_Click(object sender, EventArgs e)
        {
            if (AccountTableView.RowCount != 0)
            {
                var yesNo = MessageBox.Show("Are you sure you want to permanently delete this Account from Cafe Azula Database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (yesNo == DialogResult.Yes)
                {
                    string sql = "Select AccountNo from Accounts where Username = '" + AccountTableView.CurrentRow.Cells[0].Value.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(sql, cnn);
                    int Accid = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();

                    Login log = new Login(cnn);
                    int success = log.deleteAccount(Accid);

                    if (success == 1)
                    {
                        MessageBox.Show("Account successfully Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CreateStaffDelete_Click(this, EventArgs.Empty);
                    }
                }
            }
        }

        private void FromSeatDeleteTOStaffPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromAddSeatTOStaffPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromDeleteComputerTOStaffPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromDeleteGameTOStaffPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromDeleteLeaderBoardTOStaffPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromDeleteEventTOStaffPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void FromDeleteAccountTOStaffPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = StaffPageReloaded;
        }

        private void Rank_ig_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void Staff_Logout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Staff_Profile_Click(object sender, EventArgs e)
        {
            string newQuery = "SELECT Staff.StaffName, Staff.PhoneNo, Staff.Position, Staff.Salary, Accounts.Username from Staff, Accounts where Staff.AccountNo = Accounts.AccountNo and Staff.StaffID = " + staffID_From1;
            SqlCommand cmd = new SqlCommand(newQuery, cnn);
            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            StaffNameProfile.Text = reader["StaffName"].ToString();
            StaffPhoneProfile.Text = reader["PhoneNo"].ToString();
            PositionProfile.Text = reader["Position"].ToString();
            StaffProfile_Username.Text = reader["Username"].ToString();
            StaffProfile_Salary.Text = reader["Salary"].ToString();

            reader.Close();
            cmd.Dispose();

            if(staff_accessibility > 2)
            {
                supervisingStatus.Hide();
                SupervisedTable.Hide();
            }
            else
            {
                string sql = "Select StaffName, Position from Staff where Supervisor_ID in ( select StaffID from Staff where StaffID = " + staffID_From1 + " )";
                SqlCommand cmd1 = new SqlCommand(sql, cnn);
                SqlDataReader reader1 = cmd1.ExecuteReader();

                while(reader1.Read())
                {
                    string[] row = { reader1["StaffName"].ToString(), reader1["Position"].ToString() };
                    SupervisedTable.Rows.Add(row);
                }
            }

            reader.Close();
            cmd.Dispose();

            tabControl1.SelectedTab = StaffProfile;
        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void ComputerInfoBookingLabel_Click(object sender, EventArgs e)
        {

        }

        private void ProfilePageBackButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = CustomerPage;
        }

        private void EditPhoneNumberButton_Click(object sender, EventArgs e)
        {
            CustomerChangeNameForm changePhone = new CustomerChangeNameForm(cnn, customerID_Form1, "Phone Number");
            changePhone.ShowDialog();
            CustomerProfile();
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePassword = new ChangePasswordForm(cnn, customerID_Form1);
            changePassword.ShowDialog();
            CustomerProfile();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void StaffDropTime_Tick(object sender, EventArgs e)
        {
            if(staffCollapse)
            {
                StaffPanel.Height += 20;
                if(StaffPanel.Size == StaffPanel.MaximumSize)
                {
                    StaffDropTime.Stop();
                    staffCollapse = false;
                }
            }
            else
            {
                StaffPanel.Height -= 20;
                if(StaffPanel.Size == StaffPanel.MinimumSize)
                {
                    StaffDropTime.Stop();
                    staffCollapse = true;
                }
            }
        }

        private void StaffCreateAccount_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
            string Query = "select StaffID, StaffName from Staff where Position in ('Chairman', 'CEO', 'Manager')";
            SqlCommand cmd = new SqlCommand(Query, cnn);
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Dispose();

            trackid = new List<int> ();
            StaffSupervisor.Items.Clear();
            while(reader.Read())
            {
                trackid.Add(Convert.ToInt32(reader["StaffID"].ToString()));
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
