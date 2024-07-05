using System.Numerics;

namespace Windows.Components
{
    public struct MouseState
    {
        public Vector2 position;
        public Vector2 scroll;
        public byte buttons;

        public bool this[Mouse.Button button]
        {
            readonly get => this[(uint)button];
            set => this[(uint)button] = value;
        }

        public bool this[uint index]
        {
            readonly get => (buttons & 1 << (int)index) != 0;
            set
            {
                if (value)
                {
                    buttons |= (byte)(1 << (int)index);
                }
                else
                {
                    buttons &= (byte)~(1 << (int)index);
                }
            }
        }

        public MouseState(Vector2 position, Vector2 scroll, byte buttons = default)
        {
            this.position = position;
            this.scroll = scroll;
            this.buttons = buttons;
        }
    }
}
