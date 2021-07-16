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
    public partial class Alldates : Form
    {
        
        int y = 0;
        public Alldates()
        {
            InitializeComponent();
            


        }
        public void adddates(List<string> s) {
            
           
            foreach (string x in s) {
                
                string a = y+" : "+x;
                listBox1.Items.Add(a);
                y++;
            }
           


        }
      
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Alldates_Load(object sender, EventArgs e)
        {
            
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //Mainpage mp = new Mainpage();
            //mp.resettimer();
        }
    }
}
