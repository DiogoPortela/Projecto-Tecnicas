using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    internal struct Coordinate
    {
        public int tileX;
        public int tileY;

        public Coordinate(int x, int y)
        {
            tileX = x;
            tileY = y;
        }
    }
    internal enum ScreenSelect
    {
        MainMenu, Playing, ScoreScreen
    }
    enum Cons
    {
        MAXWIDTH=100,
        MAXHEIGHT=75
    }
    internal enum PlayerNumber
    {
        playerOne = 1, playerTwo
    }
    internal enum CurrentInput
    {
        NoInput, Up, Down, Right, Left, Attack, Defend
    }
}
