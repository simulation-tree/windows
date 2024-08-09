using Simulation;
using System;
using Unmanaged;
using Windows.Components;

namespace Windows
{
    public readonly struct Mouse : IMouse, IDisposable
    {
        private readonly Entity entity;

        World IEntity.World => entity.world;
        eint IEntity.Value => entity.value;

        public Mouse()
        {
            throw new InvalidOperationException("Cannot create a mouse without a world.");
        }

        public Mouse(World world, eint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public Mouse(World world)
        {
            entity = new(world);
            entity.AddComponent(new IsMouse());
            entity.AddComponent(new LastMouseState());
            entity.AddComponent(new LastDeviceUpdateTime());
        }

        public readonly void Dispose()
        {
            entity.Dispose();
        }

        static Query IEntity.GetQuery(World world)
        {
            return new Query(world, RuntimeType.Get<IsMouse>());
        }

        public enum Button : byte
        {
            LeftButton = 1,
            MiddleButton = 2,
            RightButton = 3,
            ForwardButton = 4,
            BackButton = 5
        }
    }
}
