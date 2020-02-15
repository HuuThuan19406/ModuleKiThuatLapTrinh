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
using System.Data.OleDb;
using System.Data.SqlClient;

namespace BaiTapNhom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con;//Khai báo đối tượng thực hiện kết nối đến cơ sở dữ liệu
        SqlCommand cmd;//Khai báo đối tượng thực hiện các câu lệnh truy vấn
        string query = "INSERT INTO [dbo].[ThongTin] (STT,HoVaTen,CMND,MaNhanVien,GioiTinh,NgaySinh,QueQuan,SoDienThoai,BoPhan,ChucVu,NgayVao) VALUES (@STT,@HoVaTen,@CMND,@MaNhanVien,@GioiTinh,@NgaySinh,@QueQuan,@SoDienThoai,@BoPhan,@ChucVu,@NgayVao)";
        double defaultSize = 15.5;
        public MainWindow()
        {
            InitializeComponent();
            ThongTin_NewLoad();
            CaiDat_NewLoad();
            FontLoading();
            HienThiThongTinList();          
        }

        public void HienThiThongTinList()
        {
            //Tạo đối tượng Connection
            con = new SqlConnection();
            //Truyền vào chuỗi kết nối tới cơ sở dữ liệu
            //Gọi Application.StartupPath để lấy đường dẫn tới thư mục chứa file chạy chương trình 
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\D\ModuleKiThuatLapTrinh\BaiTapNhom\ThongTinNhanSu.mdf;Integrated Security=True";
            //Gọi phương thức Load dự liệu
            LoadDuLieu("SELECT * FROM [dbo].[ThongTin]");
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //Xóa List trước khi tải vào
            lstvThongTin.Items.Clear();
            while (dr.Read())
            {
                NhanSu nhanSu = new NhanSu();
                nhanSu.STT = dr["STT"].ToString();
                nhanSu.HoTen = dr["HoVaTen"].ToString();
                nhanSu.CMND = dr["CMND"].ToString();
                nhanSu.MaNhanVien = dr["MaNhanVien"].ToString();
                nhanSu.GioiTinh = dr["GioiTinh"].ToString();
                nhanSu.NgaySinh = Convert.ToDateTime(dr["NgaySinh"].ToString()).ToString("dd/MM/yyyy");
                nhanSu.NgayVao = Convert.ToDateTime(dr["NgayVao"].ToString()).ToString("dd/MM/yyyy");
                nhanSu.QueQuan = dr["QueQuan"].ToString();
                nhanSu.SoDienThoai = dr["SoDienThoai"].ToString();
                nhanSu.ChucVu = dr["ChucVu"].ToString();
                nhanSu.BoPhan = dr["BoPhan"].ToString();
                lstvThongTin.Items.Add(nhanSu);
            }
        }

        private void LoadDuLieu(String sql)
        {
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            cmd = new SqlCommand(sql, con);
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

        public void FontLoading()
        {

            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                cboKieuChu.Items.Add(fontFamily.ToString());
            }
        }
        public void CaiDat_NewLoad()
        {
            sliderFontSize.Value = defaultSize;
            cboKieuChu.SelectedItem = "UTM Daxline";
        }

        private void lstvThongTin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imgAnhDaiDien_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileSource = new Microsoft.Win32.OpenFileDialog();
            if (fileSource.ShowDialog() == true)
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

        private void sliderFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FontSize = sliderFontSize.Value;
        }

        private void cboKieuChu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontFamily = new System.Windows.Media.FontFamily(cboKieuChu.SelectedItem.ToString());
        }

        ComboBoxItem boxItem;
        private void btnThemNhanSu_Click(object sender, RoutedEventArgs e)
        {
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@STT", lstvThongTin.Items.Count + 1);
            cmd.Parameters.AddWithValue("@HoVaTen", txtHoVaTen.Text);
            cmd.Parameters.AddWithValue("@CMND", txtCMND_CCCD.Text);
            cmd.Parameters.AddWithValue("@MaNhanVien", txtMaNhanVien.Text);
            boxItem = (ComboBoxItem)cboGioiTinh.SelectedItem;
            cmd.Parameters.AddWithValue("@GioiTinh", boxItem.Content.ToString());
            cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.SelectedDate.Value);
            boxItem = (ComboBoxItem)cboQueQuan.SelectedItem;
            cmd.Parameters.AddWithValue("@QueQuan", boxItem.Content.ToString());
            cmd.Parameters.AddWithValue("@BoPhan", txtBoPhan.Text);
            cmd.Parameters.AddWithValue("@ChucVu", txtChucVu.Text);
            cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
            cmd.Parameters.AddWithValue("@NgayVao", dtpNgayVao.SelectedDate.Value);
            cmd.ExecuteNonQuery();
            ThongTin_NewLoad();
            HienThiThongTinList();
        }
    }
}
