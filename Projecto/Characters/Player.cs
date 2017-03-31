using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    public class Player : GameObject
    {
        private int playerNumber;

        public int HP { get; set; }
        public int MP { get; set; }
        public float CDR { get; set; }
        public float AoE { get; set; }
        public float MS { get; set; }
        public double PhysDmg { get; set; }
        public double MagicDmg { get; set; }

        public Player()
        {
            this.HP = 100;
            this.MP = 100;
            this.CDR = 1.0f;
            this.AoE = 1.0f;
            this.MS = 1.0f;
            this.PhysDmg = 18.0;
            this.MagicDmg = 8.0;
        }
    }
}
