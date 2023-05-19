
namespace ChessCompStompWithHacks
{
	using DTLibrary;
	using Microsoft.Xna.Framework.Input;
	using System;

	public class MonoGameKeyboard : IKeyboard
    {
        public MonoGameKeyboard()
        {
        }

        private static Keys[] MapDTLibraryKeyToMonoGameKeys(Key key)
        {
            switch (key)
            {
                case Key.A: return new Keys[] { Keys.A };
                case Key.B: return new Keys[] { Keys.B };
                case Key.C: return new Keys[] { Keys.C };
                case Key.D: return new Keys[] { Keys.D };
                case Key.E: return new Keys[] { Keys.E };
                case Key.F: return new Keys[] { Keys.F };
                case Key.G: return new Keys[] { Keys.G };
                case Key.H: return new Keys[] { Keys.H };
                case Key.I: return new Keys[] { Keys.I };
                case Key.J: return new Keys[] { Keys.J };
                case Key.K: return new Keys[] { Keys.K };
                case Key.L: return new Keys[] { Keys.L };
                case Key.M: return new Keys[] { Keys.M };
		        case Key.N: return new Keys[] { Keys.N };
                case Key.O: return new Keys[] { Keys.O };
                case Key.P: return new Keys[] { Keys.P };
                case Key.Q: return new Keys[] { Keys.Q };
                case Key.R: return new Keys[] { Keys.R };
                case Key.S: return new Keys[] { Keys.S };
                case Key.T: return new Keys[] { Keys.T };
                case Key.U: return new Keys[] { Keys.U };
                case Key.V: return new Keys[] { Keys.V };
                case Key.W: return new Keys[] { Keys.W };
                case Key.X: return new Keys[] { Keys.X };
                case Key.Y: return new Keys[] { Keys.Y };
                case Key.Z: return new Keys[] { Keys.Z };
		        case Key.Zero: return new Keys[] { Keys.D0 };
                case Key.One: return new Keys[] { Keys.D1 };
                case Key.Two: return new Keys[] { Keys.D2 };
                case Key.Three: return new Keys[] { Keys.D3 };
                case Key.Four: return new Keys[] { Keys.D4 };
                case Key.Five: return new Keys[] { Keys.D5 };
                case Key.Six: return new Keys[] { Keys.D6 };
                case Key.Seven: return new Keys[] { Keys.D7 };
                case Key.Eight: return new Keys[] { Keys.D8 };
                case Key.Nine: return new Keys[] { Keys.D9 };
                case Key.UpArrow: return new Keys[] { Keys.Up };
                case Key.DownArrow: return new Keys[] { Keys.Down };
                case Key.LeftArrow: return new Keys[] { Keys.Left };
                case Key.RightArrow: return new Keys[] { Keys.Right };
                case Key.Delete: return new Keys[] { Keys.Delete };
                case Key.Backspace: return new Keys[] { Keys.Back };
                case Key.Enter: return new Keys[] { Keys.Enter };
                case Key.Shift: return new Keys[] { Keys.LeftShift, Keys.RightShift };
                case Key.Space: return new Keys[] { Keys.Space };
                case Key.Esc: return new Keys[] { Keys.Escape };
                default: throw new Exception();
            }
        }

        public bool IsPressed(Key key)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] mappedKeys = MapDTLibraryKeyToMonoGameKeys(key);

            for (int i = 0; i < mappedKeys.Length; i++)
            {
                if (keyboardState.IsKeyDown(mappedKeys[i]))
                    return true;
            }

            return false;
        }
    }
}
