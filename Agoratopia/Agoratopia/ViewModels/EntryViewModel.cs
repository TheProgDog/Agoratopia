using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SQLite;
using Xamarin.Forms;
using Agoratopia.Database;

namespace Agoratopia.ViewModels
{
    class EntryViewModel : ObservableCollection<DailyEntry>
    {

        public EntryViewModel() : base()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
            {
                conn.CreateTable<DailyEntry>();

                foreach(DailyEntry entry in conn.Table<DailyEntry>().ToList())
                {
                    Add(entry);
                }
            }
        }

        public EntryViewModel(String date) : base()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.EntryPath))
            {
                conn.CreateTable<DailyEntry>();

                foreach (DailyEntry entry in conn.Query<DailyEntry>("SELECT * FROM DailyEntry WHERE DateRecorded='" + date + "'"))
                {
                    Add(entry);
                }
            }
        }
    }
}
