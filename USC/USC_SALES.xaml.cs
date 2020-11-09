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
using PharmaMev.Flags;
using PharmaMev.CRReport;

namespace PharmaMev.USC
{
    
    public partial class USC_SALES : UserControl
    {
        
        Linq.PharmaLinqDataContext PharmaLinq;
        flags flag = new flags();
        public int IDUser = new int();
        int quantity = 1;
        int dtcount = 0;
        double salePrice = 0;

        public static USC_SALES  usc;
        DataTable Dt = new DataTable();
        DataTable DelDt = new DataTable();
        DataTable QuantityDt = new DataTable();
        DataTable DtCustomer = new DataTable();

        public static TextBox txt { get; set; }

        bool isnew,plus,isnewitem = false;     
        double total = new double();

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


        public void Quantity_Checker(Linq.Product product,string unit)
        {
            int qua, product_qua, SubQuantity = new int();
            if (product.SubQuantity == null)
            {
                SubQuantity = 1;
            }
            else
            {
                SubQuantity = (int)product.SubQuantity;
            }
            if (unit == "قطعة")
            {            
                
                if (!isnew && dgSale.SelectedIndex < dtcount)
                {
                    if (product.BiggerUnit == "الثانوية")
                       product_qua = product.Quantity + (Convert.ToInt32(QuantityDt.Rows[dgSale.SelectedIndex].ItemArray[0]) * SubQuantity);

                    else
                        product_qua = product.Quantity + Convert.ToInt32(QuantityDt.Rows[dgSale.SelectedIndex].ItemArray[0]);

                }
                else
                {
                    product_qua = product.Quantity;
                }

                qua = (int)((product.UnitQuantity * SubQuantity) + product_qua);

                if (product.BiggerUnit == "الثانوية" )
                {
                    if (Convert.ToInt32(txtQuantity.Text) > qua)
                    {
                        MessageBox.Show("الكمية المتوفره في المخزن " + qua.ToString() + " قطعة");
                        txtQuantity.Text = qua.ToString();
                    }
                }
                else
                {
                    if (Convert.ToInt32(txtQuantity.Text) > product_qua)
                    {
                        MessageBox.Show("الكمية المتوفره في المخزن " + product_qua.ToString() + " قطعة");
                        txtQuantity.Text = product_qua.ToString();

                    }
                }
            }
            else
            {
                
                if (!isnew && dgSale.SelectedIndex < dtcount)
                {
                    product_qua = Convert.ToInt32(product.UnitQuantity) + Convert.ToInt32(QuantityDt.Rows[dgSale.SelectedIndex].ItemArray[0]);
                    qua = (int)(product.Quantity * SubQuantity) + Convert.ToInt32(QuantityDt.Rows[dgSale.SelectedIndex].ItemArray[0]);

                }
                else
                {
                    product_qua = Convert.ToInt32(product.UnitQuantity);
                    qua = (int)(product.Quantity * SubQuantity);

                }


                if (product.BiggerUnit == "الثانوية" )
                {
                    if (Convert.ToInt32(txtQuantity.Text) > product_qua)
                    {
                        MessageBox.Show("الكمية المتوفره في المخزن " + product_qua.ToString() + " " + product.Units);
                        txtQuantity.Text = product_qua.ToString();
                    }
                }
                else
                {
                    if (Convert.ToInt32(txtQuantity.Text) > qua)
                    {
                        MessageBox.Show("الكمية المتوفره في المخزن " + qua.ToString() + " " + product.Units);
                        txtQuantity.Text = qua.ToString();

                    }
                }
            }
            
        }

        public bool EXDate_Checker(Linq.Product product)
        {
            try
            {
                if (Convert.ToDateTime(product.EX_Date) <= txtSaleDate.SelectedDate)
                {
                    MessageBox.Show("لقد انتهت صلاحية هذا المنتج");
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public double Precentage(double price)
        {
                double precentage =  Convert.ToSingle(txtSalePrecentage.Text) ;
                double ttl = (price  * Convert.ToInt32(txtQuantity.Text)) - (float)Math.Round(precentage, 2);
                salePrice = Math.Round(salePrice - Convert.ToDouble(Dt.Rows[dgSale.SelectedIndex].ItemArray[7]) + ttl, 2);

                return Math.Round(ttl,2);

        }


        public USC_SALES()
        {
            InitializeComponent();
            txtSalePrecentage.Text = "0";
            MainWindow.GetMainForm.txtbarcode = txtBarcode;


        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                DtCustomer = flag.Fill_DataGrid_join("select CustomerName,ID_Customer from Customers where Exist = 'true'");

                string[] names = { "ID_Product" ,"Name", "Price", "Unit", "Quantity_SaleAction", "EX_Date", "SalePrecentage", "TotalPrice" };

                flag.Create_Columns(names,Dt);

                DelDt.Columns.Add("Del");

                QuantityDt.Columns.Add("Quantity");
                QuantityDt.Columns.Add("Unit");

                flag.Fill_ComboBox(DtCustomer, cmbCustomer);

                string[] items = { "نقدي", "شيك", "بطاقة ائتمانية" };
                cmbPayType.ItemsSource = items;
                cmbPayType.SelectedIndex = 0;

                cmbUnit.IsEnabled = Properties.Settings.Default.UnitEnabled;

                if (Properties.Settings.Default.UnitEnabled==false)
                {
                    UnitColumn.Visibility = Visibility.Collapsed;
                }

                if (Properties.Settings.Default.EX_DateEnabled == false)
                {
                    EX_DateColumn.Visibility = Visibility.Collapsed;
                }

                flag.Focus_txt(txtBarcode,txtSearchBill);

                
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

        private void btnNewSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);

                isnew = true;
                txtBarcode.IsEnabled = true;
                btnDeletProduct.IsEnabled = true;
                btnPrint.IsEnabled = true;
                txtBarcode.Clear();

                txtQuantity.Text="0";
                txtSalePrecentage.Text="0";
                txtPaid.Text="0";

                
                txtSaleDate.SelectedDate = DateTime.Now;
                txttime.Text = DateTime.Now.ToString("hh:mm tt");
                txtUser.Text = PharmaLinq.Users.Single(item => item.Exist == true && item.ID_User == IDUser).User_Name;
                txtSalePrecentage.Text = "0";
                salePrice = 0;
                try
                {
                    txtSaleID.Text = Convert.ToString(PharmaLinq.Sales.Where(item => item.Exist == true).Max(item => item.ID_Sale) + 1);
                }
                catch (InvalidOperationException ex)
                {
                    txtSaleID.Text = "1";
                }
                Dt.Clear();
               // btnEditProduct.IsEnabled = false;
                btnSaveSale.IsEnabled = true;
                salePrice = 0;
                txtSalePrice.Text = "0";
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        public void txtBarcode_KeyDown(object sender, RoutedEventArgs e)
        {
            try
            {
                
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);

                var product = PharmaLinq.Products.Single(item => item.Exist == true && item.BarCode == txtBarcode.Text);

                if(EXDate_Checker(product))return;

                total = Convert.ToSingle(product.Price) * quantity;
                txtPaid.IsEnabled = true;
                txtQuantity.IsEnabled = true;


                //for (int i = 0; i < Dt.Rows.Count; i++)
                //{
                //    if (product.ID_Product == Convert.ToInt32(Dt.Rows[i].ItemArray[0]))
                //    {
                //        txtQuantity.Text = (Convert.ToInt32(txtQuantity.Text) + 1).ToString();
                //        goto end;
                //    }
                //}

                salePrice =Math.Round( salePrice + total,2) ;
                txtSalePrice.Text = salePrice.ToString();
                Dt.Rows.Add(product.ID_Product, product.Name,product.Price, "قطعة", quantity, product.EX_Date,"0", Math.Round(total,2) );
                dgSale.DataContext = Dt;

                dgSale.SelectedIndex = dgSale.Items.Count - 1;
                cmbUnit.SelectedIndex = 0;

                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                flag.Con.Close();

            }
        }

        private void dgSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                if (dgSale.SelectedIndex != -1)
                {
                    PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);

                    isnewitem = true;

                    string[] Units = { "قطعة", PharmaLinq.Products.Single(item =>  item.ID_Product == Convert.ToInt32(Dt.Rows[dgSale.SelectedIndex].ItemArray[0])).Units };
                    cmbUnit.ItemsSource = Units;

                    txtQuantity.IsEnabled = true;
                    txtQuantity.Text = Dt.Rows[dgSale.SelectedIndex].ItemArray[4].ToString();
                    txtQuantity.Focus();

                    txtSalePrecentage.IsEnabled = true;
                    txtSalePrecentage.Text = Dt.Rows[dgSale.SelectedIndex].ItemArray[6].ToString();

                    if (Convert.ToString(Dt.Rows[dgSale.SelectedIndex].ItemArray[3]) == "قطعة")
                    {
                        cmbUnit.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbUnit.SelectedIndex = 1;
                    }

                    txtBarcode.Clear();
                    isnewitem = false;

                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }

        private void cmbUnit_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            try
            {
                if (cmbUnit.SelectedIndex < 0) goto skip;
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                var product = PharmaLinq.Products.Single(item =>  item.ID_Product == Convert.ToInt32(Dt.Rows[dgSale.SelectedIndex].ItemArray[0]));
                int txt;
                if (isnewitem)
                {
                    txt = 0;
                }
                else
                {
                    txt = Convert.ToInt32(txtQuantity.Text);
                }

                Quantity_Checker(product, cmbUnit.SelectedValue.ToString());

                if (cmbUnit.SelectedValue.ToString()== "قطعة")
                {
                    total = Precentage(product.Price);    
                }

                else
                {
                    total = Precentage((double)product.Unit_Price);

                }

                txtSalePrice.Text = salePrice.ToString();
                object[] temp = { Dt.Rows[dgSale.SelectedIndex].ItemArray[0], Dt.Rows[dgSale.SelectedIndex].ItemArray[1], cmbUnit.SelectedValue.ToString()== "قطعة"?product.Price:product.Unit_Price,cmbUnit.SelectedValue, Dt.Rows[dgSale.SelectedIndex].ItemArray[4], Dt.Rows[dgSale.SelectedIndex].ItemArray[5], Dt.Rows[dgSale.SelectedIndex].ItemArray[6], total  };
                flag.Edit_Dt(Dt, dgSale.SelectedIndex, temp);

                skip:;
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
                if (isnew || dgSale.SelectedIndex + 1 > dtcount)
                {
                    salePrice =Math.Round( salePrice - Convert.ToSingle(Dt.Rows[dgSale.SelectedIndex].ItemArray[7]),2);
                    Dt.Rows[dgSale.SelectedIndex].Delete();
                    txtSalePrice.Text = salePrice.ToString();
 
                    MessageBox.Show("تم الحذف");
                    txtBarcode.Clear();
                    txtQuantity.Text = "0";

                }
                else
                {
                    DelDt.Rows.Add(Dt.Rows[dgSale.SelectedIndex].ItemArray[8]);
                    MessageBox.Show("اضغط على زر حفظ الفاتورة لتتم عملية الحذف");
                    DataGridRow row = (DataGridRow)dgSale.ItemContainerGenerator.ContainerFromIndex(dgSale.SelectedIndex);
                    row.Background = Brushes.Tomato;

                }
                if (dgSale.SelectedIndex < 0)
                {
                    cmbUnit.ItemsSource = "";
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message );
            }
        }

        private void txtSaleID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                btnSaveSale.IsEnabled = true;
                btnDeletProduct.IsEnabled = true;
                btnPrint.IsEnabled = true;

                if (txtSaleID.Text != (PharmaLinq.Sales.Where(item => item.Exist == true).Max(item => item.ID_Sale) + 1).ToString())
                {
                    Linq.Sale sale = PharmaLinq.Sales.Single(item => item.Exist == true && item.ID_Sale == Convert.ToInt32(txtSaleID.Text));
                    txtBarcode.IsEnabled = true;
                    txtBarcode.Clear();
                    txtSaleDate.SelectedDate = sale.SaleDate;
                    txtUser.Text = PharmaLinq.Users.Single(item => item.Exist == true && item.ID_User == IDUser).User_Name;
                    salePrice =Math.Round( Convert.ToDouble(sale.SalePrice),2);
                    txtSalePrice.Text = salePrice.ToString();

                    Dt.Clear();
                    QuantityDt.Clear();
                    DelDt.Clear();
                    Dt = flag.Fill_DataGrid_join(" SELECT [SalesAction].ID_Product ,Products.Name ,SalesAction.Price ,[Unit] ,[Quantity_SaleAction],Products.EX_Date ,[SalePrecentage] ,[TotalPrice],ID_SaleAction FROM [dbo].[SalesAction] inner join Products on Products.ID_Product=SalesAction.ID_Product  where SalesAction.Exist ='true' And  ID_Sale = '" + txtSaleID.Text + "'");

                    for(int i = 0; i < Dt.Rows.Count; i++)
                    {
                        QuantityDt.Rows.Add(Dt.Rows[i].ItemArray[4], Dt.Rows[i].ItemArray[3]);

                    }

                    dgSale.DataContext = Dt;
                    dtcount = Dt.Rows.Count;
                    txtPaid.Text = sale.paid.ToString();
                    cmbCustomer.Text = PharmaLinq.Customers.SingleOrDefault(item => item.ID_Customer == sale.ID_Customer).CustomerName;
                    
                    for (int i = 0; i < cmbPayType.Items.Count; i++)
                    {
                        cmbPayType.SelectedIndex = i;

                        if (cmbPayType.SelectedValue.ToString() == sale.PayType)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }


        //textSalePrecentage
        private void txtSalePrecentage_TextChanged(object sender, TextChangedEventArgs e)                                                                   
        {
            
                try
                {
                    try
                    {
                        Convert.ToSingle(txtSalePrecentage.Text);
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("يجب ملء هذا العنصر برقم ");
                        txtSalePrecentage.Focus();
                        txtSalePrecentage.Text = "0";
                        txtSalePrecentage.SelectAll();
                        goto end;
                    }

                    if (dgSale.SelectedIndex != -1)
                {
                    PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                    var product = PharmaLinq.Products.Single(item =>  item.ID_Product == Convert.ToInt32(Dt.Rows[dgSale.SelectedIndex].ItemArray[0]));
                    double price = 0;
                    if (cmbUnit.Text=="قطعة")
                    {
                        price = product.Price;
                    }
                    else
                    {
                        price = (double)product.Unit_Price;
                    }
                    if (txtSalePrecentage.Text=="")
                    {
                        txtSalePrecentage.Text = "0";
                        
                    }
                    else if (Convert.ToSingle(txtSalePrecentage.Text) <= price * Convert.ToSingle(Dt.Rows[dgSale.SelectedIndex].ItemArray[4]))
                    {
                        total = Precentage(Convert.ToDouble(Dt.Rows[dgSale.SelectedIndex].ItemArray[2]));

                        txtSalePrice.Text = salePrice.ToString();

                        object[] temp = { Dt.Rows[dgSale.SelectedIndex].ItemArray[0], Dt.Rows[dgSale.SelectedIndex].ItemArray[1], Dt.Rows[dgSale.SelectedIndex].ItemArray[2], Dt.Rows[dgSale.SelectedIndex].ItemArray[3], Dt.Rows[dgSale.SelectedIndex].ItemArray[4], Dt.Rows[dgSale.SelectedIndex].ItemArray[5], Convert.ToSingle(txtSalePrecentage.Text),total };
                        flag.Edit_Dt(Dt, dgSale.SelectedIndex, temp);

                    }
                    else
                    {
                        txtSalePrecentage.Text = (price * Convert.ToSingle(Dt.Rows[dgSale.SelectedIndex].ItemArray[4])).ToString();
                        salePrice = Math.Round( salePrice - Convert.ToDouble(Dt.Rows[dgSale.SelectedIndex].ItemArray[7]),2);
                    }
                       
                }
                    end:;
                }
            catch (Exception ex)
            {
                
                flag.Con.Close();
                MessageBox.Show(ex.Message);
                
            }

        }

        private void txtSalePrecentage_GotMouseCapture(object sender, MouseEventArgs e)
        {
            txtSalePrecentage.SelectAll();

        }
        //textSalePrecentage


        //textQuantity
        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (dgSale.SelectedIndex != -1)
                {
                    try
                    {
                        Convert.ToInt32(txtQuantity.Text);
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("يجب ملء هذا العنصر برقم صحيح");
                        txtQuantity.Focus();
                        txtQuantity.Text = "0";
                        txtQuantity.SelectAll();
                        goto end;
                    }

                    PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                    var product = PharmaLinq.Products.Single(item =>  item.ID_Product == Convert.ToInt32(Dt.Rows[dgSale.SelectedIndex].ItemArray[0]));

                    Quantity_Checker(product, Dt.Rows[dgSale.SelectedIndex].ItemArray[3].ToString());




                    total = Precentage(Convert.ToDouble(Dt.Rows[dgSale.SelectedIndex].ItemArray[2]));

                    
                    txtSalePrice.Text = salePrice.ToString();

                    object[] temp = { Dt.Rows[dgSale.SelectedIndex].ItemArray[0], Dt.Rows[dgSale.SelectedIndex].ItemArray[1], Dt.Rows[dgSale.SelectedIndex].ItemArray[2], Dt.Rows[dgSale.SelectedIndex].ItemArray[3], txtQuantity.Text, Dt.Rows[dgSale.SelectedIndex].ItemArray[5], Dt.Rows[dgSale.SelectedIndex].ItemArray[6], total };
                    flag.Edit_Dt(Dt, dgSale.SelectedIndex, temp);

                    
                }
                else if (txtQuantity.Text == "0" || txtQuantity.Text == "")
                {
                    txtQuantity.Text = "";
                }
                else
                {
                    MessageBox.Show("يجب تحديد منتج من القائمة");

                }

                end:;
            }

            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }


        private void txtQuantity_GotMouseCapture(object sender, MouseEventArgs e)
        {
            txtQuantity.SelectAll();
        }
        //textQuantity


        //textPaid
        private void txtPaid_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToSingle(txtPaid.Text) <= Convert.ToSingle(txtSalePrice.Text)) 
                txtRemainder.Text = (Convert.ToSingle(txtSalePrice.Text) - Convert.ToSingle(txtPaid.Text)).ToString();
                else
                    txtPaid.Text = salePrice.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("يجب ملء هذا العنصر برقم ");
                txtPaid.Focus();
                txtPaid.Text = "0";
                txtPaid.SelectAll();
            }
        }

        private void txtPaid_GotMouseCapture(object sender, MouseEventArgs e)
        {
            txtPaid.SelectAll();

        }
        //textPaid


        #region Number's Buttons
        void buttons(string n)
        {
            try
            {
                if (txtSalePrecentage.IsFocused == true)
                {
                    if(plus || n == ".")
                        txtSalePrecentage.Text += n;
                    else
                        txtSalePrecentage.Text = n;

                }
                else if (txtPaid.IsFocused == true)
                {
                    if (plus || n == ".")
                        txtPaid.Text += n;
                    else
                        txtPaid.Text = n;
                }

                else if (txtQuantity.IsEnabled == true)
                {
                    if(plus || n == ".")
                        txtQuantity.Text += n;
                    else
                        txtQuantity.Text = n;
                }

                plus = false;
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            buttons("1");
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            buttons("2");
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            buttons("3");
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            buttons("4");
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            buttons("5");
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            buttons("6");
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            buttons("7");
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            buttons("8");
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            buttons("9");
        }

        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            buttons("0");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            plus = true;
        }

        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            buttons(".");
            plus = true;
        }
        #endregion

        private void txtSearchBill_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                txtSaleID.Text = PharmaLinq.Sales.Single(item => item.Exist == true && item.ID_Sale == Convert.ToInt32(txtSearchBill.Text)).ID_Sale.ToString();
                isnew = false;

                txtSalePrecentage.IsEnabled = true;
                txtPaid.IsEnabled = true;
            }
            catch (Exception ex)
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

                    txtSearchBill.Text = Convert.ToString(PharmaLinq.Sales.Where(item => item.Exist == true).Min(item => item.ID_Sale));
                }
                else if (txtSearchBill.Text == Convert.ToString(PharmaLinq.Sales.Where(item => item.Exist == true).Min(item => item.ID_Sale)))
                {

                }
                else
                {
                    txtSearchBill.Text = Convert.ToString(Convert.ToInt32(txtSearchBill.Text) - 1);
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

                    txtSearchBill.Text = Convert.ToString(PharmaLinq.Sales.Where(item => item.Exist == true).Max(item => item.ID_Sale));
                }
                else if (txtSearchBill.Text == Convert.ToString(PharmaLinq.Sales.Where(item => item.Exist == true).Max(item => item.ID_Sale)))
                {

                }
                else
                {
                    txtSearchBill.Text = Convert.ToString(Convert.ToInt32(txtSearchBill.Text) + 1);
                }
            }
            catch (Exception ex)
            {

            }


        }

        #region Save Buttons
        public void Save()
        {
            if (dgSale.Items.Count > 0)
            {


                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                int j = 0;

                Linq.Sale sale = new Linq.Sale();

                if (!isnew)
                {
                    if (DelDt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgSale.Items.Count; i++)
                        {
                            try
                            {
                                for (j = 0; j < DelDt.Rows.Count; j++)
                                {
                                    if (Convert.ToInt32(Dt.Rows[i].ItemArray[8]) == Convert.ToInt32(DelDt.Rows[j].ItemArray[0]))
                                    {
                                        salePrice -= Convert.ToDouble(Dt.Rows[i].ItemArray[7]);
                                        txtPaid.Text = (Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(Dt.Rows[i].ItemArray[7])).ToString();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                        }
                    }
                    sale = PharmaLinq.Sales.Single(item => item.Exist == true && item.ID_Sale == Convert.ToInt32(txtSaleID.Text));
                }


                for (int i = 0; i < Dt.Rows.Count; i++)
                {

                    Linq.SalesAction saleaction = new Linq.SalesAction();
                    Linq.Product product = PharmaLinq.Products.Single(item =>  item.ID_Product == Convert.ToInt32(Dt.Rows[i].ItemArray[0]));

                    if (!isnew && (i + 1) <= dtcount)
                    {
                        saleaction = PharmaLinq.SalesActions.Single(item => item.Exist == true && item.ID_Sale == Convert.ToInt32(txtSaleID.Text) && item.ID_SaleAction == Convert.ToInt32(Dt.Rows[i].ItemArray[8]));
                        if (QuantityDt.Rows[i].ItemArray[1].ToString() == "قطعة")
                        {
                            if (product.BiggerUnit == "الثانوية")
                            {
                                product.Quantity += (Convert.ToInt32(QuantityDt.Rows[i].ItemArray[0]) % (int)product.SubQuantity);
                                product.UnitQuantity += (Convert.ToInt32(QuantityDt.Rows[i].ItemArray[0]) / product.SubQuantity);

                                if (product.Quantity >= product.SubQuantity)
                                {
                                    product.Quantity -= (int)product.SubQuantity;
                                    product.UnitQuantity++;
                                }

                            }
                            else
                            {
                                product.Quantity += Convert.ToInt32(QuantityDt.Rows[i].ItemArray[0]);

                            }

                        }
                        else
                        {
                            if (product.BiggerUnit == "الثانوية")
                            {
                                product.UnitQuantity += Convert.ToInt32(QuantityDt.Rows[i].ItemArray[0]);
                            }
                            else
                            {
                                product.Quantity += (Convert.ToInt32(QuantityDt.Rows[i].ItemArray[0]) / (int)product.SubQuantity);
                                product.UnitQuantity += (Convert.ToInt32(QuantityDt.Rows[i].ItemArray[0]) % product.SubQuantity);

                                if (product.UnitQuantity >= product.SubQuantity)
                                {
                                    product.UnitQuantity -= (int)product.SubQuantity;
                                    product.Quantity++;
                                }
                            }

                        }
                        for (j = 0; j < DelDt.Rows.Count; j++)
                        {
                            if (saleaction.ID_SaleAction == Convert.ToInt32(DelDt.Rows[j].ItemArray[0]))
                            {
                                saleaction.Exist = false;
                                goto skip;
                            }
                        }

                    }



                    saleaction.ID_Sale = Convert.ToInt32(txtSaleID.Text);
                    saleaction.ID_Product = Convert.ToInt32(Dt.Rows[i].ItemArray[0]);
                    saleaction.Quantity_SaleAction = Convert.ToInt32(Dt.Rows[i].ItemArray[4]);
                    saleaction.Price = Convert.ToDecimal(Dt.Rows[i].ItemArray[2]);
                    saleaction.TotalPrice = Convert.ToDecimal(Dt.Rows[i].ItemArray[7]);
                    saleaction.SalePrecentage = Convert.ToInt32(Dt.Rows[i].ItemArray[6]);
                    saleaction.Unit = Convert.ToString(Dt.Rows[i].ItemArray[3]);
                    saleaction.Exist = true;

                    if (isnew || i + 1 > dtcount)
                    {
                        PharmaLinq.SalesActions.InsertOnSubmit(saleaction);
                    }
                    //Quantity Decrement
                    if (Dt.Rows[i].ItemArray[3].ToString() == "قطعة")
                    {
                        if (product.BiggerUnit == "الثانوية")
                        {
                            product.Quantity -= (Convert.ToInt32(Dt.Rows[i].ItemArray[4]) % (int)product.SubQuantity);
                            product.UnitQuantity -= (Convert.ToInt32(Dt.Rows[i].ItemArray[4]) / product.SubQuantity);

                            if (product.Quantity < 0)
                            {
                                product.Quantity += (int)product.SubQuantity;
                                product.UnitQuantity--;
                            }

                        }
                        else
                        {
                            product.Quantity -= Convert.ToInt32(Dt.Rows[i].ItemArray[4]);

                        }

                    }
                    else
                    {
                        if (product.BiggerUnit == "الثانوية")
                        {
                            product.UnitQuantity -= Convert.ToInt32(Dt.Rows[i].ItemArray[4]);
                        }
                        else
                        {
                            product.Quantity -= (Convert.ToInt32(Dt.Rows[i].ItemArray[4]) / (int)product.SubQuantity);
                            product.UnitQuantity -= (Convert.ToInt32(Dt.Rows[i].ItemArray[4]) % product.SubQuantity);

                            if (product.UnitQuantity < 0)
                            {
                                product.UnitQuantity += (int)product.SubQuantity;
                                product.Quantity--;
                            }
                        }

                    }
                    //End Quantity Decrement

                    skip:;
                    PharmaLinq.SubmitChanges();

                }



                sale.SalePrice = Convert.ToDecimal(salePrice);
                sale.Exist = true;
                sale.PayType = cmbPayType.Text;
                sale.paid = Convert.ToDecimal(txtPaid.Text);
                sale.ID_Customer = Convert.ToInt32(DtCustomer.Rows[cmbCustomer.SelectedIndex].ItemArray[1]);

                if (isnew)
                {
                    sale.ID_Sale = Convert.ToInt32(txtSaleID.Text);
                    sale.SaleDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                    sale.ID_User = IDUser;

                    PharmaLinq.Sales.InsertOnSubmit(sale);

                }
                PharmaLinq.SubmitChanges();

                MessageBox.Show("تم حفظ الفاتورة");



                USC_SALES usc_sale = new USC_SALES { IDUser = IDUser };
                MainWindow.GetMainForm.GridUsc.Children.Clear();
                MainWindow.GetMainForm.GridUsc.Children.Add(usc_sale);

            }
            else
            {
                MessageBox.Show("يجب اضافة منتجات");

            }

        }

        private void btnSaveSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Save();
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
                DataTable ReDT= flag.Fill_DataGrid_join("SELECT Products.Name,Users.User_Name,SalesAction.Price,[Quantity_SaleAction],[TotalPrice],Unit,SalesAction.ID_Sale FROM [dbo].[SalesAction] inner join Products on Products.ID_Product=SalesAction.ID_Product inner join Sales on Sales.ID_Sale=SalesAction.ID_Sale inner join Users on Users.ID_User=Sales.ID_User where SalesAction.Exist='true' and Sales.ID_Sale ='" + txtSaleID.Text + "'");
                Save();
                SaleBill Bill = new SaleBill();
                Bill.SetDataSource(ReDT);
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

        #endregion
    }
}
