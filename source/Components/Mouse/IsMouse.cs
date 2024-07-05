using System.Numerics;

namespace Windows.Components
{
    public struct IsMouse
    {
        public MouseState state;

        public Vector2 Position
        {
            readonly get => state.position;
            set => state = new(value, state.scroll, state.buttons);
        }

        public Vector2 Scroll
        {
            readonly get => state.scroll;
            set => state = new(state.position, value, state.buttons);
        }

        public IsMouse(MouseState state)
        {
            this.state = state;
        }
    }
}
