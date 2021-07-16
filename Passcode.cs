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
    public partial class Passcode : Form
    {
        DBconn db = new DBconn();
        bool allowaccess = false;
        int accesscode;
        int deletecode;
        string user;
        bool delete = false;
        bool deletconfirm = false;
        string dltname1, dltname2;
        string name;
      
        public Passcode()
        {
            InitializeComponent();
            
        }

       
        private void conn() {
            MySqlConnection con = db.DBconnect();

            try
            {

                con.Open();

                MySqlCommand cm = db.DBcmd(8, con);
                cm.Parameters.Add("@code", MySqlDbType.Int32).Value = int.Parse(textBox1.Text);
                cm.Parameters.Add("@name", MySqlDbType.String).Value = name;
                MySqlDataReader myreader = cm.ExecuteReader();
                while (myreader.Read())
                {

                    accesscode = myreader.GetInt32("Acess");
                    deletecode = myreader.GetInt32("Deletecode");
                    user = myreader.GetString("User");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR:" + ex.Message);
            }
            finally
            {
                con.Close();
                
            }



        }
        
        public void dltcode(string x) {

            dltname1 = x;
            name = x;
            delete = true;

        }
        public void dltnamecode(string x)
        {

           name = x;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 4)
            {

                conn();


                if (delete)
                {
                    
                    if (textBox1.Text == deletecode.ToString())
                    {
                        
                        ItemInfo ii = new ItemInfo();
                        ii.User(user);
                        ii.dltname(dltname1);
                       
                        this.Hide();
                      
                    }
                    else
                        textBox1.Text = "";
                }
                else {
                   

                    if (textBox1.Text == accesscode.ToString())
                    {
                        //MessageBox.Show(deletecode.ToString());
                        Mainpage mp = new Mainpage();
                        mp.username(user);
                        mp.Show();
                        this.Hide();
                    }
                    else
                        textBox1.Text = "";
                }
               
            }
                
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (delete)
            {
                this.Hide();
            }else
            Application.Exit();
        }
    }
}
