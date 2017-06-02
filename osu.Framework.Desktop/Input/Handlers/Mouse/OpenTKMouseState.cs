using OpenTK;
using OpenTK.Input;
using MouseState = osu.Framework.Input.MouseState;

namespace osu.Framework.Desktop.Input.Handlers.Mouse
{
    internal class OpenTKMouseState : MouseState
    {
        public readonly bool WasActive;

        public override int WheelDelta => WasActive ? base.WheelDelta : 0;

        public OpenTKMouseState(OpenTK.Input.MouseState tkState, bool active, Vector2? mappedPosition)
        {
            WasActive = active;

            // While not focused, let's silently ignore everything but position.
            if (active && tkState.IsAnyButtonDown)
            {
                addIfPressed(tkState.LeftButton, MouseButton.Left);
                addIfPressed(tkState.MiddleButton, MouseButton.Middle);
                addIfPressed(tkState.RightButton, MouseButton.Right);
                addIfPressed(tkState.XButton1, MouseButton.Button1);
                addIfPressed(tkState.XButton2, MouseButton.Button2);
            }

            Wheel = tkState.Wheel;
            Position = new Vector2(mappedPosition?.X ?? tkState.X, mappedPosition?.Y ?? tkState.Y);
        }

        private void addIfPressed(ButtonState tkState, MouseButton button)
        {
            if (tkState == ButtonState.Pressed)
                SetPressed(button, true);
        }
    }
}