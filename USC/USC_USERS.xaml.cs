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
using System.Data;
namespace PharmaMev.USC
{
    /// <summary>
    /// Interaction logic for USC_USERS.xaml
    /// </summary>
    public partial class USC_USERS : UserControl
    {
        Linq.PharmaLinqDataContext PharmaLinq;
        int UserId = new int();
        Flags.flags flag = new Flags.flags();
        DataTable Dt = new DataTable();
        public USC_USERS()
        {
            InitializeComponent();
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Dt = flag.Fill_DataGrid_join("SELECT * FROM [dbo].[Users] where  Exist = 'true'");
                dgUsers.DataContext = Dt;

                StackMainDetails.IsEnabled = false;
                StackPrmDetails.IsEnabled = false;
                btnSaveUser.IsEnabled = false;

                flag.Focus_txt(txtSearch, txtUserName, txtUserPassWard); 
            }
            catch(Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            btnSaveUser.IsEnabled = true;
            btnNewUser.IsEnabled = false;
            btnEditUser.IsEnabled = false;
            btnDeletUser.IsEnabled = false;
            StackMainDetails.IsEnabled = true;
            StackPrmDetails.IsEnabled = true;

            txtUserName.Clear();
            txtUserPassWard.Clear();
            txtUserName.Focus();

            chbBuyScreen.IsChecked = false;
            chbProductsMange.IsChecked = false;
            chbReportsMange.IsChecked = false;
            chbSaleScreen.IsChecked = false;
            chbSetting.IsChecked = false;
            chbUsersMange.IsChecked = false;
            chbStorage.IsChecked = false;
            dgUsers.SelectedIndex = -1;
        }

        private void btnSaveUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (flag.Null_Checker(txtUserName) || flag.Null_Checker(txtUserPassWard))
                {
                    return;
                }
                
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Linq.User user = new Linq.User();
                user.User_Name = txtUserName.Text;
                user.User_Psw = txtUserPassWard.Text;
                user.Exist = true;
                user.SalePermissions =(bool)chbSaleScreen.IsChecked;
                user.BuyPermissions = (bool)chbBuyScreen.IsChecked;
                user.ProductsPermissions = (bool)chbProductsMange.IsChecked;
                user.ReportsPermissions = (bool)chbReportsMange.IsChecked;
                user.UsersPermissions = (bool)chbUsersMange.IsChecked;
                user.SettingPermissions = (bool)chbSetting.IsChecked;
                user.StoragePermissions = (bool)chbStorage.IsChecked;

                PharmaLinq.Users.InsertOnSubmit(user);
                PharmaLinq.SubmitChanges();


                MessageBox.Show("تم الحفظ");
                Dt = flag.Fill_DataGrid_join("SELECT * FROM [dbo].[Users] where  Exist = 'true'");
                dgUsers.DataContext = Dt;

                USC.USC_USERS frm = new USC_USERS();
                MainWindow.GetMainForm.GridUsc.Children.Clear();
                MainWindow.GetMainForm.GridUsc.Children.Add(frm);

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);

            }

        }

        private void dgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgUsers.SelectedIndex != -1)
                {

                    UserId = Convert.ToInt32(Dt.Rows[dgUsers.SelectedIndex].ItemArray[0]);
                    StackMainDetails.IsEnabled = true;
                    StackPrmDetails.IsEnabled = true;
                    btnSaveUser.IsEnabled = false;
                    btnNewUser.IsEnabled = true;
                    btnEditUser.IsEnabled = true;
                    btnDeletUser.IsEnabled = true;


                    txtUserName.Text = Dt.Rows[dgUsers.SelectedIndex].ItemArray[1].ToString() ;
                    txtUserPassWard.Text = Dt.Rows[dgUsers.SelectedIndex].ItemArray[2].ToString();
                    chbSaleScreen.IsChecked = Convert.ToBoolean(Dt.Rows[dgUsers.SelectedIndex].ItemArray[4].ToString());
                    chbBuyScreen.IsChecked = Convert.ToBoolean(Dt.Rows[dgUsers.SelectedIndex].ItemArray[5].ToString());
                    chbProductsMange.IsChecked = Convert.ToBoolean(Dt.Rows[dgUsers.SelectedIndex].ItemArray[6].ToString());
                    chbReportsMange.IsChecked = Convert.ToBoolean(Dt.Rows[dgUsers.SelectedIndex].ItemArray[7].ToString());
                    chbUsersMange.IsChecked = Convert.ToBoolean(Dt.Rows[dgUsers.SelectedIndex].ItemArray[8].ToString());
                    chbSetting.IsChecked = Convert.ToBoolean(Dt.Rows[dgUsers.SelectedIndex].ItemArray[9].ToString());
                    chbStorage.IsChecked = Convert.ToBoolean(Dt.Rows[dgUsers.SelectedIndex].ItemArray[10].ToString());

                }
            }
            catch(Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        


        }

        private void btnDeletUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserId != 0)
                {
                    PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);

                    if (MessageBox.Show("هل تريد حذف العنصر المحدد؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var user = PharmaLinq.Users.Single(item => item.ID_User == UserId);
                        user.Exist = false;
                        PharmaLinq.SubmitChanges();
                        MessageBox.Show("تم الحذف");
                        Dt = flag.Fill_DataGrid_join("SELECT * FROM [dbo].[Users] where  Exist = 'true'");
                        dgUsers.DataContext = Dt;

                        USC.USC_USERS frm = new USC_USERS();
                        MainWindow.GetMainForm.GridUsc.Children.Clear();
                        MainWindow.GetMainForm.GridUsc.Children.Add(frm);

                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار عنصر");
                }
            }
            catch(Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);

            }

        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UserId != 0)
                {
                    if (MessageBox.Show("هل تريد تعديل العنصر المحدد؟", "تعديل", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {                       
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        var user = PharmaLinq.Users.Single(item => item.ID_User == UserId);
                        user.User_Name = txtUserName.Text;
                        user.User_Psw = txtUserPassWard.Text;
                        user.Exist = true;
                        user.SalePermissions = (bool)chbSaleScreen.IsChecked;
                        user.BuyPermissions = (bool)chbBuyScreen.IsChecked;
                        user.ProductsPermissions = (bool)chbProductsMange.IsChecked;
                        user.ReportsPermissions = (bool)chbReportsMange.IsChecked;
                        user.UsersPermissions = (bool)chbUsersMange.IsChecked;
                        user.SettingPermissions = (bool)chbSetting.IsChecked;
                        user.StoragePermissions = (bool)chbStorage.IsChecked;

                        PharmaLinq.SubmitChanges();
                        MessageBox.Show("تم  التعديل");

                        var Dt = PharmaLinq.Users.Where(item => item.Exist == true);
                        flag.Fill_DataGrid(dgUsers, Dt);

                        USC.USC_USERS frm = new USC_USERS();
                        MainWindow.GetMainForm.GridUsc.Children.Clear();
                        MainWindow.GetMainForm.GridUsc.Children.Add(frm);

                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار عنصر");
                }

            }
            catch(Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext();
                if (txtSearch.Text == "")
                {
                    dgUsers.DataContext = flag.Fill_DataGrid_join("SELECT * FROM [dbo].[Users] where  Exist = 'true'");
                       
                }
                else
                {
                    dgUsers.DataContext = flag.Fill_DataGrid_join("SELECT * FROM [dbo].[Users] where  Exist = 'true' and User_Name like '%'+ '" + txtSearch.Text + "' +'%'");
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
