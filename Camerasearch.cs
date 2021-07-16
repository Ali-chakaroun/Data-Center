using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using System.IO;
using MySql.Data.MySqlClient;

namespace project
{
    public partial class Camerasearch : Form
    {
        public static string ItemID =  "";
        DBconn db = new DBconn();
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        string user;
        public Camerasearch()
        {
            InitializeComponent();
        }
       

        private void Form3_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
               
                
            try 
            {
                comboBox1.Items.Add(Device.Name);
               
            }
            catch {
                MessageBox.Show("no camera found" );

            }
            comboBox1.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            disablecamera();
            Application.Exit();
            
        }
        public void disablecamera() {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
            pictureBox1.Image = null;
        }
        private void pictureBox1_Click(object sender, EventArgs es)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (videoCaptureDevice != null) {
                if (videoCaptureDevice.IsRunning == true)
                    videoCaptureDevice.Stop();
                pictureBox1.Image = null;
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboBox1.SelectedIndex].MonikerString);
                videoCaptureDevice.NewFrame += captureDevise_NewFrame;
                videoCaptureDevice.Start();
                timer1.Start();
            }
            catch {
                MessageBox.Show("no camera found");
            }
        }

        private void captureDevise_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
            
        }

       

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                BarcodeReader br = new BarcodeReader();
                Result res = br.Decode((Bitmap)pictureBox1.Image);
                if (res != null) {
                    textBox1.Text = res.ToString();
                    timer1.Stop();
                    if (videoCaptureDevice.IsRunning == true)
                    {
                        videoCaptureDevice.Stop();
                        string brcode = textBox1.Text;
                        MySqlConnection con = db.DBconnect();
                        con.Open();
                        MySqlCommand cm = db.DBcmd(5, con);
                        cm.Parameters.Add("@barcode", MySqlDbType.VarChar).Value = brcode;
                        MySqlDataReader myreader = cm.ExecuteReader();
                        while (myreader.Read())
                        {

                            BRName.Text = myreader.GetString("barcode_name");

                            ID.Text = myreader.GetString("id");
                            ItemID = ID.ToString();
                            Byte[] img = (Byte[])myreader["barcode_image"];
                            MemoryStream ms = new MemoryStream(img);
                            BRcodePIC.Image = Image.FromStream(ms);

                        }con.Close();
                        //this.Hide();
                        ItemInfo it = new ItemInfo();
                        it.Data(BRName.Text);
                        it.User(user);
                        it.Show();

                    }
                    
                }
              
                
            }
          





        }
        public void User(string x)
        {
            user = x;
        }

        private void Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void BRcodePIC_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
            Search sr = new Search();
            sr.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void Camerasearch_Deactivate(object sender, EventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }

        private void Camerasearch_Leave(object sender, EventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BR_code1_Click(object sender, EventArgs e)
        {

        }

        private void Name1_Click(object sender, EventArgs e)
        {

        }

        private void ID1_Click(object sender, EventArgs e)
        {

        }

        private void BRcodePIC1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            //Mainpage mp = new Mainpage();
            //mp.resettimer();
        }
    }
}
