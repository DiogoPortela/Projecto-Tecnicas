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
        protected Vector2 MinBound;
        protected Vector2 MaxBound;

        //------------->CONSTRUCTORS<-------------//

        public Collider(Vector2 position, Vector2 size)
        {
            this.MinBound = position - new Vector2(0, size.Y);
            this.MaxBound = position + new Vector2(size.X, 0);
        }

        //------------->FUNCTIONS && METHODS<-------------//
        public void InitTiles(Vector2 coordinates)
        {
            Tiles[0] = MapGenerator.TilesMap[(int)coordinates.X + 1, (int)coordinates.Y];
            Tiles[1] = MapGenerator.TilesMap[(int)coordinates.X, (int)coordinates.Y + 1];
            Tiles[2] = MapGenerator.TilesMap[(int)coordinates.X - 1, (int)coordinates.Y];
            Tiles[3] = MapGenerator.TilesMap[(int)coordinates.X, (int)coordinates.Y - 1];
            TilesDiagonal[0] = MapGenerator.TilesMap[(int)coordinates.X + 1, (int)coordinates.Y + 1];
            TilesDiagonal[1] = MapGenerator.TilesMap[(int)coordinates.X - 1, (int)coordinates.Y + 1];
            TilesDiagonal[2] = MapGenerator.TilesMap[(int)coordinates.X - 1, (int)coordinates.Y - 1];
            TilesDiagonal[3] = MapGenerator.TilesMap[(int)coordinates.X + 1, (int)coordinates.Y - 1];
            MapGenerator.TilesMap[(int)coordinates.X, (int)coordinates.Y].isSomethingOnTop = true;
        }
        private void UpdateTiles(ref Vector2 coordinates, Vector2 nextCoordinates)
        {
            //Rounds the position to coordinates;
            Vector2 coorAux = new Vector2((int)nextCoordinates.X, (int)nextCoordinates.Y);

            //If the coordinates have changed then so too should the Tile array.
            if (coorAux != coordinates)
            {
                MapGenerator.TilesMap[(int)coordinates.X, (int)coordinates.Y].isSomethingOnTop = false;
                Tiles[0] = MapGenerator.TilesMap[(int)coorAux.X + 1, (int)coorAux.Y];
                Tiles[1] = MapGenerator.TilesMap[(int)coorAux.X, (int)coorAux.Y + 1];
                Tiles[2] = MapGenerator.TilesMap[(int)coorAux.X - 1, (int)coorAux.Y];
                Tiles[3] = MapGenerator.TilesMap[(int)coorAux.X, (int)coorAux.Y - 1];
                TilesDiagonal[0] = MapGenerator.TilesMap[(int)coordinates.X + 1, (int)coordinates.Y + 1];
                TilesDiagonal[1] = MapGenerator.TilesMap[(int)coordinates.X - 1, (int)coordinates.Y + 1];
                TilesDiagonal[2] = MapGenerator.TilesMap[(int)coordinates.X - 1, (int)coordinates.Y - 1];
                TilesDiagonal[3] = MapGenerator.TilesMap[(int)coordinates.X + 1, (int)coordinates.Y - 1];
                MapGenerator.TilesMap[(int)coorAux.X, (int)coorAux.Y].isSomethingOnTop = true;

                coordinates = coorAux;
            }

        }
        /// <summary>
        /// Returns a struct that store whether if there has been a colision and where.
        /// </summary>
        /// <param name="minAux">MinBound with deltaPostion</param>
        /// <param name="maxAux">MaxBound with deltaPosition</param>
        /// <returns></returns>
        private CollisionBool CollisionDirection(Vector2 minAux, Vector2 maxAux)
        {
            CollisionBool result = new CollisionBool();
            result.Init();
            if ((!Tiles[0].isWalkable || Tiles[0].isSomethingOnTop) && Tiles[0].Collider.MinBound.X < maxAux.X)
            {
                result.hasCollided = result.right = true;
            }
            if ((!Tiles[1].isWalkable || Tiles[1].isSomethingOnTop) && Tiles[1].Collider.MinBound.Y < maxAux.Y)
            {
                result.hasCollided = result.top = true;
            }
            if ((!Tiles[2].isWalkable || Tiles[2].isSomethingOnTop) && Tiles[2].Collider.MaxBound.X > minAux.X)
            {
                result.hasCollided = result.left = true;
            }
            if ((!Tiles[3].isWalkable || Tiles[3].isSomethingOnTop) && Tiles[3].Collider.MaxBound.Y > minAux.Y)
            {
                result.hasCollided = result.bottom = true;
            }            
            return result;
        }
        public Vector2 UpdateDeltaWithCollisions(Vector2 deltaPosition, ref Vector2 coordinates, Vector2 position)
        {
            Vector2 minAux = MinBound + deltaPosition;
            Vector2 maxAux = MaxBound + deltaPosition;
            CollisionBool testCollisions = CollisionDirection(minAux, maxAux);
            if (testCollisions.hasCollided)
            {
                if (testCollisions.right && deltaPosition.X > 0)
                {
                    deltaPosition.X = 0;
                }
                if (testCollisions.top && deltaPosition.Y > 0)
                {
                    deltaPosition.Y = 0;
                }
                if (testCollisions.left && deltaPosition.X < 0)
                {
                    deltaPosition.X = 0;
                }
                if (testCollisions.bottom && deltaPosition.Y < 0)
                {
                    deltaPosition.Y = 0;
                }
            }
            UpdateTiles(ref coordinates, (deltaPosition + position) / 5);
            MinBound += deltaPosition;
            MaxBound += deltaPosition;
            return deltaPosition;
        }
    }

    static class CollisionManager
    {
        public static bool TouchingTopOf(this Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Bottom >= rect2.Top - 1 &&
                    rect1.Bottom <= rect2.Top + (rect2.Height / 2) &&
                    rect1.Right >= rect2.Left + rect2.Width / 5 &&
                    rect1.Left <= rect2.Right - rect2.Width / 5);
        }
        public static bool TouchingBottomOf(this Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Top <= rect2.Bottom + (rect2.Height / 5) &&
                    rect1.Top >= rect2.Bottom - 1 &&
                    rect1.Right >= rect2.Left + (rect2.Width / 5) &&
                    rect1.Left <= rect2.Right - (rect2.Width / 5));
        }
        public static bool TouchingLeftOf(this Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Right <= rect2.Right &&
                    rect1.Right >= rect2.Left - 5 &&
                    rect1.Top <= rect2.Bottom - (rect2.Width / 4) &&
                    rect1.Bottom >= rect2.Top + (rect2.Width / 4));
        }
        public static bool TouchingRightOf(this Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Left >= rect2.Left &&
                    rect1.Left <= rect2.Right + 5 &&
                    rect1.Top <= rect2.Bottom - (rect2.Width / 4) &&
                    rect1.Bottom >= rect2.Top + (rect2.Width / 4));
        }
    }
}
