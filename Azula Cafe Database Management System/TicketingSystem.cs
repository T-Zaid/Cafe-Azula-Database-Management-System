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
        //DateTime currentDateTime = DateTime.Now;
        public TicketingSystem(SqlConnection connection)
        {
            cnn = connection;
        }
        public bool BookSeat(int SeatNo, DateTime Start_Time, DateTime End_Time, int CustomerID)
        {
            if (Start_Time > End_Time || Start_Time < DateTime.Now || End_Time < DateTime.Now)
            {
                MessageBox.Show("Invalid time period entered.");
                return false;
            }

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
                MessageBox.Show("Invalid Seat Number selected.");
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
                MessageBox.Show("Seat Number: " + SeatNo.ToString() + " is not available at the specified time.");
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
    }
}