using Microsoft.Win32;
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

namespace PharmaMev.USC
{
    /// <summary>
    /// Interaction logic for USC_BACKUP.xaml
    /// </summary>
    public partial class USC_BACKUP : System.Windows.Controls.UserControl
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        Flags.flags flag = new Flags.flags();
        System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
        
        public USC_BACKUP()
        {
            try
            {
                InitializeComponent();
                txtServer.Text = Properties.Settings.Default.ServerName;
                txtDataBase.Text = Properties.Settings.Default.DataBase;
                txtUser.Text = Properties.Settings.Default.User;
                txtPassWoard.Text = Properties.Settings.Default.PassWord;
                txtPath.Text = Properties.Settings.Default.Path;
                cbxAuto.IsChecked = Properties.Settings.Default.AutoBackUp;
                cbxUnitEnabled.IsChecked = Properties.Settings.Default.UnitEnabled;
                cbxEX_DateEnabled.IsChecked = Properties.Settings.Default.EX_DateEnabled;

                flag.Focus_txt(txtDataBase, txtName, txtPassWoard, txtPath, txtServer, txtUser);
            }
            catch(Exception ex)
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
                System.Windows.Forms.MessageBox.Show("تم الحفظ", "", MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void btnPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = fbd.SelectedPath;
                    Properties.Settings.Default.Path = fbd.SelectedPath;
                    Properties.Settings.Default.Save();

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btnName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtName.Text = "PharmaMev";
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btnBackUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPath.Text == "" || txtName.Text == "")
                {
                    System.Windows.Forms.MessageBox.Show("يجب اختيار اسم و مسار الملف","",MessageBoxButtons.OK);
                }
                else
                {
                    flag.BackUp(txtPath.Text,txtName.Text);
                    System.Windows.Forms.MessageBox.Show("تم انشاء النسحة بنجاح", "", MessageBoxButtons.OK);

                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
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
                    System.Windows.Forms.MessageBox.Show("تم استعادة النسحة بنجاح", "", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void cbxAuto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.AutoBackUp = (bool)cbxAuto.IsChecked;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void cbxUnitEnabled_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.UnitEnabled = (bool)cbxUnitEnabled.IsChecked;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void cbxEX_DateEnabled_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.EX_DateEnabled = (bool)cbxEX_DateEnabled.IsChecked;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                flag.Con.Close();
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
