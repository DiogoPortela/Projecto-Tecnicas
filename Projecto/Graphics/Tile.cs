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
        public bool isSomthingOnTop;
        public bool isWalkable;


        //------------->CONSTRUCTORS<-------------//

        public Tile(int tileNumber, Rectangle newRectangle, Vector2 coordinates)
        {
            Texture = Game1.content.Load<Texture2D>("Tile" + tileNumber);
            this.Rectangle = newRectangle;
            this.Position.X = newRectangle.X;
            this.Position.Y = newRectangle.Y;
            this.Coordinates = coordinates;
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void Draw(SpriteBatch spriteBatch)
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
