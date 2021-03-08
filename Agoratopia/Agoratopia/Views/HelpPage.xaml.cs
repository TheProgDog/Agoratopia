using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Agoratopia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        async public void HelpSection(object sender, System.EventArgs e)
        {
            // Go back to the last page
            await Navigation.PopAsync();
        }

        async private void GoHome(object sender, System.EventArgs e)
        {
            // Return back to home
            await Navigation.PopToRootAsync();
        }

        async private void ToSettings(object sender, System.EventArgs e)
        {
            await DisplayAlert("Woah", "Sorry, I'm not ready yet!", "OK");
        }
    }
}