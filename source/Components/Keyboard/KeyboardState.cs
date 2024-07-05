using System;
using System.Diagnostics;

namespace Windows.Components
{
    public unsafe struct KeyboardState
    {
        public const uint MaxKeyCount = 320;

        private fixed ulong keys[5];

        [Conditional("DEBUG")]
        private readonly void ThrowIfOutOfRange(uint index)
        {
            if (index >= MaxKeyCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                for (uint i = 0; i < 5; i++)
                {
                    hash = hash * 23 + keys[i].GetHashCode();
                }

                return hash;
            }
        }

        public readonly bool IsKeyDown(uint index)
        {
            ThrowIfOutOfRange(index);
            uint arrayIndex = index / 64;
            uint bitIndex = index % 64;
            ulong mask = 1UL << (int)bitIndex;
            return (keys[arrayIndex] & mask) != 0;
        }

        public void SetKeyDown(uint index, bool value)
        {
            ThrowIfOutOfRange(index);
            uint arrayIndex = index / 64;
            uint bitIndex = index % 64;
            ulong mask = 1UL << (int)bitIndex;
            if (value)
            {
                keys[arrayIndex] |= mask;
            }
            else
            {
                keys[arrayIndex] &= ~mask;
            }
        }
    }
}
