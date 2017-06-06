using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Projecto
{
    class Collider
    {
        protected Tile[] Tiles = new Tile[4];
        protected Tile[] TilesDiagonal = new Tile[4];
        internal Vector2 MinBound;
        internal Vector2 MaxBound;
        private static List<Enemy> movingEnemiesList = new List<Enemy>();

        //------------->CONSTRUCTORS<-------------//

        public Collider(Vector2 position, Vector2 size)
        {
            UpdateBounds(position, size);
        }

        //------------->FUNCTIONS && METHODS<-------------//
        /// <summary>
        /// Updates the list of tiles surrounding an object.
        /// </summary>
        /// <param name="position">Position of the object.</param>
        /// <param name="size">Size of the object.</param>
        /// <returns></returns>
        public Vector2 UpdateTiles(Vector2 position, Vector2 size)
        {
            //Rounds the position to coordinates;
            Vector2 coorAux = new Vector2((int)(position.X / size.X + 0.5), (int)(position.Y / size.X + 0.5));
            try
            {
                Tiles[0] = MapGenerator.TilesMap[(int)coorAux.X + 1, (int)coorAux.Y];
                Tiles[1] = MapGenerator.TilesMap[(int)coorAux.X, (int)coorAux.Y + 1];
                Tiles[2] = MapGenerator.TilesMap[(int)coorAux.X - 1, (int)coorAux.Y];
                Tiles[3] = MapGenerator.TilesMap[(int)coorAux.X, (int)coorAux.Y - 1];
                TilesDiagonal[0] = MapGenerator.TilesMap[(int)coorAux.X + 1, (int)coorAux.Y + 1];
                TilesDiagonal[1] = MapGenerator.TilesMap[(int)coorAux.X - 1, (int)coorAux.Y + 1];
                TilesDiagonal[2] = MapGenerator.TilesMap[(int)coorAux.X - 1, (int)coorAux.Y - 1];
                TilesDiagonal[3] = MapGenerator.TilesMap[(int)coorAux.X + 1, (int)coorAux.Y - 1];
            }
            catch
            {
                Debug.NewLine("Out of World bounds.");
            }

            return coorAux;
        }
        /// <summary>
        /// Updates the MinBound and Maxbound of an object. (use after movement)
        /// </summary>
        /// <param name="position">Position of the object.</param>
        /// <param name="size">Size of the object.</param>
        public void UpdateBounds(Vector2 position, Vector2 size)
        {
            MaxBound = new Vector2(position.X + size.X, position.Y);
            MinBound = new Vector2(position.X, position.Y - size.Y);
        }
        /// <summary>
        /// Updates the transformation delta using the tiles that surround the object.
        /// </summary>
        /// <param name="deltaPosition">The delta to be updated.</param>
        public void UpdateDelta(ref Vector2 deltaPosition)
        {
            if (deltaPosition.X > 0)
            {
                if ((!Tiles[0].isWalkable(this) || Tiles[0].isSomethingOnTop) && Tiles[0].Collider.MinBound.X <= MaxBound.X)
                {
                    //position.X = Tiles[0].Collider.MinBound.X - size.X;
                    deltaPosition.X = 0;
                }
                else if ((!TilesDiagonal[0].isWalkable(this) || TilesDiagonal[0].isSomethingOnTop) && TilesDiagonal[0].Collider.MinBound.Y < MaxBound.Y && TilesDiagonal[0].Collider.MinBound.X <= MaxBound.X)
                {
                    deltaPosition.X = 0;
                }
                else if ((!TilesDiagonal[3].isWalkable(this) || TilesDiagonal[3].isSomethingOnTop) && TilesDiagonal[3].Collider.MaxBound.Y > MinBound.Y && TilesDiagonal[3].Collider.MinBound.X <= MaxBound.X)
                {
                    deltaPosition.X = 0;
                }
            }

            if (deltaPosition.X < 0)
            {
                if ((!Tiles[2].isWalkable(this) || Tiles[2].isSomethingOnTop) && Tiles[2].Collider.MaxBound.X >= MinBound.X)
                {
                    deltaPosition.X = 0;
                }
                else if ((!TilesDiagonal[1].isWalkable(this) || TilesDiagonal[1].isSomethingOnTop) && TilesDiagonal[1].Collider.MinBound.Y < MaxBound.Y && TilesDiagonal[1].Collider.MaxBound.X >= MinBound.X)
                {
                    deltaPosition.X = 0;
                }
                else if ((!TilesDiagonal[2].isWalkable(this) || TilesDiagonal[2].isSomethingOnTop) && TilesDiagonal[2].Collider.MaxBound.Y > MinBound.Y && TilesDiagonal[2].Collider.MaxBound.X >= MinBound.X)
                {
                    deltaPosition.X = 0;
                }
            }
            if (deltaPosition.Y > 0)
            {
                if ((!Tiles[1].isWalkable(this) || Tiles[1].isSomethingOnTop) && Tiles[1].Collider.MinBound.Y <= MaxBound.Y)
                {
                    deltaPosition.Y = 0;
                }
                else if ((!TilesDiagonal[0].isWalkable(this) || TilesDiagonal[0].isSomethingOnTop) && TilesDiagonal[0].Collider.MinBound.X < MaxBound.X && TilesDiagonal[0].Collider.MinBound.Y <= MaxBound.Y)
                {
                    deltaPosition.Y = 0;
                }
                else if ((!TilesDiagonal[1].isWalkable(this) || TilesDiagonal[1].isSomethingOnTop) && TilesDiagonal[1].Collider.MaxBound.X > MinBound.X && TilesDiagonal[1].Collider.MinBound.Y <= MaxBound.Y)
                {
                    deltaPosition.Y = 0;
                }
            }
            if (deltaPosition.Y < 0)
            {
                if ((!Tiles[3].isWalkable(this) || Tiles[3].isSomethingOnTop) && Tiles[3].Collider.MaxBound.Y >= MinBound.Y)
                {
                    deltaPosition.Y = 0;
                }
                else if ((!TilesDiagonal[2].isWalkable(this) || TilesDiagonal[2].isSomethingOnTop) && TilesDiagonal[2].Collider.MaxBound.X > MinBound.X && TilesDiagonal[2].Collider.MaxBound.Y >= MinBound.Y)
                {
                    deltaPosition.Y = 0;
                }
                else if ((!TilesDiagonal[3].isWalkable(this) || TilesDiagonal[3].isSomethingOnTop) && TilesDiagonal[3].Collider.MinBound.X < MaxBound.X && TilesDiagonal[3].Collider.MaxBound.Y >= MinBound.Y)
                {
                    deltaPosition.Y = 0;
                }
            }
        }
        /// <summary>
        /// Updates the position of an object. Forces it to not go "beyond" colliders.
        /// </summary>
        /// <param name="position">Position of the object.</param>
        /// <param name="size">Size of the obejct.</param>
        /// <returns></returns>
        public Vector2 UpdatePosition(Vector2 position, Vector2 size)
        {
            if ((!Tiles[0].isWalkable(this) || Tiles[0].isSomethingOnTop) && Tiles[0].Collider.MinBound.X < MaxBound.X)
            {
                position.X = Tiles[0].Collider.MinBound.X - size.X;
            }
            if ((!Tiles[1].isWalkable(this) || Tiles[1].isSomethingOnTop) && Tiles[1].Collider.MinBound.Y < MaxBound.Y)
            {
                position.Y = Tiles[1].Collider.MinBound.Y;
            }
            if ((!Tiles[2].isWalkable(this) || Tiles[2].isSomethingOnTop) && Tiles[2].Collider.MaxBound.X > MinBound.X)
            {
                position.X = Tiles[2].Collider.MaxBound.X;
            }
            if ((!Tiles[3].isWalkable(this) || Tiles[3].isSomethingOnTop) && Tiles[3].Collider.MaxBound.Y > MinBound.Y)
            {
                position.Y = Tiles[3].Collider.MaxBound.Y + size.Y;
            }
            return position;
        }

        public static void UpdateMovingEnemies()
        {
            foreach (Enemy e in GameState.EnemyList)
            {
                if (e.listVectors.Count() > 0)
                    movingEnemiesList.Add(e);
            }
            for (int i = 0; i < movingEnemiesList.Count(); i++)
            {
                if (movingEnemiesList[i].listVectors.Count() == 0)
                    movingEnemiesList.Remove(movingEnemiesList[i]);
            }
        }
        public void EnemyCollision(Enemy thisEnemy, ref Vector2 deltaPosition)
        {
            foreach (Enemy e in movingEnemiesList)
            {
                //if (thisEnemy != e && thisEnemy.Rectangle.Intersects(e.Rectangle))
                //    deltaPosition = -deltaPosition;

                if (thisEnemy != e && thisEnemy.Rectangle.Intersects(e.Rectangle))
                {
                    deltaPosition = Vector2.Zero;
                }
            }
        }
    }
}
