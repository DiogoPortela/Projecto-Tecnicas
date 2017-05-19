using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Projecto
{
    internal class DamageManager : GameObject
    {
        public int HP { get; set; }
        public float PhysDmg { get; set; }
        public float MagicDmg { get; set; }
        public float PhysDmgRes { get; set; }
        public float MagicDmgRes { get; set; }
        private int range;
        private bool isDefending;

        //------------->CONSTRUCTORS<-------------//

        public DamageManager(string texture, Vector2 position, Vector2 size, int range) : base(texture, position, size, 0f)
        {
            isDefending = false;
            this.range = range;
        }

        //------------->FUNCTIONS && METHODS<-------------//
        /// <summary>
        /// Use this method to resolve attacks between Figures vs enemy
        /// </summary>
        /// <param name="defender">The enemy to take damage.</param>
        /// <returns>An Enemy if it dies.</returns>
        public Enemy EnemyTakeDamage(Enemy defender)
        {
            if (PhysDmg >= defender.PhysDmgRes)
            {
                defender.HP -= (int)(PhysDmg - PhysDmgRes);
                if (defender.HP <= 0)
                {
                    // When an enemies health dropped below 0 they died
                    // Remove that enemy from the game
                    Debug.NewLine("Enemy died.");
                    return defender;
                }
            }
            if (MagicDmg >= defender.MagicDmgRes)
            {
                defender.HP -= (int)(MagicDmg - defender.MagicDmgRes);
                if (defender.HP <= 0)
                {
                    // When an enemies health dropped below 0 they died
                    // Remove that enemy from the game
                    Debug.NewLine("Enemy died.");
                    return defender;
                }
            }
            else
            {
                // Show the miss message in the Debug log for now
                // Debug.NewLine("{0} missed {1}", defender.Name, defender.HP);
            }
            Debug.NewLine("Enemy's HP is " + defender.HP.ToString());
            return null;

        }
        /// <summary>
        /// Use this method to defense
        /// </summary>
        public void PlayerTakeDamage(PlayerManager defender)
        {
            Debug.NewLine(defender.HP.ToString());
            if (PhysDmg >= defender.PhysDmgRes)
            {
                defender.HP -= (int)(PhysDmg - PhysDmgRes);
            }
            if (MagicDmg >= defender.MagicDmgRes)
            {
                defender.HP -= (int)(MagicDmg - defender.MagicDmgRes);
            }
            else
            {
                // Show the miss message in the Debug log for now
                // Debug.NewLine("{0} missed {1}", defender.Name, defender.HP);
            }
            Debug.NewLine(defender.HP.ToString());
        }
        /// <summary>
        /// Returns true if an enemy is in range.
        /// </summary>
        /// <param name="enemy">Enemy to test distance with.</param>
        /// <returns></returns>
        public bool IsInRange(Enemy enemy)
        {
            Vector2 v = (this.Position) - (enemy.Position);
            float distance = Math.Abs(v.Length());
            if ((this.Size.X + range) + enemy.Size.X > distance) return true;
            return false;
        }
        /// <summary>
        ///  Returns true if a player is in range.
        /// </summary>
        /// <param name="player">Player to test distance with.</param>
        /// <returns></returns>
        public bool IsInRange(PlayerManager player)
        {
            Vector2 v = (this.Position) - (player.Position);
            float distance = Math.Abs(v.Length());
            if ((this.Size.X + range) + player.Size.X > distance) return true;
            return false;
        }
    }
}
