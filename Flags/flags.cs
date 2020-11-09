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
using System.Data.SqlClient;
using System.Data;
using PharmaMev.Properties;


namespace PharmaMev.Flags
{
    
    class flags
    {

        public SqlConnection Con = new SqlConnection(@"server  =" + Settings.Default.ServerName + "; Database =" + Settings.Default.DataBase +
                                                      "; Integrated security = false;" + "User ID =" + Settings.Default.User +
                                                      ";PassWord =" + Settings.Default.PassWord + "");

        //public SqlConnection Con = new SqlConnection(@"Data Source=SQL5034.site4now.net;Initial Catalog=DB_A63D81_PharmaMev;User Id=DB_A63D81_PharmaMev_admin;Password=015903570m;");

        SqlDataAdapter da;
        Thickness th = new Thickness(13, 13, 13, 13);
        SqlCommand cmd;

        public void Fill_DataGrid(DataGrid Dg,IQueryable Dt)
        {
            Dg.AutoGenerateColumns = false;
            Dg.DataContext = Dt;
        }

        public DataTable Fill_DataGrid_join(string procedur)
        {
            DataTable dt = new DataTable();
            da = new SqlDataAdapter(procedur, Con);
            da.Fill(dt);
            Con.Close();
            return dt;
        }

        public bool Null_Checker(TextBox txt)
        {
            if (txt.Text == "")
            {
                MessageBox.Show(" !!! لا يمكن ترك هذا العنصر فارغا ");
                txt.Focus();
            }

            return (txt.Text == "") ?  true: false;
        }

        public void Edit_Dt(DataTable dt,int index,params object[] temp)
        {
            dt.Rows[index].BeginEdit();
            dt.Rows[index].ItemArray = temp;
            dt.Rows[index].EndEdit();

        }
        public void Fill_ComboBox(DataTable Dt, ComboBox Cmb)
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Cmb.Items.Insert(i, Dt.Rows[i].ItemArray[0].ToString());
            }
            Cmb.SelectedIndex = 0;
        }

        public void Create_Columns(string[] names, DataTable Dt)
        {
            for (int i = 0; i < names.Length;i++)
            {
                Dt.Columns.Add(names[i]);

            }
        }

        public Button[] Add_Button(DataTable Dt,WrapPanel wrp)
        {
            Button[] btn1 = new Button[Dt.Rows.Count];
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Button btn = new Button();

                btn.Width = 190;
                btn.Height = 50;
                btn.Margin = th;
                btn.Content = Dt.Rows[i].ItemArray[0].ToString();
                btn.Name = "btn" + i.ToString();
                btn1[i] = btn;
                wrp.Children.Add(btn);
                if ( i % 3 == 0)
                {
                    wrp.Height += 76;
                }
            }
            return btn1;
        }

        public void BackUp(string path,string filename)
        {
            string txt = path + "\\" + filename + "  " + DateTime.Now.ToShortDateString().Replace('/', '-') + "  " + DateTime.Now.ToShortTimeString().Replace(':', '-');
            string txtquery = "Backup DataBase " + Settings.Default.DataBase + "  to Disk ='" + txt + ".bak'";
            cmd = new SqlCommand(txtquery,Con);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();

        }

        public void Restore(string path)
        {
            string txtquery = " Alter  DataBase " + Settings.Default.DataBase + " set Offline with Rollback immediate; Use master  Restore DataBase " + Settings.Default.DataBase + "  from Disk ='" + path + "' WITH REPLACE";
            cmd = new SqlCommand(txtquery, Con);
            Con.Open();
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public void Focus_txt(params TextBox[] txt)
        {
            foreach (TextBox t in txt)
            {
                t.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(MainWindow.GetMainForm.TxtSelected_PreviewMouseLeftButtonUp);
            }
        }

        public bool Dt_Comparer(DataTable Dt ,int  index ,string txt)
        {
            for(int i = 0; i < Dt.Rows.Count; i++)
            {
               if( Dt.Rows[i].ItemArray[index].ToString().Equals(txt))return true;
            }
            return false;
        }
    }
}
