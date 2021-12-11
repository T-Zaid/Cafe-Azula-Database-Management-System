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
    public partial class CustomerChangeNameForm : Form
    {
        private int customerID_NameForm;
        private SqlConnection cnn;
        private string changeAttribute;

        public CustomerChangeNameForm(SqlConnection connection, int CustID, string Attribute)
        {
            InitializeComponent();
            customerID_NameForm = CustID;
            cnn = connection;
            label1.Text = "Enter new " + Attribute + ":";
            changeAttribute = Attribute;
        }

        private void UpdateNameButton_Click(object sender, EventArgs e)
        {
            if (changeAttribute == "Name")
            {
                if (NewNameTextBox.Text.Length != 0 && NewNameTextBox.Text.Length <= 50)
                {
                    string newQuery = "UPDATE Customers SET CustName = '" + NewNameTextBox.Text.ToString() + "' WHERE CustomerID = " + customerID_NameForm.ToString();
                    SqlCommand cmd = new SqlCommand(newQuery, cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    MessageBox.Show("Profile Name updated successfully.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Name size must be greater than 0 and less than 50.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if(changeAttribute == "Phone Number")
            {
                if (NewNameTextBox.Text.Length == 11)
                {
                    string newQuery = "UPDATE Customers SET PhoneNo = '" + NewNameTextBox.Text.ToString() + "' WHERE CustomerID = " + customerID_NameForm.ToString();
                    SqlCommand cmd = new SqlCommand(newQuery, cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    MessageBox.Show("Phone Number updated successfully.", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Phone Number length can only be 11 numbers long.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void NewNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(changeAttribute == "Phone Number")
            {
                if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
                    e.Handled = true;
            }
        }
    }
}
