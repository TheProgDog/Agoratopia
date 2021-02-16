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
        }

        private void ChangeEdit(object sender, EventArgs e)
        {
            if (ExposureBool.SelectedIndex == 0)
            { 
                DailyDescription.IsEnabled = true;
                WentOutAlone.IsEnabled = true;
            }
            else
            {
                DailyDescription.IsEnabled = false;
                DailyDescription.Text = "";

                WentOutAlone.IsEnabled = false;
                WentOutAlone.SelectedIndex = 4;
            }
        }

        async private void SubmitReport(object sender, EventArgs e)
        {
            var time = DateTime.Now.TimeOfDay;

            int rowAdded = 0;

            // Populate the entry with all of the forms on the page
            DailyEntry entry = new DailyEntry()
            {
                GoneOutside = ExposureBool.SelectedItem.ToString(),
                StressLevel = StressScale.SelectedIndex + 1,
                BearableLevel = BearableAnxiety.SelectedItem.ToString(),
                DateRecorded = DateTime.Today.ToShortDateString(),
                WentAlone = WentOutAlone.SelectedItem.ToString(),
                DailyDescription = "•              " + DailyDescription.Text.ToString()
            };

            // Submit entry into SQL database
            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
            {
                conn.CreateTable<DailyEntry>();

                // rowAdded for debugging purposes
                // 0 = failure
                // 1 = success
                rowAdded = conn.Insert(entry);

                conn.Close();
            }

            // Refreshes the table in the main page so that the entry is immediately recognized
            // This is the best thing I can come up with *shrugs*
            Navigation.NavigationStack[0].Resources["EntryListData"] = new EntryViewModel();

            if (rowAdded == 1)
                await Navigation.PopAsync();
            else
                await DisplayAlert("ERROR:", "Could not submit entry. Try changing something and try again!", "OK");
        }
    }
}