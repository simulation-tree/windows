using System;
using System.Numerics;

namespace Windows.Components
{
    public struct WindowTransform : IEquatable<WindowTransform>
    {
        public Vector2 position;
        public Vector2 size;
        public Vector2 anchor;

        public WindowTransform(Vector2 position, Vector2 size, Vector2 anchor = default)
        {
            this.position = position;
            this.size = size;
            this.anchor = anchor;
        }

        public readonly override string ToString()
        {
            return $"Position: {position}, Size: {size}";
        }

        public readonly override bool Equals(object? obj)
        {
            return obj is WindowTransform position && Equals(position);
        }

        public readonly bool Equals(WindowTransform other)
        {
            return position.Equals(other.position) && size.Equals(other.size);
        }

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(position, size);
        }

        public static bool operator ==(WindowTransform left, WindowTransform right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowTransform left, WindowTransform right)
        {
            return !(left == right);
        }
    }
}
