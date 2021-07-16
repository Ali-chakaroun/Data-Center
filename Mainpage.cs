using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Mainpage : Form
    {
        Settings st = new Settings();
        public static Timer timer1 = new Timer();
        bool idle = false;
        string userName;
        int mouseX ;
        int mouseY ;
        public Mainpage()
        {
            InitializeComponent();
          

          
            //labelchg("Home");

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //do whatever you want 
            timer1.Interval = 1200000;//5 seconds
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Stop();
            
            Application.Restart();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            
            st.directory(null);
            timer1.Interval = 1200000;//5 seconds
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            
        }
        public void username(string x) {
            userName = x;
            label3.Text = userName;
        }
       


        public void button2_Click(object sender, EventArgs e)
        {
           
            //this.Hide();
            this.panel3.Controls.Clear();
            this.panel4.Controls.Clear();
            label1.Text = "Add item";
            label3.Text = userName;
            Additem fr = new Additem() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, BackgroundImageLayout = ImageLayout.Center };
            fr.FormBorderStyle = FormBorderStyle.None;
            fr.User(userName);
            this.panel3.Controls.Add(label3);
            this.panel3.Controls.Add(button4);
            this.panel3.Controls.Add(label1);
            this.panel4.Controls.Add(fr);
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            this.panel3.Controls.Clear();
           this.panel4.Controls.Clear();
            label1.Text = "Search";
            label3.Text = userName;
            Search fr = new Search() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, BackgroundImageLayout = ImageLayout.Stretch };
            fr.FormBorderStyle = FormBorderStyle.None;
            fr.User(userName);
            this.panel3.Controls.Add(label3);
            this.panel3.Controls.Add(button4);
            this.panel3.Controls.Add(label1);
            this.panel4.Controls.Add(fr);
            fr.Show();
            

        }
      
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
           Application.Exit();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
          
        }
        public void labelchg(string x) {
            
            panel3.Controls.Clear();
            label1.Text = x;
            panel3.Controls.Add(label1);
            MessageBox.Show(x);
           
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
        
            this.panel4.Controls.Clear();
            this.panel3.Controls.Clear();
            label1.Text = "Setting";
            label3.Text = userName;
            Settings st = new Settings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, BackgroundImageLayout = ImageLayout.Stretch };
            st.FormBorderStyle = FormBorderStyle.None;
            this.panel3.Controls.Add(label3);
            this.panel3.Controls.Add(button4);
            this.panel3.Controls.Add(label1);
            this.panel4.Controls.Add(st);
            st.Show();
        }
       
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Mainpage_Deactivate(object sender, EventArgs e)
        {
            
            timer1.Start();
           
        }

        private void Mainpage_Move(object sender, EventArgs e)
        {
           
        }

        private void Mainpage_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void Mainpage_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void Mainpage_Activated(object sender, EventArgs e)
        {
            timer1.Stop();
           
        }

        private void Mainpage_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void Mainpage_Click(object sender, EventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        private void panel1_MouseMove_1(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(e.X.ToString());
            //resettimer();
        }

        public void resettimer() {
            timer1.Stop();
            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
