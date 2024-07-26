using Simulation;
using System;
using System.Diagnostics;

namespace Windows.Components
{
    public unsafe struct WindowCloseCallback
    {
        private delegate* unmanaged<World, eint, void> value;

        public WindowCloseCallback(delegate* unmanaged<World, eint, void> value)
        {
            this.value = value;
        }

        [Conditional("DEBUG")]
        private readonly void ThrowIfDisposed()
        {
            if (value is null)
            {
                throw new ObjectDisposedException(nameof(WindowCloseCallback));
            }
        }

        public readonly void Invoke(World world, eint entity)
        {
            ThrowIfDisposed();
            value(world, entity);
        }
    }
}
