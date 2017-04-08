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
        public void UpdateCollisions(Vector2 coordinates)
        {
            Tiles[0] = MapGenerator.TilesMap[(int)coordinates.X + 1, (int)coordinates.Y];
            Tiles[1] = MapGenerator.TilesMap[(int)coordinates.X, (int)coordinates.Y + 1];
            Tiles[2] = MapGenerator.TilesMap[(int)coordinates.X - 1, (int)coordinates.Y];
            Tiles[3] = MapGenerator.TilesMap[(int)coordinates.X, (int)coordinates.Y - 1];
            MapGenerator.TilesMap[(int)coordinates.X, (int)coordinates.Y].isSomethingOnTop = true;
        }
        public void UpdateCollisions(Vector2 coordinates, Vector2 deltaPosition, Vector2 position)
        {
            Vector2 aux = deltaPosition + position;
            Vector2 coorAux = new Vector2((int)aux.X, (int)aux.Y);

            if(coorAux != coordinates)
            {
                MapGenerator.TilesMap[(int)coordinates.X, (int)coordinates.Y].isSomethingOnTop = false;
                Tiles[0] = MapGenerator.TilesMap[(int)coorAux.X + 1,    (int)coorAux.Y];
                Tiles[1] = MapGenerator.TilesMap[(int)coorAux.X,        (int)coorAux.Y + 1];
                Tiles[2] = MapGenerator.TilesMap[(int)coorAux.X - 1,    (int)coorAux.Y];
                Tiles[3] = MapGenerator.TilesMap[(int)coorAux.X,        (int)coorAux.Y - 1];
                MapGenerator.TilesMap[(int)coorAux.X, (int)coorAux.Y].isSomethingOnTop = true;
            }
            
        }
        public CollisionBool CollisionDirection(Vector2 minBound, Vector2 maxBound)
        {
            CollisionBool result = new CollisionBool();
            result.Init();
            if ((!Tiles[0].isWalkable || Tiles[0].isSomethingOnTop) && Tiles[0].MinBound.X < maxBound.X)
            {
                result.hasCollided = result.right = true;
            }
            if ((!Tiles[1].isWalkable || Tiles[1].isSomethingOnTop) && Tiles[1].MinBound.Y < maxBound.Y)
            {
                result.hasCollided = result.top = true;
            }
            if ((!Tiles[2].isWalkable || Tiles[2].isSomethingOnTop) && Tiles[2].MaxBound.X > minBound.Y)
            {
                result.hasCollided = result.left = true;
            }
            if ((!Tiles[3].isWalkable || Tiles[3].isSomethingOnTop) && Tiles[3].MaxBound.Y > minBound.Y)
            {
                result.hasCollided = result.bottom = true;
            }
            return result;
        }
        public Vector2 UpdateDeltaWithCollisions(Vector2 deltaPosition, Vector2 minBound, Vector2 maxBound)
        {
            CollisionBool testCollisions = CollisionDirection(minBound, maxBound);
            if(testCollisions.hasCollided)
            {
                if(testCollisions.right && deltaPosition.X > 0)
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
