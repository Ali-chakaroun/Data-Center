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
    public partial class Barcodeimage : Form
    {
     
        public Barcodeimage()
        {
            InitializeComponent();
        }


        public void BRcodeimg(Image img) {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox1.Image = img;
        }
        private  void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            Additem ai = new Additem();
            ai.refresh();

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            //Mainpage mp = new Mainpage();
            //mp.resettimer();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
