﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    internal class Bullet : GameObject
    {
        private int range;
        private Vector2 start;
        public Bullet(Vector2 position, Vector2 direction, int range) : base("DebugPixel", position - Vector2.One, new Vector2(2,2), 0)
        {
            this.start = position - Vector2.One;
            this.range = range;
            direction.Normalize();
            this.objectDiretion = direction * 0.5f;
        }

        public void Update()
        {
            this.Move(this.objectDiretion);
            float auxVector = Math.Abs(Position.X - start.X) + Math.Abs(Position.Y - start.Y);
            if(auxVector > range)
            {
                GameState.BulletList.Remove(this);
            }
        }
        public override void DrawObject(Camera camera)
        {
            if (this.isActive)
            {
                this.Rectangle = camera.CalculatePixelRectangle(this.Position, this.Size);
                Game1.spriteBatch.Draw(this.Texture, this.Rectangle, Color.Green);
            }
        }
    }
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

        public Weapon mainHandWeapon { get; set; }
        public Weapon offHandWeapon { get; set; }



        //------------->CONSTRUCTORS<-------------//

        public DamageManager(string texture, Vector2 position, Vector2 size, int range) : base(texture, position, size, 0f)
        {
            isDefending = false;
            this.Range = range;
        }

        //------------->FUNCTIONS && METHODS<-------------//

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

            this.MagicDmgRes = (float)Cons.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
            this.PhysDmgRes = (float)Cons.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico

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
        }
        /// <summary>
        /// Changes a player weapon by a new one.
        /// </summary>
        /// <param name="hand">Hand of the player.</param>
        /// <param name="newWeapon">New weapon to replace old one.</param>
        public void ChangeWeapon(WeaponHand hand, Weapon newWeapon)
        {
            if (hand == WeaponHand.MainHand)
                this.mainHandWeapon = newWeapon;
            else
                this.offHandWeapon = newWeapon;
            GetDamageValues();
        }

        public void Attack()
        {
            if (mainHandWeapon.isRanged)
            {
                Bullet bullet = new Bullet(this.Center, this.objectDiretion, mainHandWeapon.MainAtackRange);
                GameState.BulletList.Add(bullet);
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
                    EnemyTakeDamage(enemy);
                    if (enemy.HP <= 0)
                        GameState.EnemyList.Remove(enemy);
                }
            }
        }
        /// <summary>
        /// Use this method to resolve attacks between Figures vs enemy
        /// </summary>
        /// <param name="defender">The enemy to take damage.</param>
        /// <returns>An Enemy if it dies.</returns>
        public void EnemyTakeDamage(Enemy defender)
        {

            if (PhysDmg >= defender.PhysDmgRes)
            {
                if (!defender.isDefending)
                    defender.HP -= (int)(PhysDmg - defender.PhysDmgRes);
            }
            else if (MagicDmg >= defender.MagicDmgRes)
            {
                if (!defender.isDefending)
                    defender.HP -= (int)(MagicDmg - defender.MagicDmgRes);
            }
            else
            {
                // Show the miss message in the Debug log for now
                // Debug.NewLine("{0} missed {1}", defender.Name, defender.HP);
            }
            Debug.NewLine("Enemy's HP is " + defender.HP.ToString());

        }
        /// <summary>
        /// Use this method to defense
        /// </summary>
        public void PlayerTakeDamage(PlayerManager defender)
        {
            if (PhysDmg >= defender.PhysDmgRes)
            {
                if (!defender.isDefending)
                    defender.HP -= (int)(PhysDmg - defender.PhysDmgRes);
            }
            else if (MagicDmg >= defender.MagicDmgRes)
            {
                if (!defender.isDefending)
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
    }
}
