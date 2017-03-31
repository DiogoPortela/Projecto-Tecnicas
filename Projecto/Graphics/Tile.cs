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
        public bool isSomethingOnTop;
        public bool isWalkable;


        //------------->CONSTRUCTORS<-------------//

        public Tile(int tileNumber, Vector2 coordinates, int size) : base("Tile" + tileNumber,coordinates * size, Vector2.One, 0f)
        {
            //Texture = Game1.content.Load<Texture2D>("Tile" + tileNumber);
            this.Rectangle = Camera.CalculatePixelRectangle(new Vector2(coordinates.X * size, coordinates.Y * size),new Vector2(size, size));
            this.Coordinates = coordinates;
            //this.Position = coordinates * size;
            //this.Size = Vector2.One;
            //this.isActive = true;
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void DrawTile(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }

    //class CollisionTiles : Tile
    //{
    //    public CollisionTiles(int i, Rectangle newRectangle)
    //    {            
    //        Texture = Game1.content.Load<Texture2D>("Tile" + i);
    //        this.Rectangle = newRectangle;
    //    }
    //}
}
