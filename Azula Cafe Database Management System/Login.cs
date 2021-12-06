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

        public int[] checkaccount(string accname, string accpass)
        {
            int[] Staff_or_Cust = new int[2]; //Index 1: 0 for customer, 1 for staff, Index 2: ID of either customer or staff
            string sql = "select * from accounts where username = '" + accname + "' and AccPassword = '" + accpass + "'";
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                int AccNo = Convert.ToInt32(reader["AccountNo"]);

                sql = "select * from Customers where AccountNo = " + AccNo;
                cmd = new SqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                //return (reader.Read()) ? 0 : 1;
                if(reader.Read())
                {
                    Staff_or_Cust[0] = 0;
                    Staff_or_Cust[1] = Convert.ToInt32(reader["CustomerID"]);
                }
                else
                {
                    sql = "select * from Staff where AccountNo = " + AccNo;
                    cmd = new SqlCommand(sql, cnn);
                    reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        Staff_or_Cust[0] = 1;
                        Staff_or_Cust[1] = Convert.ToInt32(reader["StaffID"]);
                    }
                }
                return Staff_or_Cust;
            }
            Staff_or_Cust[0] = -1;
            return Staff_or_Cust;
        }

        public int CustomerAccountCreate(string Name, string phone, string username, string password)
        {
            string sql = "select * from Accounts where Username = '" + username + "'";
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            int existingUserName = (reader.Read()) ? 1 : 0; //1 if the account with the specific username exists, 0 if not
            if (existingUserName != 1)
            {
                string sql1 = "select max(AccountNo) from Accounts", sql2 = "select max(CustomerID) from Customers";
                cmd = new SqlCommand(sql1, cnn);
                int MaxAcc = Convert.ToInt32(cmd.ExecuteScalar());
                cmd = new SqlCommand(sql2, cnn);
                int MaxCum = Convert.ToInt32(cmd.ExecuteScalar());

                sql1 = "Insert into Accounts (AccountNo, Username, AccPassword) values (" + (MaxAcc + 1) + ", '" + username + "', '" + password + "')";
                sql2 = "Insert into Customers (CustomerID, CustName, PhoneNo, AccountNo) values (" + (MaxCum + 1) + ", '" + Name + "', '" + phone + "', " + (MaxAcc + 1) + ")";
                cmd = new SqlCommand(sql1, cnn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(sql2, cnn);
                cmd.ExecuteNonQuery();
                return 1;

            }
            return 0;
        }

        public int StaffAccountCreate(string Name, string username, string password, string phone, string Position, string salary)
        {
            string sql = "select * from Accounts where Username = '" + username + "'";
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            int existingUserName = (reader.Read()) ? 1 : 0; //1 if the account with the specific username exists, 0 if not
            if (existingUserName != 1)
            {
                string sql1 = "select max(AccountNo) from Accounts", sql2 = "select max(StaffID) from Staff";
                cmd = new SqlCommand(sql1, cnn);
                int MaxAcc = Convert.ToInt32(cmd.ExecuteScalar());
                cmd = new SqlCommand(sql2, cnn);
                int MaxStf = Convert.ToInt32(cmd.ExecuteScalar());

                sql1 = "Insert into Accounts (AccountNo, Username, AccPassword) values (" + (MaxAcc + 1) + ", '" + username + "', '" + password + "')";
                sql2 = "Insert into Staff (StaffID, StaffName, PhoneNo, Salary, AccountNo, Position) values (" + (MaxStf + 1) + ", '" + Name + "', '" + phone + "', " + salary + ", " + (MaxAcc + 1) + ", " + Position + ")";
                cmd = new SqlCommand(sql1, cnn);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(sql2, cnn);
                cmd.ExecuteNonQuery();
                return 1;

            }
            return 0;
        }

        ~Login()
        {
            if(reader.HasRows)  //if it wasn't open, there's no point in closing it...
                reader.Close();
            cmd.Dispose();
            //cnn.Close();
        }
    }
}
