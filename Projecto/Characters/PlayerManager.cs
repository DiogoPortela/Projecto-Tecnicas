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

        private const float movingSpeed = 0.4f;
        //------------->CONSTRUCTORS<-------------//

        public PlayerManager(Vector2 position, Vector2 size, PlayerNumber number) : base("Drude", position, size, 0f)
        {
            this.animations = new Animation[18];
            this.pNumber = number;
            this.currentInput = CurrentInput.NoInput;

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
                    this.Move(Vector2.UnitX * movingSpeed);
                }
                if (InputManager.MovementPlayerOne.Left == ButtonState.Pressed && InputManager.MovementPlayerOne.Right != ButtonState.Pressed)
                {
                    this.Move(-Vector2.UnitX * movingSpeed);
                }
                if (InputManager.MovementPlayerOne.Up == ButtonState.Pressed && InputManager.MovementPlayerOne.Down != ButtonState.Pressed)
                {
                    this.Move(Vector2.UnitY * movingSpeed);
                }
                if (InputManager.MovementPlayerOne.Down == ButtonState.Pressed && InputManager.MovementPlayerOne.Up != ButtonState.Pressed)
                {
                    this.Move(-Vector2.UnitY * movingSpeed);
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
                    this.Move(Vector2.UnitX * movingSpeed);
                }
                if (InputManager.MovementPlayerTwo.Left == ButtonState.Pressed && InputManager.MovementPlayerTwo.Right != ButtonState.Pressed)
                {
                    this.Move(-Vector2.UnitX * movingSpeed);
                }
                if (InputManager.MovementPlayerTwo.Up == ButtonState.Pressed && InputManager.MovementPlayerTwo.Down != ButtonState.Pressed)
                {
                    this.Move(Vector2.UnitY * movingSpeed);
                }
                if (InputManager.MovementPlayerTwo.Down == ButtonState.Pressed && InputManager.MovementPlayerTwo.Up != ButtonState.Pressed)
                {
                    this.Move(-Vector2.UnitY * movingSpeed);
                }
            }
            #endregion


            //this.currentAnimation.Play(gameTime);
        }
        #region
        //para detetar collisão de attack
        public Rectangle playercollison()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        
      
        public bool intersect(Rectangle rectangle)
        {
            return Rectangle.Intersects(rectangle);
        }

        #endregion


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
=======

        public void CombatMod(PlayerNumber player, List<Enemy> enemies)
        {
            player = player;
            enemies = new List<Enemy>();
        }

        // Use this method to resolve attacks between Figures
        public void Attack(PlayerNumber player, Enemy defender)
        {
            if (PhysDmg >= defender.PhysDmgRes)
            {
                // Lower the defender's health by the amount of damage
                defender.HP -= (int)(PhysDmg - PhysDmgRes);
                // Write a combat message to the debug log.ideia
                /* Debug.WriteLine("{0} hit {1} for {2} and he has {3} health remaining.",
                   attacker.Name, defender.Name, damage, defender.Health);*/
                if (defender.HP <= 0)
                {
                    if (defender is Enemy)
                    {
                        var enemy = defender as Enemy;
                        // When an enemies health dropped below 0 they died
                        // Remove that enemy from the game
                        enemies.Remove(enemy);
                    }
                }
            }
            else if (MagicDmg >= defender.MagicDmgRes)
            {
                defender.HP -= (int)(MagicDmg - defender.MagicDmgRes);
                if (defender.HP <= 0)
                {
                    if (defender is Enemy)
                    {
                        var enemy = defender as Enemy;
                        // When an enemies health dropped below 0 they died
                        // Remove that enemy from the game
                        enemies.Remove(enemy);
                    }
                }
            }
            else
            {
                // Show the miss message in the Debug log for now
               // Debug.WriteLine("{0} missed {1}", attacker.Name, defender.HP);
            }
        }

        public void Defense(PlayerNumber defensive)
        {
            defensive.HP = defensive.HP;
        }


    }


    /// <summary>
    /// Draws on screen an object, using a camera.
    /// </summary>
    /// <param name="camera">Camera to draw the image at.</param>
    public override void DrawObject(Camera camera)
>>>>>>> Stashed changes
        {
            if (isActive)
            {
                this.Rectangle = camera.CalculatePixelRectangle(this.Position, this.Size);
                Game1.spriteBatch.Draw(Texture, Rectangle, Color.White);
                //Game1.spriteBatch.Draw(currentAnimation.spriteTexture, this.Rectangle, currentAnimation.currentFrameRec, Color.White);
            }
        }
    }

                //attack def button
                if (InputManager.attckdefOne.Space == ButtonState.Pressed && InputManager.attckdefOne.Space != ButtonState.Pressed)
                {
                    //animação de attack
                    this.Atackrange(-Vector2.UnitY,2f);
                    Attack();
                }
                if (InputManager.attckdefOne.Q == ButtonState.Pressed && InputManager.attckdefOne.Q != ButtonState.Pressed)
                {
                    //animação de defense
                    Defense();
                    
                }


                if (InputManager.attckdefOne.Space == ButtonState.Pressed && InputManager.attckdefOne.Space != ButtonState.Pressed)
                {
                    //o que fazer no attack
                }
                if (InputManager.attckdefOne.Q == ButtonState.Pressed && InputManager.attckdefOne.Q != ButtonState.Pressed)
                {
                    //o que fazer no DEFESA
                }


