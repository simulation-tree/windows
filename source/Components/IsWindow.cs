using System;
using Unmanaged;
using Worlds;

namespace Windows.Components
{
    [Component]
    public struct IsWindow
    {
        public FixedString title;
        public rint displayReference;
        public State state;
        public Flags flags;
        public WindowCloseCallback closeCallback;
        public uint id;

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

        public bool AlwaysOnTop
        {
            readonly get => (flags & Flags.AlwaysOnTop) != 0;
            set
            {
                if (value)
                {
                    flags |= Flags.AlwaysOnTop;
                }
                else
                {
                    flags &= ~Flags.AlwaysOnTop;
                }
            }
        }

        public bool IsTransparent
        {
            readonly get => (flags & Flags.Transparent) != 0;
            set
            {
                if (value)
                {
                    flags |= Flags.Transparent;
                }
                else
                {
                    flags &= ~Flags.Transparent;
                }
            }
        }

        public readonly bool IsFocused => (flags & Flags.Focused) != 0;

#if NET
        [Obsolete("Default constructor not available", true)]
        public IsWindow()
        {
            throw new NotImplementedException();
        }
#endif

        public IsWindow(USpan<char> title, WindowCloseCallback closeCallback)
        {
            this.title = new(title);
            this.closeCallback = closeCallback;
        }

        public IsWindow(FixedString title, WindowCloseCallback closeCallback)
        {
            this.title = title;
            this.closeCallback = closeCallback;
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
            Minimized = 8,
            AlwaysOnTop = 16,
            Transparent = 32
        }
    }
}
