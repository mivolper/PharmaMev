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
    /// Interaction logic for USC_KEYBOARd.xaml
    /// </summary>
    public partial class USC_KEYBOARd : UserControl
    {
        public USC_KEYBOARd()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                MainWindow.GetMainForm.txtSelected.Text += btn.Content;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                MainWindow.GetMainForm.txtSelected.Text = MainWindow.GetMainForm.txtSelected.Text.Remove(MainWindow.GetMainForm.txtSelected.Text.Length-1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            wrp2.Visibility = Visibility.Visible;
            wrp.Visibility = Visibility.Collapsed;

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            wrp2.Visibility = Visibility.Collapsed;
            wrp.Visibility = Visibility.Visible;
        }
    }
}
