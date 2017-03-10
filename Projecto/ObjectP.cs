using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecto
{
    public class ObjectP
    {
        public Texture2D Image;
        public Vector2 Position;
        public Vector2 Size;
                
        public ObjectP(Texture2D img, Vector2 pos, Vector2 size)
        {
            this.Image = img;
            this.Position = pos;
            this.Size = size;
        } 
    }
}
