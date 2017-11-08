using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eu4ProvincePicker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control();

            /*string filePath = @"C:\Users\polo\Documents\Paradox Interactive\Europa Universalis IV\mod\extendedtimeline\map\definition.csv";
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0\";";
            OleDbConnection connection = new OleDbConnection(connectionString);
            string cmdText = "SELECT * FROM [definition$]";
            OleDbCommand command = new OleDbCommand(cmdText, connection);

            command.Connection.Open();
            OleDbDataReader reader = command.ExecuteReader();

            List<string> ids = new List<string>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader[5].ToString() == "x")
                    {
                        ids.Add(reader[0].ToString());
                    }
                }
            }*/

            Color color = Color.FromArgb(16, 140, 192);

            Bitmap bmp = new Bitmap(pictureBox1.Image);

            if (bmp.GetPixel(3001, 2048 - 1361) == color)
            {
                int x = 3001 - pictureBox1.Width / 2; 
                int y = 2048 - 1361 - pictureBox1.Height / 2;
                Rectangle rect = new Rectangle(x, y, pictureBox1.Width, pictureBox1.Height);

                flowLayoutPanel1.Controls[0].
            }
        }

        public void Control()
        {
            PixelPictureBox p1 = new PixelPictureBox();
            p1.Height = 984;
            p1.Width = 741;
            p1.SizeMode = PictureBoxSizeMode.AutoSize;
            p1.BackColor = Color.Black;
            p1.Image = new Bitmap(@"C: \Users\polo\Documents\Paradox Interactive\Europa Universalis IV\mod\extendedtimeline\map\provinces.BMP");

            flowLayoutPanel1.Controls.Add(p1);
        }
    }
}
