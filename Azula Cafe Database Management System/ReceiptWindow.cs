using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;

namespace Azula_Cafe_Database_Management_System
{
    public partial class ReceiptWindow : Form
    {
        private int CustomerID, numSeats;
        private List<int> SeatNums;
        private DateTime Start, End;
        private DateTime bookingDate;
        private PrintDocument printDocument1 = new PrintDocument();

        public ReceiptWindow(int custID, int numSeats, List<int> SeatNums, DateTime start, DateTime end, DateTime booking)
        {
            InitializeComponent();
            CustomerID = custID;
            Start = start;
            End = end;
            bookingDate = booking;
            this.numSeats = numSeats;
            this.SeatNums = SeatNums;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }

        Bitmap memoryImage;

        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = new Size(this.Size.Width-20, this.Size.Height-65);
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X+10, this.Location.Y+25, 0, 0, s);
        }

        private void printDocument1_PrintPage(System.Object sender,
           System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            CaptureScreen();
            printDocument1.Print();
        }

        private void ReceiptWindow_Load(object sender, EventArgs e)
        {
            CustID_Label.Text = CustomerID.ToString();
            BookingTime_Label.Text = bookingDate.ToString("yyyy-MM-dd");
            StartTime_Label.Text = Start.ToString("yyyy-MM-dd HH:mm:ss");
            EndTime_Label.Text = End.ToString("yyyy-MM-dd HH:mm:ss");

            for (int i = 0; i < numSeats; i++)
            {
                string[] row = { (i + 1).ToString(), SeatNums[i].ToString() };

                dataGridView1.Rows.Add(row);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
