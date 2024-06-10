using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWTBM.Screens.ViewModels.Commands;

namespace WWTBM.Screens.ViewModels
{
    public partial class GameViewModel : BaseModel
    {
        private Risposta1 _Risposta1;
        public Risposta1 Risposta1
    { 
            get { return _Risposta1 ?? (_Risposta1 = new Risposta1(this)); }}

        private Risposta2 _Risposta2;
        public Risposta2 Risposta2
        {
            get { return _Risposta2 ?? (_Risposta2 = new Risposta2(this)); }
        }

        private Risposta3 _Risposta3;
        public Risposta3 Risposta3
        {
            get { return _Risposta3 ?? (_Risposta3 = new Risposta3(this)); }
        }

        private Risposta4 _Risposta4;
        public Risposta4 Risposta4
        {
            get { return _Risposta4 ?? (_Risposta4 = new Risposta4(this)); }
        }





    }
}
