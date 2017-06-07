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

        public Dictionary<string, Item> ItemDictionary;

        //------------->CONSTRUCTORS<-------------//

        public PlayerManager(Vector2 position, Vector2 size, PlayerNumber number, int range) : base(null, position, size, range)
        {
            this.animations = new Animation[18];
            this.pNumber = number;
            this.currentInput = CurrentInput.NoInput;
            Coordinates = (position / size);
            Coordinates.X = (int)Coordinates.X;
            Coordinates.Y = (int)Coordinates.Y;
            this.playerCollider = new Collider(position, size);
            this.playerCollider.UpdateTiles(position, size);
            this.ItemDictionary = new Dictionary<string, Item>();

            #region Stats initializer
            this.HP = 100;
            #endregion

            if (number == PlayerNumber.playerOne)
            {
                animations[0] = new Animation("Standing", "PlayerOne_Standing_NoWeapon", Vector2.One * 32, 2, 4);
                animations[1] = new Animation("Walking", "PlayerOne_Walking_NoWeapon", Vector2.One * 32, 2, 4);
                animations[2] = new Animation("BackWalking", "PlayerOne_BackWalking_NoWeapon", Vector2.One * 32, 2, 4);
                animations[3] = new Animation("SideWalking", "PlayerOne_SideWalk_NoWeapon", Vector2.One * 32, 2, 4);
                animations[4] = new Animation("SideWalking2", "PlayerOne_SideWalk2_NoWeapon", Vector2.One * 32, 2, 4);
                animations[5] = new Animation("SwordAttack", "PlayerOne_attack", Vector2.One * 32, 6, 12);
                animations[6] = new Animation("StaffAttack", "PlayerOne_stick", Vector2.One * 32, 2, 4);
                this.currentAnimation = animations[0];
            }
            else
            {
                animations[0] = new Animation("Standing", "PlayerOne_Standing_NoWeapon", Vector2.One * 32, 2, 4);
                animations[1] = new Animation("Walking", "PlayerOne_Walking_NoWeapon", Vector2.One * 32, 2, 4);
                animations[2] = new Animation("BackWalking", "PlayerOne_BackWalking_NoWeapon", Vector2.One * 32, 2, 4);
                animations[3] = new Animation("SideWalking", "PlayerOne_SideWalk_NoWeapon", Vector2.One * 32, 2, 4);
                animations[4] = new Animation("SideWalking2", "PlayerOne_SideWalk2_NoWeapon", Vector2.One * 32, 2, 4);
                animations[5] = new Animation("SwordAttack", "PlayerOne_attack", Vector2.One * 32, 6, 12);
                animations[6] = new Animation("StaffAttack", "PlayerOne_stick", Vector2.One * 32, 2, 4);
                this.currentAnimation = animations[0];
            }
        }

        //------------->FUNCTIONS && METHODS<-------------//
        public void Update(GameTime gameTime)
        {
            if (currentAnimation.isDone)
            {
                currentAnimation = animations[0];
                currentInput = CurrentInput.NoInput;
            }

            PlayerMovement(gameTime);
            PlayerDamage();

            List<Item> auxList = new List<Item>();
            foreach (KeyValuePair<string, Item> i in GameState.ItemDictionary)
            {
                if (this.Rectangle.Intersects(i.Value.Rectangle))
                {
                    i.Value.Pickup(this);
                    auxList.Add(i.Value);
                }
            }
            foreach (Item i in auxList)
            {
                GameState.ItemDictionary.Remove(i.Name);
            }


            currentAnimation.Play(gameTime);
        }
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
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[4];
                        this.objectDiretion = Vector2.UnitX;
                        currentInput = CurrentInput.Right;
                    }
                    deltaPosition += Vector2.UnitX * movingSpeed;
                }
                if (InputManager.PlayerOne.Left == ButtonState.Pressed && InputManager.PlayerOne.Right != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Left)
                    {
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[3];
                        this.objectDiretion = -Vector2.UnitX;
                        currentInput = CurrentInput.Left;
                    }
                    deltaPosition += -Vector2.UnitX * movingSpeed;
                }
                if (InputManager.PlayerOne.Up == ButtonState.Pressed && InputManager.PlayerOne.Down != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Up)
                    {
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[2];
                        this.objectDiretion = Vector2.UnitY;
                        currentInput = CurrentInput.Up;
                    }
                    deltaPosition += Vector2.UnitY * movingSpeed;
                }
                if (InputManager.PlayerOne.Down == ButtonState.Pressed && InputManager.PlayerOne.Up != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Down)
                    {
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[1];
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
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[4];
                        currentInput = CurrentInput.Right;
                    }
                    deltaPosition += Vector2.UnitX * movingSpeed;
                }
                if (InputManager.PlayerTwo.Left == ButtonState.Pressed && InputManager.PlayerTwo.Right != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Left)
                    {
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[3];
                        this.objectDiretion = -Vector2.UnitX;
                        currentInput = CurrentInput.Left;
                    }
                    deltaPosition += -Vector2.UnitX * movingSpeed;
                }
                if (InputManager.PlayerTwo.Up == ButtonState.Pressed && InputManager.PlayerTwo.Down != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Up)
                    {
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[2];
                        this.objectDiretion = Vector2.UnitY;
                        currentInput = CurrentInput.Up;
                    }
                    deltaPosition += Vector2.UnitY * movingSpeed;
                }
                if (InputManager.PlayerTwo.Down == ButtonState.Pressed && InputManager.PlayerTwo.Up != ButtonState.Pressed)
                {
                    if (currentInput != CurrentInput.Down)
                    {
                        this.currentAnimation.Stop();
                        this.currentAnimation = animations[1];
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

        }
        /// <summary>
        /// Deals with all player/enemy damage.
        /// </summary>
        public void PlayerDamage()
        {
            #region playerone

            if (pNumber == PlayerNumber.playerOne)
            {
                if (InputManager.PressedLastFrame.Space == ButtonState.Pressed)
                {
                    if (mainHandWeapon != null)
                    {
                        if (mainHandWeapon.isRanged == true)
                        {
                            currentAnimation.Stop();
                            currentAnimation = animations[6];
                        }
                        else
                        {
                            currentAnimation.Stop();
                            currentAnimation = animations[5];

                        }
                    }
                    Attack();
                }
                if (InputManager.PressedLastFrame.V == ButtonState.Pressed)
                {
                    //animação de defense   
                    //Defense(player);
                }
                if (InputManager.PressedLastFrame.B == ButtonState.Pressed)
                    this.SwitchWeapons();
            }
            #endregion
            #region playertwo
            if (pNumber == PlayerNumber.playerTwo)
            {
                if (InputManager.PressedLastFrame.Numpad0 == ButtonState.Pressed)
                {
                     if (mainHandWeapon.isRanged == true)
                    {
                        currentAnimation.Stop();
                        currentAnimation = animations[6];
                    }
                    else
                    {
                        currentAnimation.Stop();
                        currentAnimation = animations[5];

                    }
                    Attack();
                }
                if (InputManager.PressedLastFrame.Numpad1 == ButtonState.Pressed)
                {
                    //o que fazer no DEFESA
                    // Defense(player);
                }
                if (InputManager.PressedLastFrame.Numpad1 == ButtonState.Pressed)
                    this.SwitchWeapons();
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
                //Game1.spriteBatch.Draw(Texture, Rectangle, Color.White);
                Game1.spriteBatch.Draw(currentAnimation.spriteTexture, this.Rectangle, currentAnimation.currentFrameRec, Color.White);
            }
        }
    }
}