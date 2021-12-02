using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Azula_Cafe_Database_Management_System
{
    class Login
    {
        private SqlConnection cnn;
        private SqlCommand cmd;
        private SqlDataReader reader;


        //public Login(string connstring)
        //{
        //    cnn = new SqlConnection(connstring);
        //    cnn.Open();
        //}
        public Login(SqlConnection connection)
        {
            cnn = connection;
        }

        public int checkaccount(string accname, string accpass)
        {
            int Staff_or_Cust; //0 for customer, 1 for staff
            string sql = "select * from accounts where username = '" + accname + "' and AccPassword = '" + accpass + "'";
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                int AccNo = Convert.ToInt32(reader["AccountNo"]);

                sql = "select * from Customers where AccountNo = " + AccNo;
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                return (reader.Read()) ? 0 : 1;
            }
            return -1;
        }

        ~Login()
        {
            reader.Close();
            cmd.Dispose();
            cnn.Close();
        }
    }
}
