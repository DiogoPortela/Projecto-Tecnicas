﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    internal class UI_Static_Item
    {
        public Texture2D Texture;
        public Rectangle Rectangle;

        public UI_Static_Item(string texture, Vector2 position, Vector2 size, Camera camera)
        {
            if(Game1.textureList.ContainsKey(texture))
            {
                this.Texture = Game1.textureList[texture];           
            }
            this.Rectangle = camera.CalculatePixelRectangle(position, size);
        }

        public void Draw()
        {
            Game1.spriteBatch.Draw(Texture, Rectangle, Color.White);
        }
    }
    internal enum TextAlignment
    {
        Left,
        Right,
        Central,
    }
    internal class UI_Text_Box
    {
        public string Text;
        public Vector2 Position;
        public Vector2 Center;
        public float Scale;
        public Color Color;
        public TextAlignment Alignment;

        public UI_Text_Box(string text, Vector2 position, TextAlignment alignment, float scale)
        {
            this.Text = text;
            this.Position = position;
            this.Alignment = alignment;
            if(this.Alignment == TextAlignment.Right)
            {
                this.Center = new Vector2(UI.GameFont.MeasureString(text).X, 0);
            }
            else if (this.Alignment == TextAlignment.Left)
            {
                this.Center = Vector2.Zero;
            }
            else
            {
                this.Center = new Vector2(UI.GameFont.MeasureString(text).X / 2, 0);
            }
            this.Scale = scale;
            this.Color = Color.White;
        }
    }
    static class UI
    {
        static public SpriteFont GameFont;
        static private Color pauseColor;
        static public Rectangle ScreenArea;

        static private UI_Text_Box ContinueOne;
        static private UI_Text_Box MainMenuOne;
        static private UI_Text_Box QuitOne;

        static private UI_Text_Box ContinueTwo;
        static private UI_Text_Box MainMenuTwo;
        static private UI_Text_Box QuitTwo;

        static private string cont;
        static private string mainMenu;
        static private string quit;

        static private int selectedOne;
        static private int selectedTwo;

        //------------->FUNCTIONS && METHODS<-------------//
        /// <summary>
        /// Starts the UI.
        /// </summary>
        /// <param name="font"> Font for writing. </param>
        static public void Start(SpriteFont font)
        {
            if (font == null)
            {
                GameFont = Game1.content.Load<SpriteFont>("Arial");
            }
            else
            {
                GameFont = font;
            }
            pauseColor = new Color(Color.Black, 0.5f);
            ScreenArea = new Rectangle(0, 0, Game1.graphics.PreferredBackBufferWidth, Game1.graphics.PreferredBackBufferHeight);

            cont = "Continue";
            mainMenu = "Main Menu";
            quit = "Quit";

            selectedOne = 0;
            selectedTwo = 0;

            ContinueOne = new UI_Text_Box(cont, Game1.mainCamera.CalculatePixelPoint(new Vector2(5, 10)), TextAlignment.Left, 2);
            MainMenuOne = new UI_Text_Box(mainMenu, Game1.mainCamera.CalculatePixelPoint(new Vector2(5, 20)), TextAlignment.Left, 2);
            QuitOne = new UI_Text_Box(quit, Game1.mainCamera.CalculatePixelPoint(new Vector2(5, 30)), TextAlignment.Left, 2);

            ContinueTwo = new UI_Text_Box(cont, Game1.mainCamera.CalculatePixelPoint(new Vector2(95, 10)), TextAlignment.Right, 2);
            MainMenuTwo = new UI_Text_Box(mainMenu, Game1.mainCamera.CalculatePixelPoint(new Vector2(95, 20)), TextAlignment.Right, 2);
            QuitTwo = new UI_Text_Box(quit, Game1.mainCamera.CalculatePixelPoint(new Vector2(95, 30)), TextAlignment.Right, 2);

        }
        /// <summary>
        /// The Uptade for the pause menu.
        /// </summary>
        static public void PauseUpdate()
        {
            if (InputManager.PressedLastFrame.UpTwo == ButtonState.Pressed  && selectedTwo > 0)
            {
                //colors[selected] = Color.White;
                selectedTwo--;
                //colors[selected] = Color.Yellow;
            }
            
            else if (InputManager.PressedLastFrame.DownTwo == ButtonState.Pressed && selectedTwo < 2)
            {
                //colors[selected] = Color.White;
                selectedTwo++;
                //colors[selected] = Color.Yellow;
            }

            if (InputManager.PressedLastFrame.UpOne == ButtonState.Pressed && selectedOne > 0)
            {
                selectedOne--;
            }
            else if (InputManager.PressedLastFrame.DownOne == ButtonState.Pressed  && selectedOne < 2)
            {
                //colors[selected] = Color.White;
                selectedTwo++;
                //colors[selected] = Color.Yellow;
            }

            if (InputManager.PressedLastFrame.Space == ButtonState.Pressed)
            {
                switch (selectedOne)
                {
                    case 0:
                        {
                            GameState.isPaused = false;
                            break;
                        }
                    case 1:
                        {
                            SoundManager.StopAllSounds();
                            Game1.selectedScreen = ScreenSelect.MainMenu;
                            break;
                        }
                    case 2:
                        {
                            Game1.selectedScreen = ScreenSelect.Quit;
                            break;
                        }
                }
            }
            if (InputManager.PressedLastFrame.Numpad0 == ButtonState.Pressed)
            {
                switch (selectedTwo)
                {
                    case 0:
                        {
                            GameState.isPaused = false;
                            break;
                        }
                    case 1:
                        {
                            SoundManager.StopAllSounds();
                            Game1.selectedScreen = ScreenSelect.MainMenu;
                            break;
                        }
                    case 2:
                        {
                            Game1.selectedScreen = ScreenSelect.Quit;
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// The Draw for the pause menu.
        /// </summary>
        static public void DrawPauseMenu()
        {
            Game1.spriteBatch.Draw(Debug.debugTexture, ScreenArea, pauseColor);

            Game1.spriteBatch.DrawString(UI.GameFont, ContinueOne.Text, ContinueOne.Position, ContinueOne.Color, 0f, ContinueOne.Center, ContinueOne.Scale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, MainMenuOne.Text, MainMenuOne.Position, MainMenuOne.Color, 0f, MainMenuOne.Center, MainMenuOne.Scale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, QuitOne.Text, QuitOne.Position, QuitOne.Color, 0f, QuitOne.Center, QuitOne.Scale, SpriteEffects.None, 0f);

            Game1.spriteBatch.DrawString(UI.GameFont, ContinueTwo.Text, ContinueTwo.Position, ContinueOne.Color, 0f, ContinueTwo.Center, ContinueTwo.Scale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, MainMenuTwo.Text, MainMenuTwo.Position, MainMenuTwo.Color, 0f, MainMenuTwo.Center, MainMenuTwo.Scale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, QuitTwo.Text, QuitTwo.Position, QuitTwo.Color, 0f, QuitTwo.Center, QuitTwo.Scale, SpriteEffects.None, 0f);
        }
    }
}
//