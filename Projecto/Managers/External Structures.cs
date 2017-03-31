using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Projecto
{
    internal enum PlayerNumber
    {
        playerOne = 1, playerTwo
    }
    internal struct MovementInput
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
}
