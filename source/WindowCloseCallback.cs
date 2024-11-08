using System;

namespace Windows
{
    public readonly unsafe struct WindowCloseCallback : IEquatable<WindowCloseCallback>
    {
        private readonly delegate* unmanaged<Window, void> value;

        public WindowCloseCallback(delegate* unmanaged<Window, void> value)
        {
            this.value = value;
        }

        public readonly override bool Equals(object? obj)
        {
            return obj is WindowCloseCallback callback && Equals(callback);
        }

        public readonly bool Equals(WindowCloseCallback other)
        {
            return (nint)value == (nint)other.value;
        }

        public readonly override int GetHashCode()
        {
            return ((nint)value).GetHashCode();
        }

        public readonly void Invoke(Window window)
        {
            value(window);
        }

        public static bool operator ==(WindowCloseCallback left, WindowCloseCallback right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowCloseCallback left, WindowCloseCallback right)
        {
            return !(left == right);
        }
    }
}
