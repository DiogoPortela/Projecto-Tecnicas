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
        public static void DrawText()
        {
            if(isActive)
            {            
                Game1.spriteBatch.DrawString(font, textStr, Vector2.Zero, Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
            }          
        }
        /// <summary>
        /// Draws some important info on screen.
        /// </summary>
        /// <param name="camera">Camera to draw to.</param>
        /// <param name="drawPosition">Position to draw to.</param>
        /// <param name="player">Player information.</param>
        public static void DrawPlayerInfo(Camera camera,Vector2 drawPosition, PlayerManager player)
        {
            if(isActive)
            {
                Game1.spriteBatch.DrawString(font, "X:" + player.Position.X + " Y:" + player.Position.Y, drawPosition, Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
                Game1.spriteBatch.DrawString(font, "Coor X:" + player.Coordinates.X + " Coor Y:" + player.Coordinates.Y,drawPosition + new Vector2(0 , 15), Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
                Game1.spriteBatch.DrawString(font, "MinBound X:" + player.playerCollider.MinBound.X + " MinBound Y:" + player.playerCollider.MinBound.Y,drawPosition + new Vector2(0, 30), Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
                Game1.spriteBatch.DrawString(font, "MaxBound X:" + player.playerCollider.MaxBound.X + " MaxBound Y:" + player.playerCollider.MaxBound.Y,drawPosition + new Vector2(0, 45), Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
                Game1.spriteBatch.DrawString(font, "HP:" + player.HP, drawPosition + new Vector2(0, 60), Color.Green, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);

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
