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
    public partial class EntryPage : ContentPage
    {
        bool isEdit = false;

        DailyEntry entry;

        public EntryPage()
        {
            InitializeComponent();

            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
            {
                TableQuery<Settings> table = conn.Table<Settings>();

                if (table != null)
                {
                    for (int i = 0; i < table.First().NumOfTiers; i++)
                    {
                        TierSelect.Items.Add($"Tier {i+1}: {conn.Get<TierLabels>(i+1).TierLabel}");
                    }
                }
            }
        }

        public EntryPage(int pKey)
        {
            // This constructor is to let you edit an entry

            InitializeComponent();

            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
            {
                entry = conn.Query<DailyEntry>($"select * from DailyEntry where Key = {pKey}").FirstOrDefault();

                TierSelect.SelectedIndex = entry.TierLevel + 1;
                ExposureBool.SelectedItem = entry.GoneOutside;
                StressScale.SelectedIndex = entry.StressLevel - 1;
                BearableAnxiety.SelectedItem = entry.BearableLevel;
                WentOutAlone.SelectedItem = entry.WentAlone;
                DailyDescription.Text = entry.DailyDescription;
                
                
                // Populate the exposure tiers
                TableQuery<Settings> table = conn.Table<Settings>();

                if (table != null)
                {
                    for (int i = 0; i < table.First().NumOfTiers; i++)
                    {
                        TierSelect.Items.Add($"Tier {i + 1}: {conn.Get<TierLabels>(i + 1).TierLabel}");
                    }
                }

                conn.Close();
            }

            isEdit = true;
        }

        private void ChangeEdit(object sender, EventArgs e)
        {
            if (ExposureBool.SelectedIndex == 0)
            { 
                DailyDescription.IsEnabled = true;
                WentOutAlone.IsEnabled = true;
                TierSelect.IsEnabled = true;
            }
            else
            {
                StressScale.SelectedItem = "4";

                DailyDescription.IsEnabled = false;
                DailyDescription.Text = "";

                WentOutAlone.IsEnabled = false;
                WentOutAlone.SelectedIndex = 4;

                TierSelect.IsEnabled = false;
                TierSelect.SelectedIndex = 0;
            }
        }

        async private void SubmitReport(object sender, EventArgs e)
        {
            int rowAdded = 0;

            if (!isEdit)
            {
                // This is not an edited entry, so make an entirely new one

                // Populate the entry with all of the forms on the page
                entry = new DailyEntry()
                {
                    TierLevel = TierSelect.SelectedIndex,
                    GoneOutside = ExposureBool.SelectedItem.ToString(),
                    StressLevel = StressScale.SelectedIndex + 1,
                    BearableLevel = BearableAnxiety.SelectedItem.ToString(),
                    DateRecorded = DateTime.Today.ToShortDateString(),
                    WentAlone = WentOutAlone.SelectedItem.ToString(),
                    DailyDescription = DailyDescription.Text.ToString()
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
            }
            else
            {
                // This is an edited entry, only update instead of making an entirely new entry
                entry.TierLevel = TierSelect.SelectedIndex;
                entry.GoneOutside = ExposureBool.SelectedItem.ToString();
                entry.StressLevel = StressScale.SelectedIndex + 1;
                entry.BearableLevel = BearableAnxiety.SelectedItem.ToString();
                entry.DateRecorded = DateTime.Today.ToShortDateString();
                entry.WentAlone = WentOutAlone.SelectedItem.ToString();
                entry.DailyDescription = DailyDescription.Text.ToString();

                using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
                {
                    conn.CreateTable<DailyEntry>();

                    rowAdded = conn.Update(entry);

                    conn.Close();
                }
            }

            // Refreshes the table in the main page so that the entry is immediately recognized
            // This is the best thing I can come up with *shrugs*
            Navigation.NavigationStack[0].Resources["EntryListData"] = new EntryViewModel();

            if (rowAdded == 1)
                await Navigation.PopAsync();
            else
                await DisplayAlert("ERROR:", "Could not submit entry. Try changing something and try again!", "OK");
        }

        async private void Cancel(object sender, EventArgs e)
        {
            switch(await DisplayAlert("CAUTION:", "Are you sure you want to cancel this entry?", "Yes", "No"))
            {
                case true:
                    await Navigation.PopAsync();
                    break;
                case false:
                    break;
            }
        }

        async public void HelpSection(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HelpPage());
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