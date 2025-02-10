using System;

namespace Windows
{
    [Flags]
    public enum WindowFlags : byte
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