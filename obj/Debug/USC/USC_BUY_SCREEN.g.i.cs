﻿#pragma checksum "..\..\..\USC\USC_BUY_SCREEN.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E2C048CC2AF21C1ECC8A267C7B98F4A2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using PharmaMev.USC;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PharmaMev.USC {
    
    
    /// <summary>
    /// USC_BUY_SCREEN
    /// </summary>
    public partial class USC_BUY_SCREEN : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackBill;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBillID;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBillDate;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBillUser;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel StackDetails;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBarcode;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCost;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtQuantity;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEXDate;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNewBuy;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveBuy;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditBuy;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeletBuy;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgProducts;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn IDProductsColumn;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn NameProductsColumn;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn GroupColumn;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn BarcodeColumn;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn CostColumn;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn QuantityColumn;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn UserColumn;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn TotalCostColumn;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDown;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\USC\USC_BUY_SCREEN.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUp;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PharmaMev;component/usc/usc_buy_screen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\USC\USC_BUY_SCREEN.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\USC\USC_BUY_SCREEN.xaml"
            ((PharmaMev.USC.USC_BUY_SCREEN)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.StackBill = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.txtBillID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtBillDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtBillUser = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.StackDetails = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.txtBarcode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtCost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtQuantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.txtEXDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.btnNewBuy = ((System.Windows.Controls.Button)(target));
            return;
            case 12:
            this.btnSaveBuy = ((System.Windows.Controls.Button)(target));
            return;
            case 13:
            this.btnEditBuy = ((System.Windows.Controls.Button)(target));
            return;
            case 14:
            this.btnDeletBuy = ((System.Windows.Controls.Button)(target));
            return;
            case 15:
            this.dgProducts = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 16:
            this.IDProductsColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 17:
            this.NameProductsColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 18:
            this.GroupColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 19:
            this.BarcodeColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 20:
            this.CostColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 21:
            this.QuantityColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 22:
            this.UserColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 23:
            this.TotalCostColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 24:
            this.btnDown = ((System.Windows.Controls.Button)(target));
            return;
            case 25:
            this.btnUp = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
