using System.Numerics;

namespace Windows.Components
{
    public struct IsMouse
    {
        public MouseState state;

        public Vector2 Position
        {
            readonly get => new(state.positionX, state.positionY);
            set => state = new((int)value.X, (int)value.Y, state.scrollX, state.scrollY, state.buttons);
        }

        public Vector2 Scroll
        {
            readonly get => new(state.scrollX, state.scrollY);
            set => state = new(state.positionX, state.positionY, (int)value.X, (int)value.Y, state.buttons);
        }

        public IsMouse(MouseState state)
        {
            this.state = state;
        }
    }
}
