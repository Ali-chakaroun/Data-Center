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

namespace project
{
     class DBconn

    {
        public MySqlConnection DBconnect()
        {
            MySqlConnection con = new MySqlConnection(DBconnection());
            return con;
        }

        public MySqlCommand DBcmd(int x, MySqlConnection conn)
        {

            MySqlCommand cm = new MySqlCommand(DBstrings(x), conn);
            return cm;
        }
        protected string DBstrings(int x) {
            string Dbstr = " ";
            switch (x)
            {
                case 1:
                     Dbstr = "insert into barcode(id, barcode_image, barcode_idname, barcode_name) values(@idnum, @image, @idname, @name); insert into images(ID, ImageName, Image1, Image2,Image3) values(@imageID, @ImageIDnum, @Image1num, @Image2num,@Image3num); insert into iteminfo(id, Datename, Date,CurrentAmount,NewAmount,UpdatedAmount,Byuser,Price) values(@idnum, @name, @Date1,@Amount1,@Amount2,@Amount3,@user,@price);";
                    break;
                case 2:
                    Dbstr = "select id,barcode_name from barcode;";
                    break;
                case 3:
                    Dbstr = "select * FROM barcode  INNER JOIN barcode.images ON barcode_name = ImageName INNER JOIN iteminfo ON barcode_name = Datename where barcode_name = @name ORDER BY iteminfo.id ASC ; ";
                    break;
                case 4:
                    Dbstr = "Update barcode,images,iteminfo SET barcode_name = @newname,ImageName =@newname,Image1 = @image1,Image2 = @image2, Image3 = @image3 ,Datename =@newname where barcode_name =@name and  ImageName = @name and Datename=@name;insert into iteminfo(Datename,Date,CurrentAmount,NewAmount,UpdatedAmount,Byuser,Price) values(@newname,@date,@Amount1,@Amount2,@Amount3,@user,@price);";
                    break;
                case 5:
                    Dbstr = "select * from barcode where barcode_idname like @barcode";
                    break;
                case 6:
                    Dbstr = "delete barcode,images,iteminfo from barcode INNER JOIN barcode.images ON barcode_name = ImageName INNER JOIN iteminfo ON barcode_name = Datename where barcode_name = @name ;";
                    break;
                case 7:
                    Dbstr = "select * from barcode,images where barcode_name = ImageName;";
                    break;
                case 8:
                    Dbstr = "select * from passcode where Acess = @code or User = @name;";
                    break;
                default:
                    MessageBox.Show("Error no MYSQL command found");
                    break;
            
            }
            return Dbstr;
            
        }

        protected string DBconnection()
        {

            string connString = "server=localhost;user id=root;password=root123;persistsecurityinfo=True;database=barcode";
            return connString;

        }
    }
}

