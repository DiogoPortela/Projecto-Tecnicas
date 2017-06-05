using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto
{
    class Item
    {
        public string Name;
        public Texture2D Texture;
        public Rectangle Rectangle;
        public Vector2 Position;
        public PlayerManager Parent;

        //------------->CONSTRUCTORS<-------------//

        public Item(string name, string texture, Vector2 position, PlayerManager parent)
        {
            this.Name = name;
            if (Game1.textureList.ContainsKey(texture))
            {
                Texture = Game1.textureList[texture];
            }
            this.Position = position;
            this.Parent = parent;
        }

        //------------->FUNCTIONS && METHODS<-------------//

        public void Pickup(PlayerManager parent)
        {
            this.Parent = parent;
            parent.ItemDictionary.Add(this.Name, this);
            if (this is Weapon)
            {
                if (parent.mainHandWeapon == null)
                {
                    parent.mainHandWeapon = this as Weapon;
                    parent.GetDamageValues();
                }
                else if (parent.offHandWeapon == null)
                {
                    parent.offHandWeapon = this as Weapon;
                    parent.GetDamageValues();
                }
            }
        }
        /// <summary>
        /// Draws on screen an object, using a camera.
        /// </summary>
        /// <param name="camera">Camera to draw the image at.</param>
        public void Draw(Camera camera)
        {
            this.Rectangle = camera.CalculatePixelRectangle(this.Position, new Vector2((int)Constants.GRIDSIZE, (int)Constants.GRIDSIZE));
            Game1.spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
        public void DrawItemMenu(Camera camera)
        {
            this.Rectangle = camera.CalculatePixelRectangle(this.Position, new Vector2(10,10));
            Game1.spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
}
