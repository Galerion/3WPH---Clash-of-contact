﻿

#pragma checksum "C:\Users\Gaylerion\documents\visual studio 2013\Projects\ClashOfContact\ClashOfContact\UI\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F91450ED2FA2E068CEF4BE12ED23545A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClashOfContact
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 19 "..\..\..\UI\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Combat_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 20 "..\..\..\UI\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Combatant_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 21 "..\..\..\UI\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Liste_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 25 "..\..\..\UI\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.PinUnpinButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


