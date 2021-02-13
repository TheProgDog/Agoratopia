using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Agoratopia.Database;

namespace Agoratopia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

        void DoLogin(object sender, System.EventArgs e)
        {
            throw new NotImplementedException();
        }

        async void CreateEntry(object sender, System.EventArgs e)
        {
            //DisplayAlert("Test Text", "You should be sent to the entry creator now.", "Well why haven't I?");

            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
            {
                //TestText.Text = conn.Get<DailyEntry>(1).BearableLevel;

                conn.Close();
            }

            this.Resources["EntryListData"] = new EntryList();

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

                        this.Resources["EntryListData"] = new EntryList();

                        break;
                case false:
                    break;
                default:
                    await DisplayAlert("Uhhh", "What just happened?", "idk dude");
                    break;
            }
        }
    }
}