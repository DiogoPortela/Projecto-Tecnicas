using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Projecto
{
    public class Camera
    {
        #region OLDSTUFF
        /*
                static private Vector2 WorldOrigin;    //User-space world origin.
                static private float Width;            //User-space Width.
                static private float Height;           //User-space Height.
                static private float Ratio;            //User-space to Pixe space convertion ratio.

                //------------->FUNCTIONS && METHODS<-------------//

                static private void cameraWindowToPixelRatio()
                {
                    if (Ratio < 0f)
                    {
                        Ratio = (float)Game1.graphics.PreferredBackBufferWidth / Width;                
                    }
                }

                /// <summary>
                /// Set the camera window size.
                /// </summary>
                /// <param name="origin"> Origin of the world. </param>
                /// <param name="width"> Width of the camera. </param>
                static public void SetCameraWindow(Vector2 origin, float width)
                {
                    WorldOrigin = origin;
                    Width = width;
                    Height = width * (float)Game1.graphics.PreferredBackBufferHeight / (float)Game1.graphics.PreferredBackBufferWidth;
                    Ratio = -1;
                    //this.Size.X = Width;
                    //this.Size.Y = Height;
                    cameraWindowToPixelRatio();
                }
                /// <summary>
                /// Convertes the position of a point from User-space to Pixel-space.
                /// </summary>
                /// <param name="objectPosition"> The position of the object in User-space. </param>
                /// <param name="x"> The X value to be converted. </param>
                /// <param name="y"> The y value to be converted. </param>
                static public void CalculatePixelPoint(Vector2 objectPosition, out int x, out int y)
                {
                    x = (int)(((objectPosition.X - WorldOrigin.X) * Ratio) + 0.5f);
                    y = (int)(((objectPosition.Y - WorldOrigin.Y) * Ratio) + 0.5f);

                    y = Game1.graphics.PreferredBackBufferHeight - y;
                }
                /// <summary>
                /// Convertes the position and size of a rectangle from User-space to Pixel-space.
                /// </summary>
                /// <param name="position"> Position of the rectangle in User-space. </param>
                /// <param name="size"> Size of the rectangle in User-space. </param>
                /// <returns></returns>
                static public Rectangle CalculatePixelRectangle(Vector2 position, Vector2 size)
                {
                    int width = (int)((size.X * Ratio) + 0.5f);
                    int height = (int)((size.Y * Ratio) + 0.5f);

                    int x, y;
                    CalculatePixelPoint(position, out x, out y);

                    return new Rectangle(x, y, width, height);
                }
            */
        #endregion  //FOR CHECKING PURPOSE

        private Vector2 WorldOrigin;            //User-space world origin.
        private float Width;            //User-space Width.
        private float Height;           //User-space Height.
        private float Ratio;            //User-space to Pixe space convertion ratio.
        //public Matrix transform;
        //public Vector2 center;


        //------------->CONSTRUCTORS<-------------//

        /// <summary>
        /// Set the camera window size.
        /// </summary>
        /// <param name="origin"> Origin of the world. </param>
        /// <param name="width"> Width of the camera. </param>
        public Camera(Vector2 origin, float width, float heighRatio)
        {
            WorldOrigin = origin;
            //transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
            Width = width;
            Height = width * heighRatio;
            Ratio = -1;
            cameraWindowToPixelRatio();
        }

        //------------->FUNCTIONS && METHODS<-------------//

        private void cameraWindowToPixelRatio()
        {
            if (Ratio < 0f)
            {
                Ratio = (float)Game1.graphics.PreferredBackBufferWidth / Width;
            }
        }

        /// <summary>
        /// Convertes the position of a point from User-space to Pixel-space.
        /// </summary>
        /// <param name="objectPosition"> The position of the object in User-space. </param>
        /// <param name="x"> The X value to be converted. </param>
        /// <param name="y"> The y value to be converted. </param>
        public void CalculatePixelPoint(Vector2 objectPosition, out int x, out int y)
        {
            x = (int)(((objectPosition.X - WorldOrigin.X) * Ratio) + 0.5f);
            y = (int)(((objectPosition.Y - WorldOrigin.Y) * Ratio) + 0.5f);

            y = Game1.graphics.PreferredBackBufferHeight / 2 - y;
        }
        /// <summary>
        /// Convertes the position and size of a rectangle from User-space to Pixel-space.
        /// </summary>
        /// <param name="position"> Position of the rectangle in User-space. </param>
        /// <param name="size"> Size of the rectangle in User-space. </param>
        /// <returns></returns>
        public Rectangle CalculatePixelRectangle(Vector2 position, Vector2 size)
        {
            int width = (int)((size.X * Ratio) + 0.5f);
            int height = (int)((size.Y * Ratio) + 0.5f);

            int x, y;
            CalculatePixelPoint(position, out x, out y);

            return new Rectangle(x, y, width, height);
        }
        /// <summary>
        /// Moves the camera to target position.
        /// </summary>
        /// <param name="position">Position to be moved to.</param>
        public void Update(Vector2 position)    //POR A A DAR
        {
            //center = new Vector2(position.X - (Game1.graphics.PreferredBackBufferWidth / 4), position.Y - (Game1.graphics.PreferredBackBufferHeight / 2));
            //transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}

