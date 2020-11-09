using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PharmaMev.AdminUSC
{
    /// <summary>
    /// Interaction logic for USC_RESTORE.xaml
    /// </summary>
    public partial class USC_RESTORE : System.Windows.Controls.UserControl
    {
        Flags.flags flag = new Flags.flags();
        System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();

        public USC_RESTORE()
        {
            try
            {
                InitializeComponent();
                txtServer.Text = Properties.Settings.Default.ServerName;
                txtDataBase.Text = Properties.Settings.Default.DataBase;
                txtUser.Text = Properties.Settings.Default.User;
                txtPassWoard.Text = Properties.Settings.Default.PassWord;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.ServerName = txtServer.Text;
                Properties.Settings.Default.DataBase = txtDataBase.Text;
                Properties.Settings.Default.User = txtUser.Text;
                Properties.Settings.Default.PassWord = txtPassWoard.Text;
                Properties.Settings.Default.Save();
                System.Windows.Forms.MessageBox.Show("تم الحفظ");

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    flag.Restore(ofd.FileName);
                    System.Windows.Forms.MessageBox.Show("تم استعادة النسحة بنجاح");
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
