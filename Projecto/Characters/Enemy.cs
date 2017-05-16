using Microsoft.Xna.Framework;
using SettlersEngine;
using System;
using System.Collections.Generic;

namespace Projecto
{
    internal class Enemy : DamageManager
    {
        public Vector2 Coordinates;
        private Vector2 deltaPosition;
        public Collider enemyCollider;
        private SpatialAStar<Tile, Object> aStar;
        private const float movingSpeed = 0.2f;


        //------------->CONSTRUCTORS<-------------//
        public Enemy(string texture, Vector2 position, Vector2 size, int range, ref SpatialAStar<Tile, Object> aStar) : base("New Piskel", position, size, range)
        {
            this.aStar = aStar;
            Coordinates = (position / size);
            Coordinates.X = (int)Coordinates.X;
            Coordinates.Y = (int)Coordinates.Y;
            this.enemyCollider = new Collider(position, size);
            this.enemyCollider.UpdateTiles(position, size);

            #region Stats initializer
            this.HP = 50;
            this.PhysDmg = 5;
            this.MagicDmg = 5;
            this.PhysDmgRes = 1;
            this.MagicDmgRes = 1;
            #endregion
        }
        public void EnemyMovement(GameTime gameTime, PlayerManager player)
        {
            deltaPosition = Vector2.Zero;
            
            LinkedList<Tile> path = aStar.Search(new Tile(0, new Vector2(this.Coordinates.X, this.Coordinates.Y), 5),
                                new Tile(0, new Vector2(player.Coordinates.X, player.Coordinates.Y), 5), null);

            //foreach (Tile t in path)
            //{
            //    deltaPosition += new Vector2(player.Coordinates.X - t.Coordinates.X, player.Coordinates.Y - t.Coordinates.Y) * movingSpeed;

            //    if (deltaPosition != Vector2.Zero)
            //    {
            //        this.Move(deltaPosition);
            //        this.Coordinates = enemyCollider.UpdateTiles(Position, Size);
            //        this.enemyCollider.UpdateBounds(Position, Size);
            //        this.Position = this.enemyCollider.UpdatePosition(Position, Size);
            //        this.Coordinates = enemyCollider.UpdateTiles(Position, Size);
            //        this.enemyCollider.UpdateBounds(Position, Size);
            //    }

            //}

            Tile aux = path.First.Next.Value;

            Debug.NewLine(aux.Coordinates.X.ToString() + ", " + aux.Coordinates.Y.ToString());

            Vector2 auxVector = new Vector2(aux.Coordinates.X - this.Coordinates.X, aux.Coordinates.Y - this.Coordinates.Y);
            deltaPosition += auxVector * movingSpeed;
            this.Move(deltaPosition);
            this.enemyCollider.UpdateBounds(Position, Size);
            this.Position = this.enemyCollider.UpdatePosition(Position, Size);
            this.Coordinates = enemyCollider.UpdateTiles(Position, Size);
            this.enemyCollider.UpdateBounds(Position, Size);


            //this.currentAnimation.Play(gameTime);
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

