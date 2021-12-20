using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Azula_Cafe_Database_Management_System
{
    class CafeFeatures
    {
        private SqlCommand cmd;
        private SqlDataReader reader;
        private SqlConnection cnn;

        public CafeFeatures(SqlConnection connection)
        {
            cnn = connection;
        }

        public int InsertGame(string gamename, string genre, string gamedesc, int popular)
        {
            string sql = "select * from Games where GameName = '" + gamename + "'";
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            int existingUserName = (reader.Read()) ? 1 : 0; //1 if the game with the specific gamename exists, 0 if not

            if (existingUserName != 1)
            {
                string sql1 = "Select max(GameID) from Games";
                cmd = new SqlCommand(sql1, cnn);
                
                int Maxid;
                object obj = cmd.ExecuteScalar();
                if (obj == null || DBNull.Value == obj)
                    Maxid = 0;
                else
                    Maxid = Convert.ToInt32(obj);

                sql = "Insert into Games values (" + (Maxid + 1) + ", '" + gamename + "', '" + genre + "', '" + gamedesc + "', " + popular + ")";
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                return 1;
            }
            return 0;
        }

        public int deleteGame(int gameid)
        {
            string sql = "Delete from Games where GameID = " + gameid;
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public int InsertEvent(string EventName, DateTime start_time, DateTime end_time, string gamename, int max, string imagename)
        {
            if(start_time < DateTime.Now || end_time < DateTime.Now || end_time < start_time)
            {
                return -1;
            }

            string sql1 = "Select gameid from Games where GameName = '" + gamename + "'", sql2 = "select max(EventID) from Events",
                sql3 = "select * from Events where EventName = '" + EventName + "' and Start_Time = '" + start_time.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            cmd = new SqlCommand(sql1, cnn);
            int gid = Convert.ToInt32(cmd.ExecuteScalar());
            cmd = new SqlCommand(sql2, cnn);

            int eid;
            object obj = cmd.ExecuteScalar();
            if(obj == null || DBNull.Value == obj)
                eid = 0;
            else
                eid = Convert.ToInt32(obj);
            
            cmd = new SqlCommand(sql3, cnn);
            reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                string sql = "Insert into Events values (" + (eid + 1) + ", '" + EventName + "', '" + start_time.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + end_time.ToString("yyyy-MM-dd HH:mm:ss") + "', " + gid + ", " + max + ", '" + imagename + "')";
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                return 1;
            }

            return 0;
        }

        public int deleteEvent(int Eventid)
        {
            string sql = "Delete from Events where EventID = " + Eventid;
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return 1;
        }

        public int InsertComputers(string gpu, string cpu, int ram, float netspeed)
        {
            string sql = "select * from Computers where CPU = '" + cpu + "' and GPU = '" + gpu + "' and RAM = " + ram + " and NetSpeed = " + netspeed;
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();

            if(!reader.Read())
            {
                string sql1 = "Select max(ComputerID) from Computers";
                cmd = new SqlCommand(sql1, cnn);
                
                int cid;
                object obj = cmd.ExecuteScalar();
                if (obj == null || DBNull.Value == obj)
                    cid = 0;
                else
                    cid = Convert.ToInt32(obj);

                sql = "Insert into Computers values (" + (cid+1) + ", '" + cpu + "', '" + gpu + "', " + ram + ", " + netspeed + ")";
                cmd = new SqlCommand(sql, cnn);
                cmd.ExecuteNonQuery();
                return 1;
            }
            return 0;
        }

        public int deleteComputer(int computerid)
        {
            string sql = "Delete from Computers where ComputerID = " + computerid;
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public int InsertinLeaderboard(string gamename, int cid, int gamerank)
        {
            string sql1 = "select GameID from Games where GameName = '" + gamename + "'";
            cmd = new SqlCommand(sql1, cnn);
            int gid = Convert.ToInt32(cmd.ExecuteScalar());

            string sql = "Select * from Leaderboard where GameID = " + gid + " and CustomerID = " + cid + " and GameRank = " + gamerank;
            cmd = new SqlCommand(sql, cnn);
            reader = cmd.ExecuteReader();
            int existingRecord = (!reader.Read()) ? 1 : 0;
            reader.Close();
            if(existingRecord == 1)
            {
                sql = "select customerID from Leaderboard where GameID = " + gid + " and CustomerID = " + cid;
                cmd = new SqlCommand(sql, cnn);
                object obj = cmd.ExecuteScalar();
                if(obj == null || DBNull.Value == obj)  //null if there are no rows matching the WHERE clause, DBNull.Value if the first matching row has a NULL value in Col1
                {
                    sql = "Insert into Leaderboard values (" + gid + ", " + cid + ", " + gamerank + ")";
                    cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                else
                {
                    sql = "Update Leaderboard set GameRank = " + gamerank + " where GameID = " + gid + " and CustomerID = " + cid;
                    cmd = new SqlCommand(sql, cnn);
                    cmd.ExecuteNonQuery();
                    return 1;
                }

                //sql = "Insert into Leaderboard values (" + gid + ", " + cid + ", " + gamerank + ")";
                //cmd = new SqlCommand(sql, cnn);
                //cmd.ExecuteNonQuery();
                //return 1;
            }
            return 0;
        }

        public int deletefromLeaderBoard(int gameid, int customerid)
        {
            string sql = "Delete from Leaderboard where GameID = " + gameid + " and CustomerID = " + customerid;
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public int InsertSeat(int ComputerID, int PremiumStatus)
        {
            string sql1 = "Select max(SeatNo) from Seats";
            cmd = new SqlCommand(sql1, cnn);

            int sid;
            object obj = cmd.ExecuteScalar();
            if (obj == null || DBNull.Value == obj)
                sid = 0;
            else
                sid = Convert.ToInt32(obj);

            string sql = "Insert into Seats values ( " + (sid+1) + ", " + 0 + ", " + PremiumStatus + ", " + ComputerID + ")";
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            return 1;
        }

        public int DeleteSeat(int SeatID)
        {
            string sql = "Delete from Seats where SeatNo = " + SeatID;
            cmd = new SqlCommand(sql, cnn);
            cmd.ExecuteNonQuery();
            return 1;
        }
    }
}
