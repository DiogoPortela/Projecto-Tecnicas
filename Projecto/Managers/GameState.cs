using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Projecto
{
    static class GameState
    {
        static public PlayerManager PlayerOne;
        static public PlayerManager PlayerTwo;
        static public List<Enemy> EnemyList;
        static public List<ParticleSystem> ParticlesList;
        static public List<Bullet> BulletList;
        static public Dictionary<string, Item> ItemDictionary;
        static public List<UI_Static_Item> UI_Static_ItemsList;

        static public bool isPaused;
        static public MapGenerator map;
        static private Viewport defaultView, leftView, rightView;
        static public Camera cameraRight, cameraLeft;

        static private Vector2 debugPlayerOnePosition;
        static private Vector2 debugPlayerTwoPosition;
        static private Vector2 debugTextPosition;

        #region ZONA DE TESTE
        
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
            MapGenerator.Width = (int)Constants.MAXWIDTH; //100;
            MapGenerator.Height = (int)Constants.MAXHEIGHT;
            map.GenerateMap((int)Constants.GRIDSIZE);
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
            debugTextPosition = Game1.mainCamera.CalculatePixelPoint(new Vector2(60, 20));
            #endregion

            EnemyList = new List<Enemy>();
            ParticlesList = new List<ParticleSystem>();
            BulletList = new List<Bullet>();
            ItemDictionary = new Dictionary<string, Item>();
            UI_Static_ItemsList = new List<UI_Static_Item>();

            #region LoadWeapons
            Weapon aux = new Weapon("Staff", "Staff", MapGenerator.GetPlayerStartingPosition(), true, 25, 0, 25, 1.5f, 1, 1, 1, 1);
            ItemDictionary.Add(aux.Name, aux);
            aux = new Weapon("Sword", "Sword", MapGenerator.GetPlayerStartingPosition(), false, 0, 25, 3, 1, 1, 1, 1, 1);
            ItemDictionary.Add(aux.Name, aux);
            #endregion

            PlayerOne = new PlayerManager(MapGenerator.GetPlayerStartingPosition(), Vector2.One * (int)Constants.GRIDSIZE, PlayerNumber.playerOne, 3);
            PlayerTwo = new PlayerManager(MapGenerator.GetPlayerStartingPosition(), Vector2.One * (int)Constants.GRIDSIZE, PlayerNumber.playerTwo, 3);
          
            #region Enemies
            List<Vector2> enemyPosList = MapGenerator.FindEnemySpawns(50);
            for (int i = 0; i < enemyPosList.Count; i++)
            {
                Enemy enemyAux = new Enemy("New Piskel", enemyPosList[i], Vector2.One * (int)Constants.GRIDSIZE, 10);
                EnemyList.Add(enemyAux);
            }
            #endregion

            UI_Static_Item uiCentralBar = new UI_Static_Item("ui_center", new Vector2(49, 75), new Vector2(2, 75), Game1.mainCamera);
            UI_Static_ItemsList.Add(uiCentralBar);

            isPaused = false;
            SoundManager.StopAllSounds();
            //SoundManager.StartSound("mainGameTheme", true);

            #region TestZone

<<<<<<< HEAD
            List<Vector2> enemyPosList = MapGenerator.FindEnemySpawns();

            for (int i = 0; i < 100; i++)
            {
                Enemy enemyAux = new Enemy("New Piskel", enemyPosList[i], Vector2.One * 5, 10);
                EnemyList.Add(enemyAux);
            }
=======
>>>>>>> WeaponMechanics
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
                PlayerOne.Update(gameTime);
                cameraLeft.LookAt(PlayerOne);

                PlayerTwo.Update(gameTime);
                cameraRight.LookAt(PlayerTwo);

                foreach (Enemy e in EnemyList)              
                    e.Update(gameTime);               
                for(int i = 0; i < BulletList.Count; i++)
                    BulletList[i].Update();
                for (int i = 0; i < ParticlesList.Count; i++)
                {
                    ParticlesList[i].Update(gameTime);
                    if (!ParticlesList[i].isLoop && ParticlesList[i].TimeLeft.Milliseconds < 0)
                        ParticlesList.Remove(ParticlesList[i]);
                }
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
            Debug.DrawTextAt("Enemys: " + EnemyList.Count.ToString() + " Particles: " + ParticlesList.Count.ToString() + " Bullets: " + BulletList.Count.ToString()
                               + "\nItems: " + ItemDictionary.Count.ToString(), debugTextPosition);
                            
            foreach(UI_Static_Item ui in UI_Static_ItemsList)
            {
                ui.Draw();
            }

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
            foreach (KeyValuePair<string,Item> i in ItemDictionary)
                i.Value.Draw(camera);
            foreach (Bullet b in BulletList)
                b.DrawObject(camera);

            Game1.spriteBatch.End();
        }
    }
}