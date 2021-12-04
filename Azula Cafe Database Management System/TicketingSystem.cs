using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Azula_Cafe_Database_Management_System
{
    public class TicketingSystem
    {
        private SqlCommand cmd;
        private SqlDataReader reader;
        private SqlConnection cnn;
        private string errorString;
        //DateTime currentDateTime = DateTime.Now;
        public TicketingSystem(SqlConnection connection)
        {
            cnn = connection;
            errorString = "";
        }
        public bool BookSeat(int SeatNo, DateTime Start_Time, DateTime End_Time, int CustomerID)
        {
            double Amount_Paid;

            string queryString = "SELECT Premium_YES_NO FROM Seats WHERE SeatNo = " + SeatNo.ToString();
            cmd = new SqlCommand(queryString, cnn); // check seat no invalid error exception
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                TimeSpan HoursBooked = End_Time - Start_Time;
                Amount_Paid = HoursBooked.TotalHours * 100;

                bool premiumFlag = Convert.ToBoolean(reader["Premium_YES_NO"]);
                if (premiumFlag)
                    Amount_Paid += 200;
            }
            else
            {
                errorString = "Invalid Seat Number: " + SeatNo.ToString() + " selected.\n";
                reader.Close();
                cmd.Dispose();
                return false;
            }

            reader.Close();
            cmd.Dispose();

            if (CheckSeatAvailability(SeatNo, Start_Time, End_Time))
            {
                queryString = "INSERT INTO Bookings VALUES(" + SeatNo.ToString() + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + Start_Time.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + End_Time.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Amount_Paid.ToString() + ", " + CustomerID.ToString() + ")";
                cmd = new SqlCommand(queryString, cnn);

                int rows = cmd.ExecuteNonQuery();

                MessageBox.Show("Seat Number: " + SeatNo.ToString() + " booked successfully.");
                cmd.Dispose();

                return true;
            }
            else
            {
                errorString = "Seat Number: " + SeatNo.ToString() + " is not available at the specified time.\n";
                return false;
            }
        }

        public bool CheckSeatAvailability(int SeatNo, DateTime Start_Time, DateTime End_Time)
        {
            string cond1 = " (Start_Time > '" + Start_Time.ToString("yyyy-MM-dd HH:mm:ss") + "' AND Start_Time < '" + End_Time.ToString("yyyy-MM-dd HH:mm:ss") + "') ",
                    cond2 = " (Start_Time <= '" + Start_Time.ToString("yyyy-MM-dd HH:mm:ss") + "' AND End_Time > '" + Start_Time.ToString("yyyy-MM-dd HH:mm:ss") + "') ",
                    cond3 = " (Start_Time < '" + End_Time.ToString("yyyy-MM-dd HH:mm:ss") + "' AND End_Time >= '" + End_Time.ToString("yyyy-MM-dd HH:mm:ss") + "') ",
                    conditions = cond1 + " OR " + cond2 + " OR " + cond3;
            string queryString = "SELECT * FROM Bookings WHERE SeatNo = " + SeatNo.ToString() + " AND (" + conditions + ")";
            cmd = new SqlCommand(queryString, cnn);
            reader = cmd.ExecuteReader();

            bool result = reader.Read() ? false : true;

            reader.Close();
            cmd.Dispose();

            return result;
        }

        public void BookMultipleSeats(int numSeats, List<int> SeatNums, DateTime Start_Time, DateTime End_Time, int CustomerID)
        {
            errorString = "";
            if (Start_Time > End_Time || Start_Time < DateTime.Now || End_Time < DateTime.Now)
            {
                MessageBox.Show("Invalid time period entered.");
                return;
            }

            string details = "";
            List<int> notBooked = new List<int>();

            for(int i = 0; i < numSeats; i++)
            {
                if (!BookSeat(SeatNums[i], Start_Time, End_Time, CustomerID))
                {
                    notBooked.Add(SeatNums[i]);
                    details += errorString;
                }
            }

            if(notBooked.Count() > 0)
            {
                MessageBox.Show(notBooked.Count().ToString() + " Seats could not be booked. Reasons for each are:\n\n" + details, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            List<int> receiptSeatNums = new List<int>();
            bool notBookedFlag;

            for(int i = 0; i < SeatNums.Count(); i++)
            {
                notBookedFlag = false;
                for(int j = 0; j < notBooked.Count(); j++)
                {
                    if(SeatNums[i] == notBooked[j])
                        notBookedFlag = true;
                }

                if (!notBookedFlag)
                    receiptSeatNums.Add(SeatNums[i]);
            }

            ReceiptWindow ticketReceipt = new ReceiptWindow(CustomerID, receiptSeatNums.Count(), receiptSeatNums, Start_Time, End_Time, DateTime.Now);
            ticketReceipt.ShowDialog();
        }

        public void CancelTicket(int SeatNo, DateTime Start_Time, DateTime End_Time, int CustomerID)
        {
            if(Start_Time < DateTime.Now || End_Time < DateTime.Now)
            {
                MessageBox.Show("Cannot cancel a booking of which the starting time has passed.");
                return;
            }

            string queryString = "SELECT * FROM Bookings WHERE SeatNo = " + SeatNo.ToString() + " AND Start_Time = '" + Start_Time.ToString("yyyy-MM-dd HH:mm:ss") + "' AND End_Time = '" + End_Time.ToString("yyyy-MM-dd HH:mm:ss") + "' AND CustomerID = " + CustomerID.ToString();
            cmd = new SqlCommand(queryString, cnn);
            reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                MessageBox.Show("The specified booking does not exist.");
                reader.Close();
                cmd.Dispose();
                return;
            }

            reader.Close();
            cmd.Dispose();

            queryString = "DELETE FROM Bookings WHERE SeatNo = " + SeatNo.ToString() + " AND Start_Time = '" + Start_Time.ToString("yyyy-MM-dd HH:mm:ss") + "' AND End_Time = '" + End_Time.ToString("yyyy-MM-dd HH:mm:ss") + "' AND CustomerID = " + CustomerID.ToString();
            cmd = new SqlCommand(queryString, cnn);
            int rowsDeleted = cmd.ExecuteNonQuery();
            cmd.Dispose();

            MessageBox.Show("Booking cancelled successfully.");
        }
    }
}