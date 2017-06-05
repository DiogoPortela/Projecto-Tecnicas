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
<<<<<<< Updated upstream
=======

        /// <summary>
        /// Devolve o tipo de dano baseado nas armas que o player usa
        /// </summary>
        public void GetDamageValues()
        {
            if (mainHandWeapon != null)
            {
                this.PhysDmg = this.mainHandWeapon.MainPhysicalDamage;
                this.MagicDmg = this.mainHandWeapon.MainMagicalDamage;
                this.Range = this.mainHandWeapon.MainAtackRange;
                this.AtackSpeed = this.mainHandWeapon.MainAtackSpeed;
            }

            if (offHandWeapon != null)
            {
                this.PhysDmg *= offHandWeapon.ModPhysicalDamage;
                this.MagicDmg *= offHandWeapon.ModMagicalDamage;
                this.Range = (int)(this.Range * offHandWeapon.ModAttackRange);
                this.AtackSpeed *= offHandWeapon.ModAttackSpeed;
            }

            this.MagicDmgRes = (float)Constants.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
            this.PhysDmgRes = (float)Constants.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico

            //if (this.MHweapon == GameState.AllWeapons[1] && this.OHweapon == GameState.AllWeapons[0]) // Staff + Sword
            //{
            //    //this.PhysDmg = PhysDmg + MHweapon.WeaponPhysicalDamage+(OHweapon.WeaponPhysicalDamage * 0.5f); // dano fisico
            //    this.PhysDmg = 0f;
            //    this.MagicDmg = (float)Cons.MagicalDamage + MHweapon.WeaponMagicalDamage + (OHweapon.WeaponMagicalDamage * 0.5f); // dano magico
            //    this.MagicDmgRes = (float)Cons.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
            //    this.PhysDmgRes = (float)Cons.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico
            //    this.Range = MHweapon.WeaponAtackRange + (int)Cons.AtackRange; // Range
            //    this.AtackSpeed = MHweapon.WeaponAtackSpeed * (float)Cons.AtackSpeed; // AtackSpeed
            //    Debug.NewLine(this.MagicDmg.ToString());
            //    Debug.NewLine(this.PhysDmg.ToString());
            //}
            //else if (this.MHweapon == GameState.AllWeapons[0] && this.OHweapon == GameState.AllWeapons[1]) // sword+ staff
            //{
            //    this.PhysDmg = (float)Cons.PhysicalDamage + MHweapon.WeaponPhysicalDamage + (OHweapon.WeaponPhysicalDamage * 0.5f); // dano fisico
            //    //this.MagicDmg = MagicDmg + MHweapon.WeaponMagicalDamage + (OHweapon.WeaponMagicalDamage * 0.5f); // dano magico
            //    this.MagicDmg = 0f;
            //    this.MagicDmgRes = (float)Cons.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
            //    this.PhysDmgRes = (float)Cons.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico
            //    this.Range = MHweapon.WeaponAtackRange + (int)Cons.AtackRange; // Range
            //    this.AtackSpeed = MHweapon.WeaponAtackSpeed * (float)Cons.AtackSpeed; // Atack Speed
            //    Debug.NewLine(this.MagicDmg.ToString());
            //    Debug.NewLine(this.PhysDmg.ToString());
            //}
        }
        /// <summary>
        /// Switches player weapons
        /// </summary>
        public void SwitchWeapons()
        {
            //if (InputManager.PressedLastFrame.K == ButtonState.Pressed && this.MHweapon == GameState.AllWeapons[0]
            //    && this.OHweapon == GameState.AllWeapons[1])
            //{
            //    this.MHweapon = GameState.AllWeapons[1];
            //    this.OHweapon = GameState.AllWeapons[0];
            //}else if(InputManager.PressedLastFrame.K == ButtonState.Pressed && this.MHweapon == GameState.AllWeapons[1]
            //    && this.OHweapon == GameState.AllWeapons[0])
            //{
            //    this.MHweapon = GameState.AllWeapons[0];
            //    this.OHweapon = GameState.AllWeapons[1];
            //}
            Weapon aux = this.mainHandWeapon;
            this.mainHandWeapon = this.offHandWeapon;
            this.offHandWeapon = aux;
            SoundManager.StartSound("troca_arma", false);

        }
        /// <summary>
        /// Changes a player weapon by a new one.
        /// </summary>
        /// <param name="hand">Hand of the player.</param>
        /// <param name="newWeapon">New weapon to replace old one.</param>
        public void ChangeWeapon(WeaponHand hand, Weapon newWeapon)
        {
            if (hand == WeaponHand.MainHand)
            {
                this.mainHandWeapon = newWeapon;
                
            }
            else
            {
                this.offHandWeapon = newWeapon;

                
            }
           
            GetDamageValues();
            
        }

        public void Attack()
        {
            if (mainHandWeapon == null)
                return;
            else if (mainHandWeapon.isRanged)
            {
                Bullet bullet = new Bullet(this.Center, this.objectDiretion, mainHandWeapon.MainAtackRange, this);
                GameState.BulletList.Add(bullet);
                SoundManager.StartSound("attack_magico", false);

            }
            else
            {
                List<Enemy> auxEnemy = new List<Enemy>();

                foreach (Enemy enemy in GameState.EnemyList)
                {
                    if (this.IsInRange(enemy) == true)
                    {
                        auxEnemy.Add(enemy);
                        
                    }
                    
                }

                foreach (Enemy enemy in auxEnemy)
                {
                    ParticleSystem p = new ParticleSystem(ParticleType.Smoke, "DebugPixel", enemy.Center, Vector2.One * 0.5f, 4, 0, 3, 500, 500, 10);
                    p.Start();
                    GameState.ParticlesList.Add(p);
                    EnemyTakeDamage(enemy);
                    if (enemy.HP <= 0)
                    {
                        GameState.EnemyList.Remove(enemy);
                        SoundManager.StartSound("morrer", false);
                    }
                }
                SoundManager.StartSound("attack1", false);
            }
            
        }
>>>>>>> Stashed changes
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
