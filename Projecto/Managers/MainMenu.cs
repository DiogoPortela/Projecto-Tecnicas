using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
namespace Projecto
{
    class MainMenu
    {
        Camera camera;
        Rectangle StartBox;
        Vector2 StartBoxPos;
        Vector2 StartBoxScale;

        Rectangle QuitBox;
        Vector2 QuitBoxPos;
        Vector2 QuitBoxScale;

        string Start;
        string Quit;

        int selected;

        public MainMenu()
        {
            camera = new Camera(Vector2.Zero, 100);
            StartBoxScale = QuitBoxScale = camera.CalculatePixelPoint(new Vector2(1, 1));
            StartBoxPos = camera.CalculatePixelPoint(new Vector2(50, 10));
            QuitBoxPos = camera.CalculatePixelPoint(new Vector2(50, 20));

            StartBox = camera.CalculatePixelRectangle(StartBoxPos, StartBoxScale);
            QuitBox = camera.CalculatePixelRectangle(QuitBoxPos, QuitBoxScale);

            Start = "Start";
            Quit = "Quit";
            selected = 1;
        }

        public void Update()
        {
            //if (StartBox.Contains(UI.mousePostion) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    Game1.selectedScreen = ScreenSelect.Playing;
            //}
            //if (QuitBox.Contains(UI.mousePostion) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    Game1.selectedScreen = ScreenSelect.Quit;
            //}

            if ((InputManager.PlayerOne.Up == ButtonState.Pressed || InputManager.PlayerTwo.Up == ButtonState.Pressed) && selected > 1)
            {
                selected--;
                Debug.NewLine(selected.ToString());

            }
            else if ((InputManager.PlayerOne.Down == ButtonState.Pressed || InputManager.PlayerTwo.Down == ButtonState.Pressed) && selected < 2)
            {
                selected++;
                Debug.NewLine(selected.ToString());
            }

            if (InputManager.PressedLastFrame.Space == ButtonState.Pressed)
            {
                switch (selected)
                {
                    case 1:
                        {
                            Game1.selectedScreen = ScreenSelect.Playing;
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

        public void Draw()
        {
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);  //THIS WAY DOESNT AFFECT PIXEL ASPECT

            Game1.spriteBatch.DrawString(UI.GameFont, Start, StartBoxPos, Color.White, 0f, Vector2.Zero, StartBoxScale, SpriteEffects.None, 0f);
            Game1.spriteBatch.DrawString(UI.GameFont, Quit, QuitBoxPos, Color.White, 0f, Vector2.Zero, QuitBoxScale, SpriteEffects.None, 0f);

            Debug.DrawText();

            Game1.spriteBatch.End();
        }
    }
}
