using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Agoratopia.Database
{
    [Table("TierLabels")]
    class TierLabels
    {
        [PrimaryKey,AutoIncrement]
        public int PKey
        {
            get;
            set;
        }

        public string TierLabel
        {
            get; set;
        }

        public TierLabels()
        {

        }
    }
}
