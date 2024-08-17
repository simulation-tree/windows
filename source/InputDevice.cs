using Simulation;
using System;
using Unmanaged;
using Windows.Components;

namespace Windows
{
    public readonly struct InputDevice : IEntity, IDisposable
    {
        private readonly Entity entity;

        World IEntity.World => entity.world;
        eint IEntity.Value => entity.value;

        public InputDevice(World world, eint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public InputDevice(World world)
        {
            entity = new(world);
            entity.AddComponent(new LastDeviceUpdateTime());
        }

        public readonly void Dispose()
        {
            entity.Dispose();
        }

        Query IEntity.GetQuery(World world)
        {
            return new Query(world, RuntimeType.Get<LastDeviceUpdateTime>());
        }

        public readonly void SetUpdateTime(TimeSpan timestamp)
        {
            ref LastDeviceUpdateTime state = ref entity.GetComponentRef<LastDeviceUpdateTime>();
            state.value = timestamp;
        }

        public static implicit operator Entity(InputDevice device) => device.entity;
    }
}
