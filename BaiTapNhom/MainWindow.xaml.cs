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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace BaiTapNhom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThongTin_NewLoad();
        }

        public void ThongTin_NewLoad()
        {
            txtHoVaTen.Clear();
            txtCMND_CCCD.Clear();
            txtSoDienThoai.Clear();
            cboGioiTinh.SelectedIndex = -1;
            cboQueQuan.SelectedIndex = -1;
            dtpNgaySinh.SelectedDate = DateTime.Today;
            dtpNgayVao.SelectedDate = DateTime.Today;
            txtMaNhanVien.Text = "Mã được cấp tự động";
            txtBoPhan.Text = "Phân tại TỔ CHỨC";
            txtChucVu.Text = "Phân tại TỔ CHỨC";
        }     


        private void lstvThongTin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imgAnhDaiDien_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileSource = new Microsoft.Win32.OpenFileDialog();
            if(fileSource.ShowDialog()==true)
            {
                imgAnhDaiDien.Source = new BitmapImage(new Uri(fileSource.FileName));
            }            
        }

        TraCuu frmTraCuu;
        private void btnTraCuuNhanSu_Click(object sender, RoutedEventArgs e)
        {
            if (frmTraCuu == null)
                frmTraCuu = new TraCuu();

            // you need to take care of form closing because the object being disposed)    
            frmTraCuu.Closed += delegate { frmTraCuu = null; };

            if (frmTraCuu.WindowState == WindowState.Minimized)
                frmTraCuu.WindowState = WindowState.Normal;

            frmTraCuu.Show();
            frmTraCuu.Activate();
        }
    }
}
