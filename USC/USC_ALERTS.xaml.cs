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
    /// Interaction logic for USC_ALERTS.xaml
    /// </summary>
    public partial class USC_ALERTS : UserControl
    {
        System.Windows.Media.Effects.Effect ef  ;
        Flags.flags flag = new Flags.flags();
        Linq.PharmaLinqDataContext PharmaLinq;
        DataTable DtDate = new DataTable();
        DataTable DtQuantity = new DataTable();

        public USC_ALERTS()
        {
            try
            {
                InitializeComponent();

                DtQuantity = flag.Fill_DataGrid_join("SELECT [ID_Product],[Name],[Quantity]  FROM [dbo].[Products] where exist='true' and Quantity <= '"+ Properties.Settings.Default.Quantity +"' and UnitQuantity ='0'");
                DtDate = flag.Fill_DataGrid_join("SELECT [ID_Product],[Name],[EX_Date]  FROM [dbo].[Products] where exist='true' and TRY_CONVERT(date,EX_Date) <= '" + DateTime.Now.AddDays(Convert.ToDouble(Properties.Settings.Default.Date)).ToShortDateString() + "' ");

                for (int i = 0; i < DtQuantity.Rows.Count; i++)
                {
                    Grid grd = new Grid { Height = 100, FlowDirection = FlowDirection.RightToLeft, Background = Brushes.White };

                    grd.MouseEnter += StackPanel_PreviewMouseMove;
                    grd.MouseLeave += stptst_MouseLeave;

                    Thickness th = new Thickness(20);
                    TextBlock txt = new TextBlock { FlowDirection = FlowDirection.RightToLeft, FontSize = 25, Text = (DtQuantity.Rows[i].ItemArray[1] +" : " + "  الكمية المتبقية  "  + DtQuantity.Rows[i].ItemArray[2] + "  قطعة"), Padding = th, FontFamily = txtx.FontFamily };
                    grd.Children.Add(txt);

                    ef = grd.Effect;
                    stp.Children.Add(grd);
                }

                for (int i = 0; i < DtDate.Rows.Count; i++)
                {
                    if (DtDate.Rows[i].ItemArray[2].ToString() == "")  continue; 
                    Grid grd = new Grid { Height = 100, FlowDirection = FlowDirection.RightToLeft, Background = Brushes.White };

                    grd.MouseEnter += StackPanel_PreviewMouseMove;
                    grd.MouseLeave += stptst_MouseLeave;

                    Thickness th = new Thickness(20);
                    TextBlock txt = new TextBlock { FlowDirection = FlowDirection.RightToLeft, FontSize = 25, Text = (DtDate.Rows[i].ItemArray[1] + " : " + "   تاريخ انتهاء الصلاحية   " + DtDate.Rows[i].ItemArray[2] ), Padding = th, FontFamily = txtx.FontFamily };
                    grd.Children.Add(txt);

                    ef = grd.Effect;
                    stp.Children.Add(grd);
                }
                //DateTime dt = DateTime.Now.AddDays(Convert.ToDouble(Properties.Settings.Default.Date));
                //MessageBox.Show(dt.ToShortDateString());

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StackPanel_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Grid stt = (Grid)sender;
            Thickness th = new Thickness(0, 10, 0, 10);
            stt.Background = Brushes.LightGray;
            stt.Height = 110;
            stt.Margin = th;

            try
            {
                System.Windows.Media.Effects.DropShadowEffect d = new System.Windows.Media.Effects.DropShadowEffect();
                d.ShadowDepth = 1;
                stt.Effect = d;

            }
            catch(Exception ex)
            {

            }
        }

        private void stptst_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid stt = (Grid)sender;
            Thickness th = new Thickness(0, 0, 0, 0);
            stt.Background = Brushes.White;
            stt.Height = 100;
            stt.Margin = th;

            stt.Effect = ef;
        }
    }
}
