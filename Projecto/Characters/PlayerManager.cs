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
            this.playerCollider = new Collider(position, size);
            this.playerCollider.UpdateTiles(position, size);

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
                    if (currentInput != CurrentInput.Right)
                    {
                        //this.currentAnimation.Stop();
                        //this.currentAnimation = animations[3];
                        this.objectDiretion = Vector2.UnitX;
                        currentInput = CurrentInput.Right;
                    }
                    deltaPosition += Vector2.UnitX * movingSpeed;
                }
                if (InputManager.MovementPlayerOne.Left == ButtonState.Pressed && InputManager.MovementPlayerOne.Right != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Left)
                    {
                        this.objectDiretion = -Vector2.UnitX;
                        currentInput = CurrentInput.Left;
                    }
                    deltaPosition += -Vector2.UnitX * movingSpeed;
                }
                if (InputManager.MovementPlayerOne.Up == ButtonState.Pressed && InputManager.MovementPlayerOne.Down != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Up)
                    {
                        this.objectDiretion = Vector2.UnitY;
                        currentInput = CurrentInput.Up;
                    }
                    deltaPosition += Vector2.UnitY * movingSpeed;
                }
                if (InputManager.MovementPlayerOne.Down == ButtonState.Pressed && InputManager.MovementPlayerOne.Up != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Down)
                    {
                        this.objectDiretion = -Vector2.UnitY;
                        currentInput = CurrentInput.Down;
                    }
                    deltaPosition += -Vector2.UnitY * movingSpeed;
                }
                //attack def button
                if (InputManager.attckdefOne.Space == ButtonState.Pressed && InputManager.attckdefOne.Space != ButtonState.Pressed)
                {
                    //animação de attack
                    this.Atackrange(-Vector2.UnitY, 2f);
                    //Attack();
                }
                if (InputManager.attckdefOne.Q == ButtonState.Pressed && InputManager.attckdefOne.Q != ButtonState.Pressed)
                {
                    //animação de defense
                    //Defense();
                }
            }
            #endregion
            #region PlayerTwo
            if (pNumber == PlayerNumber.playerTwo)
            {
                //Movement Controls.
                if (InputManager.MovementPlayerTwo.Right == ButtonState.Pressed && InputManager.MovementPlayerTwo.Left != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Right)
                    {
                        this.objectDiretion = Vector2.UnitX;
                        //this.currentAnimation.Stop();
                        //this.currentAnimation = animations[3];
                        currentInput = CurrentInput.Right;
                    }
                    deltaPosition += Vector2.UnitX * movingSpeed;
                }
                if (InputManager.MovementPlayerTwo.Left == ButtonState.Pressed && InputManager.MovementPlayerTwo.Right != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Left)
                    {
                        this.objectDiretion = -Vector2.UnitX;
                        currentInput = CurrentInput.Left;
                    }
                    deltaPosition += -Vector2.UnitX * movingSpeed;
                }
                if (InputManager.MovementPlayerTwo.Up == ButtonState.Pressed && InputManager.MovementPlayerTwo.Down != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Up)
                    {
                        this.objectDiretion = Vector2.UnitY;
                        currentInput = CurrentInput.Up;
                    }
                    deltaPosition += Vector2.UnitY * movingSpeed;
                }
                if (InputManager.MovementPlayerTwo.Down == ButtonState.Pressed && InputManager.MovementPlayerTwo.Up != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Down)
                    {
                        this.objectDiretion = -Vector2.UnitY;
                        currentInput = CurrentInput.Down;
                    }
                    deltaPosition += -Vector2.UnitY * movingSpeed;
                }
                if (InputManager.attckdefOne.Space == ButtonState.Pressed && InputManager.attckdefOne.Space != ButtonState.Pressed)
                {
                    //o que fazer no attack
                }
                if (InputManager.attckdefOne.Q == ButtonState.Pressed && InputManager.attckdefOne.Q != ButtonState.Pressed)
                {
                    //o que fazer no DEFESA
                }
            }
            #endregion

            if (deltaPosition != Vector2.Zero)
            {
                this.playerCollider.UpdateDelta(ref deltaPosition);
                if (deltaPosition != Vector2.Zero)
                {
                    this.Move(deltaPosition);
                    this.Coordinates = playerCollider.UpdateTiles(Position, Size);
                    this.playerCollider.UpdateBounds(Position, Size);
                    this.Position = this.playerCollider.UpdatePosition(Position, Size);
                    this.Coordinates = playerCollider.UpdateTiles(Position, Size);
                    this.playerCollider.UpdateBounds(Position, Size);
                }
            }
            //this.currentAnimation.Play(gameTime);
        }

        ///// <summary>
        ///// Draws on screen an object, using a camera.
        ///// </summary>
        ///// <param name="camera">Camera to draw the image at.</param>
        //public override void DrawObject(Camera camera)
        //{
        //    if (isActive)
        //    {
        //        this.Rectangle = camera.CalculatePixelRectangle(this.Position, this.Size);
        //        Game1.spriteBatch.Draw(Texture, Rectangle, Color.White);
        //        //Game1.spriteBatch.Draw(currentAnimation.spriteTexture, this.Rectangle, currentAnimation.currentFrameRec, Color.White);
        //    }
        //}
    }
}

