namespace Windows.Components
{
    public struct MouseState
    {
        public int positionX;
        public int positionY;
        public int scrollX;
        public int scrollY;
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

        public MouseState(int positionX, int positionY, int scrollX, int scrollY, byte buttons = default)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.scrollX = scrollX;
            this.scrollY = scrollY;
            this.buttons = buttons;
        }
    }
}
