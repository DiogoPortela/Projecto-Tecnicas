using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Projecto
{
    internal class PlayerManager : GameObject
    {
        public PlayerNumber pNumber;
        private CurrentInput currentInput;
        protected Animation[] animations;
        public Animation currentAnimation;
        public Collider playerCollider;
        public Vector2 Coordinates;
        private const float movingSpeed = 0.4f;
        private Vector2 deltaPosition;

        //------------->CONSTRUCTORS<-------------//

        public PlayerManager(Vector2 position, Vector2 size, PlayerNumber number) : base("Drude", position, size, 0f)
        {
            this.animations = new Animation[18];
            this.pNumber = number;
            this.currentInput = CurrentInput.NoInput;

            Coordinates = (position / size);
            Coordinates.X = (int)Coordinates.X;
            Coordinates.Y = (int)Coordinates.Y;

            this.playerCollider = new Collider(Coordinates, size);
            this.playerCollider.InitTiles(Coordinates);

            if (number == PlayerNumber.playerOne)
            {
                //animations[2] = new Animation("walkAdult", "Walk Final P1", Vector2.One * 64, 8, 100f);
                //animations[3] = new Animation("walkKid", "Walk Kid P1", Vector2.One * 32, 8, 100f);
                //this.currentAnimation = animations[3];
            }
            else
            {
                //animations[1] = new Animation("walk", "Walk Final P2", Vector2.One * 64, 8, 100f);
                //this.currentAnimation = animations[1];
            }
        }

        //------------->FUNCTIONS && METHODS<-------------//


        /// <summary>
        /// Deals with all the movement and animations.
        /// </summary>
        /// <param name="gameTime"></param>
        public void PlayerMovement(GameTime gameTime)
        {
            deltaPosition = Vector2.Zero;
            #region PlayerOne
            if (pNumber == PlayerNumber.playerOne)
            {                
                //Movement Controls.
                if (InputManager.MovementPlayerOne.Right == ButtonState.Pressed && InputManager.MovementPlayerOne.Left != ButtonState.Pressed)
                {
                    /*if (currentInput != CurrentInput.Right)
                    {
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[3];
                        currentInput = CurrentInput.Right;

                    }*/
                    deltaPosition += Vector2.UnitX * movingSpeed;
                }
                if (InputManager.MovementPlayerOne.Left == ButtonState.Pressed && InputManager.MovementPlayerOne.Right != ButtonState.Pressed)
                {
                    deltaPosition += -Vector2.UnitX * movingSpeed;
                }
                if (InputManager.MovementPlayerOne.Up == ButtonState.Pressed && InputManager.MovementPlayerOne.Down != ButtonState.Pressed)
                {
                    deltaPosition += Vector2.UnitY * movingSpeed;
                }
                if (InputManager.MovementPlayerOne.Down == ButtonState.Pressed && InputManager.MovementPlayerOne.Up != ButtonState.Pressed)
                {
                    deltaPosition += -Vector2.UnitY * movingSpeed;
                }
            }
            #endregion
            #region PlayerTwo
            if (pNumber == PlayerNumber.playerTwo)
            {
                //Movement Controls.
                if (InputManager.MovementPlayerTwo.Right == ButtonState.Pressed && InputManager.MovementPlayerTwo.Left != ButtonState.Pressed)
                {
                    /*if (currentInput != CurrentInput.Right)
                    {
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[3];
                        currentInput = CurrentInput.Right;

                    }*/
                    deltaPosition += Vector2.UnitX * movingSpeed;
                }
                if (InputManager.MovementPlayerTwo.Left == ButtonState.Pressed && InputManager.MovementPlayerTwo.Right != ButtonState.Pressed)
                {
                    deltaPosition  += -Vector2.UnitX * movingSpeed;
                }
                if (InputManager.MovementPlayerTwo.Up == ButtonState.Pressed && InputManager.MovementPlayerTwo.Down != ButtonState.Pressed)
                {
                    deltaPosition += Vector2.UnitY * movingSpeed;
                }
                if (InputManager.MovementPlayerTwo.Down == ButtonState.Pressed && InputManager.MovementPlayerTwo.Up != ButtonState.Pressed)
                {
                    deltaPosition += - Vector2.UnitY * movingSpeed;
                }
            }
            #endregion

            if(deltaPosition != Vector2.Zero)
            {
                deltaPosition = playerCollider.UpdateDeltaWithCollisions(deltaPosition, ref Coordinates, Position);
                this.Move(deltaPosition);
            }
            //this.currentAnimation.Play(gameTime);
        }
        /*
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (Rectangle.TouchingTopOf(newRectangle))
            {
                Rectangle.Y = newRectangle.Y - Rectangle.Height;
                speedDirection.Y = 0f;
                hasJumped = false;
            }

            if (Rectangle.TouchingLeftOf(newRectangle))
            {
                position.X = newRectangle.X - Rectangle.Width - 2;
            }

            if (Rectangle.TouchingRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }

            if (Rectangle.TouchingBottomOf(newRectangle))
            {
                speedDirection.Y = 1f;
            }

            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.X > xOffset - Rectangle.Width)
            {
                position.X = xOffset - Rectangle.Width;
            }

            if (position.Y < 0)
            {
                speedDirection.Y = 1f;
            }

            if (position.Y > yOffset - Rectangle.Height)
            {
                position.Y = yOffset - Rectangle.Height;
            }

        }*/
        /// <summary>
        /// Draws on screen an object, using a camera.
        /// </summary>
        /// <param name="camera">Camera to draw the image at.</param>
        public override void DrawObject(Camera camera)
        {
            if (isActive)
            {
                this.Rectangle = camera.CalculatePixelRectangle(this.Position, this.Size);
                Game1.spriteBatch.Draw(Texture, Rectangle, Color.White);
                //Game1.spriteBatch.Draw(currentAnimation.spriteTexture, this.Rectangle, currentAnimation.currentFrameRec, Color.White);
            }
        }
    }
}
