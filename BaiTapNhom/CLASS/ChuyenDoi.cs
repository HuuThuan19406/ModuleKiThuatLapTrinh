using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BaiTapNhom
{
    class ChuyenDoi
    {
        public static string Base64(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }
        public static void HinhAnh(string base64String, Image image)
        {
            byte[] imgBytes = Convert.FromBase64String(base64String);
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream ms = new MemoryStream(imgBytes);
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();
            image.Source = bitmapImage;
        }
        public static BitmapImage BitMapImage(string base64String)
        {
            byte[] imgBytes = Convert.FromBase64String(base64String);
            BitmapImage bitmapImage = new BitmapImage();
            MemoryStream ms = new MemoryStream(imgBytes);
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}
