﻿#pragma checksum "C:\Users\aksha\Documents\GitHub\dreamer_ar\Scribby\Canvas_Page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "881A2576BE37A5A5937AA94A0D84F0F4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Scribby
{
    partial class Canvas_Page : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.DrawingArea = (global::Windows.UI.Xaml.Controls.InkCanvas)(target);
                }
                break;
            case 2:
                {
                    this.Appbar = (global::Windows.UI.Xaml.Controls.AppBar)(target);
                }
                break;
            case 3:
                {
                    this.NextBar = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    #line 16 "..\..\..\Canvas_Page.xaml"
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.NextBar).Click += this.NextBar_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

