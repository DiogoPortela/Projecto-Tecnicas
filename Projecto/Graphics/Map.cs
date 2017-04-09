﻿using System;
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

        public Vector2 ArraySize; // guar

        //------------->CONSTRUCTORS<-------------//

        public Map()
        {
            listOfTiles = new List<Tile>();
        }

        //------------->FUNCTIONS && METHODS<-------------//
        /// <summary>
        /// Generates a map using an array.
        /// </summary>
        /// <param name="map">The Array to generate the map.</param>
        /// <param name="size">Size of each item in the array.</param>
        public void Generate(int[,] map, int size)
        {
            for(int x= 0; x < map.GetLength(1); x++)
                for(int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                        listOfTiles.Add(new Tile(number, new Vector2(x, y + 1), size));
                    //width = (x + 1) * size;
                    //height = (y + 1) * size;
                }
        }
        /// <summary>
        /// Draws the map using a camera.
        /// </summary>
        /// <param name="camera">Camera to draw to.</param>
        public void Draw(Camera camera)
        {
            foreach (Tile tile in ListOfTiles)
                tile.DrawTile(camera);
        }
    }
}
