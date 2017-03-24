using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto
{
    class Map
    {
        private List<CollisionTiles> collisionTiles;
        private int height;
        private int width;

        public List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }
        public int Height
        {
            get { return height; }
        }
        public int Width
        {
            get { return width; }
        }


        public Map()
        {
            collisionTiles = new List<CollisionTiles>();
        }

        public void Generate(int[,] map, int size)
        {
            for(int x= 0; x < map.GetLength(1); x++)
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                        CollisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));
                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in collisionTiles)
                tile.Draw(spriteBatch);

        }
    }
}
