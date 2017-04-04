using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Projecto
{
    
    static class CollisionManager
    {       
        public static bool TouchingTopOf(this Rectangle rect1, Rectangle rect2)
        { 
            return (rect1.Bottom >= rect2.Top - 1 &&
                    rect1.Bottom <= rect2.Top + (rect2.Height / 2) &&
                    rect1.Right >= rect2.Left + rect2.Width / 5 &&
                    rect1.Left <= rect2.Right - rect2.Width / 5);
        }
        public static bool TouchingBottomOf(this Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Top <= rect2.Bottom + (rect2.Height / 5) &&
                    rect1.Top >= rect2.Bottom - 1 &&
                    rect1.Right >= rect2.Left + (rect2.Width / 5) &&
                    rect1.Left <= rect2.Right - (rect2.Width / 5));
        }
        public static bool TouchingLeftOf(this Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Right <= rect2.Right &&
                    rect1.Right >= rect2.Left - 5 &&
                    rect1.Top <= rect2.Bottom - (rect2.Width / 4) &&
                    rect1.Bottom >= rect2.Top + (rect2.Width / 4));
        }
        public static bool TouchingRightOf(this Rectangle rect1, Rectangle rect2)
        {
            return (rect1.Left >= rect2.Left &&
                    rect1.Left <= rect2.Right + 5 &&
                    rect1.Top <= rect2.Bottom - (rect2.Width / 4) &&
                    rect1.Bottom >= rect2.Top + (rect2.Width / 4));
        }
    }
}
