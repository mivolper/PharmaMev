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

namespace PharmaMev.AdminUSC
{
    /// <summary>
    /// Interaction logic for USC_ADMIN.xaml
    /// </summary>
    public partial class USC_ADMIN : UserControl
    {
        public USC_ADMIN()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.Name=="btnClose")
                {
                    Admin.frm.Close();
                }
                if (txtUserName.Text=="miv" && txtPassWard.Password=="159357")
                {
                    USC_RESTORE usc = new USC_RESTORE();
                    Admin.frm.grdusc.Children.Clear();
                    Admin.frm.grdusc.Children.Add(usc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
