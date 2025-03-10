using System;
using System.Numerics;
using Unmanaged;
using Windows.Functions;
using Worlds;

namespace Windows.Components
{
    public struct IsWindow
    {
        public ASCIIText256 title;
        public rint displayReference;
        public WindowState windowState;
        public WindowFlags windowFlags;
        public WindowCloseCallback closeCallback;
        public CursorState cursorState;
        public Vector4 cursorArea;
        public uint id;

        public bool IsBorderless
        {
            readonly get => (windowFlags & WindowFlags.Borderless) != 0;
            set
            {
                if (value)
                {
                    windowFlags |= WindowFlags.Borderless;
                }
                else
                {
                    windowFlags &= ~WindowFlags.Borderless;
                }
            }
        }

        public bool IsResizable
        {
            readonly get => (windowFlags & WindowFlags.Resizable) != 0;
            set
            {
                if (value)
                {
                    windowFlags |= WindowFlags.Resizable;
                }
                else
                {
                    windowFlags &= ~WindowFlags.Resizable;
                }
            }
        }

        public bool IsMinimized
        {
            readonly get => (windowFlags & WindowFlags.Minimized) != 0;
            set
            {
                if (value)
                {
                    windowFlags |= WindowFlags.Minimized;
                }
                else
                {
                    windowFlags &= ~WindowFlags.Minimized;
                }
            }
        }

        public bool AlwaysOnTop
        {
            readonly get => (windowFlags & WindowFlags.AlwaysOnTop) != 0;
            set
            {
                if (value)
                {
                    windowFlags |= WindowFlags.AlwaysOnTop;
                }
                else
                {
                    windowFlags &= ~WindowFlags.AlwaysOnTop;
                }
            }
        }

        public bool IsTransparent
        {
            readonly get => (windowFlags & WindowFlags.Transparent) != 0;
            set
            {
                if (value)
                {
                    windowFlags |= WindowFlags.Transparent;
                }
                else
                {
                    windowFlags &= ~WindowFlags.Transparent;
                }
            }
        }

        public readonly bool IsFocused => (windowFlags & WindowFlags.Focused) != 0;

#if NET
        [Obsolete("Default constructor not available", true)]
        public IsWindow()
        {
            throw new NotImplementedException();
        }
#endif

        public IsWindow(ReadOnlySpan<char> title, WindowCloseCallback closeCallback)
        {
            this.title = new(title);
            this.closeCallback = closeCallback;
        }

        public IsWindow(ASCIIText256 title, WindowCloseCallback closeCallback)
        {
            this.title = title;
            this.closeCallback = closeCallback;
        }
    }
}