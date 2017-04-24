using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    public class Enemy : GameObject
    {

        public int HP { get; set; }

        public int PhysDmg { get; set; }

        public int MagicDmg { get; set; }

        public int PhysDmgRes { get; set; }

        public int MagicDmgRes { get; set; }

        public Enemy(string texture, Vector2 position, float size) : base(texture, position, new Vector2(size, size), 0f)
        {

            #region Stats initializer
            this.HP = 50;
            this.PhysDmg = 5;
            this.MagicDmg = 5;
            this.PhysDmgRes = 1;
            this.MagicDmgRes = 1;
            #endregion
        }

    }
}
