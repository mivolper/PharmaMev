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
using MaterialDesignThemes;

namespace PharmaMev
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Flags.flags flag = new Flags.flags();
        public  int IDUSER_main = new int();
        private static MainWindow frm;
        public TextBox txtbarcode = new TextBox();
        public TextBox txtSelected = new TextBox();
        bool txtFocused = false;
        public string Bill = "0";

        static void frm_Closed(object sender, EventArgs e)
        {
            frm = null;
        }
        public static MainWindow GetMainForm
        {
            get
            {
                if (frm == null)
                {
                    frm = new MainWindow();
                    frm.Closed += new EventHandler(frm_Closed);
                }
                return frm;
            }
        }
        public MainWindow()
        {
            
            InitializeComponent();
            if (frm == null)
                frm = this;
            
            btnOpenMenu.Visibility = Visibility.Collapsed;
            btnAddClose.Visibility = Visibility.Collapsed;

            USC.USC_KEYBOARd Usc = new USC.USC_KEYBOARd();
            GridKeyBoard.Children.Clear();
            GridKeyBoard.Children.Add(Usc);
        }


        private void btnPower_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد اغلاق البرنامج؟", "اغلاق", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (Properties.Settings.Default.AutoBackUp == true)
                    {
                        flag.BackUp(Properties.Settings.Default.Path, Properties.Settings.Default.DataBase);
                        MessageBox.Show("تم انشاء النسحة بنجاح");
                    }
                    App.Current.Shutdown();
                    
                }
            }
            catch(Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        #region Menu Buttons
        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnCloseMenu.Visibility = Visibility.Visible;
                btnOpenMenu.Visibility = Visibility.Collapsed;
            }
            catch(Exception ex)
            {

            }
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnCloseMenu.Visibility = Visibility.Collapsed;
                btnOpenMenu.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region ListView Items
        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            USC.USC_SALES Sales = new USC.USC_SALES();
            GridUsc.Children.Clear();
            Sales.IDUser = IDUSER_main;
            GridUsc.Children.Add(Sales);
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            USC.USC_USERS Users = new USC.USC_USERS();
            GridUsc.Children.Clear();
            GridUsc.Children.Add(Users);
        }

        private void btnSettingScreen_Selected(object sender, RoutedEventArgs e)
        {
            USC.USC_BACKUP backup = new USC.USC_BACKUP();
            GridUsc.Children.Clear();
            GridUsc.Children.Add(backup);
        }

        private void btnProductsMange_Selected(object sender, RoutedEventArgs e)
        {
            USC.USC_PRODUCTS_MANGE Products = new USC.USC_PRODUCTS_MANGE();
            GridUsc.Children.Clear();
            Products.IDUser = IDUSER_main;
            GridUsc.Children.Add(Products);
        }

        private void btnGroupsMange_Selected(object sender, RoutedEventArgs e)
        {
            USC.USC_GROUPS_MANGE Groups = new USC.USC_GROUPS_MANGE();
            GridUsc.Children.Clear();
            Groups.IDUser = IDUSER_main;
            GridUsc.Children.Add(Groups);
        }

        private void btnBuyScreen_Selected(object sender, RoutedEventArgs e)
        {
            USC.USC_BUYS Buy = new USC.USC_BUYS();
            GridUsc.Children.Clear();
            Buy.IDUser = IDUSER_main;
            GridUsc.Children.Add(Buy);
        }

        private void btnCauleMange_Selected(object sender, RoutedEventArgs e)
        {
            
            USC.USC_CUAL_CUSTOMER Cual_Customer = new USC.USC_CUAL_CUSTOMER();
            GridUsc.Children.Clear();
            GridUsc.Children.Add(Cual_Customer);
        }

        private void btnReportsMange_Selected(object sender, RoutedEventArgs e)
        {
            USC.USC_REPORTS Reports = new USC.USC_REPORTS();
            GridUsc.Children.Clear();
            GridUsc.Children.Add(Reports);
            
        }

        private void btnStoreMange_Selected(object sender, RoutedEventArgs e)
        {
            USC.USC_Store Store = new USC.USC_Store();
            GridUsc.Children.Clear();
            GridUsc.Children.Add(Store);
        }

        #endregion

        #region Add Buttons
        private void btnAddOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAddOpen.Visibility = Visibility.Collapsed;
                btnAddClose.Visibility = Visibility.Visible;
                btnAddClose.Margin = btnAddOpen.Margin;

                USC.USC_SELECT_PRODUCT usc = new USC.USC_SELECT_PRODUCT();
                usc.txtbarcode = this.txtbarcode;
                SubGrid.Children.Clear();
                SubGrid.Children.Add(usc);
            }
            catch (Exception ex)
            {

            }

        }

        public void btnAddClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAddOpen.Visibility = Visibility.Visible;
                btnAddClose.Visibility = Visibility.Collapsed;
                btnAlerts.Visibility = Visibility.Visible;

            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        private void list_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SubBorder.Height == 750)
                {
                    btnAddClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }

            }
            catch (Exception ex)
            {

            }
        }

        #region KeyBoard Trigger
        private void GridUsc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (GridMenu.Width == 220)
                {
                    btnCloseMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                if (SubBorder.Height == 750)
                {
                    btnAddClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                }
                if (GridKeyBoard.Height == 370 )
                {
                    txt1.RaiseEvent(new RoutedEventArgs(TextBox.GotFocusEvent));
                }
                txtFocused = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void TxtSelected_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cbxTouchScreen.IsChecked == true)
                {
                    if (GridKeyBoard.Height != 370)
                    {
                        txt.RaiseEvent(new RoutedEventArgs(TextBox.GotFocusEvent));
                    }
                    txtSelected = (TextBox)sender;
                    txtFocused = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txt1_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void cbxTouchScreen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxTouchScreen.IsChecked==false)
                {
                    if (GridKeyBoard.Height == 370 && txtFocused == false)
                    {
                        txt1.RaiseEvent(new RoutedEventArgs(TextBox.GotFocusEvent));
                    }
                }
                txtFocused = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        #endregion

        private void btnAlerts_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAddClose.Visibility = Visibility.Visible;
                btnAlerts.Visibility = Visibility.Collapsed;
                btnAddClose.Margin = btnAlerts.Margin;

                USC.USC_ALERTS frm = new USC.USC_ALERTS();
                SubGrid.Children.Clear();
                SubGrid.Children.Add(frm);
            }
            catch(Exception ex)
            {

            }
        }

        private void btnMinmize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
