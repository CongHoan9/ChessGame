﻿#pragma checksum "..\..\..\..\BoardItems\Board.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DB756E247B631D64CD7D8A274E9EEB2F3E8EE5D9"
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
    /// Board
    /// </summary>
    public partial class Board : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\BoardItems\Board.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Boardd;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\BoardItems\Board.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl OverlayGrid;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\BoardItems\Board.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl HoverGrid;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\BoardItems\Board.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl SquareGrid;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\..\BoardItems\Board.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl HintGrid;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\..\..\BoardItems\Board.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl ArrowCanvas;
        
        #line default
        #line hidden
        
        
        #line 188 "..\..\..\..\BoardItems\Board.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl IconGrid;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\..\..\BoardItems\Board.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas MoveCanvas;
        
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
            System.Uri resourceLocater = new System.Uri("/ChessGame;component/boarditems/board.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\BoardItems\Board.xaml"
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
            this.Boardd = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.OverlayGrid = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 3:
            this.HoverGrid = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 4:
            this.SquareGrid = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 5:
            this.HintGrid = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 6:
            this.ArrowCanvas = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 7:
            this.IconGrid = ((System.Windows.Controls.ItemsControl)(target));
            return;
            case 8:
            this.MoveCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

