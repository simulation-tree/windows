using System;
using System.Numerics;

namespace Windows.Components
{
    public struct WindowPosition : IEquatable<WindowPosition>
    {
        public int x;
        public int y;

        public WindowPosition(Vector2 value)
        {
            x = (int)value.X;
            y = (int)value.Y;
        }

        public WindowPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public readonly Vector2 AsVector2()
        {
            return new(x, y);
        }

        public override bool Equals(object? obj)
        {
            return obj is WindowPosition position && Equals(position);
        }

        public bool Equals(WindowPosition other)
        {
            return x == other.x &&
                   y == other.y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
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
