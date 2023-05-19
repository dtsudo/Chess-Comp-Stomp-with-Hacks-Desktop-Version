
namespace ChessCompStompWithHacks
{
	using DTLibrary;
	using Microsoft.Xna.Framework.Input;

	public class MonoGameMouse : IMouse
    {
        private int windowHeight;

        public MonoGameMouse(int windowHeight)
        {
            this.windowHeight = windowHeight;
        }

        public int GetX()
        {
            return Mouse.GetState().X;
        }

        public int GetY()
        {
            int y = Mouse.GetState().Y;
            return this.windowHeight - y - 1;
        }

        public bool IsLeftMouseButtonPressed()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed;
        }

        public bool IsRightMouseButtonPressed()
        {
            return Mouse.GetState().RightButton == ButtonState.Pressed;
        }
    }
}
