using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Projecto
{
    class Player : GameObject
    {
        private PlayerNumber playerNumber;

        #region Atribute Stats
        private int maxHP;
        public int HP { get; set; }
        public int MP { get; set; }
        public float CDR { get; set; }
        public float AoE { get; set; }
        public float MS { get; set; }
        public double PhysDmg { get; set; }
        public double MagicDmg { get; set; }
        #endregion

        public Player(string texture, Vector2 position, float size, PlayerNumber playerNumber) : base(texture, position, new Vector2(size,size), 0f)
        {
            this.playerNumber = playerNumber;

            #region Stats initializer
            this.HP = 100;
            this.MP = 100;
            this.CDR = 1.0f;
            this.AoE = 1.0f;
            this.MS = 1.0f;
            this.PhysDmg = 18.0;
            this.MagicDmg = 8.0;
            #endregion
        }
    }
}
