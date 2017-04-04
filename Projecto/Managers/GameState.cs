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
        //static public PlayerManager PlayerTwo;

        Viewport defaultView, upView, downView;
        Camera cameraUp, cameraDown;

        //------------->CONSTRUCTORS<-------------//

        public GameState()
        {
            #region map generation
            //Map mapTop = new Map(Vector2.Zero);
            //Map mapBottom = new Map(new Vector2(0, 50));
            //mapateste.Generate();
            #endregion

            #region Camera. Split screen
            //Viewports
            
            defaultView = Game1.graphics.GraphicsDevice.Viewport;
            upView = downView = defaultView;

            //Dividing it in half, and adjusting the positioning.
            upView.Height /= 2;
            downView.Height /= 2;
            downView.Y = upView.Height;

            //Initializing cameras.
            cameraUp = new Camera(Vector2.Zero, 100, ((float)upView.Height / upView.Width));
            cameraDown = new Camera(new Vector2(0,50), 100, ((float)downView.Height / downView.Width));
            #endregion

            #region TestZone
            PlayerOne = new PlayerManager(new Vector2(0, 10), Vector2.One * 5, PlayerNumber.playerOne);
            //PlayerTwo = new PlayerManager(new Vector2(0, 60), Vector2.One * 10, PlayerNumber.playerTwo);
            #endregion

        }

        //------------->FUNCTIONS && METHODS<-------------//

        /// <summary>
        /// Updates the whole gamestate.
        /// </summary>
        public void StateUpdate(GameTime gameTime)
        {
            PlayerOne.PlayerMovement(gameTime);
            //PlayerTwo.PlayerMovement(gameTime);

        }
        /// <summary>
        /// Draws the whole gamestate.
        /// </summary>
        public void Draw()
        {
            //Draws the left side
            Game1.graphics.GraphicsDevice.Viewport = upView;
            DrawCameraView(cameraUp);

            //Draws the right side
            Game1.graphics.GraphicsDevice.Viewport = downView;
            DrawCameraView(cameraDown);

            #region Draws the whole picture.
            Game1.graphics.GraphicsDevice.Viewport = defaultView;
            Game1.spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);  //THIS WAY DOESNT AFFECT PIXEL ASPECT

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
            PlayerOne.DrawObject();
            //PlayerTwo.DrawObject();
            Game1.spriteBatch.End();
        }
    }
}
