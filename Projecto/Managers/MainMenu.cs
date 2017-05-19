using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Projecto
{
    static class MainMenu
    {
        static private Vector2 startBoxPos;
        static private Vector2 startBoxCenter;
        static private int startBoxScale;

        static private Vector2 quitBoxPos;
        static private Vector2 quitBoxCenter;
        static private int quitBoxScale;

        static private string start;
        static private string quit;

        static private int selected;
        static private Color[] colors;

        //------------->FUNCTIONS && METHODS<-------------//

        static public void Start()
        {
            colors = new Color[2];
            startBoxScale = quitBoxScale = 2;
            startBoxPos = Game1.mainCamera.CalculatePixelPoint(new Vector2(50, 10));
            quitBoxPos = Game1.mainCamera.CalculatePixelPoint(new Vector2(50, 20));

            colors[0] = Color.Yellow;
            for(int i = 1; i < colors.Length; i++)
            {
                colors[i] = Color.White;
            }

            start = "Start";
            quit = "Quit";
            selected = 0;

            startBoxCenter = UI.GameFont.MeasureString(start) / 2;
            quitBoxCenter = UI.GameFont.MeasureString(quit) / 2;

            SoundManager.StartSound("mainGameTheme", true);
        }

        static public void Update()
        {
            if ((InputManager.PlayerOne.Up == ButtonState.Pressed || InputManager.PlayerTwo.Up == ButtonState.Pressed) && selected > 0)
            {
                colors[selected] = Color.White;
                selected--;
                Debug.NewLine(selected.ToString());
                colors[selected] = Color.Yellow;

            }
            else if ((InputManager.PlayerOne.Down == ButtonState.Pressed || InputManager.PlayerTwo.Down == ButtonState.Pressed) && selected < 1)
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
                            GameState.Start();
                            Game1.selectedScreen = ScreenSelect.Playing;
                            break;
                        }
                    case 1:
                        {
                            Game1.selectedScreen = ScreenSelect.Quit;
                            break;
                        }
                }
            }
        }

        static public void Draw()
        {
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);  //THIS WAY DOESNT AFFECT PIXEL ASPECT

            Game1.spriteBatch.DrawString(UI.GameFont, start, startBoxPos, colors[0], 0f, startBoxCenter, startBoxScale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, quit, quitBoxPos, colors[1], 0f, quitBoxCenter, quitBoxScale, SpriteEffects.None, 0f);
            Debug.DrawText();

            Game1.spriteBatch.End();
        }
    }
}
