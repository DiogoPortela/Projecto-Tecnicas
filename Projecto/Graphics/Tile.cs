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
    class Tile : GameObject
    {
        public Vector2 Coordinates;
        public Collider Collider;
        public bool isSomethingOnTop;
        public bool isWalkable;
        public int TileNumber;

        //------------->CONSTRUCTORS<-------------//

        public Tile(int tileNumber, Vector2 coordinates, int size) : base("Tile" + tileNumber, coordinates * size, new Vector2(size, size), 0f)
        {
            this.TileNumber = tileNumber;
            this.Coordinates = coordinates;
            this.isSomethingOnTop = false;
            this.isWalkable = true;
            this.Collider = new Collider(coordinates * size, new Vector2(size, size));

            if(tileNumber == 1)
            {
                isWalkable = false;
            }
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void DrawTile(Camera camera)
        {
            this.Rectangle = camera.CalculatePixelRectangle(Position, Size);
            Game1.spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
