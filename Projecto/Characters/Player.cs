using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Projecto
{
    class Player : GameObject
    {
        private PlayerNumber playerNumber;

        #region Atribute Stats
        private int maxHP;
        public int HP { get; set; }
        public int MP { get; set; }
        public float CDR { get; set; }
        public float AoE { get; set; }
        public float MS { get; set; }
        public double PhysDmg { get; set; }
        public double MagicDmg { get; set; }
        public Circle Range { get; set; }
        #endregion


        public struct Circle
        {
            public Circle(int x, int y, int radius)
                : this()
            {
                X = x;
                Y = y;
                Radius = radius;
            }

            public int Radius { get; private set; }
            public int X { get; private set; }
            public int Y { get; private set; }

            public bool Intersects(Rectangle rectangle)
            {
                // the first thing we want to know is if any of the corners intersect
                var corners = new[]
                {
            new Point(rectangle.Top, rectangle.Left),
            new Point(rectangle.Top, rectangle.Right),
            new Point(rectangle.Bottom, rectangle.Right),
            new Point(rectangle.Bottom, rectangle.Left)
        };

                foreach (var corner in corners)
                {
                    if (ContainsPoint(corner))
                        return true;
                }

                // next we want to know if the left, top, right or bottom edges overlap
                if (X - Radius > rectangle.Right || X + Radius < rectangle.Left)
                    return false;

                if (Y - Radius > rectangle.Bottom || Y + Radius < rectangle.Top)
                    return false;

                return true;
            }
            public bool ContainsPoint(Point point)
            {
                var vector2 = new Vector2(point.X - X, point.Y - Y);
                return vector2.Length() <= Radius;
            }
        }

        public Player(string texture, Vector2 position, float size, PlayerNumber playerNumber, float Range) : base(texture, position, new Vector2(size, size), 0f)
        {
            this.playerNumber = playerNumber;

            #region Stats initializer
            this.HP = 100;
            this.MP = 100;
            this.CDR = 1.0f;
            this.AoE = 1.0f;
            this.MS = 1.0f;
            this.PhysDmg = 18.0;
            this.MagicDmg = 8.0;
            this.Range = new Circle();
            #endregion
        }
        public void checkcollision(PlayerNumber one, List<Enemy> enemies)
        {

        }


    }
}
