﻿#pragma checksum "..\..\..\PromotionControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8AAB3C57EC53EA29DA04272523A0EE9BCFF86519"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ChessGame;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace ChessGame {
    
    
    /// <summary>
    /// PromotionControl
    /// </summary>
    public partial class PromotionControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\PromotionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridSelect;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\PromotionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Queen;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\PromotionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Knight;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\PromotionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Rook;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\PromotionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Bishop;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\PromotionControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnClose;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ChessGame;component/promotioncontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PromotionControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.GridSelect = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.Queen = ((System.Windows.Controls.Image)(target));
            
            #line 26 "..\..\..\PromotionControl.xaml"
            this.Queen.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Queen_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Knight = ((System.Windows.Controls.Image)(target));
            
            #line 27 "..\..\..\PromotionControl.xaml"
            this.Knight.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Knight_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Rook = ((System.Windows.Controls.Image)(target));
            
            #line 28 "..\..\..\PromotionControl.xaml"
            this.Rook.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Rook_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Bishop = ((System.Windows.Controls.Image)(target));
            
            #line 29 "..\..\..\PromotionControl.xaml"
            this.Bishop.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Bishop_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnClose = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\PromotionControl.xaml"
            this.BtnClose.Click += new System.Windows.RoutedEventHandler(this.Close);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

