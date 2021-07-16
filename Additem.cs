using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Security;

namespace project
{
    
    public partial class Additem : Form
    {
        DBconn db = new DBconn();
        Settings st = new Settings();
        PictureBox[] boxes;
        Image BRimg;
        string user;
        bool save = false;
        bool dcm = false;
        public Additem()
        {
            InitializeComponent();
            
            boxes = new PictureBox[]{ pictureBox3, pictureBox4, pictureBox5 };
         
           
            
        }

       

        private static Random random = new Random();



        public static string RandomString()
        {


            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }
        string barcode = "";
        private Image pictureBox;

        private void button1_Click(object sender, EventArgs e)
        {
           
           


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Settings st = new Settings();
            st.directory(null);
           
        }

        private void save_Click(object sender, EventArgs e)
        {
           

        }
        private void save_as_Click(object sender, EventArgs e)
        {
            
            barcode = RandomString();
            Zen.Barcode.CodeQrBarcodeDraw QRcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            BRimg = QRcode.Draw(barcode, 50);
            
            SaveImageCapture(BRimg, barcode, textBox2.Text);




        }

        public  void SaveImageCapture(System.Drawing.Image image, String barcd, String Textbox2)
        {

            SaveFileDialog s = new SaveFileDialog();
            s.FileName = Textbox2;// Default file name
            s.DefaultExt = ".Jpg";// Default file extension
            s.Filter = "Image (.jpg;png;bmp)|*.jpg;*.png;*.BMP"; // Filter files by extension
            s.InitialDirectory = st.saveDir();


            // Show save file dialog box
            // Process save file dialog box results
            if (s.ShowDialog() == DialogResult.OK)
            {
                dbAdd();
             
                // Save Image
                if (save) {
                    string filename = s.FileName;
                    FileStream fstream = new FileStream(filename, FileMode.Create);
                    image.Save(fstream, ImageFormat.Jpeg);
                    fstream.Close();
                }
            } 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void User(string x) {
            user = x;
        }
        
        private void dbAdd() {
           
            string Name = textBox2.Text;
            string Name2 = barcode;
            float price;
            if (textBox3.TextLength == 0)
            {
                price = 0;
            }
            else price = float.Parse(textBox3.Text);

            string Date1 = CurrentDate();
            int Amount1 = 0;
            int Amount2 = 0;
            int Amount3 = 0;
            if (textBox1.Text != null && textBox1.TextLength > 0)
            {
                try
                {
                    Amount1 = int.Parse(textBox1.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("invalid amount setting amount to 0");
                }
               
                Amount3 = Amount1;
            }
            else Amount1 = 0;
            MySqlConnection con = db.DBconnect();

            Image img1 = BRimg;
            Image img2 = pictureBox3.Image;
            Image img3 = pictureBox4.Image;
            Image img4 = pictureBox5.Image;
            byte[] arr1, arr2, arr3, arr4;

            ImageConverter converter = new ImageConverter();
            arr1 = (byte[])converter.ConvertTo(img1, typeof(byte[]));
            arr2 = (byte[])converter.ConvertTo(img2, typeof(byte[]));
            arr3 = (byte[])converter.ConvertTo(img3, typeof(byte[]));
            arr4 = (byte[])converter.ConvertTo(img4, typeof(byte[]));
            try
            {
                // Open the connection to the database. 

                con.Open();

                // Prepare the command to be executed on the db

                MySqlCommand cm = db.DBcmd(1, con);

                // Create and set the parameters values 

                cm.Parameters.Add("@idnum", MySqlDbType.Int32).Value = null;
                cm.Parameters.Add("@image", MySqlDbType.Blob).Value = arr1;
                cm.Parameters.Add("@idname", MySqlDbType.VarChar).Value = Name2;
                cm.Parameters.Add("@name", MySqlDbType.VarChar).Value = Name;

                cm.Parameters.Add("@imageID", MySqlDbType.Int32).Value = null;
                cm.Parameters.Add("@ImageIDnum", MySqlDbType.VarChar).Value = Name;
                cm.Parameters.Add("@Image1num", MySqlDbType.LongBlob).Value = arr2;
                cm.Parameters.Add("@Image2num", MySqlDbType.LongBlob).Value = arr3;
                cm.Parameters.Add("@Image3num", MySqlDbType.LongBlob).Value = arr4;

                cm.Parameters.Add("@Date1", MySqlDbType.LongBlob).Value = Date1;
                cm.Parameters.Add("@Amount1", MySqlDbType.Int32).Value = Amount1;
                cm.Parameters.Add("@Amount2", MySqlDbType.Int32).Value = Amount2;
                cm.Parameters.Add("@Amount3", MySqlDbType.Int32).Value = Amount3;
                cm.Parameters.Add("@user", MySqlDbType.VarChar).Value = user; 
                cm.Parameters.Add("@price", MySqlDbType.VarChar).Value = price;
                // Let's ask the db to execute the query
                int rowsAdded = cm.ExecuteNonQuery();
                if (rowsAdded > 0)
                {
                    save = true;
                    Barcodeimage brimg = new Barcodeimage();
                    brimg.BRcodeimg(BRimg);
                    brimg.Show();
                    refresh();

                }
                else
                {
                    MessageBox.Show("No row inserted");
                    save = false;
                }
                  

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR: " + ex.Message);
                save = false;
            }
            finally
            {
                con.Close();

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != null && textBox2.TextLength > 0)
            {
                string l = textBox2.Text;
                
                barcode = RandomString();
                //Zen.Barcode.Code128BarcodeDraw barCode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                //pictureBox1.Image = barCode.Draw(barcode, 50); 
                //pictureBox2.Image = QRcode.Draw(barcode, 50);
                // textBox1.Text = barcode;

                Zen.Barcode.CodeQrBarcodeDraw QRcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                string path = st.saveDir();
                BRimg = QRcode.Draw(barcode, 50);
               
                //pictureBox2.Image.Save(path + @"\" + textBox2.Text + ".jpg", ImageFormat.Jpeg);
                dbAdd();
                if (save)
                {
                    
                    BRimg.Save(path + @"\" + l + ".jpg", ImageFormat.Jpeg);

                }


            }
            else
            {
                MessageBox.Show("Name is empty");
            }
        }

        public string CurrentDate() {
            string date = DateTime.Now.ToShortDateString();
            return date;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainpage fr = new Mainpage();
            fr.Show();
        }
       

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            int x = 0;
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.Title = "Select images";
            o.DefaultExt = ".Jpg";// Default file extension
            o.Filter = "Image (.jpg;png;bmp)|*.jpg;*.png;*.BMP"; // Filter files by extension
            o.InitialDirectory = st.saveDir();

            if (o.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in o.FileNames)
                {
                    string filename = file;

                    if (x < boxes.Length) {
                        boxes[x].SizeMode = PictureBoxSizeMode.StretchImage;
                        boxes[x].Image = Bitmap.FromFile(file);
                    }
                    x++;

                }
            }
            x = 0;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            changeimg(pictureBox3);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            changeimg(pictureBox4);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            changeimg(pictureBox5);
        }

        public void changeimg(PictureBox p) {

            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            o.DefaultExt = ".Jpg";// Default file extension
            o.Filter = "Image (.jpg;png;bmp)|*.jpg;*.png;*.BMP"; // Filter files by extension
            o.InitialDirectory = st.saveDir();

            if (o.ShowDialog() == DialogResult.OK)
            {

                string filename = o.FileName;

                    
                 p.SizeMode = PictureBoxSizeMode.StretchImage;
                      
                 p.Image = Bitmap.FromFile(filename);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            removeimage(pictureBox3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            removeimage(pictureBox4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            removeimage(pictureBox5);
        }
        public void removeimage(PictureBox p) {

            p.Image = null;
        }

        private void button4_Paint(object sender, PaintEventArgs e)
        {
            

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        public void refresh() {
          
            //FormCollection fc = Application.OpenForms;
            //foreach (Form frm in fc)
            //{
            //    //iterate through
            //    if (frm.Name == "Additem")
            //    {
            //        //frm.Hide();
                   
            //        frm.Controls.Clear();
            //        frm.Controls.Add(panel3);
            //        frm.Controls.Add(panel2);
            //        frm.Controls.Add(panel1);
                    

            //    }
            //}
            this.pictureBox3.Image = null;
            this.pictureBox4.Image = null;
            this.pictureBox5.Image = null;
            this.textBox1.Text = "";
            this.textBox2.Text = "";
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            //Mainpage mp = new Mainpage();
            //mp.resettimer();
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox3.TextLength == 0 || !textBox3.Text.Contains("."))
            {
                dcm = false;
              

            }
            else if (textBox3.Text.Contains("."))
            {
                dcm = true;
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = char.IsControl(e.KeyChar);
            if (!dcm)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == '.');
                if (e.KeyChar == '.')
                {
                    dcm = true;
                }
               
            }
            else if (dcm)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
               
            }
                

        }
    }
}



