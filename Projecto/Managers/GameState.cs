using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    class GameState : Game
    {
        static public PlayerManager PlayerOne;
        static public PlayerManager PlayerTwo;
        static public List<Enemy> EnemyList;
        static public List<ParticleSystem> ParticlesList;
        static public bool isPaused;
        static public MapGenerator map;

        private Viewport defaultView, leftView, rightView;
        static public Camera cameraRight, cameraLeft, cameraScreen;

        private Vector2 debugPlayerOnePosition;
        private Vector2 debugPlayerTwoPosition;
        private Color pauseColor;

        #region ZONA DE TESTE
        Enemy e;
        ParticleSystem teste2;
        #endregion

        //------------->CONSTRUCTORS<-------------//

        public GameState()
        {
            #region Map Generation
            map = new MapGenerator();
            map.UseRandomSeed = true;
            map.RandomFillPercent = 50;
            MapGenerator.Width = (int)Cons.MAXWIDTH; //100;
            MapGenerator.Height =(int)Cons.MAXHEIGHT;
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
            cameraRight = new Camera(Vector2.Zero, 50, ((float)rightView.Height / rightView.Width));
            cameraScreen = new Camera(Vector2.Zero, 100,(float)(Game1.graphics.PreferredBackBufferHeight / (float)Game1.graphics.PreferredBackBufferWidth));
            #endregion

            debugPlayerOnePosition = cameraScreen.CalculatePixelPoint(new Vector2(60, 0));
            debugPlayerTwoPosition = cameraScreen.CalculatePixelPoint(new Vector2(60, 10));
            Debug.Init(null, 30);   //Starting Debug.

            isPaused = false;
            pauseColor = new Color(Color.Black, 0.5f);
            #region TestZone
            EnemyList = new List<Enemy>();
            ParticlesList = new List<ParticleSystem>();

            PlayerOne = new PlayerManager(MapGenerator.GetPlayerStartingPosition(), Vector2.One * 5, PlayerNumber.playerOne, 10);
            PlayerTwo = new PlayerManager(MapGenerator.GetPlayerStartingPosition(), Vector2.One * 5, PlayerNumber.playerTwo, 10);

            teste2 = new ParticleSystem("DebugPixel", PlayerOne.Position, Vector2.One / 2, 40, 100, 10, 1000, 1000, 4);
            teste2.Start();
            ParticlesList.Add(teste2);

            e = new Enemy("New Piskel", PlayerOne.Position, 5, 10);
            EnemyList.Add(e);
            #endregion
        }

        //------------->FUNCTIONS && METHODS<-------------//

        /// <summary>
        /// Updates the whole gamestate.
        /// </summary>
        public void StateUpdate(GameTime gameTime)
        {
            if(InputManager.PressedLastFrame.Esc == ButtonState.Pressed)
            {
                isPaused = !isPaused;
            }
            if(!isPaused)
            {
                PlayerOne.PlayerMovement(gameTime);
                PlayerOne.DamageManager();
                cameraLeft.LookAt(PlayerOne);

                PlayerTwo.PlayerMovement(gameTime);
                PlayerTwo.DamageManager();
                cameraRight.LookAt(PlayerTwo);


                //Particle Update.
                teste2.Update(gameTime, PlayerOne.Center);
            }
            else
            {

            }
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

            Debug.DrawText();
            Debug.DrawPlayerInfo(cameraScreen, debugPlayerOnePosition, PlayerOne);
            Debug.DrawPlayerInfo(cameraScreen, debugPlayerTwoPosition, PlayerTwo);

            if (isPaused)
                Game1.spriteBatch.Draw(Debug.debugTexture, Game1.cameraArea, pauseColor);
            Game1.spriteBatch.End();
            #endregion

        }
        /// <summary>
        /// Draws the whole world for one camera.
        /// </summary>
        /// <param name="camera">Target camera to draw.</param>
        private void DrawCameraView(Camera camera)
        {
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);  //THIS WAY DOESNT AFFECT PIXEL ASPECT
            map.Draw(camera);
            PlayerOne.DrawObject(camera);
            PlayerTwo.DrawObject(camera);

            foreach(Enemy e in EnemyList)
            {
                e.DrawObject(camera);
            }
            foreach(ParticleSystem p in ParticlesList)
            {
                p.Draw(camera);
            }

            Debug.DrawColliders(camera, PlayerOne, PlayerOne.playerCollider);
            Game1.spriteBatch.End();
        }
    }
}