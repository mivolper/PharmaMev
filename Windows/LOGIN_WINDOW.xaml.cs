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
using System.Data;

namespace PharmaMev.Windows
{
    /// <summary>
    /// Interaction logic for LOGIN_WINDOW.xaml
    /// </summary>
    public partial class LOGIN_WINDOW : Window
    {
        Linq.PharmaLinqDataContext PharmaLinq;
        Flags.flags flag = new Flags.flags();
        DataTable Dt = new DataTable();
        public LOGIN_WINDOW()
        {
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Dt = flag.Fill_DataGrid_join("select User_Name from Users where Exist='True'");

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    cmbName.Items.Add(Dt.Rows[i].ItemArray[0]) ;
                }

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show("خطاء في التصال بالقاعدة البيانات" + ex.Message);
                AdminUSC.Admin admin = new AdminUSC.Admin();
                admin.Show();
            }
        }

        private void btnColse_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);

                var Temp = PharmaLinq.Users.Where(item => item.User_Name == cmbName.Text && item.User_Psw == txtPassWard.Password && item.Exist == true);
                if (Temp.Count() > 0)
                {
                    var user = Temp.First();
                    MainWindow frm = new MainWindow();
                    frm.IDUSER_main = user.ID_User;
                    Close();                  
                    frm.btnSaleScreen.IsEnabled = user.SalePermissions;
                    frm.btnBuyScreen.IsEnabled = user.BuyPermissions;
                    frm.btnProductsMange.IsEnabled=frm.btnGroupsMange.IsEnabled =frm.btnCauleMange.IsEnabled =user.ProductsPermissions;
                    frm.btnReportsMange.IsEnabled = user.ReportsPermissions;
                    frm.btnUsersMange.IsEnabled = user.UsersPermissions;
                    frm.btnSettingScreen.IsEnabled = user.SettingPermissions;
                    frm.btnStoreMange.IsEnabled = user.StoragePermissions;

                    frm.ShowDialog();

                }
                else
                {
                    MessageBox.Show("اسم المستخدم او كلمة المرور غير صحيحة");
                    txtPassWard.Clear();
                    txtPassWard.Focus();
                }
            }
            catch(Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

    }
}
