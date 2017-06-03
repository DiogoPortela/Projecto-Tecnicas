using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using SettlersEngine;
using System;

namespace Projecto
{
    static class GameState
    {
        static public PlayerManager PlayerOne;
        static public PlayerManager PlayerTwo;
        static public List<Enemy> EnemyList;
        static public List<ParticleSystem> ParticlesList;
        static public bool isPaused;
        static public MapGenerator map;
        //static public SpatialAStar<Tile, Object> aStar;

        static private Viewport defaultView, leftView, rightView;
        static public Camera cameraRight, cameraLeft;

        static private Vector2 debugPlayerOnePosition;
        static private Vector2 debugPlayerTwoPosition;

        #region ZONA DE TESTE
        static ParticleSystem teste2;
        #endregion

        //------------->FUNCTIONS && METHODS<-------------//

        /// <summary>
        /// Starts the class values.
        /// </summary>
        static public void Start()
        {
            #region Map Generation
            map = new MapGenerator();
            map.UseRandomSeed = true;
            map.RandomFillPercent = 50;
            MapGenerator.Width = (int)Cons.MAXWIDTH; //100;
            MapGenerator.Height = (int)Cons.MAXHEIGHT;
            map.GenerateMap(5);
            //aStar = new SpatialAStar<Tile, Object>(MapGenerator.TilesMap);
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
            #endregion

            #region Debugger
            debugPlayerOnePosition = Game1.mainCamera.CalculatePixelPoint(new Vector2(60, 0));
            debugPlayerTwoPosition = Game1.mainCamera.CalculatePixelPoint(new Vector2(60, 10));
            #endregion

            EnemyList = new List<Enemy>();
            ParticlesList = new List<ParticleSystem>();

            PlayerOne = new PlayerManager(MapGenerator.GetPlayerStartingPosition(), Vector2.One * 5, PlayerNumber.playerOne, 10);
            PlayerTwo = new PlayerManager(MapGenerator.GetPlayerStartingPosition(), Vector2.One * 5, PlayerNumber.playerTwo, 10);
            isPaused = false;
            SoundManager.StopAllSounds();
            //SoundManager.StartSound("mainGameTheme", true);

            #region TestZone            
            teste2 = new ParticleSystem("DebugPixel", PlayerOne.Position, Vector2.One / 2, 40, 100, 10, 1000, 1000, 4);
            teste2.Start();
            ParticlesList.Add(teste2);


            for(int i = 0; i < 30; i++)
            {
                Enemy enemyAux = new Enemy("New Piskel", MapGenerator.FindEnemySpawns()[0], Vector2.One * 5, 10);
                EnemyList.Add(enemyAux);
            }
            #endregion
        }
        /// <summary>
        /// Updates the whole gamestate.
        /// </summary>
        static public void StateUpdate(GameTime gameTime)
        {
            if (InputManager.PressedLastFrame.Esc == ButtonState.Pressed)
            {
                isPaused = !isPaused;
            }
            if (!isPaused)
            {
                PlayerOne.PlayerMovement(gameTime);
                PlayerOne.DamageManager();
                cameraLeft.LookAt(PlayerOne);

                PlayerTwo.PlayerMovement(gameTime);
                PlayerTwo.DamageManager();
                cameraRight.LookAt(PlayerTwo);

                foreach (Enemy e in EnemyList)
                {
                    e.EnemyMovement(gameTime, ref PlayerOne);
                }

                //Particle Update.
                teste2.Update(gameTime, PlayerOne.Center);

            }
            else
            {
                UI.PauseUpdate();
            }
        }
        /// <summary>
        /// Draws the whole gamestate.
        /// </summary>
        static public void Draw()
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
            Debug.DrawPlayerInfo(debugPlayerOnePosition, PlayerOne);
            Debug.DrawPlayerInfo(debugPlayerTwoPosition, PlayerTwo);

            if (isPaused)
            {
                UI.DrawPauseMenu();
            }

            Game1.spriteBatch.End();
            #endregion
        }
        /// <summary>
        /// Draws the whole world for one camera.
        /// </summary>
        /// <param name="camera">Target camera to draw.</param>
        static private void DrawCameraView(Camera camera)
        {
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);  //THIS WAY DOESNT AFFECT PIXEL ASPECT
            map.Draw(camera);
            PlayerOne.DrawObject(camera);
            PlayerTwo.DrawObject(camera);

            foreach (Enemy e in EnemyList)
            {
                e.DrawObject(camera);
            }
            foreach (ParticleSystem p in ParticlesList)
            {
                p.Draw(camera);
            }

            Debug.DrawColliders(camera, PlayerOne, PlayerOne.playerCollider);
            Game1.spriteBatch.End();
        }
    }
}