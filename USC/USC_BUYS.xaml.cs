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
    /// Interaction logic for USC_BUYS.xaml
    /// </summary>
    public partial class USC_BUYS : UserControl
    {
        Linq.PharmaLinqDataContext PharmaLinq;
        Flags.flags flag = new Flags.flags();
        public int IDUser = new int();
        int IDproduct = new int();
        DataTable Dt = new DataTable();
        DataTable DelDt = new DataTable();
        public static USC_BUYS usc;
        double buyCost = 0;
        bool isnew = false;
        double total = new double();
        int dtcount = 0;

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                usc = null;
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        public USC_BUYS()
        {
            try
            {
                InitializeComponent();
                MainWindow.GetMainForm.txtbarcode = txtBarcode;
                flag.Focus_txt(txtBarcode, txtCost, txtQuantity, txtSearchBill);
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable Dtcual = flag.Fill_DataGrid_join("select CauleName from Caule where Exist = 'true'");

                string[] names = { "ID_Buy", "ID_Product" , "Name" , "BarCode" , "Cost" , "Quantit_BuysAction" , "EX_Date" , "totatCost" };

                flag.Create_Columns(names, Dt);

                DelDt.Columns.Add("Del");

                flag.Fill_ComboBox(Dtcual, cmbCaule);

                if (!Properties.Settings.Default.EX_DateEnabled)
                {
                    EX_DateColumn.Visibility = Visibility.Collapsed;
                }
                usc = this;

                if (MainWindow.GetMainForm.Bill != "0")
                {

                    txtSearchBill.Text = MainWindow.GetMainForm.Bill;
                }
                MainWindow.GetMainForm.Bill = "0";

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        private void btnNewBuy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);

                isnew = true;
                txtBarcode.IsEnabled = true;
                txtBarcode.Clear();
                txtBarcode.Focus();
                txtBuyDate.SelectedDate = DateTime.Now;
                txtUser.Text = PharmaLinq.Users.Single(item => item.Exist == true && item.ID_User == IDUser).User_Name;
                buyCost = 0;
                txtbBuyCost.Text = "0";
                try
                {
                    txtBuyID.Text = Convert.ToString(PharmaLinq.Buys.Where(item => item.Exist == true).Max(item => item.ID_Buy) + 1);
                }
                catch (InvalidOperationException ex)
                {
                    txtBuyID.Text = "1";
                }
                Dt.Clear();
                btnEditProduct.IsEnabled = false;
                btnDeletProduct.IsEnabled = false;
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        public void txtBarcode_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                txtProductName.Clear();
                txtCost.Clear();
                txtQuantity.Clear();

                txtCost.IsEnabled = false;
                txtQuantity.IsEnabled = false;
                txtExDate.IsEnabled = false;

                txtProductName.Text = PharmaLinq.Products.Single(item => item.Exist == true && item.BarCode == txtBarcode.Text).Name;
                IDproduct = PharmaLinq.Products.Single(item => item.Exist == true && item.BarCode == txtBarcode.Text).ID_Product;

            }
            catch (Exception ex)
            {
                flag.Con.Close();              
            }
        }

        private void txtProductName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtProductName.Text != "")
            {
                txtCost.IsEnabled = true;
                txtQuantity.IsEnabled = true;
                txtExDate.IsEnabled = Properties.Settings.Default.EX_DateEnabled;
                btnAddProduct.IsEnabled = true;
            }
            else
            {
                btnAddProduct.IsEnabled = false;
            }
        }

        private void btnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            txtBarcode.Clear();
            txtProductName.Clear();
            txtCost.Clear();
            txtQuantity.Clear();
            txtBarcode.Focus();

            txtCost.IsEnabled = false;
            txtQuantity.IsEnabled = false;
            txtExDate.IsEnabled = false;
            btnEditProduct.IsEnabled = false;
            btnDeletProduct.IsEnabled = false;
            dgBill.SelectedIndex = -1;

        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtCost.Focus();
                txtCost.SelectAll();
                total = Convert.ToSingle(txtCost.Text);
                txtQuantity.Focus();
                txtQuantity.SelectAll();
                total =total * Convert.ToInt32(txtQuantity.Text);
                buyCost =buyCost + total;
                txtbBuyCost.Text = buyCost.ToString();
                Dt.Rows.Add(txtBuyID.Text, IDproduct, txtProductName.Text, txtBarcode.Text, txtCost.Text, txtQuantity.Text, txtExDate.Text, total);
                dgBill.DataContext = Dt;

                btnAddProduct.IsEnabled = false;
            }
            catch
            {
                MessageBox.Show("يجب ملء هذا العنصر برقم");
            }

        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                buyCost = buyCost - Convert.ToDouble(Dt.Rows[dgBill.SelectedIndex].ItemArray[7]);
                total = Convert.ToDouble(txtCost.Text);
                total = total * Convert.ToInt32(txtQuantity.Text);
                if (isnew)
                {
                    object[] a = { Dt.Rows[dgBill.SelectedIndex].ItemArray[0], Dt.Rows[dgBill.SelectedIndex].ItemArray[1], Dt.Rows[dgBill.SelectedIndex].ItemArray[2], Dt.Rows[dgBill.SelectedIndex].ItemArray[3], txtCost.Text, txtQuantity.Text, txtExDate.Text, total };

                    flag.Edit_Dt(Dt, dgBill.SelectedIndex, a);

                }
                else
                {
                    object[] a = { Dt.Rows[dgBill.SelectedIndex].ItemArray[0], Dt.Rows[dgBill.SelectedIndex].ItemArray[1], Dt.Rows[dgBill.SelectedIndex].ItemArray[2], Dt.Rows[dgBill.SelectedIndex].ItemArray[3], txtCost.Text, txtQuantity.Text, txtExDate.Text, total, Dt.Rows[dgBill.SelectedIndex].ItemArray[8] };
                    flag.Edit_Dt(Dt, dgBill.SelectedIndex, a);
                }
                buyCost = buyCost + total;
                txtbBuyCost.Text = buyCost.ToString();
                MessageBox.Show("تم التعديل");
                dgBill.DataContext = Dt;
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeletProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isnew || dgBill.SelectedIndex + 1 > dtcount)
                {
                    buyCost -= Convert.ToInt32(Dt.Rows[dgBill.SelectedIndex].ItemArray[7]);
                    Dt.Rows[dgBill.SelectedIndex].Delete();
                    txtbBuyCost.Text = buyCost.ToString();
                    MessageBox.Show("تم الحذف");
                }
                else
                {
                    DelDt.Rows.Add(Dt.Rows[dgBill.SelectedIndex].ItemArray[8]);
                    MessageBox.Show("اضغط على زر حفظ الفاتورة لتتم عملية الحذف");
                    DataGridRow row = (DataGridRow)dgBill.ItemContainerGenerator.ContainerFromIndex(dgBill.SelectedIndex);
                    row.Background = Brushes.Tomato;

                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveBuy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgBill.Items.Count > 0)
                {
                    PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                    int j = 0;

                    


                    Linq.Buy buy = new Linq.Buy();

                    if (isnew == false)
                    {
                        buyCost = 0;
                        for (int i = 0; i < dgBill.Items.Count; i++)
                        {
                            try
                            {
                                for (j = 0; j < DelDt.Rows.Count; j++)
                                {

                                    if (Convert.ToInt32(Dt.Rows[i].ItemArray[8]) == Convert.ToInt32(DelDt.Rows[j].ItemArray[0]))
                                    {
                                        
                                        goto skip;

                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            buyCost += Convert.ToDouble(Dt.Rows[i].ItemArray[7]);
                            skip:;
                        }

                        buy = PharmaLinq.Buys.Single(item => item.Exist == true && item.ID_Buy == Convert.ToInt32(txtBuyID.Text));
                        j = 0;
                    }

                    buy.BuyCost = Convert.ToDecimal(buyCost);
                    buy.ID_Caule = (int)PharmaLinq.Caules.SingleOrDefault(item => item.Exist == true && item.CauleName == cmbCaule.Text).ID_Caule;
                    buy.ID_User = IDUser;
                    buy.Exist = true;

                    for (int i = 0; i < dgBill.Items.Count; i++)
                    {
                        Linq.Product product = PharmaLinq.Products.SingleOrDefault(item =>  item.ID_Product == Convert.ToInt32(Dt.Rows[i].ItemArray[1]));
                        Linq.BuysAction buyaction = new Linq.BuysAction();

                        product.Cost = Convert.ToDouble(Dt.Rows[i].ItemArray[4]);
                        if (isnew == false && (i + 1) <= dtcount)
                        {
                            buyaction = PharmaLinq.BuysActions.Single(item => item.Exist == true && item.ID_Buy == Convert.ToInt32(txtBuyID.Text) && item.ID_BuyAction == Convert.ToInt32(Dt.Rows[i].ItemArray[8]));
                            if (product.BiggerUnit == "الثانوية")
                            {
                                product.Quantity -= ((int)PharmaLinq.BuysActions.Single(item => item.Exist == true && item.ID_Buy == Convert.ToInt32(txtBuyID.Text) && item.ID_BuyAction == Convert.ToInt32(Dt.Rows[i].ItemArray[8])).Quantit_BuysAction % (int)product.SubQuantity);
                                product.UnitQuantity -= ((int)PharmaLinq.BuysActions.Single(item => item.Exist == true && item.ID_Buy == Convert.ToInt32(txtBuyID.Text) && item.ID_BuyAction == Convert.ToInt32(Dt.Rows[i].ItemArray[8])).Quantit_BuysAction / (int)product.SubQuantity);

                                if (product.UnitQuantity<0)
                                {
                                    product.UnitQuantity = 0;
                                    product.Quantity = 0;
                                }

                                if (product.Quantity < 0)
                                {
                                        product.Quantity += (int)product.SubQuantity;
                                        product.UnitQuantity--;
                                }
                            }
                            else
                            {
                                product.Quantity -= (int)PharmaLinq.BuysActions.Single(item => item.Exist == true && item.ID_Buy == Convert.ToInt32(txtBuyID.Text) && item.ID_BuyAction == Convert.ToInt32(Dt.Rows[i].ItemArray[8])).Quantit_BuysAction;

                                if (product.Quantity<0)
                                {
                                    product.Quantity = 0;
                                }
                            }
                        }
                        try
                        {
                            for(j=0;j<DelDt.Rows.Count;j++)
                            if (Convert.ToInt32(Dt.Rows[i].ItemArray[8]) == Convert.ToInt32(DelDt.Rows[j].ItemArray[0]))
                            {
                                buyaction.Exist = false;                               
                                goto skip;
                            }
                        }
                        catch(Exception ex)
                        {

                        }
                        if (product.BiggerUnit == "الثانوية" )
                        {
                            product.Quantity += (Convert.ToInt32(Dt.Rows[i].ItemArray[5]) % (int)product.SubQuantity);
                            product.UnitQuantity += (Convert.ToInt32(Dt.Rows[i].ItemArray[5]) / product.SubQuantity);

                            if (product.Quantity >= product.SubQuantity)
                            {
                                product.Quantity -= (int)product.SubQuantity;
                                product.UnitQuantity++;
                            }
                        }
                        else
                        {
                            product.Quantity += Convert.ToInt32(Dt.Rows[i].ItemArray[5]);
                        }
                        product.EX_Date = Convert.ToString(Dt.Rows[i].ItemArray[6]);

                        buyaction.ID_Buy = Convert.ToInt32(Dt.Rows[i].ItemArray[0]);
                        buyaction.ID_Product = Convert.ToInt32(Dt.Rows[i].ItemArray[1]);
                        buyaction.Cost = Convert.ToDecimal(Dt.Rows[i].ItemArray[4]);
                        buyaction.Quantit_BuysAction = Convert.ToInt32(Dt.Rows[i].ItemArray[5]);
                        buyaction.EX_Date = Convert.ToString(Dt.Rows[i].ItemArray[6]);
                        buyaction.totatCost = Convert.ToDecimal(Dt.Rows[i].ItemArray[7]);
                        buyaction.Exist = true;

                        if (isnew==true || (i + 1) > dtcount)
                        {
                            PharmaLinq.BuysActions.InsertOnSubmit(buyaction);
                        }
                        skip:;
                        PharmaLinq.SubmitChanges();
                    }

                    if (isnew== true)
                    {
                        buy.ID_Buy = Convert.ToInt32(txtBuyID.Text);
                        buy.DateBuy = txtBuyDate.SelectedDate;
                        PharmaLinq.Buys.InsertOnSubmit(buy);
                    }

                    MessageBox.Show("تم حفظ الفاتورة");

                    PharmaLinq.SubmitChanges();
                    

                    USC_BUYS USC_Buy = new USC_BUYS
                    {
                        IDUser = IDUser
                    };
                    MainWindow.GetMainForm.GridUsc.Children.Clear();
                    MainWindow.GetMainForm.GridUsc.Children.Add(USC_Buy);
                }
                else
                {
                    MessageBox.Show("يجب اضافة مشتريات");

                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //selection
        private void dgBill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgBill.SelectedIndex != -1)
                {
                    txtBarcode.Text = Dt.Rows[dgBill.SelectedIndex].ItemArray[3].ToString();
                    txtCost.Text = Dt.Rows[dgBill.SelectedIndex].ItemArray[4].ToString();
                    txtQuantity.Text = Dt.Rows[dgBill.SelectedIndex].ItemArray[5].ToString();
                    txtExDate.Text = Dt.Rows[dgBill.SelectedIndex].ItemArray[6].ToString();
                    btnAddProduct.IsEnabled = false;
                    btnEditProduct.IsEnabled = true;
                    btnDeletProduct.IsEnabled = true;
                    txtQuantity.IsEnabled = true;
                    txtCost.IsEnabled = true;
                    txtExDate.IsEnabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dgBill_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.SelectedIndex != -1)
                {
                    txtBarcode.Text = Dt.Rows[dgBill.SelectedIndex].ItemArray[3].ToString();
                    txtCost.Text = Dt.Rows[dgBill.SelectedIndex].ItemArray[4].ToString();
                    txtQuantity.Text = Dt.Rows[dgBill.SelectedIndex].ItemArray[5].ToString();
                    txtExDate.Text = Dt.Rows[dgBill.SelectedIndex].ItemArray[6].ToString();
                    btnAddProduct.IsEnabled = false;
                    btnEditProduct.IsEnabled = true;
                    btnDeletProduct.IsEnabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //selection

        private void txtBuyID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                btnSaveBuy.IsEnabled = true;
                if(txtBuyID.Text != (PharmaLinq.Buys.Where(item => item.Exist == true).Max(item => item.ID_Buy) + 1).ToString())
                {
                    Linq.Buy buy = PharmaLinq.Buys.Single(item => item.Exist == true && item.ID_Buy == Convert.ToInt32(txtBuyID.Text));
                    txtBarcode.IsEnabled = true;
                    txtBarcode.Clear();
                    txtBuyDate.SelectedDate = buy.DateBuy ;
                    txtUser.Text = PharmaLinq.Users.Single(item => item.Exist == true && item.ID_User == IDUser).User_Name;
                    buyCost = Convert.ToDouble(buy.BuyCost);
                    txtbBuyCost.Text = buyCost.ToString();

                    DelDt.Clear();
                    Dt.Clear();
                    Dt = flag.Fill_DataGrid_join("SELECT  [ID_Buy],BuysAction.ID_Product,Products.Name,Products.BarCode,[BuysAction].Cost,[Quantit_BuysAction],[BuysAction].EX_Date,[totatCost],ID_BuyAction FROM [dbo].[BuysAction] inner join Products on Products.ID_Product=BuysAction.ID_Product where BuysAction.Exist ='true' And  ID_Buy = '" + txtBuyID.Text + "'");
                    dgBill.DataContext = Dt;
                    dtcount = Dt.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                txtBuyID.Text = PharmaLinq.Buys.Single(item=>item.Exist==true && item.ID_Buy==Convert.ToInt32(txtSearchBill.Text)).ID_Buy.ToString();
                isnew = false;
            }
            catch(Exception ex)
            {
                flag.Con.Close();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                if (txtSearchBill.Text == "")
                {
                    
                    txtSearchBill.Text = Convert.ToString(PharmaLinq.Buys.Where(item => item.Exist == true).Min(item => item.ID_Buy));
                }
                else if (txtSearchBill.Text ==  Convert.ToString(PharmaLinq.Buys.Where(item => item.Exist == true).Min(item => item.ID_Buy)))
                {

                }
                else
                {
                    txtSearchBill.Text = Convert.ToString(Convert.ToInt32(txtSearchBill.Text)-1);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                if (txtSearchBill.Text == "")
                {

                    txtSearchBill.Text = Convert.ToString(PharmaLinq.Buys.Where(item => item.Exist == true).Max(item => item.ID_Buy));
                }
                else if (txtSearchBill.Text == Convert.ToString(PharmaLinq.Buys.Where(item => item.Exist == true).Max(item => item.ID_Buy)))
                {

                }
                else
                {
                    txtSearchBill.Text = Convert.ToString(Convert.ToInt32(txtSearchBill.Text) + 1);
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
