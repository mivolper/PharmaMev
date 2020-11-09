using System;
using System.Collections.Generic;
using System.Data;
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


namespace PharmaMev.USC
{
    /// <summary>
    /// Interaction logic for USC_GROUPS_MANGE.xaml
    /// </summary>
    public partial class USC_GROUPS_MANGE : UserControl
    {
        Flags.flags flag = new Flags.flags();
        Linq.PharmaLinqDataContext PharmaLinq;
        DataTable  DtGroup = new DataTable();
        int  IDGroup = 0;
        public int IDUser = new int();
        public USC_GROUPS_MANGE()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DtGroup = flag.Fill_DataGrid_join("SELECT * FROM [dbo].[Groups] where Exist = 'true'");
                dgGroups.DataContext = DtGroup;

                txtNameGroup.IsEnabled = false;
                btnSaveGroup.IsEnabled = false;

                flag.Focus_txt(txtNameGroup, txtSearch);
            }
            catch(Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dgGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgGroups.SelectedIndex != -1)
                {
                    IDGroup = (int)DtGroup.Rows[dgGroups.SelectedIndex].ItemArray[0];
                    txtNameGroup.Text = DtGroup.Rows[dgGroups.SelectedIndex].ItemArray[1].ToString();

                    txtNameGroup.IsEnabled = true;
                    btnSaveGroup.IsEnabled = false;
                    btnNewGroup.IsEnabled = true;
                    btnEditGroup.IsEnabled = true;
                    btnDeletGroup.IsEnabled = true;

                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewGroup_Click(object sender, RoutedEventArgs e)
        {
            txtNameGroup.IsEnabled = true;
            txtNameGroup.Clear();
            txtNameGroup.Focus();

            btnSaveGroup.IsEnabled = true;
            btnNewGroup.IsEnabled = false;
            btnEditGroup.IsEnabled = false;
            btnDeletGroup.IsEnabled = false;
            dgGroups.SelectedIndex = -1;
        }

        private void btnSaveGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Linq.Group group = new Linq.Group();

                group.Exist = true;
                group.GroupName = txtNameGroup.Text;
                group.ID_User = IDUser;

                PharmaLinq.Groups.InsertOnSubmit(group);
                PharmaLinq.SubmitChanges();

                MessageBox.Show("تم الحفظ");

                btnNewGroup.IsEnabled = true;
                btnEditGroup.IsEnabled = true;
                btnDeletGroup.IsEnabled = true;
                btnSaveGroup.IsEnabled = false;

                USC.USC_GROUPS_MANGE frm = new USC_GROUPS_MANGE();
                MainWindow.GetMainForm.GridUsc.Children.Clear();
                MainWindow.GetMainForm.GridUsc.Children.Add(frm);
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IDGroup != 0)
                {
                    if (MessageBox.Show("هل تريد تعديل العنصر المحدد؟", "تعديل", MessageBoxButton.YesNo) == MessageBoxResult.Yes)

                    {
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        Linq.Group group = PharmaLinq.Groups.Single(item => item.ID_Groups == IDGroup);

                        group.Exist = true;
                        group.GroupName = txtNameGroup.Text;
                        group.ID_User = IDUser;

                        PharmaLinq.SubmitChanges();

                        MessageBox.Show("تم التعديل");

                        USC.USC_GROUPS_MANGE frm = new USC_GROUPS_MANGE();
                        MainWindow.GetMainForm.GridUsc.Children.Clear();
                        MainWindow.GetMainForm.GridUsc.Children.Add(frm);

                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار عنصر");
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (IDGroup != 0)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر المحدد؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        Linq.Group group = PharmaLinq.Groups.Single(item => item.ID_Groups == IDGroup);

                        group.Exist = false;
                        PharmaLinq.SubmitChanges();

                        MessageBox.Show("تم الحذف");


                        USC.USC_GROUPS_MANGE frm = new USC_GROUPS_MANGE();
                        MainWindow.GetMainForm.GridUsc.Children.Clear();
                        MainWindow.GetMainForm.GridUsc.Children.Add(frm);

                    }
                }
                else
                {
                    MessageBox.Show("الرجاء اختيار عنصر");
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtSearch.Text == "")
                {
                    DtGroup = flag.Fill_DataGrid_join("SELECT * FROM [dbo].[Groups] where Exist = 'true'");
                    dgGroups.DataContext = DtGroup;
                }
                else 
                {
                    dgGroups.DataContext = flag.Fill_DataGrid_join("SELECT * FROM [dbo].[Groups]  where Groups.Exist = 'true' and GroupName like '%'+ '" + txtSearch.Text + "' +'%'");

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
