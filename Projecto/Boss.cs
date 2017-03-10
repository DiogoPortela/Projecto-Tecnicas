using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pew_pew
{
    public class Boss : Enemy
    {
        public CombatMod mod { get; set; }

        public Boss(CombatMod mod) : base()
        {
            
        }


    }
}
