using System;
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
        private GameObject parent;
        //------------->CONSTRUCTORS<-------------//

        public Bullet(Vector2 position, Vector2 direction, int range, GameObject parent) : base("DebugPixel", position + new Vector2(-1, 1), new Vector2(2, 2), 0)
        {
            this.start = position + new Vector2(-1, 1);
            this.range = range;
            this.parent = parent;
            direction.Normalize();
            this.objectDiretion = direction * 0.9f;
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void Update()
        {
            this.Move(this.objectDiretion);
            float auxVector = Math.Abs((Position - start).Length());
            if (auxVector > range)
                GameState.BulletList.Remove(this);
            else if (MapGenerator.infoMap[(int)(this.Position.X / (int)Constants.GRIDSIZE + 0.5f), (int)(this.Position.Y / (int)Constants.GRIDSIZE + 0.5f)] == 1)
                GameState.BulletList.Remove(this);
            else if (parent is PlayerManager)
            {
                List<Enemy> auxList = new List<Enemy>();

                foreach (Enemy e in GameState.EnemyList)
                {
                    if (this.Rectangle.Intersects(e.Rectangle))
                    {
                        auxList.Add(e);
                        ParticleSystem p = new ParticleSystem(ParticleType.Explosion, "DebugPixel", e.Center, Vector2.One * 0.9f, 20, 0, 3, 500, 500, 10);
                        p.Start();
                        GameState.ParticlesList.Add(p);
                        GameState.BulletList.Remove(this);
                        break;
                    }
                }
                for (int i = 0; i < auxList.Count; i++)
                {
                    (parent as DamageManager).EnemyTakeDamage(auxList[i]);
                    if (auxList[i].HP <= 0)
                        GameState.EnemyList.Remove(auxList[i]);
                }

            }
            else if (parent is Enemy)
            {
                if (this.Rectangle.Contains(GameState.PlayerOne.Rectangle))
                {
                    (parent as Enemy).PlayerTakeDamage(GameState.PlayerOne);
                    GameState.ParticlesList.Add(new ParticleSystem(ParticleType.Explosion, "DebugPixel", GameState.PlayerOne.Center, Vector2.One * 0.9f, 20, 0, 3, 500, 500, 10));
                    GameState.BulletList.Remove(this);
                }
                if (this.Rectangle.Contains(GameState.PlayerTwo.Rectangle))
                {
                    (parent as Enemy).PlayerTakeDamage(GameState.PlayerTwo);
                    GameState.ParticlesList.Add(new ParticleSystem(ParticleType.Explosion, "DebugPixel", GameState.PlayerTwo.Center, Vector2.One * 0.9f, 20, 0, 3, 500, 500, 10));
                    GameState.BulletList.Remove(this);
                }
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

            this.MagicDmgRes = (float)Constants.MagicalResistance + (MagicDmg * 0.2f); // resistencia a magic
            this.PhysDmgRes = (float)Constants.PhysicalResistance + (PhysDmgRes * 0.2f); // resistencia a fisico
        }
        /// <summary>
        /// Switches player weapons
        /// </summary>
        public void SwitchWeapons()
        {
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
                this.mainHandWeapon = newWeapon;
            else
                this.offHandWeapon = newWeapon;
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
                        GameState.EnemyList.Remove(enemy);
                    SoundManager.StartSound("morrer", false);
                }
                SoundManager.StartSound("attack1", false);
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

        public void PlayerGetKnockedBack(PlayerManager defender, Vector2 direction)
        {
            //player e empurrado
            defender.Position -= direction;
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
            if ((this.Size.X + Range) + enemy.Size.X > distance)
                return true;
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
