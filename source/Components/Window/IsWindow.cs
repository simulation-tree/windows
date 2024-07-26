using System;
using Unmanaged;

namespace Windows.Components
{
    public struct IsWindow
    {
        public FixedString title;
        public State state;
        public Flags flags;

        public bool IsBorderless
        {
            readonly get => (flags & Flags.Borderless) != 0;
            set
            {
                if (value)
                {
                    flags |= Flags.Borderless;
                }
                else
                {
                    flags &= ~Flags.Borderless;
                }
            }
        }

        public bool IsResizable
        {
            readonly get => (flags & Flags.Resizable) != 0;
            set
            {
                if (value)
                {
                    flags |= Flags.Resizable;
                }
                else
                {
                    flags &= ~Flags.Resizable;
                }
            }
        }

        public bool IsMinimized
        {
            readonly get => (flags & Flags.Minimized) != 0;
            set
            {
                if (value)
                {
                    flags |= Flags.Minimized;
                }
                else
                {
                    flags &= ~Flags.Minimized;
                }
            }
        }

        public readonly bool IsFocused => (flags & Flags.Focused) != 0;

        public IsWindow()
        {
            throw new NotImplementedException();
        }

        public IsWindow(ReadOnlySpan<char> title)
        {
            this.title = new(title);
        }

        public IsWindow(FixedString title)
        {
            this.title = title;
        }

        public enum State
        {
            Windowed,
            Maximized,
            Fullscreen
        }

        [Flags]
        public enum Flags
        {
            None = 0,
            Borderless = 1,
            Resizable = 2,
            Focused = 4,
            Minimized = 8
        }
    }
}
