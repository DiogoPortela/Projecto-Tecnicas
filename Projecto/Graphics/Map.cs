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
        private List<Tile> listOfTiles;
        private int height;
        private int width;

        public List<Tile> ListOfTiles
        {
            get { return listOfTiles; }
        }
        public int Height
        {
            get { return height; }
        }
        public int Width
        {
            get { return width; }
        }

        //------------->CONSTRUCTORS<-------------//

        public Map()
        {
            listOfTiles = new List<Tile>();
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void Generate(int[,] map, int size)
        {
            for(int x= 0; x < map.GetLength(1); x++)
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                        listOfTiles.Add(new Tile(number, Camera.CalculatePixelRectangle(new Vector2(x * size, (y + 1) * size), new Vector2(size, size)), new Vector2(x + 1, y + 1)));
                    //width = (x + 1) * size;
                    //height = (y + 1) * size;
                }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in ListOfTiles)
                tile.Draw(spriteBatch);
        }
    }
}
