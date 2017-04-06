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

        static private string textStr = "";
        static private int counter;

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

            textStr = "";
            counter = (text.Count() > MAXLINES) ? MAXLINES : text.Count();
            for (int i = 0; i < counter; i++)
            {
                textStr += text[text.Count() - i - 1] + "\n";
            }
        }
        public static void Draw(Camera camera)
        {
            if(isActive)
            {            
                Game1.spriteBatch.DrawString(font, textStr, camera.Position, Color.Black);
            }          
        }
    }
}
