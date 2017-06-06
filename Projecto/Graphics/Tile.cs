using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Projecto
{
    internal class Tile : GameObject
    {
        public Vector2 Coordinates;
        public Collider Collider;
        public bool isSomethingOnTop;
        public int TileNumber;
        public bool isWall;
        private Color Color;
        //------------->CONSTRUCTORS<-------------//

        public Tile() { }

        public Tile(int tileNumber, Vector2 coordinates, int size) : base("Tile" + tileNumber, coordinates * size, new Vector2(size, size), 0f)
        {
            if (tileNumber < 10)
            {
                isWall = true;
                this.Color = new Color(Game1.random.Next(150, 256), Game1.random.Next(150, 256), 255);
            }
            else
                this.Color = new Color(Game1.random.Next(200, 256), Game1.random.Next(200, 256), 255);
            this.TileNumber = tileNumber;
            this.Coordinates = coordinates;
            this.isSomethingOnTop = false;
            this.Collider = new Collider(coordinates * size, new Vector2(size, size));

        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void DrawTile(Camera camera)
        {
            this.Rectangle = camera.CalculatePixelRectangle(Position, Size);
            Game1.spriteBatch.Draw(Texture, Rectangle, Color);
        }

        public void DrawPathTile(Camera camera)
        {
            this.Rectangle = camera.CalculatePixelRectangle(Position, Size);
            Game1.spriteBatch.Draw(Game1.textureList["DebugPixel"], Rectangle, Color);
        }

        public bool isWalkable(Object unused)
        {
            return !isWall;
        }
    }
}