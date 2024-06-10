using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WWTBM.DataStructure
{
    public class AllDomanda
    {
        public string Titolo { get; set; }
        public string[] Risposte { get; set; }
        public int corretta { get; set; }
        public AllDomanda(string Titolo, string rs1, string rs2, string rs3, string rs4, int corretta)
        {
            Risposte = new string[4];
            this.Titolo = Titolo;
            this.corretta = corretta;
            this.Risposte[0] = rs1;
            this.Risposte[1] = rs2;
            this.Risposte[2] = rs3;
            this.Risposte[3] = rs4;
        }
        public AllDomanda() 
        {
            Titolo = string.Empty;
            Risposte = new string[4];
            for (int i = 0; i < Risposte.Length; i++)
                Risposte[i] = string.Empty;
            corretta = 0;

        }
    }
}
