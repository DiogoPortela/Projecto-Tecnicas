using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    public class Enemy
    {
        public List<Enemy> enemies = new List<Enemy>();

        public int HP { get; set; }

        public int MP { get; set; }

        public float MS { get; set; }

        public double PhysDmg { get; set; }

        public double MagicDmg { get; set; }

        public double PhysDmgRes { get; set; }

        public double MagicDmgRes { get; set; }

        public Enemy()
        {
            this.HP = 30;
            this.MP = 30;
            this.MS = 0.8f;
            this.PhysDmg = 10;
            this.MagicDmg = 5;
            this.PhysDmgRes = 1;
            this.MagicDmgRes = 1;
        }

    }
}
