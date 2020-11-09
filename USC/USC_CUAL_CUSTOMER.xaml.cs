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

namespace PharmaMev.USC
{
    /// <summary>
    /// Interaction logic for USC_CUAL_CUSTOMER.xaml
    /// </summary>
    public partial class USC_CUAL_CUSTOMER : UserControl
    {
        Flags.flags flag = new Flags.flags();
        Linq.PharmaLinqDataContext PharmaLinq;
        int CaulID,CustomerID = 0;

        void ReInitialize()
        {
            USC_CUAL_CUSTOMER frm = new USC_CUAL_CUSTOMER();
            MainWindow.GetMainForm.GridUsc.Children.Clear();
            MainWindow.GetMainForm.GridUsc.Children.Add(frm);
        }
        public USC_CUAL_CUSTOMER()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);

                flag.Fill_DataGrid(dgCual, PharmaLinq.Caules.Where(item => item.Exist == true));

                flag.Fill_DataGrid(dgCustomer, PharmaLinq.Customers.Where(item => item.Exist == true));

                flag.Focus_txt(txtNameCual, txtNameCustomer, txtPhoneCual, txtPhoneCustomer);
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //Caul Controls
        private void btnNewCual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtNameCual.Clear();
                txtPhoneCual.Clear();
                txtNameCual.Focus();

                txtNameCual.IsEnabled = true;
                txtPhoneCual.IsEnabled = true;

                btnSaveCual.IsEnabled = true;
                btnNewCual.IsEnabled = false;
                btnEditCual.IsEnabled = false;
                btnDeletCual.IsEnabled = false;
                dgCual.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

            }

        }

        private void btnSaveCual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Linq.Caule caul = new Linq.Caule();

                txtPhoneCual.Focus();
                txtPhoneCual.SelectAll();
                try
                {
                    if (txtPhoneCual.Text == "")
                    {

                    }
                    else caul.Phone = Convert.ToInt32(txtPhoneCual.Text);
                }
                catch (Exception ex)
                {
                    flag.Con.Close();
                    MessageBox.Show("يجب ملء هذا العنصر برقم صحيح");
                    return;
                }

                caul.CauleName = txtNameCual.Text;
                caul.Exist = true;

                PharmaLinq.Caules.InsertOnSubmit(caul);
                PharmaLinq.SubmitChanges();

                MessageBox.Show("تم الحفظ");
                flag.Fill_DataGrid(dgCual, PharmaLinq.Caules.Where(item => item.Exist == true));

                ReInitialize();
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);

            }

        }

        private void dgCual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgCual.SelectedIndex != -1)
                {
                    Linq.Caule caul = (Linq.Caule)dgCual.SelectedItem;

                    CaulID = caul.ID_Caule;

                    txtNameCual.IsEnabled = true;
                    txtPhoneCual.IsEnabled = true;

                    btnSaveCual.IsEnabled = false;
                    btnNewCual.IsEnabled = true;
                    btnEditCual.IsEnabled = true;
                    btnDeletCual.IsEnabled = true;

                    txtNameCual.Text = caul.CauleName;
                    txtPhoneCual.Text = caul.Phone.ToString();
                }

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);

            }
        }

        private void btnEditCual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CaulID != 0)
                {
                    if (MessageBox.Show("هل تريد تعديل العنصر المحدد؟", "تعديل", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        Linq.Caule caul = PharmaLinq.Caules.Single(item=> item.Exist==true && item.ID_Caule==CaulID);

                        txtPhoneCual.Focus();
                        txtPhoneCual.SelectAll();
                        try
                        {
                            if (txtPhoneCual.Text == "")
                            {

                            }
                            else caul.Phone = Convert.ToInt32(txtPhoneCual.Text);
                        }
                        catch (Exception ex)
                        {
                            flag.Con.Close();
                            MessageBox.Show("يجب ملء هذا العنصر برقم صحيح");
                            return;
                        }

                        caul.CauleName = txtNameCual.Text;
                        caul.Exist = true;

                        MessageBox.Show("تم التعديل");
                        PharmaLinq.SubmitChanges();

                        flag.Fill_DataGrid(dgCual, PharmaLinq.Caules.Where(item => item.Exist == true));
                        ReInitialize();
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

        private void btnDeletCual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgCual.SelectedIndex == 0)
                {
                    MessageBox.Show("لايمكن حذف هذا العنصر");
                    return;
                }
                if (CaulID != 0)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر المحدد؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        Linq.Caule caul = PharmaLinq.Caules.Single(item => item.Exist == true && item.ID_Caule == CaulID);

                        caul.Exist = false;
                        PharmaLinq.SubmitChanges();

                        MessageBox.Show("تم الحذف");
                        flag.Fill_DataGrid(dgCual, PharmaLinq.Caules.Where(item => item.Exist == true));

                        txtNameCual.Clear();
                        txtPhoneCual.Clear();
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

        //Caul Controls

        //Customer Controls
        private void btnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtNameCustomer.Clear();
                txtPhoneCustomer.Clear();
                txtNameCustomer.Focus();

                txtNameCustomer.IsEnabled = true;
                txtPhoneCustomer.IsEnabled = true;

                btnSaveCustomer.IsEnabled = true;
                btnNewCustomer.IsEnabled = false;
                btnEditCustomer.IsEnabled = false;
                btnDeletCustomer.IsEnabled = false;
                dgCustomer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

            }

        }

        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Linq.Customer customer = new Linq.Customer();

                txtPhoneCustomer.Focus();
                txtPhoneCustomer.SelectAll();
                try
                {
                    if (txtPhoneCustomer.Text == "")
                    {

                    }
                    else customer.CustomerPhone = Convert.ToInt32("0" + txtPhoneCustomer.Text);
                }
                catch (Exception ex)
                {
                    flag.Con.Close();
                    txtPhoneCustomer.Focus();
                    MessageBox.Show("يجب ملء هذا العنصر برقم صحيح");
                    return;
                }

                customer.CustomerName = txtNameCustomer.Text;
                customer.Exist = true;

                PharmaLinq.Customers.InsertOnSubmit(customer);
                PharmaLinq.SubmitChanges();

                MessageBox.Show("تم الحفظ");
                flag.Fill_DataGrid(dgCustomer, PharmaLinq.Customers.Where(item => item.Exist == true));

                ReInitialize();

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);

            }

        }

        private void dgCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgCustomer.SelectedIndex != -1)
                {
                    Linq.Customer customer = (Linq.Customer)dgCustomer.SelectedItem;

                    CustomerID = customer.ID_Customer;

                    txtNameCustomer.IsEnabled = true;
                    txtPhoneCustomer.IsEnabled = true;

                    btnSaveCustomer.IsEnabled = false;
                    btnNewCustomer.IsEnabled = true;
                    btnEditCustomer.IsEnabled = true;
                    btnDeletCustomer.IsEnabled = true;

                    txtNameCustomer.Text = customer.CustomerName;
                    txtPhoneCustomer.Text = customer.CustomerPhone.ToString();
                }

            }
            catch (Exception ex)
            {
                flag.Con.Close();
                MessageBox.Show(ex.Message);

            }

        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustomerID != 0)
                {
                    if (MessageBox.Show("هل تريد تعديل العنصر المحدد؟", "تعديل", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        Linq.Customer customer = PharmaLinq.Customers.Single(item => item.Exist == true && item.ID_Customer == CustomerID);

                        txtPhoneCustomer.Focus();
                        txtPhoneCustomer.SelectAll();
                        try
                        {
                            if (txtPhoneCustomer.Text == "")
                            {

                            }
                            else customer.CustomerPhone = Convert.ToInt32(txtPhoneCustomer.Text);
                        }
                        catch (Exception ex)
                        {
                            flag.Con.Close();
                            MessageBox.Show("يجب ملء هذا العنصر برقم صحيح");
                            return;
                        }

                        customer.CustomerName = txtNameCustomer.Text;
                        customer.Exist = true;

                        MessageBox.Show("تم التعديل");
                        PharmaLinq.SubmitChanges();

                        flag.Fill_DataGrid(dgCustomer, PharmaLinq.Customers.Where(item => item.Exist == true));
                        ReInitialize();
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

        private void btnDeletCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgCustomer.SelectedIndex == 0)
                {
                    MessageBox.Show("لايمكن حذف هذا العنصر");
                    return;
                }

                if (CustomerID != 0)
                {
                    if (MessageBox.Show("هل تريد حذف العنصر المحدد؟", "حذف", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                        Linq.Customer customer = PharmaLinq.Customers.Single(item => item.Exist == true && item.ID_Customer == CustomerID);

                        customer.Exist = false;
                        PharmaLinq.SubmitChanges();

                        MessageBox.Show("تم الحذف");
                        flag.Fill_DataGrid(dgCustomer, PharmaLinq.Customers.Where(item => item.Exist == true));

                        txtNameCustomer.Clear();
                        txtPhoneCustomer.Clear();
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


    }
}
