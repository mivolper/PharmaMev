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
    /// Interaction logic for USC_Store.xaml
    /// </summary>
    public partial class USC_Store : UserControl
    {
        Flags.flags flag = new Flags.flags();
        Linq.PharmaLinqDataContext PharmaLinq;
        DataTable Dt = new DataTable();

        public USC_Store()
        {
            
            InitializeComponent();
            rbtnSearchName.IsChecked = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Dt = flag.Fill_DataGrid_join("SELECT Products.ID_Product,[Name],[Quantity],[UnitQuantity],[Units],Defective,EX_Date FROM[dbo].[Products]   inner join Groups on Groups.ID_Groups=Products.ID_Groups inner join Store on Store.ID_Product=Products.ID_Product Where Products.Exist = 'true'");
                dgProducts.DataContext = Dt;

                flag.Focus_txt(txtDefectiveProduct, txtSearchProduct);

                if (Properties.Settings.Default.UnitEnabled == false)
                {
                    UnitColumn.Visibility = Visibility.Collapsed;
                    UnitQuantityColumn.Visibility = Visibility.Collapsed;

                }

                if (Properties.Settings.Default.EX_DateEnabled == false)
                {
                    EX_DateColumn.Visibility = Visibility.Collapsed;
                }


                txtQuantity.Text = Properties.Settings.Default.Quantity;
                txtDate.Text = Properties.Settings.Default.Date;
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSaveDefective_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (dgProducts.SelectedIndex != -1)
                {

                    PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                    Linq.Store store = PharmaLinq.Stores.SingleOrDefault(item => item.ID_Product == Convert.ToInt32(Dt.Rows[dgProducts.SelectedIndex].ItemArray[0]));
                    Linq.Product product = PharmaLinq.Products.SingleOrDefault(item => item.ID_Product == Convert.ToInt32(Dt.Rows[dgProducts.SelectedIndex].ItemArray[0]));

                    try
                    {
                        int quantity = Convert.ToInt32(txtDefectiveProduct.Text);
                        txtDefectiveProduct.Focus();
                        if (btn.Content.ToString() == "اضافة")
                        {


                            product.Quantity -= quantity;
                            if (product.Quantity < 0)
                            {
                                if (product.BiggerUnit == "الثانوية" && product.UnitQuantity > 0)
                                {
                                    product.Quantity = (int)product.SubQuantity + product.Quantity;
                                    product.UnitQuantity--;
                                }
                                else
                                {
                                    quantity += product.Quantity;
                                    product.Quantity = 0;
                                }
                            }
                            store.Defective += quantity;
                            MessageBox.Show("تمت الاضافة");


                        }
                        else if (btn.Content.ToString() == "ازالة")
                        {
                            store.Defective -= quantity;

                            if (store.Defective < 0)
                            {
                                quantity += store.Defective;
                                store.Defective = 0;
                            }
                            if (product.BiggerUnit == "الثانوية" && quantity >= product.SubQuantity)
                            {
                                quantity += -(int)product.SubQuantity;
                                product.UnitQuantity++;
                            }
                            product.Quantity += quantity;

                            MessageBox.Show("تمت الازالة ");

                        }
                        PharmaLinq.SubmitChanges();


                    }
                    catch (Exception ex)
                    {
                        flag.Con.Close();
                        MessageBox.Show("يجب ملء هذا العنصر بعدد صحيح من دون فاصلة ");

                    }
                    UserControl_Loaded(sender, e);


                }
                else
                {
                    MessageBox.Show("يجب تحديد عنصر ");

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
                
                string text =" ";
                if (rbtnSearchName.IsChecked == true)
                {
                    text = "Name";
                }
                else if(rbtnSearchBarcode.IsChecked == true)
                {
                    text = "Barcode";
                }
                Dt = flag.Fill_DataGrid_join("SELECT Products.ID_Product,[Name],[Quantity],[UnitQuantity],[Units],Defective,EX_Date FROM[dbo].[Products]   inner join Groups on Groups.ID_Groups=Products.ID_Groups inner join Store on Store.ID_Product=Products.ID_Product Where Products.Exist = 'true' And " + text + " like '%'+ '" + txtSearchProduct.Text + "' +'%'");
                dgProducts.DataContext = Dt;
                dgProducts.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }

        }



        private void btInitializeDefective_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Linq.Store store = PharmaLinq.Stores.SingleOrDefault(item => item.ID_Product == Convert.ToInt32(Dt.Rows[dgProducts.SelectedIndex].ItemArray[0]));
                store.Defective = 0;
                PharmaLinq.SubmitChanges();
                UserControl_Loaded(sender, e);
                MessageBox.Show("تم التصفير ");

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSaveQuantity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    Convert.ToUInt32(txtQuantity.Text);
                }
                catch (Exception ex)
                {
                    txtQuantity.Focus();
                    MessageBox.Show("يجب ملء خذا العنصر برقم صحيح");
                }

                Properties.Settings.Default.Quantity = txtQuantity.Text;
                Properties.Settings.Default.Save();

                MessageBox.Show(" تم الحفط");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    Convert.ToUInt32(txtDate.Text);
                }
                catch(Exception ex)
                {
                    txtDate.Focus();
                    MessageBox.Show("يجب ملء خذا العنصر برقم صحيح");
                }
                Properties.Settings.Default.Date = txtDate.Text;
                Properties.Settings.Default.Save();

                MessageBox.Show(" تم الحفط");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSerchQuantity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dt = flag.Fill_DataGrid_join("SELECT Products.ID_Product,[Name],[Quantity],[UnitQuantity],[Units],Defective,EX_Date FROM[dbo].[Products]   inner join Groups on Groups.ID_Groups=Products.ID_Groups inner join Store on Store.ID_Product=Products.ID_Product Where Products.Exist = 'true' and Quantity <= '" + Properties.Settings.Default.Quantity + "'");
                dgProducts.DataContext = Dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSerchDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dt = flag.Fill_DataGrid_join("SELECT Products.ID_Product,[Name],[Quantity],[UnitQuantity],[Units],Defective,EX_Date FROM[dbo].[Products]   inner join Groups on Groups.ID_Groups=Products.ID_Groups inner join Store on Store.ID_Product=Products.ID_Product where Products.Exist='true' and TRY_CONVERT(date,EX_Date) <= '" + DateTime.Now.AddDays(Convert.ToDouble(Properties.Settings.Default.Date)).ToShortDateString() + "' ");
                dgProducts.DataContext = Dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
