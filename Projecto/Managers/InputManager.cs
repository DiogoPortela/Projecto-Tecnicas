using Microsoft.Xna.Framework.Input;

namespace Projecto
{
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
    internal struct PressedLastFrame
    {
        private ButtonState GetState(Keys key)
        {
            if (Keyboard.GetState().IsKeyDown(key) && Game1.lastFrameState.IsKeyUp(key))
                return ButtonState.Pressed;
            return ButtonState.Released;
        }

        public ButtonState UpOne { get { return GetState(Keys.W); } }
        public ButtonState DownOne { get { return GetState(Keys.S); } }
        public ButtonState LeftOne { get { return GetState(Keys.A); } }
        public ButtonState RightOne { get { return GetState(Keys.D); } }
        public ButtonState UpTwo { get { return GetState(Keys.Up); } }
        public ButtonState DownTwo { get { return GetState(Keys.Down); } }
        public ButtonState LeftTwo { get { return GetState(Keys.Left); } }
        public ButtonState RightTwo { get { return GetState(Keys.Right); } }
        public ButtonState Space { get { return GetState(Keys.Space); } }
        public ButtonState Q { get { return GetState(Keys.Q); } }
        public ButtonState L { get { return GetState(Keys.L); } }
        public ButtonState K { get { return GetState(Keys.K); } }
        public ButtonState Esc { get { return GetState(Keys.Escape); } }
        public ButtonState Enter { get { return GetState(Keys.Enter); } }
    }
    static class InputManager
    {
        static public MovementInput1 PlayerOne = new MovementInput1();
        static public MovementInput2 PlayerTwo = new MovementInput2();
        static public PressedLastFrame PressedLastFrame = new PressedLastFrame();
    }
}
