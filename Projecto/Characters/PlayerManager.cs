using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    internal class PlayerManager : DamageManager
    {
        public PlayerNumber pNumber;
        private CurrentInput currentInput;
        protected Animation[] animations;
        public Animation currentAnimation;
        private const float movingSpeed = 0.4f;
        public Collider playerCollider;
        public Vector2 Coordinates;
        private Vector2 deltaPosition;

        //------------->CONSTRUCTORS<-------------//

        public PlayerManager(Vector2 position, Vector2 size, PlayerNumber number, int range) : base("Drude", position, size, range)
        {
            this.animations = new Animation[18];
            this.pNumber = number;
            this.currentInput = CurrentInput.NoInput;
            Coordinates = (position / size);
            Coordinates.X = (int)Coordinates.X;
            Coordinates.Y = (int)Coordinates.Y;
            this.playerCollider = new Collider(position, size);
            this.playerCollider.UpdateTiles(position, size);

            #region Stats initializer
            this.HP = 100;
            //this.PhysDmg = 18.0f;
            //this.MagicDmg = 8.0f;
            //this.PhysDmgRes = 5.0f;
            //this.MagicDmgRes = 5.0f;
            #endregion

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
                if (InputManager.PlayerOne.Right == ButtonState.Pressed && InputManager.PlayerOne.Left != ButtonState.Pressed)
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
                if (InputManager.PlayerOne.Left == ButtonState.Pressed && InputManager.PlayerOne.Right != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Left)
                    {
                        this.objectDiretion = -Vector2.UnitX;
                        currentInput = CurrentInput.Left;
                    }
                    deltaPosition += -Vector2.UnitX * movingSpeed;
                }
                if (InputManager.PlayerOne.Up == ButtonState.Pressed && InputManager.PlayerOne.Down != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Up)
                    {
                        this.objectDiretion = Vector2.UnitY;
                        currentInput = CurrentInput.Up;
                    }
                    deltaPosition += Vector2.UnitY * movingSpeed;
                }
                if (InputManager.PlayerOne.Down == ButtonState.Pressed && InputManager.PlayerOne.Up != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Down)
                    {
                        this.objectDiretion = -Vector2.UnitY;
                        currentInput = CurrentInput.Down;
                    }
                    deltaPosition += -Vector2.UnitY * movingSpeed;
                }
            }
            #endregion
            #region PlayerTwo
            if (pNumber == PlayerNumber.playerTwo)
            {
                //Movement Controls.
                if (InputManager.PlayerTwo.Right == ButtonState.Pressed && InputManager.PlayerTwo.Left != ButtonState.Pressed)
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
                if (InputManager.PlayerTwo.Left == ButtonState.Pressed && InputManager.PlayerTwo.Right != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Left)
                    {
                        this.objectDiretion = -Vector2.UnitX;
                        currentInput = CurrentInput.Left;
                    }
                    deltaPosition += -Vector2.UnitX * movingSpeed;
                }
                if (InputManager.PlayerTwo.Up == ButtonState.Pressed && InputManager.PlayerTwo.Down != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Up)
                    {
                        this.objectDiretion = Vector2.UnitY;
                        currentInput = CurrentInput.Up;
                    }
                    deltaPosition += Vector2.UnitY * movingSpeed;
                }
                if (InputManager.PlayerTwo.Down == ButtonState.Pressed && InputManager.PlayerTwo.Up != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Down)
                    {
                        this.objectDiretion = -Vector2.UnitY;
                        currentInput = CurrentInput.Down;
                    }
                    deltaPosition += -Vector2.UnitY * movingSpeed;
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
        /// <summary>
        /// Deals with all player/enemy damage.
        /// </summary>
        public void DamageManage()
        {
            #region playerone

            if (pNumber == PlayerNumber.playerOne)
            {
                if (InputManager.PressedLastFrame.Space == ButtonState.Pressed)
                {
                     Attack();
                }
                if (InputManager.PressedLastFrame.Q == ButtonState.Pressed)
                {
                    //animação de defense   
                    //Defense(player);
                }
            }
            #endregion
            #region playertwo
            if (pNumber == PlayerNumber.playerTwo)
            {
                if (InputManager.PressedLastFrame.L == ButtonState.Pressed)
                {
                    Attack();
                }
                if (InputManager.PressedLastFrame.K == ButtonState.Pressed)
                {
                    //o que fazer no DEFESA
                    // Defense(player);
                }
            }
            #endregion
        }

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