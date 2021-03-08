using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;

using Agoratopia.Database;
using Agoratopia.ViewModels;

namespace Agoratopia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var date = DateTime.Today.ToShortDateString();
            var hour = DateTime.Now.Hour;
            var timeOfDay = "";
            var name = "";

            if (hour < 12)
                timeOfDay = "morning";
            else if (hour < 18)
                timeOfDay = "afternoon";
            else
                timeOfDay = "evening";

            // Get name here
            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
            {
                if (conn.Table<Settings>() != null)
                    name = conn.Table<Settings>().First().Name;
            }

            if (name != "")
                AgoratopiaTitle.Text = $"Good {timeOfDay}, {name}! Today is:\n{date}";
            else
                AgoratopiaTitle.Text = $"Good {timeOfDay}! Today is:\n{date}";

            Resources["EntryListData"] = new EntryViewModel();
        }

        void DoLogin(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        async void CreateEntry(object sender, System.EventArgs e)
        {
            //DisplayAlert("Test Text", "You should be sent to the entry creator now.", "Well why haven't I?");

            await Navigation.PushAsync(new EntryPage());
        }

        async void DeleteTable(object sender, System.EventArgs e)
        {
            switch(await DisplayAlert("CAUTION: ", "Are you sure you want to delete the whole table?", "Yes", "No"))
            {
                case true:
                        using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
                        {
                            conn.DropTable<DailyEntry>();

                            conn.Close();
                        }

                        Resources["EntryListData"] = new EntryViewModel();

                        break;
                case false:
                    break;
                default:
                    break;
            }
        }

        async void TakeToGif(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new GifPage());
        }

        async void RemoveEntry(object sender, System.EventArgs e)
        {

            switch (await DisplayAlert("CAUTION:", "Are you sure you want to delete this entry?", "Yes", "No"))
            {
                case true:
                    using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
                    {
                        conn.Delete<DailyEntry>((int)((SwipeItem)sender).BindingContext);

                        conn.Close();
                    }

                    // Refresh resource to reflect change on the main page
                    Resources["EntryListData"] = new EntryViewModel();
                    break;
                case false:
                    // Do nothing
                    break;
                default:
                    break;
            }
        }

        async void DeleteEverything(object sender, System.EventArgs e)
        {

            switch (await DisplayAlert("CAUTION:", "Are you sure you want to delete ALL your info? This is NOT recoverable.", "Yes", "No"))
            {
                case true:

                    // Second confirmation, for safety
                    switch (await DisplayAlert("CAUTION:", "Final check: Are you sure you want to delete EVERYTHING?", "Yes", "No"))
                    {
                        case true:
                            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
                            {
                                conn.DropTable<DailyEntry>();
                                conn.DropTable<Settings>();
                                conn.DropTable<TierLabels>();

                                conn.Close();
                            }

                            await Navigation.PushAsync(new TierSetting());
                            Navigation.RemovePage(Navigation.NavigationStack[0]);

                            break;
                    }

                    break;
                default:
                    break;
            }            
        }

        async void EditEntry(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EntryPage((int)((SwipeItem)sender).BindingContext));
        }

        //private void SelectDate(object sender, DateChangedEventArgs e)
        //{
        //    Resources["EntryListData"] = new EntryViewModel(DatePick.Date.ToShortDateString());
        //} 

        private void SelectAll(object sender, System.EventArgs e)
        {
            Resources["EntryListData"] = new EntryViewModel();
        }

        async private void Pictures(object sender, System.EventArgs e)
        {
            await DisplayAlert("SORRY", "I'm still working on this function!", "ok why'd you include this tho");
        }

        private void ListViewChange(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("Hello, this is the line. Element changed etc. etc.");
            DisplayAlert("hE", "has no grace", "such a face");
        }

        async public void HelpSection(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HelpPage());
        }

        async private void GoHome(object sender, System.EventArgs e)
        {
            // Already home, probably don't do anything
        }

        async private void ToSettings(object sender, System.EventArgs e)
        {
            await DisplayAlert("Woah", "Sorry, I'm not ready yet!", "OK");
        }

        async private void MoreDetails(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EntryDetails((int)((SwipeItem)sender).BindingContext));
        }
    }
}