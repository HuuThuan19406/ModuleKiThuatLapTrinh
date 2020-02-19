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
    class SQL
    {
        private static SqlConnection con;
        private static SqlCommand cmd;
        public SqlCommand Cmd
        {
            get
            {
                return cmd;
            }
            set
            {
                cmd = value;
            }
        }
        public SqlConnection Con
        {
            get
            {
                return con;
            }
            set
            {
                con = value;
            }
        }
        public static SqlCommand LoadDuLieu(String sql, SqlConnection con)
        {
            cmd = new SqlCommand();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            cmd = new SqlCommand(sql, con);
            return cmd;
        }
        public void DocDuLieu(string connection, string SELECT_FROM)
        {
            //Tạo đối tượng Connection
            con = new SqlConnection();
            //Truyền vào chuỗi kết nối tới cơ sở dữ liệu
            //Gọi Application.StartupPath để lấy đường dẫn tới thư mục chứa file chạy chương trình 
            con.ConnectionString = connection;
            con.Open();
            SELECT_FROM = "SELECT * FROM " + SELECT_FROM;
            //Gọi phương thức Load dự liệu
            cmd = LoadDuLieu(SELECT_FROM, con);
        }

    }
}
