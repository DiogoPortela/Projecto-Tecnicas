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
        NoInput, Up, Down, Right, Left
    }
    internal struct MovementInput1
    {
        private ButtonState GetState(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key))
                return ButtonState.Pressed;
            return ButtonState.Released;
        }

        public ButtonState Up { get { return GetState(Keys.W); } }
        public ButtonState Down { get { return GetState(Keys.S); } }
        public ButtonState Left { get { return GetState(Keys.A); } }
        public ButtonState Right { get { return GetState(Keys.D); } }
        public ButtonState Start { get { return GetState(Keys.Escape); } }
        public ButtonState Select { get { return GetState(Keys.Back); } }

        //Buttons1..2..3
    }


    internal struct MovementInput2
    {
        private ButtonState GetState(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key))
                return ButtonState.Pressed;
            return ButtonState.Released;
        }

        public ButtonState Up { get { return GetState(Keys.Up); } }
        public ButtonState Down { get { return GetState(Keys.Down); } }
        public ButtonState Left { get { return GetState(Keys.Left); } }
        public ButtonState Right { get { return GetState(Keys.Right); } }
        public ButtonState Start { get { return GetState(Keys.Escape); } }
        public ButtonState Select { get { return GetState(Keys.Back); } }

        //Buttons1..2..3
    }

    internal struct AttackDefenseinput1
    {
        private ButtonState GetState(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key))
                return ButtonState.Pressed;
            return ButtonState.Released;
        }

        public ButtonState Space { get { return GetState(Keys.Space); } }
        public ButtonState Q { get { return GetState(Keys.Q); } }
        public ButtonState L { get { return GetState(Keys.L); } }
        public ButtonState K { get { return GetState(Keys.K); } }
    }
}
