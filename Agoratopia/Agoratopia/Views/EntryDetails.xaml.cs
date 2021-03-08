using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Agoratopia.ViewModels;

namespace Agoratopia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryDetails : ContentPage
    {
        public EntryDetails()
        {
            InitializeComponent();
        }

        public EntryDetails(int pkey)
        {
            InitializeComponent();

            Resources["EntryListData"] = new EntryViewModel(pkey);
        }


        async public void HelpSection(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HelpPage());
        }

        async private void GoHome(object sender, System.EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        async private void ToSettings(object sender, System.EventArgs e)
        {
            await DisplayAlert("Woah", "Sorry, I'm not ready yet!", "OK");
        }
    }
}