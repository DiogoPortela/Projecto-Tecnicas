using Microsoft.Xna.Framework;

namespace Projecto
{
    internal class Enemy : DamageManager
    {
        public Enemy(string texture, Vector2 position, float size, int range) : base("New Piskel", position, new Vector2(size, size), range)
        {
            #region Stats initializer
            this.HP = 50;
            this.PhysDmg = 5;
            this.MagicDmg = 5;
            this.PhysDmgRes = 1;
            this.MagicDmgRes = 1;
            #endregion
        }

        //public void AttackEnemy(Enemy enemy)
        //{
        //    if (PhysDmg >= enemy.PhysDmgRes)
        //    {
        //        // Lower the defender's health by the amount of damage
        //        enemy.HP -= (int)(PhysDmg - PhysDmgRes);
        //        // Write a combat message to the debug log.ideia
        //        /* Debug.WriteLine("{0} hit {1} for {2} and he has {3} health remaining.",
        //           damage, player.Health);*/
        //        /*     if (player.HP <= 0)
        //             {
        //                 if (player is PlayerManager)
        //                 {
        //                     var enemy = player as PlayerManager;
        //                     // When an enemies health dropped below 0 they died
        //                     // Remove that enemy from the game
        //                     GameState.p
        //                 }
        //             }*/
        //    }
        //    else if (MagicDmg >= MagicDmgRes)
        //    {
        //        enemy.HP -= (int)(MagicDmg - enemy.MagicDmgRes);
        //        /*if (player.HP <= 0)
        //        {
        //            if (player is PlayerManager)
        //            {
        //                var enemy = player as PlayerManager;
        //                // When an enemies health dropped below 0 they died
        //                // Remove that enemy from the game
        //                GameState.PlayerOne.Remove(enemy);
        //            }
        //        }*/
        //    }
        //}
    }
}
