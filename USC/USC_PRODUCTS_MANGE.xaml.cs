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
    /// Interaction logic for USC_PRODUCTS_MANGE.xaml
    /// </summary>
    public partial class USC_PRODUCTS_MANGE : UserControl
    {
        Flags.flags flag = new Flags.flags();
        Linq.PharmaLinqDataContext PharmaLinq;
        DataTable DtProduct, DtGroup = new DataTable();        
        int IDProduct = 0;
        public int IDUser = new int();
       
        public void Initil()
        {
            USC.USC_PRODUCTS_MANGE USC = new USC_PRODUCTS_MANGE();

            MainWindow.GetMainForm.GridUsc.Children.Clear();
            MainWindow.GetMainForm.GridUsc.Children.Add(USC);
        }
        public USC_PRODUCTS_MANGE()
        {
            InitializeComponent();
            rbtnSearchName.IsChecked = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DtProduct= flag.Fill_DataGrid_join("SELECT [ID_Product],[Name],[BarCode],[Cost],[Price],[Units],[Unit_Price],[SubQuantity],[BiggerUnit],[Products].Exist,[EX_Date],[GroupName] FROM[dbo].[Products]   inner join Groups on Groups.ID_Groups=Products.ID_Groups Where Products.Exist = 'true'");
                dgProducts.DataContext = DtProduct;

                DtGroup = flag.Fill_DataGrid_join( "SELECT * FROM [dbo].[Groups] where Groups.Exist = 'true'");

                for (int i = 0; i < DtGroup.Rows.Count; i++)
                {
                    cmbGroup.Items.Insert(i, DtGroup.Rows[i].ItemArray[1].ToString());
                }
                cmbGroup.SelectedIndex = 0;
                cmbGroup.IsReadOnly = true;
                GridMainDatails.IsEnabled = false;
                GridUnitDatails.IsEnabled = false;
                btnSaveProduct.IsEnabled = false;

                rbtnSearchEX_Date.IsEnabled = Properties.Settings.Default.EX_DateEnabled;
                if (Properties.Settings.Default.UnitEnabled == false)
                {
                    UnitsColumn.Visibility = Visibility.Collapsed;
                    UnitPrieceColumn.Visibility = Visibility.Collapsed;
                    UnitPrecentageColumn.Visibility = Visibility.Collapsed;
                    UnitBiggerColumn.Visibility = Visibility.Collapsed;

                }

                if (Properties.Settings.Default.EX_DateEnabled == false)
                {
                    EX_DateColumn.Visibility = Visibility.Collapsed;
                }

                flag.Focus_txt(txtBarcode, txtCost, txtNameProduct, txtNameProduct, txtPriece, txtSearchProduct, txtUnit, txtUnitPrecentage, txtUnitPriece);
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dgProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(dgProducts.SelectedIndex != -1)
                {
                    IDProduct = (int)DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[0];
                    txtNameProduct.Text = DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[1].ToString();
                    txtBarcode.Text = DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[2].ToString();
                    txtCost.Text = DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[3].ToString();
                    txtPriece.Text = DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[4].ToString();
                    txtEX_Date.Text= DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[10].ToString();

                    for (int i=0;i<DtGroup.Rows.Count;i++)
                    {
                        if (DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[11].ToString() == DtGroup.Rows[i].ItemArray[1].ToString())
                        {
                            cmbGroup.SelectedIndex = i;
                        }
                    }
                    txtUnit.Text = DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[5].ToString();
                    txtUnitPriece.Text = DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[6].ToString();
                    txtUnitPrecentage.Text = DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[7].ToString();
                    if (DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[8].ToString() == "الأساسية")
                    {
                        rbtnMain.IsChecked = true;
                    }else if (DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[8].ToString() == "الثانوية")
                    {
                        rbtnSub.IsChecked = true;
                    }
                    else
                    {
                        rbtnMain.IsChecked = false;
                        rbtnSub.IsChecked = false;
                    }
                    GridMainDatails.IsEnabled = true;
                    txtEX_Date.IsEnabled = Properties.Settings.Default.EX_DateEnabled;
                    GridUnitDatails.IsEnabled = Properties.Settings.Default.UnitEnabled;
                    btnSaveProduct.IsEnabled = false;
                    btnNewProduct.IsEnabled = true;
                    btnEditProduct.IsEnabled = true;
                    btnDeletProduct.IsEnabled = true;

                }
            }
            catch(Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNewProduct_Click(object sender, RoutedEventArgs e)
        {

            txtNameProduct.Clear();
            txtBarcode.Clear();
            txtCost.Clear();
            txtPriece.Clear();
            cmbGroup.SelectedItem = 0;
            txtUnit.Clear();
            txtUnitPriece.Clear();
            txtUnitPrecentage.Clear();
            txtEX_Date.Text = "";
            rbtnMain.IsChecked = false;
            rbtnSub.IsChecked = false;
            btnSaveProduct.IsEnabled = true;
            btnNewProduct.IsEnabled = false;
            btnEditProduct.IsEnabled = false;
            btnDeletProduct.IsEnabled = false;
            cmbGroup.SelectedIndex = 0;
            GridMainDatails.IsEnabled = true;
            txtEX_Date.IsEnabled = Properties.Settings.Default.EX_DateEnabled;
            GridUnitDatails.IsEnabled = Properties.Settings.Default.UnitEnabled;
            dgProducts.SelectedIndex = -1;
        }

        private void btnDeletProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDProduct != 0)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر المحدد؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        var temp = PharmaLinq.Products.Single(item => item.ID_Product == IDProduct);
                        temp.Exist = false;
                        PharmaLinq.SubmitChanges();
                        MessageBox.Show("تم الحذف");
                        UserControl_Loaded(sender, e);

                        Initil();
                        IDProduct = 0;
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

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IDProduct != 0)
                {
                    if (MessageBox.Show("هل تريد تعديل العنصر المحدد؟", "تعديل", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        Linq.Product product = PharmaLinq.Products.Single(item => item.ID_Product == IDProduct);


                        if (flag.Null_Checker(txtNameProduct) || flag.Null_Checker(txtBarcode) ||  flag.Null_Checker(txtPriece))
                        {
                            goto end;
                        }


                        try
                        {
                            txtPriece.Focus();
                            txtPriece.SelectAll();
                            product.Price = Convert.ToSingle(txtPriece.Text);
                            if (txtUnitPriece.Text != "")
                            {
                                txtUnitPriece.Focus();
                                txtUnitPriece.SelectAll();
                                product.Unit_Price = Convert.ToSingle(txtUnitPriece.Text);

                            }

                            if (txtCost.Text != "")
                            {
                                txtCost.Focus();
                                txtCost.SelectAll();
                                product.Cost = Convert.ToSingle(txtCost.Text);

                            }

                        }
                        catch
                        {
                            flag.Con.Close();
                            MessageBox.Show("يجب ملء هذا العنصر بعدد");
                            goto end;
                        }

                        try
                        {
                            if (txtUnitPrecentage.Text != "")
                            {
                                txtUnitPrecentage.Focus();
                                txtUnitPrecentage.SelectAll();
                                product.SubQuantity = Convert.ToInt32(txtUnitPrecentage.Text);
                            }


                        }
                        catch
                        {
                            flag.Con.Close();
                            MessageBox.Show("يجب ملء هذا العنصر بعدد صحيح من دون فاصلة ");
                            goto end;
                        }

                        if (DtProduct.Rows[dgProducts.SelectedIndex].ItemArray[2].ToString()==txtBarcode.Text)
                        {

                        }
                        else if (flag.Dt_Comparer(DtProduct, 2, txtBarcode.Text))
                        {

                            MessageBox.Show("هذا الباركود موجود");
                            txtBarcode.Focus();
                            txtBarcode.SelectAll();
                            return;
                        }

                        product.Name = txtNameProduct.Text;
                        product.BarCode = txtBarcode.Text;
                        product.ID_Groups = PharmaLinq.Groups.Single(item =>item.Exist==true && item.GroupName == cmbGroup.Text).ID_Groups;
                        product.Exist = true;
                        product.EX_Date = txtEX_Date.Text;
                        product.Units = txtUnit.Text;
                        product.ID_User = IDUser;

                        //Bigger Unit
                        if (rbtnMain.IsChecked == true)
                        {
                            product.BiggerUnit = "الأساسية";
                        }
                        else if (rbtnSub.IsChecked == true)
                        {
                            product.BiggerUnit = "الثانوية";
                        }
                        else
                        {
                            product.BiggerUnit = "";
                        }


                        PharmaLinq.SubmitChanges();

                        MessageBox.Show("تم التعديل");
                        UserControl_Loaded(sender, e);

                        cmbGroup.SelectedIndex = 0;
                        Initil();
                        end:;
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

        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            txtQuantity.Text = "0";
            brdQuantity.Visibility = Visibility.Visible;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Linq.Product product = new Linq.Product();


                if (flag.Null_Checker(txtNameProduct) || flag.Null_Checker(txtBarcode) || flag.Null_Checker(txtPriece))
                {
                    goto end;
                }


                try
                {
                    txtPriece.Focus();
                    txtPriece.SelectAll();
                    product.Price = Convert.ToSingle(txtPriece.Text);
                    if (txtUnitPriece.Text != "")
                    {
                        txtUnitPriece.Focus();
                        txtUnitPriece.SelectAll();
                        product.Unit_Price = Convert.ToSingle(txtUnitPriece.Text);

                    }

                    if (txtCost.Text != "")
                    {
                        txtCost.Focus();
                        txtCost.SelectAll();
                        product.Cost = Convert.ToSingle(txtCost.Text);

                    }

                }
                catch
                {
                    flag.Con.Close();
                    MessageBox.Show("يجب ملء هذا العنصر بعدد");
                    goto end;
                }


                try
                {
                    if (txtUnitPrecentage.Text != "")
                    {
                        txtUnitPrecentage.Focus();
                        txtUnitPrecentage.SelectAll();
                        product.SubQuantity = Convert.ToInt32(txtUnitPrecentage.Text);
                    }

                    txtQuantity.Focus();
                    txtQuantity.SelectAll();
                    product.Quantity = Convert.ToInt32(txtQuantity.Text);
                }
                catch
                {
                    flag.Con.Close();
                    MessageBox.Show("يجب ملء هذا العنصر بعدد صحيح من دون فاصلة ");
                    goto end;
                }

                if (flag.Dt_Comparer(DtProduct, 2, txtBarcode.Text))
                {

                    MessageBox.Show("هذا الباركود موجود");
                    txtBarcode.Focus();
                    txtBarcode.SelectAll();
                    return;
                }
                product.Name = txtNameProduct.Text;
                product.BarCode = txtBarcode.Text;
                product.ID_Groups = PharmaLinq.Groups.SingleOrDefault(item => item.Exist == true && item.GroupName == cmbGroup.Text).ID_Groups;
                product.Exist = true;
                product.EX_Date = txtEX_Date.Text;
                product.Units = txtUnit.Text;
                product.ID_User = IDUser;
                product.UnitQuantity = 0;
                //Bigger Unit
                if (rbtnMain.IsChecked == true)
                {
                    product.BiggerUnit = "الأساسية";
                }
                else if (rbtnSub.IsChecked == true)
                {
                    product.BiggerUnit = "الثانوية";
                }
                else
                {
                    product.BiggerUnit = "";
                }



                PharmaLinq.Products.InsertOnSubmit(product);
                PharmaLinq.SubmitChanges();

                Linq.Store store = new Linq.Store();
                store.ID_Product = product.ID_Product;
                store.Defective = 0;

                PharmaLinq.Stores.InsertOnSubmit(store);
                PharmaLinq.SubmitChanges();

                MessageBox.Show("تم الحفظ");
                UserControl_Loaded(sender, e);

                //btnSaveProduct.IsEnabled = false;
                //btnNewProduct.IsEnabled = true;
                //cmbGroup.SelectedIndex = 0;
                //btnEditProduct.IsEnabled = true;
                //btnDeletProduct.IsEnabled = true;
                Initil();

                end:;
            }
            catch (Exception ex)
            {


                flag.Con.Close();
                MessageBox.Show(ex.Message);

            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            brdQuantity.Visibility = Visibility.Collapsed;
        }

        //Searsh Screen Details
        private void txtSearchProduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = " ";
            try
            {
                if (txtSearchProduct.Text == "")
                {
                    DtProduct = flag.Fill_DataGrid_join("SELECT [ID_Product],[Name],[BarCode],[Cost],[Price],[Units],[Unit_Price],[SubQuantity],[BiggerUnit],[Products].Exist,[EX_Date],[GroupName] FROM[dbo].[Products]   inner join Groups on Groups.ID_Groups=Products.ID_Groups Where Products.Exist = 'true'");
                    dgProducts.DataContext = DtProduct;
                    return;
                }
                else if (rbtnSearchName.IsChecked == true)
                {
                    text = "Name";
                }
                else if (rbtnSearchBarcode.IsChecked == true)
                {
                    text = "Barcode";
                }
                else if (rbtnSearchGroup.IsChecked == true)
                {
                    text = "GroupName";
                }
                else if (rbtnSearchEX_Date.IsChecked == true)
                {
                    text = "EX_Date";
                }

                dgProducts.DataContext = flag.Fill_DataGrid_join("SELECT [ID_Product],[Name],[BarCode],[Cost],[Price],[Units],[Unit_Price],[SubQuantity],[BiggerUnit],[Products].Exist,[EX_Date],[GroupName] FROM[dbo].[Products]   inner join Groups on Groups.ID_Groups=Products.ID_Groups Where Products.Exist = 'true' And " + text + " like '%'+ '" + txtSearchProduct.Text + "' +'%'");

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

    }
}
