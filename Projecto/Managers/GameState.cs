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
    class GameState : Game
    {
        static public PlayerManager PlayerOne;
        static public PlayerManager PlayerTwo;
                
        Viewport defaultView, leftView, rightView;
        Camera cameraRight, cameraLeft, cameraScreen;

        #region ZONA DE TESTE
        GameObject teste1;
        MapGenerator map = new MapGenerator();
        KeyboardState oldState;
        public static List<Enemy> enemies;
        #endregion



        //------------->CONSTRUCTORS<-------------//

        public GameState()
        {
            #region Map Generation
            map.UseRandomSeed = true;
            map.RandomFillPercent = 50;
            map.Width = (int)Cons.MAXWIDTH; //100;
            map.Height = 75;
            map.GenerateMap(5);
            #endregion

            #region Camera. Split screen
            //Viewports           
            defaultView = Game1.graphics.GraphicsDevice.Viewport;
            leftView = rightView = defaultView;

            //Dividing it in half, and adjusting the positioning.
            rightView.Width /= 2;
            leftView.Width /= 2;
            rightView.X = leftView.Width;

            //Initializing cameras.
            cameraLeft = new Camera(Vector2.Zero, 50, ((float)leftView.Height / leftView.Width));
            cameraRight = new Camera(new Vector2(50,0), 50, ((float)rightView.Height / rightView.Width));
            cameraScreen = new Camera(Vector2.Zero, 100, Game1.graphics.PreferredBackBufferHeight / Game1.graphics.PreferredBackBufferWidth);
            #endregion

            Debug.LoadFont();   //Starting Debug.

            #region TestZone
            PlayerOne = new PlayerManager(new Vector2(0, 0), Vector2.One * 5, PlayerNumber.playerOne);
            teste1 = new GameObject("Tile1", new Vector2(25, 0), Vector2.One * 5, 0f);
            PlayerTwo = new PlayerManager(new Vector2(50, 0), Vector2.One * 5, PlayerNumber.playerTwo);
            enemies = new List<Enemy>();
            
            #endregion

        }

        //------------->FUNCTIONS && METHODS<-------------//

        /// <summary>
        /// Updates the whole gamestate.
        /// </summary>
        public void StateUpdate(GameTime gameTime)
        {
            PlayerOne.PlayerMovement(gameTime);
            cameraLeft.LookAt(PlayerOne);
            PlayerTwo.PlayerMovement(gameTime);
            cameraRight.LookAt(PlayerTwo);

            KeyboardState teste = Keyboard.GetState();
            if(teste.IsKeyDown(Keys.H) && oldState.IsKeyUp(Keys.H))
            {
                map.GenerateMap(5);
                Debug.NewLine("New Map");
            }
            oldState = teste;

        }
        /// <summary>
        /// Draws the whole gamestate.
        /// </summary>
        public void Draw()
        {
            //Draws the left side
            Game1.graphics.GraphicsDevice.Viewport = leftView;
            DrawCameraView(cameraLeft);

            //Draws the right side
            Game1.graphics.GraphicsDevice.Viewport = rightView;
            DrawCameraView(cameraRight);

            #region Draws the whole picture.
            Game1.graphics.GraphicsDevice.Viewport = defaultView;
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);  //THIS WAY DOESNT AFFECT PIXEL ASPECT
            Debug.Draw(cameraScreen);
            Game1.spriteBatch.End();
            #endregion


        }
        /// <summary>
        /// Draws the whole world for one camera.
        /// </summary>
        /// <param name="camera">Target camera to draw.</param>
        void DrawCameraView(Camera camera)
        {
            //Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.transform);  //THIS WAY DOESNT AFFECT PIXEL ASPECT
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);  //THIS WAY DOESNT AFFECT PIXEL ASPECT
            map.Draw(camera);
            teste1.DrawObject(camera);
            PlayerOne.DrawObject(camera);
            PlayerTwo.DrawObject(camera);
            Game1.spriteBatch.End();
        }
    }
}
