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
        static private List<string> text = new List<string>();
        static private SpriteFont font;
        static private int MAXLINES = 5;
        static public bool isActive = true;

        public static void LoadFont( )
        {
            if(font == null)
            {
                font = Game1.content.Load<SpriteFont>("Arial");
            }
        }
        public static void NewLine(string newtext)
        {
            text.Add(newtext);
        }
        public static void Draw(Camera camera)
        {
            if(isActive)
            {
                string aux = "";
                int counter = (text.Count() > MAXLINES) ? MAXLINES : text.Count();
                for (int i = 0; i < counter; i++)
                {
                    aux += text[text.Count() - i - 1] + "\n";
                }
                Game1.spriteBatch.DrawString(font, aux, camera.Position, Color.Black);
            }          
        }
    }
}
