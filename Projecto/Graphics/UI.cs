using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    class UI
    {
        static public Vector2 mousePostion;
        static public SpriteFont GameFont;

        static public void Start(SpriteFont font)
        {
            if(font == null)
            {
                GameFont = Game1.content.Load<SpriteFont>("Arial");
            }
            else
            {
                GameFont = font;
            }
        }

        static public void Update()
        {
            mousePostion.X = Mouse.GetState().Position.X;
            mousePostion.Y = Mouse.GetState().Position.Y;
        }
    }
}
