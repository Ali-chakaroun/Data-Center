using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Settings : Form
    {
        //string Dpath = @"C:\Users\chaka\OneDrive\Pictures\Saved Pictures";
        string file ;
        string content;
        public Settings()
        {
            InitializeComponent();
            directory(null);
          
        }

       
        public void directory(string s) {
            string m = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\BRimages";
         var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) ;
           var Createfolder = Path.Combine(appdata, "Data center");
            if (!Directory.Exists(Createfolder)) {
                Directory.CreateDirectory(Createfolder);
            }
            file = Path.Combine(Createfolder, "save.txt");
            
            if (!Directory.Exists(m)) {
                Directory.CreateDirectory(m);
            }
            if (!File.Exists(file))
            {

                File.Create(file).Close();
                
                StreamWriter sw = new StreamWriter(file);
                content = m;
                sw.WriteLine(content);
                sw.Close();
            }
            else if (s == null)
            {
                
                StreamReader sr = new StreamReader(file);
                content = sr.ReadLine();
                sr.Close();
                if (!Directory.Exists(content))
                {
                    StreamWriter sw = new StreamWriter(file);
                    content = m;
                    sw.WriteLine(content);
                    sw.Close();
                    MessageBox.Show("Path is invalid reseting to default path." + content);
                }


            }
            else
            {
               StreamWriter sw = new StreamWriter(file);
                
                if (content == null)
                {
                    content = m;
                    sw.WriteLine(content);
                    

                }
                if (s != null)
                {
                    content = s;
                    sw.WriteLine(s);
                  

                }
                sw.Close();
                
            }


            textBox1.Text = content;
        }
        public string saveDir() {
           
            return content;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderFileDialog = new FolderBrowserDialog();
                if (folderFileDialog.ShowDialog() == DialogResult.OK)
                {
                    directory(folderFileDialog.SelectedPath);
                }
            
           
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //Mainpage mp = new Mainpage();
            //mp.resettimer();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
