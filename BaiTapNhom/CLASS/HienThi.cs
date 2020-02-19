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
    class HienThi
    {
        public static void ThongTinList(ListView listView, Hashtable source)
        {
            //Xóa List trước khi tải vào
            listView.Items.Clear();
            foreach (DictionaryEntry VALUE in source)
            {
                listView.Items.Add(VALUE.Value);
            }
        }
    }
}
