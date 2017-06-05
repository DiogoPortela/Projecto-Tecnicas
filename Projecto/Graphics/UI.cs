using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
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
        }
    }
    static class UI
    {
        static public SpriteFont GameFont;
        static private Color pauseColor;
        static public Rectangle ScreenArea;

        //static private Vector2 continueBoxPos;
        //static private Vector2 continueBoxPosTwo;
        //static private Vector2 continueBoxCenter;
        //static private int continueBoxScale;

        static private UI_Text_Box ContinueOne;
        static private UI_Text_Box MainMenuOne;
        static private UI_Text_Box QuitOne;

        static private UI_Text_Box ContinueTwo;
        static private UI_Text_Box MainMenuTwo;
        static private UI_Text_Box QuitTwo;

        //static private Vector2 mainMenuBoxPos;
        //static private Vector2 mainMenuBoxPosTwo;
        //static private Vector2 mainMenuBoxCenter;
        //static private int mainMenuBoxScale;

        //static private Vector2 quitBoxPos;
        //static private Vector2 quitBoxPosTwo;
        //static private Vector2 quitBoxCenter;
        //static private int quitBoxScale;

        static private string cont;
        static private string mainMenu;
        static private string quit;

        static private int selected;
        static private int selectedOne;
        static private int selectedTwo;
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


            //continueBoxScale = mainMenuBoxScale = quitBoxScale = 2;
            //continueBoxPos = Game1.mainCamera.CalculatePixelPoint(new Vector2(50, 10));
            //mainMenuBoxPos = Game1.mainCamera.CalculatePixelPoint(new Vector2(50, 20));
            //quitBoxPos = Game1.mainCamera.CalculatePixelPoint(new Vector2(50, 30));

            //continueBoxPosTwo = Game1.mainCamera.CalculatePixelPoint(new Vector2(100, 10));
            //mainMenuBoxPosTwo = Game1.mainCamera.CalculatePixelPoint(new Vector2(100, 20));
            //quitBoxPosTwo = Game1.mainCamera.CalculatePixelPoint(new Vector2(100, 30));

            colors[0] = Color.Yellow;
            for (int i = 1; i < colors.Length; i++)
            {
                colors[i] = Color.White;
            }

            cont = "Continue";
            mainMenu = "Main Menu";
            quit = "Quit";
            selected = 0;

            selectedOne = 0;
            selectedTwo = 0;

            //ContinueOne = new UI_Text_Box(cont, Game1.mainCamera.CalculatePixelPoint(new Vector2(5, 10)), GameFont.MeasureString(cont) - , 2);
            //MainMenuOne = new UI_Text_Box(mainMenu, Game1.mainCamera.CalculatePixelPoint(new Vector2(5, 20)), GameFont.MeasureString(mainMenu), 2);
            //QuitOne = new UI_Text_Box(quit, Game1.mainCamera.CalculatePixelPoint(new Vector2(5, 30)), GameFont.MeasureString(quit), 2);

            //ContinueTwo = new UI_Text_Box(cont, Game1.mainCamera.CalculatePixelPoint(new Vector2(95, 10)), GameFont.MeasureString(cont), 2);
            //MainMenuTwo = new UI_Text_Box(mainMenu, Game1.mainCamera.CalculatePixelPoint(new Vector2(95, 20)), GameFont.MeasureString(mainMenu), 2);
            //QuitTwo= new UI_Text_Box(quit, Game1.mainCamera.CalculatePixelPoint(new Vector2(95, 30)), GameFont.MeasureString(quit), 2);

            //continueBoxCenter = GameFont.MeasureString(cont) / 2;
            //mainMenuBoxCenter = GameFont.MeasureString(mainMenu) / 2;
            //quitBoxCenter = GameFont.MeasureString(quit) / 2;

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

            Game1.spriteBatch.DrawString(UI.GameFont, ContinueTwo.Text, ContinueTwo.Position, colors[0], 0f, ContinueTwo.Center, ContinueTwo.Scale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, MainMenuTwo.Text, MainMenuTwo.Position, colors[1], 0f, MainMenuTwo.Center, MainMenuTwo.Scale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, QuitTwo.Text, QuitTwo.Position, colors[2], 0f, QuitTwo.Center, QuitTwo.Scale, SpriteEffects.None, 0f);

            //Game1.spriteBatch.DrawString(UI.GameFont, cont, continueBoxPos, colors[0], 0f, continueBoxCenter, continueBoxScale, SpriteEffects.None, 0f);
            //Game1.spriteBatch.DrawString(UI.GameFont, mainMenu, mainMenuBoxPos, colors[1], 0f, mainMenuBoxCenter, mainMenuBoxScale, SpriteEffects.None, 0f);
            //Game1.spriteBatch.DrawString(UI.GameFont, quit, quitBoxPos, colors[2], 0f, quitBoxCenter, quitBoxScale, SpriteEffects.None, 0f);
        }
    }
}
