using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.IO;

namespace Projecto
{
    class Debug
    {
        static private List<string> text;                       //List with all the debug logs.
        static private SpriteFont font;                         //Font to use.
        static private int MAXLINES;                            //Maximum Lines to draw on screen at once.
        static public bool isActive = true;                     //Should it draw o on screen?

        //AUXILIARY
        static private string textStr;
        static private string textPlayer;
        static public Texture2D debugTexture;
        static private int counter;
        static private Stopwatch watch;

        static private Color debugColor;
        
        //------------->FUNCTIONS && METHODS<-------------//

        /// <summary>
        /// Initialize Debugging.
        /// </summary>
        /// <param name="font">Font for the text.</param>
        /// <param name="maxLines">Number Max of lines.</param>
        public static void Start(SpriteFont font, int maxLines)
        {
            debugTexture = Game1.content.Load<Texture2D>("DebugPixel");
            MAXLINES = maxLines;
            textStr = "";
            text = new List<string>();
            LoadFont(font);
            watch = new Stopwatch();
            debugColor = Color.OrangeRed;
        }
        /// <summary>
        /// Enable rendering.
        /// </summary>
        public static void Enable()
        {
            isActive = true;
        }
        /// <summary>
        /// Disable rendering.
        /// </summary>
        public static void Disable()
        {
            isActive = false;
        }
        /// <summary>
        /// Toggle rendering.
        /// </summary>
        public static void Toggle()
        {
            isActive = !isActive;
        }

        /// <summary>
        /// Loads the Arial font if none is existant.
        /// </summary>
        private static void LoadFont(SpriteFont font1)
        {
            if(font1 == null)
            {
                font = Game1.content.Load<SpriteFont>("Arial");
            }
        }

        public static void StartTimer()
        {
            watch.Reset();
            watch.Start();
        }
        public static void StopTimer(string functionName)
        {
            StreamWriter sw = new StreamWriter("debugger.txt", true);
            watch.Stop();
            NewLine(functionName + " time: " + watch.ElapsedMilliseconds);
            sw.WriteLine(functionName + " - " + watch.ElapsedMilliseconds);
            sw.Close();
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
                Rectangle rect1 = camera.CalculatePixelRectangle(new Vector2(collider.MinBound.X, collider.MinBound.Y), new Vector2((int)Constants.GRIDSIZE, 0.2f));
                Rectangle rect2 = camera.CalculatePixelRectangle(new Vector2(collider.MaxBound.X - 0.2f, collider.MaxBound.Y), new Vector2(0.2f, (int)Constants.GRIDSIZE));
                Rectangle rect3 = camera.CalculatePixelRectangle(new Vector2(gameObject.Position.X, gameObject.Position.Y), new Vector2((int)Constants.GRIDSIZE, 0.2f));
                Rectangle rect4 = camera.CalculatePixelRectangle(new Vector2(gameObject.Position.X, gameObject.Position.Y), new Vector2(0.2f, (int)Constants.GRIDSIZE));
                DrawLine(camera, rect1);
                DrawLine(camera, rect2);
                DrawLine(camera, rect3);
                DrawLine(camera, rect4);
            }                  
        }
        /// <summary>
        /// Draws the MAXLINES amount of lines on the camera.
        /// </summary>
        public static void DrawText()
        {
            if(isActive)
            {            
                Game1.spriteBatch.DrawString(font, textStr, Vector2.Zero, debugColor, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
            }          
        }
        /// <summary>
        /// Draws some important info on screen.
        /// </summary>
        /// <param name="drawPosition">Position to draw to.</param>
        /// <param name="player">Player information.</param>
        public static void DrawPlayerInfo(Vector2 drawPosition, PlayerManager player)
        {
            if(isActive)
            {
                textPlayer = "X:" + player.Position.X + " Y:" + player.Position.Y + "\n" +
                                "Coordinate X:" + player.Coordinates.X + " Coordinate Y:" + player.Coordinates.Y + "\n" +
                                "MinBound X:" + player.playerCollider.MinBound.X + " MinBound Y:" + player.playerCollider.MinBound.Y + "\n" +
                                "MaxBound X:" + player.playerCollider.MaxBound.X + " MaxBound Y:" + player.playerCollider.MaxBound.Y + "\n" /*+
                                "Weapon: " + player.MHweapon.Name*/;
                Game1.spriteBatch.DrawString(font, textPlayer, drawPosition, debugColor, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
            }
        }
        /// <summary>
        /// Draws a given text in a given position.
        /// </summary>
        /// <param name="text">Text. </param>
        /// <param name="pos">Position. </param>
        public static void DrawTextAt(string text, Vector2 pos)
        {
            if(isActive)
            {
                Game1.spriteBatch.DrawString(font, text, pos, debugColor, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1);
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
                Game1.spriteBatch.Draw(debugTexture, rect, debugColor);
            }
        }
    }
}