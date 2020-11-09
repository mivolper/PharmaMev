﻿#pragma checksum "..\..\..\AdminUSC\USC_RESTORE.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2C4DDC72A91C9DE1C0A40748995862FDD47A90339B72CAA1A4CDA091C47337A5"
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
using PharmaMev.AdminUSC;
using RootLibrary.WPF.Localization;
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


namespace PharmaMev.AdminUSC {
    
    
    /// <summary>
    /// USC_RESTORE
    /// </summary>
    public partial class USC_RESTORE : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\AdminUSC\USC_RESTORE.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtServer;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\AdminUSC\USC_RESTORE.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDataBase;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\AdminUSC\USC_RESTORE.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUser;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\AdminUSC\USC_RESTORE.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPassWoard;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\AdminUSC\USC_RESTORE.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\AdminUSC\USC_RESTORE.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRestore;
        
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
            System.Uri resourceLocater = new System.Uri("/PharmaMev;component/adminusc/usc_restore.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AdminUSC\USC_RESTORE.xaml"
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
            this.txtServer = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtDataBase = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtUser = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtPassWoard = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\AdminUSC\USC_RESTORE.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnRestore = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\AdminUSC\USC_RESTORE.xaml"
            this.btnRestore.Click += new System.Windows.RoutedEventHandler(this.btnRestore_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

