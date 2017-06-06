using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    class Portal : GameObject
    {
        public bool isOpen;
        private Texture2D openTexture;
        private Texture2D closedTexture;
        private Texture2D currentTexture;
        public Vector2 Coordinates;
        private Collider portalCollider;

        public Portal(Vector2 position, Vector2 size) : base (null, position, size, 0f)
        {
            this.isOpen = false;
            this.portalCollider = new Collider(Position, Size);
            if (Game1.textureList.ContainsKey("portal"))
                openTexture = Game1.textureList["portal"];
            if (Game1.textureList.ContainsKey("portal"))
                closedTexture = Game1.textureList["portal"];
            currentTexture = closedTexture;
            this.Coordinates = position / (int)Constants.GRIDSIZE;
        }

        public void Update(GameTime gameTime, ref List<Enemy> enemyList)
        {
            if(enemyList.Count == 0)
            {
                this.isOpen = true;
                currentTexture = openTexture;
            }
        }

        public override void DrawObject(Camera camera)
        {
            if (isActive)
            {
                this.Rectangle = camera.CalculatePixelRectangle(this.Position, this.Size);
                Game1.spriteBatch.Draw(currentTexture, Rectangle, Color.White);
            }
        }
    }
}
