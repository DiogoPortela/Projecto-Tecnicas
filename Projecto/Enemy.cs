using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pew_pew
{
    public class Enemy
    {
        public int HP { get; set; }

        public int MP { get; set; }

        public float MS { get; set; }

        public double PhysDmg { get; set; }

        public double MagicDmg { get; set; }

        public Enemy()
        {
            this.HP = 30;
            this.MP = 30;
            this.MS = 0.8f;
            this.PhysDmg = 10;
            this.MagicDmg = 5;
        }

    }
}
