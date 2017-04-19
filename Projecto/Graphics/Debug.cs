using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Projecto
{
    class Debug
    {
        static private List<string> text = new List<string>();  //List with all the debug logs.
        static private SpriteFont font;                         //Font to use.
        static private int MAXLINES = 30;                        //Maximum Lines to draw on screen at once.
        static public bool isActive = true;                     //Should it draw o on screen?

        //AUXILIARY
        static private string textStr = "";                                                     
        static private int counter;

        //------------->FUNCTIONS && METHODS<-------------//

        /// <summary>
        /// Loads the Arial font if none is existant.
        /// </summary>
        public static void LoadFont( )
        {
            if(font == null)
            {
                font = Game1.content.Load<SpriteFont>("Arial");
            }
        }
        /// <summary>
        /// Adds a line to be drawn in the screen.
        /// </summary>
        /// <param name="newtext">Line to be drawn.</param>
        public static void NewLine(string newtext)
        {
            text.Add(newtext);

            textStr = "";
            counter = (text.Count() > MAXLINES) ? MAXLINES : text.Count();
            for (int i = 0; i < counter; i++)
            {
                textStr += text[text.Count() - i - 1] + "\n";
            }
        }
        /// <summary>
        /// Draws the collider border of an object on screen.
        /// </summary>
        /// <param name="camera">Camera to draw to.</param>
        /// <param name="gameObject">Object to draw.</param>
        /// <param name="collider">Collider to draw.</param>
        public static void DrawColliders(Camera camera, GameObject gameObject, Collider collider)
        {
            if(isActive)
            {
                Rectangle rect1 = camera.CalculatePixelRectangle(new Vector2(collider.MinBound.X, collider.MinBound.Y), new Vector2(5, 0.2f));
                Rectangle rect2 = camera.CalculatePixelRectangle(new Vector2(collider.MaxBound.X - 0.2f, collider.MaxBound.Y), new Vector2(0.2f, 5));
                Rectangle rect3 = camera.CalculatePixelRectangle(new Vector2(gameObject.Position.X, gameObject.Position.Y), new Vector2(5, 0.2f));
                Rectangle rect4 = camera.CalculatePixelRectangle(new Vector2(gameObject.Position.X, gameObject.Position.Y), new Vector2(0.2f, 5));
                DrawLine(camera, rect1);
                DrawLine(camera, rect2);
                DrawLine(camera, rect3);
                DrawLine(camera, rect4);
            }                  
        }
        /// <summary>
        /// Draws the MAXLINES amount of lines on the camera.
        /// </summary>
        /// <param name="camera">Camera to draw to.</param>
        public static void DrawText(Camera camera)
        {
            if(isActive)
            {            
                Game1.spriteBatch.DrawString(font, textStr, camera.Position, Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
            }          
        }
        /// <summary>
        /// Draws some important info on screen.
        /// </summary>
        /// <param name="camera">Camera to draw to.</param>
        /// <param name="position"></param>
        /// <param name="coordinates"></param>
        /// <param name="minBound"></param>
        /// <param name="maxBound"></param>
        public static void DrawInfo(Camera camera, Vector2 position, Vector2 coordinates, Vector2 minBound, Vector2 maxBound)
        {
            if(isActive)
            {
                Game1.spriteBatch.DrawString(font, "X:" + position.X + " Y:" + position.Y, camera.Position + new Vector2((camera.Widht - 40) * 8, 0), Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
                Game1.spriteBatch.DrawString(font, "Coor X:" + coordinates.X + " Coor Y:" + coordinates.Y, camera.Position + new Vector2((camera.Widht - 40) * 8, 15), Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
                Game1.spriteBatch.DrawString(font, "MinBound X:" + minBound.X + " MinBound Y:" + minBound.Y, camera.Position + new Vector2((camera.Widht - 40) * 8, 30), Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
                Game1.spriteBatch.DrawString(font, "MaxBound X:" + maxBound.X + " MaxBound Y:" + maxBound.Y, camera.Position + new Vector2((camera.Widht - 40) * 8, 45), Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
            }
        }
        /// <summary>
        /// Draws a Line on screen.
        /// </summary>
        /// <param name="camera">Camera to draw to.</param>
        /// <param name="rect">Rectangle that will serve as line.</param>
        public static void DrawLine(Camera camera, Rectangle rect)
        {
            if(isActive)
            {
                Texture2D debugTexture = Game1.content.Load<Texture2D>("DebugPixel");
                Game1.spriteBatch.Draw(debugTexture, rect, Color.LightGreen);
            }
        }
    }
}
