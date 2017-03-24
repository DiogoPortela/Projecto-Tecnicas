using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto
{
    public class GameObject
    {
        public Texture2D Texture;
        public Rectangle Rectangle;

        public Vector2 Position;
        public Vector2 Size;
        public Vector2 Rotation;
       

        private bool isactive;
        public bool isActive
        {
            get { return isactive; }
            set {
                    if(value == true)
                {
                    //TURN OFF TEXTURE AND MOVEMENT.
                }
                    else
                {
                    //TURN ON TEXTURE AND MOVMENT.
                }
                    isactive = value;
                }
        } //If it's necessary to turn an object on/off.


        //------------->CONSTRUCTORS<-------------//


        //Empty constructor, therefore is not active.
        public GameObject()
        {
            isActive = false;
        }

        //Constructor with all the atributtes that can be set.
        public GameObject(Texture2D texture, Vector2 position, Vector2 size, Vector2 rotation)
        {
            this.Texture = texture;
            this.Position = position;
            this.Size = size;
            this.Rotation = rotation;
        }


        //------------->FUNCTIONS && METHODS<-------------//

        public void UpdateObject()
        {

        }

        public void DrawObject(SpriteBatch spritebatch)
        {

        }

    }
}
