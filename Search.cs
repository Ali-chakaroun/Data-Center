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
using System.Threading;

namespace project
{
    public partial class Search : Form
    {
        public static string Itemname = " ";
        DBconn db = new DBconn();
        List<string> imagnames = new List<string>();
        PictureBox pcimg = new PictureBox();
        Byte[] img;
        List<string> dbinfo = new List<string>();
        List<string> result = new List<string>();
        List<int> imagenumber = new List<int>();
        string[] imangenumber2;
        string user;


        public Search()
        {
            InitializeComponent();
            conn();
            

        }
        public void update() {


            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "Search")
                {
                    //frm.Hide();
                    frm.Controls.Clear();
                    dbinfo.Clear();
                    conn();
                    //frm.Controls.Add(listBox1);
                    //frm.Controls.Add(comboBox1);
                    frm.Controls.Add(panel3);
                    frm.Controls.Add(panel2);
                    frm.Controls.Add(panel1);



                }
            }



          
        }


        //public void conn() {
        //    MySqlConnection con = db.DBconnect();

        //    try
        //    {

        //        con.Open();

        //        MySqlCommand cm = db.DBcmd(2, con);

        //        MySqlDataReader myreader = cm.ExecuteReader();
        //        while (myreader.Read())
        //        {
        //            string BRName = myreader.GetString("barcode_name");

        //            string ID = myreader.GetString("id");

        //            string BRinfo = ID + " " + BRName;
        //            dbinfo.Add(BRName);


        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        // We should log the error somewhere, 
        //        MessageBox.Show("ERROR:" + ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //        tableinfo();
        //    }

        //}


        public void tableinfo()
        {
            int y = 0;
            listView1.Items.Clear();
            if (String.IsNullOrEmpty(textBox4.Text))
            {
                foreach (string item in imagnames)
                {

                    //MessageBox.Show(imagnames.Count().ToString());
                     listView1.Items.Add(item, y);
                    

                    y++;
                }
                
                y = 0;
                
            }
        }
        public void User(string x)
        {
            user = x;
        }
        private void conn()
        {
            MySqlConnection con = db.DBconnect();
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(150, 150);
            imagnames.Clear();

            try
            {

                con.Open();

                MySqlCommand cm = db.DBcmd(7, con);

                MySqlDataReader myreader = cm.ExecuteReader();
                while (myreader.Read())
                {


                img = (Byte[])myreader["Image1"];
                //MessageBox.Show(img.Length.ToString());
                if (img.Length == 0)
                {

                    img = (Byte[])myreader["Image2"];
                    // MessageBox.Show(img.Length.ToString());
                    if (img.Length == 0)
                    {

                        img = (Byte[])myreader["Image3"];
                        //MessageBox.Show(img.Length.ToString());
                        if (img.Length == 0)
                        {

                            img = (Byte[])myreader["barcode_image"];
                            //MessageBox.Show(img.Length.ToString());
                            if (img.Length == 0)
                            {
                                return;
                            }
                            else
                            {
                                MemoryStream ms = new MemoryStream(img);
                                pcimg.Image = Image.FromStream(ms);

                                imgs.Images.Add(pcimg.Image);

                            }
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(img);
                            pcimg.Image = Image.FromStream(ms);

                            imgs.Images.Add(pcimg.Image);
                        }
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        pcimg.Image = Image.FromStream(ms);

                        imgs.Images.Add(pcimg.Image);
                    }
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    pcimg.Image = Image.FromStream(ms);
                    imgs.Images.Add(pcimg.Image);
                }

                imagnames.Add(myreader.GetString("barcode_name"));
                  

                }

                listView1.LargeImageList = imgs;

                int y = 0;

                foreach (string item in imagnames)
                {
                    listView1.Items.Add(item, y);
                    y++;
                }
                imangenumber2 = imagnames.Select(x => x.ToString()).ToArray();
                
                y = 0;

        }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR: " + ex.Message);
            }
            finally
            {
                con.Close();
                tableinfo();
}


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
          
            ItemInfo it = new ItemInfo();
            it.Data(Itemname.ToString());
            it.User(user);
            it.Show();
            //listBox1.Items.Clear();
            //listBox1.Visible = false;
           
        }
        
        private void Search_Load(object sender, EventArgs e)
        {
            
        }
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imglist(true);
            this.panel3.Controls.Clear();
           
            Camerasearch fr = new Camerasearch() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, BackgroundImageLayout = ImageLayout.Center };
            fr.FormBorderStyle = FormBorderStyle.None;
            fr.User(user);
            this.panel3.Controls.Add(fr);
        
            fr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainpage mp = new Mainpage();
            mp.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string textToSearch = textBox4.Text.ToLower();
           

            if (String.IsNullOrEmpty(textToSearch))
            {
                tableinfo();
                
            }
            else {
                
                listView1.Items.Clear();
               // MessageBox.Show(imagnames.Count.ToString());
                foreach (string item in imagnames)
                {
                    if (item.ToLower().Contains(textToSearch.ToLower()))
                    {
                        for (int y = 0; y < imangenumber2.Length ; y++)
                        {
                            if (item.ToLower() == (imangenumber2[y].ToLower()) )
                            {
                              
                                    listView1.Items.Add(item, y);
                                  
                            }
                            

                        }
                        //result.Add(item);


                    }



                }
            }
               

           
            

            //if (listView1.Items.Count == 0)
            //{
            //    return; // return with listbox hidden if nothing found
            //}

            //result.Clear();
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            
            string x = listView1.SelectedItems[0].SubItems[0].Text;
            ItemInfo ii = new ItemInfo();
            ii.Data(x);
            ii.User(user);
            ii.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            imglist(false);
            this.panel3.Controls.Clear();
            this.panel3.Controls.Add(listView1);
        }
        public void imglist(bool x = false) {

            button2.Enabled = x;
            button2.Visible = x;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseMove(object sender, MouseEventArgs e)
        {
            //Mainpage mp = new Mainpage();
            //mp.resettimer();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
