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
using System.Globalization;

namespace project
{
    public partial class ItemInfo : Form
    {
        DBconn db = new DBconn();
        Additem ai = new Additem();
        Search sr = new Search();
        Settings st = new Settings();
        List<string> dates = new List<string>();
        PictureBox[] boxes;
        bool dcm = false;
        string user;


        Byte[] img1, img2, img3, img4;
        string oldbrname;
        int q = 0;
        int UpdatedAmount = 0;
        int oldupdatedAmount;
        int oldamount;
        int newamount;
        bool add = true;
        int x = 0;
        public ItemInfo()
        {
            InitializeComponent();
           
            boxes = new PictureBox[] { pictureBox4, pictureBox2, pictureBox3 };

            switchbutton(add);

        }
        public void switchbutton(bool x) {
            if (x) {
                button8.BackColor = Color.Green;
                button7.BackColor = Color.Gray;
                add = true;
                

            }
            else if (!x)
            {
                button7.BackColor = Color.Red;
                button8.BackColor = Color.Gray;
                add = false;
                
            }
        }

        public void Data(string s) {

            textBox1.Text = s;
            oldbrname = s;
            this.Text = s;

            dbsearch();
        }
        int y = 0;
        public void dbsearch() {

            string brname = textBox1.Text;
            MySqlConnection con = db.DBconnect();
            try
            {
                con.Open();



                MySqlCommand cm = db.DBcmd(3, con);
                cm.Parameters.Add("@name", MySqlDbType.String).Value = brname;
                MySqlDataReader myreader = cm.ExecuteReader();
                while (myreader.Read())
                {
                    string m = " ";
                    string k;
                    int o = myreader.GetInt32("CurrentAmount");
                    int p = myreader.GetInt32("UpdatedAmount");
                    
                     k = myreader.GetString("Byuser");
                  
                   
                   
                    if (o < p)
                    {
                        m = "+";
                    }
                    else if (o > p)
                    {
                        m = "-";
                    }
                    else m = "+";

                    string l = myreader.GetString("Date") + " : " + myreader.GetInt32("CurrentAmount").ToString() + " "+ m + " " + myreader.GetInt32("NewAmount").ToString() + " = "+ myreader.GetInt32("UpdatedAmount").ToString()+ ". Price was "+ myreader.GetFloat("Price").ToString() + " Leva  "+" Added by user : "+ k;
                    dates.Add(l);
                  
                  
                    
                    if (y == 0) {
                        
                        textBox5.Text = myreader.GetInt32("ID").ToString();
                        textBox2.Text = myreader.GetString("Date");
                        
                        img1 = (Byte[])myreader["barcode_image"];
                        img2 = (Byte[])myreader["Image1"];
                        img3 = (Byte[])myreader["Image2"];
                        img4 = (Byte[])myreader["Image3"];


                        if (img1 != null && img1.Length > 0)
                        {
                            MemoryStream ms1 = new MemoryStream(img1);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox1.Image = Image.FromStream(ms1);
                        }
                        else
                            pictureBox1.Image = null;
                        if (img2 != null && img2.Length > 0)
                        {
                            MemoryStream ms2 = new MemoryStream(img2);
                            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox4.Image = Image.FromStream(ms2);
                        }
                        else
                            pictureBox4.Image = null;

                        if (img3 != null && img3.Length > 0)
                        {
                            MemoryStream ms3 = new MemoryStream(img3);
                            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox2.Image = Image.FromStream(ms3);

                        }
                        else
                            pictureBox2.Image = null;

                        if (img4 != null && img4.Length > 0)
                        {
                            MemoryStream ms4 = new MemoryStream(img4);
                            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox3.Image = Image.FromStream(ms4);
                        }
                        else
                            pictureBox3.Image = null;

                        y++;
                    }
                    else if (myreader.GetString("Date") != null) {
                        textBox3.Text = myreader.GetString("Date");
                        
                    }
                    textBox7.Text = myreader.GetFloat("Price").ToString();
                    //textBox6.Text = myreader.GetInt32("CurrentAmount").ToString(); 
                    oldupdatedAmount = myreader.GetInt32("UpdatedAmount");
                    


                }
              
                if (oldupdatedAmount < 0)
                {
                   
                    textBox6.ForeColor = Color.Red;
                    textBox6.Text = oldupdatedAmount.ToString();
                }
                else
                {
                    textBox6.ForeColor = Color.White;
                    textBox6.Text = oldupdatedAmount.ToString();
                }
              
                  
               

            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR:" + ex.Message);
            }
            finally
            {
                y = 0;
                
                con.Close();


            }
        }
        public void addorremove() {
            if (textBox6.TextLength > 0)
            {
                oldamount = int.Parse(textBox6.Text);
            }
            else
                oldamount = 0;
           
            if (textBox4.Text  == " " || textBox4.TextLength == 0)
            {
                newamount = 0;
            }
            else try
                {
                    newamount = int.Parse(textBox4.Text);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("invalid amount setting amount to 0");
                    newamount = 0;
                }
            

            if (add)
            {
           
                UpdatedAmount = oldamount + newamount;
            }
            else if (!add)
            {
               
                UpdatedAmount = oldamount - newamount;
            }
        }
        private void UpdateInfo()
        {
          
            string brname = textBox1.Text;
            if (brname.Replace(" ", "") != "" && brname.Length > 0)
            {

                float price;
                if (textBox7.TextLength == 0)
                {
                    price = 0;
                }
                else price = float.Parse(textBox7.Text);


                MySqlConnection con = db.DBconnect();

                Image img1 = pictureBox4.Image;
                Image img2 = pictureBox2.Image;
                Image img3 = pictureBox3.Image;
                byte[] arr1, arr2, arr3;

                ImageConverter converter = new ImageConverter();
                arr1 = (byte[])converter.ConvertTo(img1, typeof(byte[]));
                arr2 = (byte[])converter.ConvertTo(img2, typeof(byte[]));
                arr3 = (byte[])converter.ConvertTo(img3, typeof(byte[]));

                string date = ai.CurrentDate();
                addorremove();
                try
                {
                    con.Open();



                    MySqlCommand cm = db.DBcmd(4, con);
                    cm.Parameters.Add("@name", MySqlDbType.String).Value = oldbrname;
                    cm.Parameters.Add("@newname", MySqlDbType.String).Value = brname;

                    cm.Parameters.Add("@image1", MySqlDbType.LongBlob).Value = arr1;
                    cm.Parameters.Add("@image2", MySqlDbType.LongBlob).Value = arr2;
                    cm.Parameters.Add("@image3", MySqlDbType.LongBlob).Value = arr3;

                    cm.Parameters.Add("@date", MySqlDbType.String).Value = date;
                    cm.Parameters.Add("@Amount1", MySqlDbType.Int32).Value = oldupdatedAmount;
                    cm.Parameters.Add("@Amount2", MySqlDbType.Int32).Value = newamount;
                    cm.Parameters.Add("@Amount3", MySqlDbType.Int32).Value = UpdatedAmount;
                    cm.Parameters.Add("@user", MySqlDbType.VarChar).Value = user;
                    cm.Parameters.Add("@price", MySqlDbType.VarChar).Value = price;
                    int rowsAdded = cm.ExecuteNonQuery();
                    if (rowsAdded > 0)
                    {
                        MessageBox.Show("Item updated!!");

                        sr.update();
                        sr.User(user);
                        oldbrname = brname;

                    }
                    else

                        MessageBox.Show("No item found");


                }
                catch (Exception ex)
                {

                    MessageBox.Show("ERROR:" + ex.Message);
                    textBox1.Text = oldbrname;
                }
                finally
                {

                    con.Close();

                    //this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Name is empty");
            }
           
          
            
            
        
        }
        private void UpdateButton(bool x = false){
            button1.Enabled = x;

            if (!button1.Enabled)
            {
                button1.BackColor = Color.Gray;

            }
            else if (button1.Enabled)
            {
                button1.BackColor = Color.LimeGreen;

            }
        }



        public void DltfromDB() {

            MySqlConnection con = db.DBconnect();

            if (MessageBox.Show("Are you sure you want to delet item","", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    con.Open();

                    MySqlCommand cm = db.DBcmd(6, con);
                    cm.Parameters.Add("@name", MySqlDbType.String).Value = oldbrname;
                    
                    int rowsAdded = cm.ExecuteNonQuery();
                    if (rowsAdded > 0)
                    {
                        MessageBox.Show("Item Deleted");
                       
                        sr.update();
                        sr.User(user);

                    }
                    else

                        MessageBox.Show("Could not delete");


                }
                catch (Exception ex)
                {

                    MessageBox.Show("ERROR:" + ex.Message);
                }
                finally
                {

                    con.Close();
                   
                    FormCollection fc = Application.OpenForms;
                    foreach (Form frm in fc)
                    {
                       
                        //iterate through
                        if (frm.Text == oldbrname)
                        {
                            //frm.Hide();
                            frm.Hide();



                        }
                    }

                }
                
            }

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string x = textBox1.Text;
            if (q != 0) { 
            
            if (x.Trim().CompareTo(oldbrname) == 0 || x.Trim() == "")
            {
               
                UpdateButton(false);
            }
            else if(x.Replace(" ", "") != "" && x.Length > 0)
                {
                UpdateButton(true);
            }
            
            }else
                    UpdateButton(false);
            q++;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dates.Clear();
            UpdateInfo();
            dbsearch();
            textBox4.Clear();
            add = true;
            switchbutton(add);
            sr.User(user);
            this.Controls.Clear();
            this.Controls.Add(panel4);
            this.Controls.Add(panel3);
            this.Controls.Add(panel2);
            this.Controls.Add(panel1);
            //this.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ai.removeimage(pictureBox4);
            UpdateButton(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ai.removeimage(pictureBox2);
            UpdateButton(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ai.removeimage(pictureBox3);
            UpdateButton(true);
        }

        private void button5_Click(object sender, EventArgs e)
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

                    if (x < boxes.Length)
                    {
                        boxes[x].SizeMode = PictureBoxSizeMode.StretchImage;
                        boxes[x].Image = Bitmap.FromFile(file);
                    }
                    x++;

                }
            }
            UpdateButton(true);
            x = 0;
        }

        private void ItemInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_Click(object sender, EventArgs e)
        {
                
            //this.panel4.Controls.Clear();
            Alldates ad = new Alldates(); 
            //{ Dock = DockStyle.Fill, TopLevel = false, TopMost = true, BackgroundImageLayout = ImageLayout.Center };
           // ad.FormBorderStyle = FormBorderStyle.None;
           ad.adddates(dates);
           // this.panel4.Controls.Add(ad);
            ad.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            Passcode pc = new Passcode();
            pc.dltcode(oldbrname);
            pc.dltnamecode(user);
            pc.Show();
           


        }
        public void dltname(string x) {
            oldbrname = x;
            DltfromDB();
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            UpdateButton(true);
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {
            
            switchbutton(true);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            switchbutton(false);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            
          e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) ;
            
        }
        public void User(string x)
        {
            user = x;
        }
        private void ItemInfo_Load(object sender, EventArgs e)
        {
            

            
        }

        private void button6_MouseMove(object sender, MouseEventArgs e)
        {
            //Mainpage mp = new Mainpage();
            //mp.resettimer();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
            if (x != 0)
            {
                UpdateButton(true);
            }
            if (textBox3.TextLength == 0 || !textBox7.Text.Contains("."))
            {
                dcm = false;
             

            }
            else if (textBox7.Text.Contains(".")) {
                dcm = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (!dcm )
            {
                
                    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !(e.KeyChar == '.');
                    if (e.KeyChar == '.')
                    {
                        dcm = true;
                   
                    }

                x++;


            }
            else if (dcm)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
                x++;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            ai.changeimg(pictureBox4);
            UpdateButton(true);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ai.changeimg(pictureBox2);
            UpdateButton(true);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ai.changeimg(pictureBox3);
            UpdateButton(true);
        }
    }
}
