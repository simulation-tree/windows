using System;
using System.Numerics;

namespace Windows.Components
{
    public struct WindowPosition : IEquatable<WindowPosition>
    {
        public Vector2 value;

        public WindowPosition(Vector2 value)
        {
            this.value = value;
        }

        public readonly override bool Equals(object? obj)
        {
            return obj is WindowPosition position && Equals(position);
        }

        public readonly bool Equals(WindowPosition other)
        {
            return value.Equals(other.value);
        }

        public readonly override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(WindowPosition left, WindowPosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowPosition left, WindowPosition right)
        {
            return !(left == right);
        }
    }
}
