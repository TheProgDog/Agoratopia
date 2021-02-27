using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Agoratopia.Database
{
    [Table("TierLabels")]
    class TierLabels
    {

        public string TierLabel
        {
            get; set;
        }

        public TierLabels()
        {
            TierLabel = "Test";
        }
    }
}
