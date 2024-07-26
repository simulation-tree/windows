using Rendering;
using Simulation;
using System;
using System.Numerics;
using Unmanaged;
using Windows.Components;
using Windows.Events;

namespace Windows
{
    public readonly struct Window : IWindow, IDisposable
    {
        public readonly Entity entity;

        eint IEntity.Value => entity.value;
        World IEntity.World => entity.world;

        public Window()
        {
            throw new InvalidOperationException("Cannot create a window without a world.");
        }

        public Window(World world, eint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public Window(World world, ReadOnlySpan<char> title, Vector2 position, Vector2 size, FixedString renderer, WindowCloseCallback closeCallback)
        {
            Destination destination = new(world, size, renderer);
            entity = destination.entity;
            entity.AddComponent(new IsWindow(title));
            entity.AddComponent(new WindowPosition(position));
            entity.AddComponent(new WindowSize(size));
            entity.AddComponent(closeCallback);

            world.Submit(new WindowUpdate());
            world.Poll();
        }

        public readonly void Dispose()
        {
            entity.Dispose();
        }

        static Query IEntity.GetQuery(World world)
        {
            return new Query(world, RuntimeType.Get<IsWindow>());
        }
    }
}
