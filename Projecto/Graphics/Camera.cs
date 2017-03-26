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
    }
}
