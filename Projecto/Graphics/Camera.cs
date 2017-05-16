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
        private Vector2 position;    //User-space camera position.
        private float width;            //User-space Width.
        private float height;           //User-space Height.
        private Vector2 centerCamera;   //Measure of the center of the camera in User-space.
        private float ratio;            //User-space to Pixe space convertion ratio.

        public Vector2 Position { get { return position; } }
        public float Widht { get { return width; } }
        public float Height { get { return height; } }

        //------------->CONSTRUCTORS<-------------//

        /// <summary>
        /// Set the camera window size;
        /// </summary>
        /// <param name="position"></param>
        /// <param name="width"></param>
        public Camera(Vector2 position, float width)
        {
            this.position = position;
            this.width = width;
            height = width * Game1.graphics.PreferredBackBufferWidth / Game1.graphics.PreferredBackBufferHeight;
            ratio = -1;
            centerCamera = new Vector2(width / 2, height / 2);
            cameraWindowToPixelRatio();
        }
        /// <summary>
        /// Set the camera window size.
        /// </summary>
        /// <param name="position"> Camera position. </param>
        /// <param name="width"> Width of the camera. </param>
        /// <param name="heigthRatio"> Ratio of the height. </param>
        public Camera(Vector2 position, float width, float heightRatio)
        {
            this.position = position;
            this.width = width;
            height = width * heightRatio;
            ratio = -1;
            centerCamera = new Vector2(width / 2, height / 2);
            cameraWindowToPixelRatio();
        }

        //------------->FUNCTIONS && METHODS<-------------//

        private void cameraWindowToPixelRatio()
        {
            if (ratio < 0f)
            {
                ratio = (float)Game1.graphics.PreferredBackBufferHeight  / height;
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
            x = (int)(((objectPosition.X - position.X) * ratio) + 0.5f);
            y = (int)(((objectPosition.Y - position.Y) * ratio) + 0.5f);

            y = Game1.graphics.PreferredBackBufferHeight - y;
        }
        /// <summary>
        /// Transforms a point to pixel-position. (doesn't invert Y)
        /// </summary>
        /// <param name="position">Position on screen to draw to.</param>
        /// <returns></returns>
        public Vector2 CalculatePixelPoint(Vector2 position)
        {
            position.X = (int)((position.X * ratio) + 0.5f);
            position.Y = (int)((position.Y * ratio) + 0.5f);

            //position.Y = Game1.graphics.PreferredBackBufferHeight - position.Y;
            return position;
        }
        /// <summary>
        /// Convertes the position and size of a rectangle from User-space to Pixel-space.
        /// </summary>
        /// <param name="position"> Position of the rectangle in User-space. </param>
        /// <param name="size"> Size of the rectangle in User-space. </param>
        /// <returns></returns>
        public Rectangle CalculatePixelRectangle(Vector2 position, Vector2 size)
        {
            int width = (int)((size.X * ratio) + 0.5f);
            int height = (int)((size.Y * ratio) + 0.5f);

            int x, y;
            CalculatePixelPoint(position, out x, out y);

            return new Rectangle(x, y, width, height);
        }
        /// <summary>
        /// Makes the camera look at an object center.
        /// </summary>
        /// <param name="obj">Object to be looked at.</param>
        public void LookAt(GameObject obj)
        {
            this.position = obj.Center - centerCamera;
        }
        /// <summary>
        /// Makes the camera look at a given position.
        /// </summary>
        /// <param name="position">Position to look at.</param>
        public void LookAt(Vector2 position)
        {
            this.position = position - centerCamera;
        }
    }
}

