using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Azula_Cafe_Database_Management_System
{
    public partial class ChangePasswordForm : Form
    {
        private SqlConnection cnn;
        private int customerID_PassForm;

        public ChangePasswordForm(SqlConnection connection, int custID)
        {
            InitializeComponent();
            cnn = connection;
            customerID_PassForm = custID;
        }

        private void UpdatePasswordButton_Click(object sender, EventArgs e)
        {
            if(NewPasswordTextBox.Text == ConfirmPasswordTextbox.Text)
            {
                if(NewPasswordTextBox.Text.Length > 0 && NewPasswordTextBox.Text.Length <= 15)
                {
                    string newQuery = "UPDATE Accounts SET AccPassword = '" + NewPasswordTextBox.Text.ToString() + "' WHERE AccountNo = (SELECT AccountNo FROM Customers WHERE CustomerID = " + customerID_PassForm.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(newQuery, cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    MessageBox.Show("Password updated successfully.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password length must be greater than 0 and less than 15", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Passwords in both fields don't match. Please try again.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
