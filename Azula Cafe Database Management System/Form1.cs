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
        string connectionString;
        SqlConnection cnn;
        //SqlCommand cmd;
        //SqlDataReader reader;
        public Form1()
        {
            InitializeComponent();
            connectionString = @"Data Source=ZAID-PC\SERWORK;Initial Catalog=AzulaDB;Integrated Security = True;MultipleActiveResultSets=true";
            cnn = new SqlConnection(connectionString);
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
                    tabControl1.SelectedTab = tabPage2;
                else if (flag[0] == 0)
                    MessageBox.Show("Welcome Customer");
                else
                    MessageBox.Show("Invalid Credentials");
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
