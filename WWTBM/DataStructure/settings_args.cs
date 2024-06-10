using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWTBM.DataStructure
{
    public class settings_args : EventArgs
    {
        // Proprietà per i dati che desideri passare
        public int Audio_Volume { get; set; }
        public int Question_Number { get; set; }
    }
}
