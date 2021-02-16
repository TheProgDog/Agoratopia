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
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();

            var date = DateTime.Today.ToShortDateString();
            var hour = DateTime.Now.Hour;
            var timeOfDay = "";

            if (hour < 12)
                timeOfDay = "morning";
            else if (hour < 18)
                timeOfDay = "afternoon";
            else
                timeOfDay = "evening";

            AgoratopiaTitle.Text = "Good " + timeOfDay + ", welcome to Agoratopia!\nToday is:\n " + date;
        }

        void DoLogin(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        async void CreateEntry(object sender, System.EventArgs e)
        {
            //DisplayAlert("Test Text", "You should be sent to the entry creator now.", "Well why haven't I?");

            await Navigation.PushAsync(new MainPage());
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
                    await DisplayAlert("Uhhh", "What just happened?", "idk dude");
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
                        conn.Delete<DailyEntry>((int)((Button)sender).BindingContext);

                        conn.Close();
                    }

                    // Refresh resource to reflect change on the main page
                    Resources["EntryListData"] = new EntryViewModel();
                    break;
                case false:
                    // Do nothing
                    break;
                default:
                    TestText.Text = "WOAH DUDE SOMETHING BAD JUST HAPPENED";

                    break;
            }
        }
    }
}