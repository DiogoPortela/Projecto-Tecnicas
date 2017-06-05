using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    static class UI
    {
        static public SpriteFont GameFont;
        static private Color pauseColor;
        static public Rectangle ScreenArea;

        static private Vector2 continueBoxPos;
        static private Vector2 continueBoxCenter;
        static private int continueBoxScale;

        static private Vector2 mainMenuBoxPos;
        static private Vector2 mainMenuBoxCenter;
        static private int mainMenuBoxScale;

        static private Vector2 quitBoxPos;
        static private Vector2 quitBoxCenter;
        static private int quitBoxScale;

        static private string cont;
        static private string mainMenu;
        static private string quit;

        static private int selected;
        static private Color[] colors;

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

            colors = new Color[3];
            continueBoxScale = mainMenuBoxScale = quitBoxScale = 2;
            continueBoxPos = Game1.mainCamera.CalculatePixelPoint(new Vector2(50, 10));
            mainMenuBoxPos = Game1.mainCamera.CalculatePixelPoint(new Vector2(50, 20));
            quitBoxPos = Game1.mainCamera.CalculatePixelPoint(new Vector2(50, 30));

            colors[0] = Color.Yellow;
            for (int i = 1; i < colors.Length; i++)
            {
                colors[i] = Color.White;
            }

            cont = "Continue";
            mainMenu = "Main Menu";
            quit = "Quit";
            selected = 0;

            continueBoxCenter = GameFont.MeasureString(cont) / 2;
            mainMenuBoxCenter = GameFont.MeasureString(mainMenu) / 2;
            quitBoxCenter = GameFont.MeasureString(quit) / 2;
            
        }
        /// <summary>
        /// The Uptade for the pause menu.
        /// </summary>
        static public void PauseUpdate()
        {
            if ((InputManager.PressedLastFrame.UpTwo == ButtonState.Pressed || InputManager.PressedLastFrame.UpOne == ButtonState.Pressed) && selected > 0)
            {
                colors[selected] = Color.White;
                selected--;
                Debug.NewLine(selected.ToString());
                colors[selected] = Color.Yellow;

            }
            else if ((InputManager.PressedLastFrame.DownOne == ButtonState.Pressed || InputManager.PressedLastFrame.DownTwo == ButtonState.Pressed) && selected < 2)
            {
                colors[selected] = Color.White;
                selected++;
                Debug.NewLine(selected.ToString());
                colors[selected] = Color.Yellow;
            }

            if (InputManager.PressedLastFrame.Enter == ButtonState.Pressed)
            {
                switch (selected)
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
            Game1.spriteBatch.DrawString(UI.GameFont, cont, continueBoxPos, colors[0], 0f, continueBoxCenter, continueBoxScale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, mainMenu, mainMenuBoxPos, colors[1], 0f, mainMenuBoxCenter, mainMenuBoxScale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, quit, quitBoxPos, colors[2], 0f, quitBoxCenter, quitBoxScale, SpriteEffects.None, 0f);
        }
    }
}
//