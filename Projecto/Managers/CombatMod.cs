using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    public class CombatMod
    {

        public bool IncHP { get; set; }

        public bool IncPhysDmg { get; set; }

        public bool IncMagicDmg { get; set; }

        public bool IncMS { get; set; }

        public bool SlowAoE { get; set; }

        public bool LowerRes { get; set; }

        public bool MagicDmgRes { get; set; }

        public bool PhysDmgRes { get; set; }

        public CombatMod()
        {
            Random rand = new Random();

            this.IncHP = false;
            this.IncPhysDmg = false;
            this.IncMagicDmg = false;
            this.IncMS = false;
            this.SlowAoE = false;
            this.LowerRes = false;
            this.MagicDmgRes = false;
            this.PhysDmgRes = false;


        }
    }
}
