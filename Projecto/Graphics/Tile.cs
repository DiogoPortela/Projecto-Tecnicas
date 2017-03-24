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
    class Tile
    {
        protected Texture2D texture;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    class CollisionTiles : Tile
    {
        public CollisionTiles(int i, Rectangle newRectangle)
        {            
            texture = Game1.content.Load<Texture2D>("Tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}
