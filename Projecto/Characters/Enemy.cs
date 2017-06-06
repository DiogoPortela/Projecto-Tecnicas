using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Projecto
{
    internal class Enemy : DamageManager
    {
        public Vector2 Coordinates;
        private Vector2 deltaPosition;
        public Collider enemyCollider;
        private const float movingSpeed = 0.2f;

        private PathFinder pathFinder;
        public Stack<Vector2> listVectors;
        private Vector2 playerLastPosition;
        private Vector2 nextPosition;
        private float playerOneDistance;
        private float playerTwoDistance;
        public float enemyTimerStart = 0f;
        public float enemyCooldown = 2f;
        public bool hasAttacked;

        //------------->CONSTRUCTORS<-------------//
        public Enemy(string texture, Vector2 position, Vector2 size, int range) : base(texture, position, size, range)
        {
            playerLastPosition = -Vector2.One;
            //this.aStar = aStar;
            Coordinates = (position / (float)Constants.GRIDSIZE);
            Coordinates.X = (int)Coordinates.X;
            Coordinates.Y = (int)Coordinates.Y;
            this.enemyCollider = new Collider(position, size);
            this.enemyCollider.UpdateTiles(position, size);
            this.listVectors = new Stack<Vector2>();
            nextPosition = Vector2.Zero;

            #region Stats initializer
            this.HP = 50;
            this.PhysDmg = 20;
            this.MagicDmg = 20;
            this.PhysDmgRes = 1;
            this.MagicDmgRes = 1;
            #endregion
        }
        public void Update(GameTime gameTime)
        {
           EnemyMovement(gameTime);
           EnemyDamage(gameTime);
        }
        public void EnemyMovement(GameTime gameTime)
        {
            deltaPosition = Vector2.Zero;

            playerOneDistance = Math.Abs((this.Coordinates - GameState.PlayerOne.Coordinates).Length());
            playerTwoDistance = Math.Abs((this.Coordinates - GameState.PlayerTwo.Coordinates).Length());

            if ((playerOneDistance <= playerTwoDistance && playerOneDistance < 5) || (playerOneDistance <= playerTwoDistance && GameState.EnemyList.Count <= 5))
            {
                playerLastPosition = GameState.PlayerOne.Coordinates;
                pathFinder = new PathFinder(this.Coordinates, GameState.PlayerOne.Coordinates, ref MapGenerator.infoMap);
                listVectors = pathFinder.FindPath();
                if (listVectors.Count > 0)
                    nextPosition = listVectors.Pop();
            }
            else if ((playerOneDistance > playerTwoDistance && playerTwoDistance < 5) || (playerOneDistance <= playerTwoDistance && GameState.EnemyList.Count <= 5))
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

            deltaPosition = new Vector2(nextPosition.X - this.Coordinates.X, nextPosition.Y - this.Coordinates.Y) * movingSpeed;

            //if (nextPosition != Vector2.Zero && deltaPosition != Vector2.Zero)
            //{
            //    this.Move(deltaPosition);
            //    this.Coordinates = new Vector2((int)(this.Position.X / (float)Constants.GRIDSIZE + 0.5f), (int)(this.Position.Y / (int)Constants.GRIDSIZE + 0.5f));
            //}
            if (deltaPosition != Vector2.Zero)
                this.enemyCollider.EnemyCollision(this, ref deltaPosition);

            if (nextPosition != Vector2.Zero && deltaPosition != Vector2.Zero)
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