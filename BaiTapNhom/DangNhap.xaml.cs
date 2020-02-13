using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BaiTapNhom
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        public DangNhap()
        {
            InitializeComponent();
            txtTaiKhoan.Text = "admin";
            pwbMatKhau.Password = "123456";
        }

        private void DangNhap_Click(object sender, RoutedEventArgs e)
        {
            if ((txtTaiKhoan.Text.ToLower() == "admin") && (pwbMatKhau.Password == "123456"))
            {
                MainWindow frmMain = new MainWindow();
                frmMain.Show();
                Close();
            }
            else
                MessageBox.Show("THÔNG TIN MẶC ĐỊNH" +
                                "\nTài khoản: admin" +
                                "\nMật khẩu: 123456", "Nhắc nhở",MessageBoxButton.OK,MessageBoxImage.Information);               
        }
    }
}
