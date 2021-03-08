using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Agoratopia.Database
{
    [Table("Settings")]
    class Settings
    {
        public string Name
        {
            get; set;
        }

        public string AgoraLength
        {
            get; set;
        }

        public int NumOfTiers
        {
            get; set;
        }
    }
}
