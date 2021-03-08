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
    public partial class Colors : ContentPage
    {
        public Colors()
        {
            InitializeComponent();
        }

        async public void HelpSection(object sender, System.EventArgs e)
        {
            await DisplayAlert("WOAH:", $"I was sent by {sender.ToString()} 1", "OK");
        }

        async private void GoHome(object sender, System.EventArgs e)
        {
            // Return back to home
            await Navigation.PopToRootAsync();
        }

        async private void ToSettings(object sender, System.EventArgs e)
        {
            await DisplayAlert("WOAH:", $"I was sent by {sender.ToString()} 3", "OK");
        }
    }
}