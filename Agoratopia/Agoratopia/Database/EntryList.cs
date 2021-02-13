using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using SQLite;
using Xamarin.Forms;

namespace Agoratopia.Database
{
    class EntryList : ObservableCollection<DailyEntry>
    {

        public EntryList() : base()
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
    }
}
