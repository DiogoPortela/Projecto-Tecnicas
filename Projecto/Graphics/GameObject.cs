﻿using System;
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
        protected Texture2D Texture;            //Texture image.
        public Vector2 TextureCenter;           //For rotations.
        protected Rectangle Rectangle;          //Rectangle to draw to.

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 SizeCenter;
        public float RotationAngle { get; set; }

        //protected float speed;
        //protected Vector2 speedDirection;
        protected Vector2 objectDiretion;
        //public static CombatMod combatmod;

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
        public GameObject(string texture, Vector2 position, Vector2 size, float rotation)
        {
            if(texture != null)
            {
                this.Texture = Game1.content.Load<Texture2D>(texture);
                this.TextureCenter.X = Texture.Width / 2;
                this.TextureCenter.Y = Texture.Height / 2;
            }            
            this.Position = position;
            this.Size = size;
            this.RotationAngle = rotation;

            this.SizeCenter = new Vector2(size.X, -size.Y)/2;
            //this.speed = 0f;
            //this.speedDirection = Vector2.Zero;
            this.objectDiretion = -Vector2.UnitY;
            this.isActive = true;
        }

        //------------->FUNCTIONS && METHODS<-------------//

            public virtual void Update(GameTime gametime, List<GameObject> enemies)
        { }
        /// <summary>
        /// Moves the object in a given direction, with a given speed.
        /// </summary>
        /// <param name="direction">Movement direction.</param>
        /// <param name="speed">Movement speed.</param>
        public void Move(Vector2 direction, float speed)
        {
            this.Position += direction * speed;
            this.objectDiretion = direction;
        }
        /// <summary>
        /// Moves the object in a given direction.
        /// </summary>
        /// <param name="direction">Movement direction.</param>
        public void Move(Vector2 direction)
        {
            this.Position += direction;
            this.objectDiretion = direction;
        }

        public void Atackrange(Vector2 direction, float Range)
        {
            this.Position += direction * Range;
        }

        /// <summary>
        /// Draws an object on screen using a camera.
        /// </summary>
        /// <param name="camera">The camera to draw to.</param>
        public virtual void DrawObject(Camera camera)
        {
            if(isactive)
            {
                this.Rectangle = camera.CalculatePixelRectangle(this.Position, this.Size);
                Game1.spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
            }            
        }
    }
}
