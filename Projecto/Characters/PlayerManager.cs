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
        private PlayerNumber playerNumber;
        private int range = 2;


        #region Atribute Stats
        public int HP { get; set; }
        public double PhysDmg { get; set; }
        public double MagicDmg { get; set; }
        public double PhysDmgRes { get; set; }
        public double MagicDmgRes { get; set; }
        #endregion
        //------------->CONSTRUCTORS<-------------//

        public PlayerManager(string texture, Vector2 position, float size, PlayerNumber playerNumber) : base(texture, position, new Vector2(size, size), 0f)
        {
            this.playerNumber = playerNumber;

            #region Stats initializer
            this.HP = 100;
            this.PhysDmg = 18.0;
            this.MagicDmg = 8.0;
            this.PhysDmgRes = 5.0;
            this.MagicDmgRes = 5.0;
            #endregion
        }
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
        public void DamageManager()
        {
            #region playerone

            if (pNumber == PlayerNumber.playerOne)
            {
                //attack button
                if (InputManager.attckdefOne.Space == ButtonState.Pressed && InputManager.attckdefOne.Space != ButtonState.Pressed)
                {
                    //animação de attack
                    foreach (Enemy enemy in GameState.enemies)
                    {
                        if (this.IsInRange(enemy) == true)
                        {
                            Attack(enemy);
                        }
                    }

                }
                if (InputManager.attckdefOne.Q == ButtonState.Pressed && InputManager.attckdefOne.Q != ButtonState.Pressed)
                {
                    //animação de defense   
                    //Defense(player);
                }
            }
            #endregion

            #region playertwo
            if (pNumber == PlayerNumber.playerTwo)
            {
                if (InputManager.attckdefOne.L == ButtonState.Pressed && InputManager.attckdefOne.L != ButtonState.Pressed)
                {
                    //o que fazer no attack
                    foreach (Enemy enemy in GameState.enemies)
                    {
                        if (this.IsInRange(enemy) == true)
                        {
                            Attack(enemy);
                        }
                    }

                }
                if (InputManager.attckdefOne.K == ButtonState.Pressed && InputManager.attckdefOne.K != ButtonState.Pressed)
                {
                    //o que fazer no DEFESA
                   // Defense(player);
                }
            }
            #endregion
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
        // Use this method to resolve attacks between Figures
        public void Attack(Enemy defender)
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
                        GameState.enemies.Remove(enemy);
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
                        GameState.enemies.Remove(enemy);
                    }
                }
            }
            else
            {
                // Show the miss message in the Debug log for now
                // Debug.WriteLine("{0} missed {1}", attacker.Name, defender.HP);
            }
        }

        public void Defense()
        {
           // this.HP = this.HP;
        }

        public bool IsInRange(Enemy enemy)
        {
            Vector2 v = (this.Position) - (enemy.Position);
            float distance = Math.Abs(v.Length());
            if ((this.Size.X + range) + enemy.Size.X < distance) return true;
            return false;
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








