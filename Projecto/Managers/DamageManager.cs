using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    internal class DamageManager : GameObject
    {
        public int HP { get; set; }
        public float PhysDmg { get; set; }
        public float MagicDmg { get; set; }
        public float PhysDmgRes { get; set; }
        public float MagicDmgRes { get; set; }
        public float AtackSpeed { get; set; }
        public int Range { get; set; }
        private bool isDefending;

        public Weapon MHweapon { get; set; }
        public Weapon OHweapon { get; set; }



        //------------->CONSTRUCTORS<-------------//

        public DamageManager(string texture, Vector2 position, Vector2 size) : base(texture, position, size, 0f)
        {
            isDefending = false;
            this.Range = (int)Cons.AtackRange;
        }

        //------------->FUNCTIONS && METHODS<-------------//
        /// <summary>
        /// Devolve o tipo de dano baseado nas armas que o player usa
        /// </summary>
        public void GetDamageValues()
        {
            if (this.MHweapon == GameState.AllWeapons[1] && this.OHweapon == GameState.AllWeapons[0]) // Staff + Sword
            {
                //this.PhysDmg = PhysDmg + MHweapon.WeaponPhysicalDamage+(OHweapon.WeaponPhysicalDamage * 0.5f); // dano fisico
                this.PhysDmg = 0f;
                this.MagicDmg = (float)Cons.MagicalDamage + MHweapon.WeaponMagicalDamage + (OHweapon.WeaponMagicalDamage * 0.5f); // dano magico
                this.MagicDmgRes = (float)Cons.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
                this.PhysDmgRes = (float)Cons.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico
                this.Range = MHweapon.WeaponAtackRange + (int)Cons.AtackRange; // Range
                this.AtackSpeed = MHweapon.WeaponAtackSpeed * (float)Cons.AtackSpeed; // AtackSpeed
                Debug.NewLine(this.MagicDmg.ToString());
                Debug.NewLine(this.PhysDmg.ToString());
            }
            else if (this.MHweapon == GameState.AllWeapons[0] && this.OHweapon == GameState.AllWeapons[1]) // sword+ staff
            {
                this.PhysDmg = (float)Cons.PhysicalDamage + MHweapon.WeaponPhysicalDamage + (OHweapon.WeaponPhysicalDamage * 0.5f); // dano fisico
                //this.MagicDmg = MagicDmg + MHweapon.WeaponMagicalDamage + (OHweapon.WeaponMagicalDamage * 0.5f); // dano magico
                this.MagicDmg = 0f;
                this.MagicDmgRes = (float)Cons.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
                this.PhysDmgRes = (float)Cons.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico
                this.Range = MHweapon.WeaponAtackRange + (int)Cons.AtackRange; // Range
                this.AtackSpeed = MHweapon.WeaponAtackSpeed * (float)Cons.AtackSpeed; // Atack Speed
                Debug.NewLine(this.MagicDmg.ToString());
                Debug.NewLine(this.PhysDmg.ToString());
            }
        }
        /// <summary>
        /// Muda as armas do player
        /// </summary>
        public void ChangeWeapons()
        {
            if (InputManager.PressedLastFrame.K == ButtonState.Pressed && this.MHweapon == GameState.AllWeapons[0]
                && this.OHweapon == GameState.AllWeapons[1])
            {
                this.MHweapon = GameState.AllWeapons[1];
                this.OHweapon = GameState.AllWeapons[0];
            }else if(InputManager.PressedLastFrame.K == ButtonState.Pressed && this.MHweapon == GameState.AllWeapons[1]
                && this.OHweapon == GameState.AllWeapons[0])
            {
                this.MHweapon = GameState.AllWeapons[0];
                this.OHweapon = GameState.AllWeapons[1];
            }
        }
        /// <summary>
        /// Use this method to resolve attacks between Figures vs enemy
        /// </summary>
        /// <param name="defender">The enemy to take damage.</param>
        /// <returns>An Enemy if it dies.</returns>
        public Enemy EnemyTakeDamage(Enemy defender)
        {
            Debug.NewLine(defender.HP.ToString());
            if (PhysDmg >= defender.PhysDmgRes)
            {
                defender.HP -= (int)(PhysDmg - PhysDmgRes);
                if (defender.HP <= 0)
                {
                    // When an enemies health dropped below 0 they died
                    // Remove that enemy from the game
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
                    return defender;
                }
            }
            else
            {
                // Show the miss message in the Debug log for now
                // Debug.NewLine("{0} missed {1}", defender.Name, defender.HP);
            }
            Debug.NewLine(defender.HP.ToString());
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
            if ((this.Size.X + Range) + enemy.Size.X > distance) return true;
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
            if ((this.Size.X + Range) + player.Size.X > distance) return true;
            return false;
        }
        /// <summary>
        ///  Returns the atack animation to perform.
        /// </summary>
        /// <param name="player">Player to test distance with.</param>
        /// <returns></returns>
    }
}
