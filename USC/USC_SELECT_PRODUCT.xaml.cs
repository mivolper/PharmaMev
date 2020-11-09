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
using System.Data.SqlClient;

namespace PharmaMev.USC
{
    /// <summary>
    /// Interaction logic for USC_SELECT_PRODUCT.xaml
    /// </summary>
    public partial class USC_SELECT_PRODUCT : UserControl
    {

        Flags.flags flag = new Flags.flags();
        Linq.PharmaLinqDataContext PharmaLinq;
        DataTable Dt = new DataTable();
        public TextBox txtbarcode = new TextBox();

        public USC_SELECT_PRODUCT()
        {
            try
            {
                InitializeComponent();
                PharmaLinq = new Linq.PharmaLinqDataContext(flag.Con);
                Dt = flag.Fill_DataGrid_join("select GroupName from Groups where Exist = 'true'");

                Button[] btn1 = new Button[Dt.Rows.Count];
                btn1 = flag.Add_Button(Dt, wrp);

                for(int i = 0; i < Dt.Rows.Count; i++)
                {
                    btn1[i].Click += new System.Windows.RoutedEventHandler(this.b_Click);
                }
                wrpProduct.Visibility = Visibility.Collapsed;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void b_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Dt  = flag.Fill_DataGrid_join("select Name,BarCode from Products inner join Groups on Groups.ID_Groups=Products.ID_Groups where Products.Exist = 'true' and GroupName='" + btn.Content +"'");

                wrpProduct.Visibility = Visibility.Visible;
                wrp.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Visible;
                scv.Visibility = Visibility.Collapsed;
                scvProduct.Visibility = Visibility.Visible;

                Button[] btn1 = new Button[Dt.Rows.Count];
                btn1 = flag.Add_Button(Dt, wrpProduct);

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    btn1[i].Click += new System.Windows.RoutedEventHandler(this.b1_Click);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                for(int i = 0; i < Dt.Rows.Count; i++)
                {
                    if (Dt.Rows[i].ItemArray[0] == btn.Content)
                    {
                        if (txtbarcode.IsEnabled == true) 
                        {
                            txtbarcode.Text = Dt.Rows[i].ItemArray[1].ToString();


                            if (USC_SALES.usc != null && USC_SALES.usc.txtBarcode.IsEnabled == true)
                            {
                                    USC_SALES.usc.txtBarcode_KeyDown(sender, e);
                            }
                            else if (USC_BUYS.usc != null && USC_BUYS.usc.txtBarcode.IsEnabled == true)
                            {
                                    USC_BUYS.usc.txtBarcode_TextChanged(sender, e);
                            }
                            MainWindow.GetMainForm.btnAddClose.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            wrpProduct.Children.Clear();
            wrpProduct.Visibility = Visibility.Collapsed;
            wrp.Visibility = Visibility.Visible;
            btnBack.Visibility = Visibility.Collapsed;
            scvProduct.Visibility = Visibility.Collapsed;
            scv.Visibility = Visibility.Visible;

        }
    }
}
