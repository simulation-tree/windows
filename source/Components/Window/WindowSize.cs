using System;
using System.Numerics;

namespace Windows.Components
{
    public struct WindowSize : IEquatable<WindowSize>
    {
        public uint width;
        public uint height;

        public WindowSize(Vector2 value)
        {
            width = (uint)value.X;
            height = (uint)value.Y;
        }

        public WindowSize(uint width, uint height)
        {
            this.width = width;
            this.height = height;
        }

        public readonly Vector2 AsVector2()
        {
            return new(width, height);
        }

        public override bool Equals(object? obj)
        {
            return obj is WindowSize size && Equals(size);
        }

        public bool Equals(WindowSize other)
        {
            return width == other.width &&
                   height == other.height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(width, height);
        }

        public static bool operator ==(WindowSize left, WindowSize right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowSize left, WindowSize right)
        {
            return !(left == right);
        }
    }
}
