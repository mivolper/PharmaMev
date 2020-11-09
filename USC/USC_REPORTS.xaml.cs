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
using PharmaMev.CRReport;
using CrystalDecisions.CrystalReports.Engine;

namespace PharmaMev.USC
{
    /// <summary>
    /// Interaction logic for USC_REPORTS.xaml
    /// </summary>
    public partial class USC_REPORTS : UserControl
    {
        Flags.flags flag = new Flags.flags();
        Linq.PharmaLinqDataContext PharmaLinq;
        DataTable Dt = new DataTable();
        DataTable UserDt = new DataTable();
        DataTable CaulDt = new DataTable();
        DataTable CustomerDt = new DataTable();

        Double total, totalCost, totalDebt, totalPrice = 0;
        public USC_REPORTS()
        {
            InitializeComponent();
            MainWindow.GetMainForm.txtbarcode = txtProduct;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UserDt = flag.Fill_DataGrid_join("SELECT [User_Name],ID_User  FROM [dbo].[Users]  where Exist ='true'");
                CaulDt = flag.Fill_DataGrid_join("SELECT [CauleName],ID_Caule  FROM [dbo].[Caule]  where Exist ='true'");
                CustomerDt = flag.Fill_DataGrid_join("SELECT [CustomerName],ID_Customer  FROM [dbo].[Customers]  where Exist ='true'");


                flag.Fill_ComboBox(UserDt, cmbUser);
                flag.Fill_ComboBox(CaulDt, cmbCaule);
                flag.Fill_ComboBox(CustomerDt, cmbCustomer);

                rbtnBuy.IsChecked = true;

                string[] items = { "نقدي", "شيك", "بطاقة ائتمانية" };
                cmbPayType.ItemsSource = items;

                flag.Focus_txt(txtBill, txtProduct);

                if (Properties.Settings.Default.UnitEnabled == false)
                {
                    UnitColumn.Visibility = Visibility.Collapsed;
                    DebtUnitColumn.Visibility = Visibility.Collapsed;

                }


            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }


        #region CheckBox
        private void cbxProducts_Click(object sender, RoutedEventArgs e)
        {
            txtProduct.IsEnabled = (bool)cbxProducts.IsChecked;
        }

        private void cbxUser_Click(object sender, RoutedEventArgs e)
        {
            cmbUser.IsEnabled = (bool)cbxUser.IsChecked;
        }

        private void cbxBill_Click(object sender, RoutedEventArgs e)
        {
            txtBill.IsEnabled = (bool)cbxBill.IsChecked;
        }

        private void cbxPayType_Click(object sender, RoutedEventArgs e)
        {
            cmbPayType.IsEnabled = (bool)cbxPayType.IsChecked;
        }

        private void cbxDate_Click(object sender, RoutedEventArgs e)
        {
            dpFirstDate.IsEnabled = dpSecondDate.IsEnabled = (bool)cbxDate.IsChecked;
        }

        private void cbxCustomer_Click(object sender, RoutedEventArgs e)
        {
            cmbCustomer.IsEnabled = (bool)cbxCustomer.IsChecked;
        }

        private void cbxCaule_Click(object sender, RoutedEventArgs e)
        {
            cmbCaule.IsEnabled = (bool)cbxCaule.IsChecked;
        }
        #endregion

        private void btnSerch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);

                string text = " ";
                string[] text1 = { " And  BuysAction.ID_Buy = '", " And  Caule.CauleName ='", " And  Products.Barcode = '", " And  Users.User_Name = '", " And Buys.DateBuy between '", " And '"," "," " };
                string[] text2 = { "  And  SalesAction.ID_Sale  = '", " ", " And  Products.Barcode = '", " And  Users.User_Name = '", " And format(SaleDate,'yyyy-MM-dd') between '", " And '", " and CustomerName = '", " And Sales.PayType ='" };
                string[] text3 = { txtBill.Text,cmbCaule.Text, txtProduct.Text ,cmbUser.Text,dpFirstDate.Text,dpSecondDate.Text,cmbCustomer.Text, cmbPayType.Text };

                CheckBox[] cbx = { cbxBill, cbxCaule, cbxProducts, cbxUser, cbxDate, cbxDate, cbxCustomer, cbxPayType };

                if (rbtnBuy.IsChecked == true)
                {
                    for (int i = 0; i < 8; i++)
                    { if (cbx[i].IsChecked == true)
                        {
                            text += text1[i] + text3[i]  + "'";
                        }
                    }
                    Dt = flag.Fill_DataGrid_join("SELECT BuysAction.ID_Buy as ID,Products.Name,[BuysAction].Cost ,[Quantit_BuysAction] as Quantity,[totatCost] as TotalPrice FROM [dbo].[BuysAction] inner join Products on Products.ID_Product=BuysAction.ID_Product inner join Buys on Buys.ID_Buy=BuysAction.ID_Buy inner join Users on Users.ID_User=Buys.ID_User inner join Caule on Caule.ID_Caule=Buys.ID_Caule where BuysAction.Exist ='true'  " + text + " Order By Buys.ID_Buy");
                    dgBuyBill.DataContext = Dt;

                }
                else if (rbtnSale.IsChecked == true)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (cbx[i].IsChecked == true)
                        {
                            text += text2[i] + text3[i] + "'";
                        }
                    }
                    Dt = flag.Fill_DataGrid_join(" SELECT  SalesAction.ID_Sale as ID,Products.Name ,SalesAction.Price ,[Unit] ,[Quantity_SaleAction] as Quantity,[SalePrecentage] ,[TotalPrice],Cost=case when Unit != 'قطعة' and BiggerUnit = 'الثانوية' then Cost * SubQuantity when Unit != 'قطعة' and BiggerUnit = 'الأساسية' then Cost / SubQuantity else Cost end ,SubQuantity,BiggerUnit,SaleDate,PayType,CustomerName FROM [dbo].[SalesAction] inner join Products on Products.ID_Product=SalesAction.ID_Product inner join Sales on Sales.ID_Sale=SalesAction.ID_Sale inner join Customers on Customers.ID_Customer=Sales.ID_Customer inner join Users on Users.ID_User=Sales.ID_User where SalesAction.Exist ='true'  and Sales.Paid = SalePrice " + text + " Order By Sales.ID_Sale");
                    dgBill.DataContext = Dt;

                }
                else if (rbtnDebt.IsChecked == true)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (cbx[i].IsChecked == true)
                        {
                            text += text2[i] + text3[i] + "'";
                        }
                    }
                    Dt = flag.Fill_DataGrid_join(" SELECT  SalesAction.ID_Sale as ID,Products.Name ,SalesAction.Price ,[Unit] ,[Quantity_SaleAction] as Quantity,[SalePrecentage] ,[TotalPrice],Cost=case when Unit != 'قطعة' and BiggerUnit = 'الثانوية' then Cost * SubQuantity when Unit != 'قطعة' and BiggerUnit = 'الأساسية' then Cost / SubQuantity else Cost end ,SubQuantity,BiggerUnit,SaleDate,Convert(decimal(20, 1),(SalePrice - Paid)) as Reminder,Convert(decimal(20, 1),Paid) as Paid,CustomerName FROM [dbo].[SalesAction] inner join Products on Products.ID_Product=SalesAction.ID_Product  inner join Sales on Sales.ID_Sale=SalesAction.ID_Sale inner join Customers on Customers.ID_Customer=Sales.ID_Customer inner join Users on Users.ID_User=Sales.ID_User where SalesAction.Exist ='true'  and Sales.Paid < SalePrice " + text + " Order By Sales.ID_Sale");
                    dgDebtBill.DataContext = Dt;

                }

                totalDebt = 0;
                totalPrice = 0;
                total = 0;
                totalCost = 0;

                if (rbtnSale.IsChecked == true)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {

                        totalCost += Convert.ToDouble(Convert.ToInt32(Dt.Rows[i].ItemArray[4]) * Convert.ToDouble(Dt.Rows[i].ItemArray[7]));
                        total += Convert.ToDouble(Dt.Rows[i].ItemArray[6]);
                    }

                }
                else if (rbtnDebt.IsChecked == true)
                {
                    object id = new object();
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {

                        if (Dt.Rows[i].ItemArray[0] != id)
                        {
                            id = Dt.Rows[i].ItemArray[0];
                            totalDebt += Convert.ToDouble(Convert.ToInt32(Dt.Rows[i].ItemArray[11]));
                            totalPrice += Convert.ToDouble(Dt.Rows[i].ItemArray[6]);
                        }
                    }
                }
                else
                {
                    total = 0;
                }
                txtTotalprofits.Text = Math.Round(total - totalCost, 2).ToString();
                txtTotal.Text = total.ToString();

                txtTotalDebt.Text = totalDebt.ToString();
                txtTotalPrice.Text = totalPrice.ToString();

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportClass Bill;

                if (rbtnBuy.IsChecked == true)
                {
                    Bill = new BuyBill();
                }
                else
                {
                    Bill = new Bill();
                }

                Bill.SetDataSource(Dt);
                Bill.SetParameterValue(0, dpFirstDate.Text);
                Bill.SetParameterValue(1, dpSecondDate.Text);


                if (cbxUser.IsChecked == true)
                {
                    Bill.SetParameterValue(2, cmbUser.Text);

                }
                else
                {
                    Bill.SetParameterValue(2, " ");
                }

                //CRReport.REPORT_WINDOW rewindow = new REPORT_WINDOW();
                //rewindow.ReportVW.ViewerCore.ReportSource = Bill;
                //rewindow.Show();
                Bill.PrintToPrinter(1, false, 0, 0);
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dpSecondDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
              if(dpSecondDate.SelectedDate < dpFirstDate.SelectedDate)
                {
                    MessageBox.Show("يجب ان يكون التاريخ المحدد بعد التاريخ اعلاه");
                    dpSecondDate.SelectedDate = dpFirstDate.SelectedDate;
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        private void rbtnBuy_Checked(object sender, RoutedEventArgs e)
        {
            cbxCaule.IsEnabled = true;

            cbxPayType.IsChecked = false;
            cbxPayType.IsEnabled = false;
            cmbPayType.IsEnabled = false;

            cbxCustomer.IsChecked = false;
            cbxCustomer.IsEnabled = false;
            cmbCustomer.IsEnabled = false;
            cmbPayType.IsEnabled = false;

            dgBuyBill.Visibility = Visibility.Visible;
            grdSales.Visibility = Visibility.Collapsed;
            grdDebt.Visibility = Visibility.Collapsed;
            dgDebtBill.Visibility = Visibility.Collapsed;


            total = 0;
            txtTotalprofits.Text = "0";
            txtTotal.Text = "0";
        }

        private void rbtnDebt_Checked(object sender, RoutedEventArgs e)
        {
            cbxPayType.IsEnabled = false;
            cbxCustomer.IsEnabled = true;

            cbxCaule.IsChecked = false;
            cbxCaule.IsEnabled = false;
            cmbCaule.IsEnabled = false;
            cmbPayType.IsEnabled = false;

            dgDebtBill.Visibility = Visibility.Visible;
            grdDebt.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbtnBuy.IsChecked == true)
                {
                    if (dgBuyBill.SelectedIndex != -1)
                    {
                        MainWindow.GetMainForm.Bill = Dt.Rows[dgBuyBill.SelectedIndex].ItemArray[0].ToString();
                        MainWindow.GetMainForm.list.SelectedIndex = 1;
                    }
                    else
                    {
                        MessageBox.Show("يجب تحديد عنصر من القائمة");
                    }

                }
                else if (rbtnSale.IsChecked == true)
                {
                    if (dgBill.SelectedIndex != -1)
                    {

                        MainWindow.GetMainForm.Bill = Dt.Rows[dgBill.SelectedIndex].ItemArray[0].ToString();
                        MainWindow.GetMainForm.list.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("يجب تحديد عنصر من القائمة");
                    }
                }
                else
                {
                    if (dgDebtBill.SelectedIndex != -1)
                    {

                        MainWindow.GetMainForm.Bill = Dt.Rows[dgDebtBill.SelectedIndex].ItemArray[0].ToString();
                        MainWindow.GetMainForm.list.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("يجب تحديد عنصر من القائمة");
                    }
                }

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        private void rbtnSale_Checked(object sender, RoutedEventArgs e)
        {
            cbxPayType.IsEnabled = true;
            cbxCustomer.IsEnabled = false;

            cbxCaule.IsChecked = false;
            cbxCaule.IsEnabled = false;
            cmbCaule.IsEnabled = false;
            cmbCustomer.IsEnabled = false;
            cbxCustomer.IsChecked = false;

            dgBuyBill.Visibility = Visibility.Collapsed;
            dgDebtBill.Visibility = Visibility.Collapsed;
            grdSales.Visibility = Visibility.Visible;
            grdDebt.Visibility = Visibility.Collapsed;
        }
    }
}
