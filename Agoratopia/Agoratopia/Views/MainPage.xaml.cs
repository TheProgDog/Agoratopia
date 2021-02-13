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
    public partial class MainPage : ContentPage
    {
        public MainPage()
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

            DateTest.Text = "Good " + timeOfDay + "! Today is:\n " + date + "\n\n\n\n\n\n\n\n";
        }

        async private void SubmitReport(object sender, EventArgs e)
        {
            var time = DateTime.Now.TimeOfDay;

            int rowAdded = 0;

            DailyEntry entry = new DailyEntry()
            {
                GoneOutside = ExposureBool.SelectedItem.ToString(),
                StressLevel = StressScale.SelectedIndex + 1,
                BearableLevel = BearableAnxiety.SelectedItem.ToString(),
                DateRecorded = DateTime.Today.ToShortDateString()
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

            await Navigation.PopAsync();
        }
    }
}