﻿using Microsoft.Xna.Framework;

namespace Projecto
{
    internal class Enemy : DamageManager
    {
        //------------->CONSTRUCTORS<-------------//
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

namespace Projecto
{
    internal class Enemy : DamageManager
    {
        public Vector2 Coordinates;
        private Vector2 deltaPosition;
        public Collider enemyCollider;
        //private SpatialAStar<Tile, Object> aStar;
        private const float movingSpeed = 0.2f;

        private PathFinder pathFinder;
        private Stack<Vector2> listVectors;
        private Vector2 playerLastPosition;
        private Vector2 nextPosition;
        private float playerOneDistance;
        private float playerTwoDistance;



        //------------->CONSTRUCTORS<-------------//
        public Enemy(string texture, Vector2 position, Vector2 size, int range) : base("New Piskel", position, size, range)
        {
            playerLastPosition = -Vector2.One;
            //this.aStar = aStar;
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
        public void EnemyMovement(GameTime gameTime, ref PlayerManager player)
        {
            deltaPosition = Vector2.Zero;

            playerOneDistance = Math.Abs((this.Coordinates - GameState.PlayerOne.Coordinates).Length());
            playerTwoDistance = Math.Abs((this.Coordinates - GameState.PlayerTwo.Coordinates).Length());

            if (playerOneDistance <= playerTwoDistance && playerOneDistance < 10 && GameState.PlayerOne.Coordinates != playerLastPosition)
            {
                playerLastPosition = GameState.PlayerOne.Coordinates;
                pathFinder = new PathFinder(this.Coordinates, GameState.PlayerOne.Coordinates, ref MapGenerator.infoMap);
                listVectors = pathFinder.FindPath();
                if (listVectors.Count > 0)
                    nextPosition = listVectors.Pop();
            }
            else if (playerOneDistance > playerTwoDistance && playerTwoDistance < 10 && GameState.PlayerTwo.Coordinates != playerLastPosition)
            {
                playerLastPosition = GameState.PlayerTwo.Coordinates;
                pathFinder = new PathFinder(this.Coordinates, GameState.PlayerTwo.Coordinates, ref MapGenerator.infoMap);
                listVectors = pathFinder.FindPath();
                if (listVectors.Count > 0)
                    nextPosition = listVectors.Pop();
            }


            if (nextPosition == this.Coordinates && listVectors.Count > 0)
            {
                nextPosition = listVectors.Pop();
            }

            //Tile aux;

            //Vector2 enemyPos = new Vector2(this.Coordinates.X, this.Coordinates.Y);
            //Vector2 playerPos = new Vector2(player.Coordinates.X, player.Coordinates.Y);

            //LinkedList<Tile> path = aStar.Search(ref enemyPos, ref playerPos, null);

            //if (path.First.Next != null)
            //    aux = path.First.Next.Value;
            //else
            //    return;

            //Debug.NewLine(aux.Coordinates.X.ToString() + ", " + aux.Coordinates.Y.ToString());

            //Vector2 auxVector = new Vector2(aux.Coordinates.X - this.Coordinates.X, aux.Coordinates.Y - this.Coordinates.Y);
            deltaPosition = new Vector2(nextPosition.X - this.Coordinates.X, nextPosition.Y - this.Coordinates.Y) * movingSpeed;

            if (deltaPosition != Vector2.Zero)
            {
                this.Move(deltaPosition);
                this.enemyCollider.UpdateBounds(Position, Size);
                this.Position = this.enemyCollider.UpdatePosition(Position, Size);
                this.Coordinates = enemyCollider.UpdateTiles(Position, Size);
                this.enemyCollider.UpdateBounds(Position, Size);
            }
            //this.currentAnimation.Play(gameTime);
        }
    }
}




﻿using Microsoft.Xna.Framework;
//using SettlersEngine;
using System;
using System.Collections.Generic;