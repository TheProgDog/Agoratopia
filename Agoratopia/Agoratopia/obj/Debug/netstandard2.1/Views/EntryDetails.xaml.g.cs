//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("Agoratopia.Views.EntryDetails.xaml", "Views/EntryDetails.xaml", typeof(global::Agoratopia.Views.EntryDetails))]

namespace Agoratopia.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\EntryDetails.xaml")]
    public partial class EntryDetails : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Agoratopia.ViewModels.EntryViewModel EntryData;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(EntryDetails));
            EntryData = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Agoratopia.ViewModels.EntryViewModel>(this, "EntryData");
        }
    }
}