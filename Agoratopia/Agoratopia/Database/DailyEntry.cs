using System;
using System.Collections.Generic;
using System.Text;
using Agoratopia.Database;
using SQLite;

namespace Agoratopia.Database
{
    [Table("DailyEntry")]
    public class DailyEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Key { get; set; }

        public String GoneOutside { get; set; }

        public int StressLevel { get; set; }
        
        public String BearableLevel { get; set; }

        public String DateRecorded { get; set; }

        public String WentAlone { get; set; }

        public String DailyDescription { get; set; }

        public DailyEntry()
        {
            
        }
    }

}
