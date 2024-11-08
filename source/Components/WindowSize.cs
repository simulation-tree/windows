using System;
using System.Numerics;

namespace Windows.Components
{
    public struct WindowSize : IEquatable<WindowSize>
    {
        public Vector2 value;

        public WindowSize(Vector2 value)
        {
            this.value = value;
        }

        public readonly override bool Equals(object? obj)
        {
            return obj is WindowSize size && Equals(size);
        }

        public readonly bool Equals(WindowSize other)
        {
            return value.Equals(other.value);
        }

        public readonly override int GetHashCode()
        {
            return value.GetHashCode();
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
