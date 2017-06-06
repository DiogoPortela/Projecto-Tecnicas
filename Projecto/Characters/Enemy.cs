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
        private float enemyTimerStart = 0f;
        private float enemyCooldown = 2f;
        private bool hasAttacked;



        //------------->CONSTRUCTORS<-------------//
        public Enemy(string texture, Vector2 position, Vector2 size, int range) : base("New Piskel", position, size, range)
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
            this.PhysDmg = 5;
            this.MagicDmg = 5;
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

            if (playerOneDistance <= playerTwoDistance && playerOneDistance < 5 /*&& GameState.PlayerOne.Coordinates != playerLastPosition*/)
            {
                playerLastPosition = GameState.PlayerOne.Coordinates;
                pathFinder = new PathFinder(this.Coordinates, GameState.PlayerOne.Coordinates, ref MapGenerator.infoMap);
                listVectors = pathFinder.FindPath();
                if (listVectors.Count > 0)
                    nextPosition = listVectors.Pop();
            }
            else if (playerOneDistance > playerTwoDistance && playerTwoDistance < 5 /*&& GameState.PlayerTwo.Coordinates != playerLastPosition*/)
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
        public void EnemyDamage(GameTime gameTime)
        {
            List<Enemy> auxEnemy1 = new List<Enemy>();
            List<Enemy> auxEnemy2 = new List<Enemy>();
            ParticleSystem p = new ParticleSystem(ParticleType.Explosion, "DebugPixel", GameState.PlayerOne.Center, Vector2.One * 0.5f, 4, 0, 3, 500, 500, 10);

            foreach (Enemy enemy in GameState.EnemyList)
            {
                if (enemy.IsInRange(GameState.PlayerOne) == true)
                    auxEnemy1.Add(enemy);
                if (enemy.IsInRange(GameState.PlayerTwo) == true)
                    auxEnemy2.Add(enemy);
            }

            foreach (Enemy e in auxEnemy1)
            {
                if (gameTime.TotalGameTime.TotalSeconds > e.enemyTimerStart + e.enemyCooldown)
                {
                    e.enemyTimerStart = (float)gameTime.TotalGameTime.TotalSeconds;
                    e.hasAttacked = false;
                }

                if (e.hasAttacked == false)
                {
                    PlayerTakeDamage(GameState.PlayerOne);
                    //PlayerGetKnockedBack(GameState.PlayerOne, new Vector2(e.Coordinates.X + GameState.PlayerOne.Coordinates.X, e.Coordinates.Y + GameState.PlayerOne.Coordinates.Y));
                    p.Start();
                    GameState.ParticlesList.Add(p);
                    e.hasAttacked = true;
                }

                if (!e.IsInRange(GameState.PlayerOne))
                    auxEnemy1.Remove(e);
            }
            for(int i = 0; i < auxEnemy2.Count; i++ )
            { 
                if (gameTime.TotalGameTime.TotalSeconds > auxEnemy2[i].enemyTimerStart + auxEnemy2[i].enemyCooldown)
                {
                    auxEnemy2[i].enemyTimerStart = (float)gameTime.TotalGameTime.TotalSeconds;
                    auxEnemy2[i].hasAttacked = false;
                }
 
                if (hasAttacked == false)
                {
                    PlayerTakeDamage(GameState.PlayerTwo);
                    //PlayerGetKnockedBack(GameState.PlayerTwo, new Vector2(e.Coordinates.X + GameState.PlayerTwo.Coordinates.X, e.Coordinates.Y + GameState.PlayerTwo.Coordinates.Y));
                    p.Start();
                    GameState.ParticlesList.Add(p);
                    auxEnemy2[i].hasAttacked = true;
                }
            
                if (!auxEnemy2[i].IsInRange(GameState.PlayerOne) == false)
                    auxEnemy2.Remove(auxEnemy2[i]);
            }
        }
    }
}