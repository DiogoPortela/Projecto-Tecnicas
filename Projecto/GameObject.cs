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
        protected Texture2D Texture;
        protected Vector2 TextureCenter; //For rotations.
        protected Rectangle Rectangle;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public float RotationAngle { get; set; }

        protected Vector2 speed;
        protected Vector2 speedDirection;
        protected Vector2 objectDiretion;

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
        public GameObject(Texture2D texture, Vector2 position, Vector2 size, float rotation)
        {
            this.Texture = texture;
            this.TextureCenter.X = texture.Width / 2;
            this.TextureCenter.Y = texture.Height / 2;
            this.Position = position;
            this.Size = size;
            this.RotationAngle = rotation;
            this.speed = Vector2.Zero;
            this.speedDirection = Vector2.Zero;
            this.objectDiretion = -Vector2.UnitY;
            this.isActive = true;
        }


        //------------->FUNCTIONS && METHODS<-------------//

        public void MovePositionByVector(Vector2 newPosition)
        {
            this.Position += newPosition;
        }
        public void MovePositionBySpeed()
        {
            this.Position = speed * speedDirection;
        }
        public void Rotate()
        {

        }
        public void DrawObject(SpriteBatch spritebatch)
        {
            spritebatch.Draw(this.Texture, this.Rectangle, Color.White);
        }
    }
}
