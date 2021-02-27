using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Agoratopia.ViewModels;
using Agoratopia.Database;

namespace Agoratopia.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();

            DebugText.Text = DateTime.Now.ToShortDateString();

            Resources["EntryListData"] = new EntryViewModel(DateTime.Now.ToShortDateString());

            //String month = "";
            //var year = DateTime.Now.Year;

            //switch (DateTime.Now.Month)
            //{
            //    case 1:
            //        month = "January";
            //        break;
            //    case 2:
            //        month = "February";
            //        break;
            //    case 3:
            //        month = "March";
            //        break;
            //    case 4:
            //        month = "April";
            //        break;
            //    case 5:
            //        month = "May";
            //        break;
            //    case 6:
            //        month = "June";
            //        break;
            //    case 7:
            //        month = "July";
            //        break;
            //    case 8:
            //        month = "August";
            //        break;
            //    case 9:
            //        month = "September";
            //        break;
            //    case 10:
            //        month = "October";
            //        break;
            //    case 11:
            //        month = "November";
            //        break;
            //    case 12:
            //        month = "December";
            //        break;
            //    default:
            //        month = "wait what why";
            //        break;
            //}

            //MonthText.Text = month + " " + year;
        }

        public void DateSelected(object sender, System.EventArgs e)
        {
            System.Console.WriteLine("I see you have selected " + DateSelect.Date);

            DebugText.Text = "You have selected " + DateSelect.Date.ToShortDateString() + "!";
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
                    DebugText.Text = "WOAH DUDE SOMETHING BAD JUST HAPPENED";

                    break;
            }
        }

        async void CreateEntry(object sender, System.EventArgs e)
        {
            //DisplayAlert("Test Text", "You should be sent to the entry creator now.", "Well why haven't I?");

            await Navigation.PushAsync(new MainPage());
        }

        async void DeleteTable(object sender, System.EventArgs e)
        {
            switch (await DisplayAlert("CAUTION: ", "Are you sure you want to delete the whole table?", "Yes", "No"))
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

    }
}