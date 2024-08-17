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
        private readonly Destination entity;

        public readonly bool IsDestroyed => entity.IsDestroyed;

        public readonly Vector2 Position
        {
            get
            {
                ref WindowPosition windowPosition = ref ((Entity)entity).GetComponentRef<WindowPosition>();
                return windowPosition.value;
            }
            set
            {
                ref WindowPosition windowPosition = ref ((Entity)entity).GetComponentRef<WindowPosition>();
                windowPosition = new(value);
            }
        }

        public readonly Vector2 Size
        {
            get
            {
                ref WindowSize windowSize = ref ((Entity)entity).GetComponentRef<WindowSize>();
                return windowSize.value;
            }
            set
            {
                ref WindowSize windowSize = ref ((Entity)entity).GetComponentRef<WindowSize>();
                windowSize = new(value);
            }
        }

        public readonly bool IsResizable
        {
            get
            {
                ref IsWindow component = ref ((Entity)entity).GetComponentRef<IsWindow>();
                return component.IsResizable;
            }
            set
            {
                ref IsWindow component = ref ((Entity)entity).GetComponentRef<IsWindow>();
                component.IsResizable = value;
            }
        }

        public readonly bool IsBorderless
        {
            get
            {
                IsWindow component = ((Entity)entity).GetComponent<IsWindow>();
                return component.IsBorderless;
            }
            set
            {
                ref IsWindow component = ref ((Entity)entity).GetComponentRef<IsWindow>();
                component.IsBorderless = value;
            }
        }

        public readonly bool IsFullscreen
        {
            get
            {
                IsWindow component = ((Entity)entity).GetComponent<IsWindow>();
                return component.state == IsWindow.State.Fullscreen;
            }
        }

        public readonly bool IsMinimized
        {
            get
            {
                IsWindow component = ((Entity)entity).GetComponent<IsWindow>();
                return component.IsMinimized;
            }
            set
            {
                ref IsWindow component = ref ((Entity)entity).GetComponentRef<IsWindow>();
                component.IsMinimized = value;
            }
        }

        public readonly bool IsMaximized
        {
            get
            {
                IsWindow component = ((Entity)entity).GetComponent<IsWindow>();
                return component.state == IsWindow.State.Maximized;
            }
            set
            {
                ref IsWindow component = ref ((Entity)entity).GetComponentRef<IsWindow>();
                component.state = value ? IsWindow.State.Maximized : IsWindow.State.Windowed;
            }
        }

        eint IEntity.Value => ((Entity)entity).value;
        World IEntity.World => ((Entity)entity).world;

#if NET
        [Obsolete("Default constructor not available", true)]
        public Window()
        {
            throw new InvalidOperationException("Cannot create a window without a world.");
        }
#endif

        public Window(World world, eint existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public Window(World world, ReadOnlySpan<char> title, Vector2 position, Vector2 size, FixedString renderer, WindowCloseCallback closeCallback)
        {
            entity = new(world, size, renderer);
            Entity windowEntity = (Entity)entity;
            windowEntity.AddComponent(new IsWindow(title));
            windowEntity.AddComponent(new WindowPosition(position));
            windowEntity.AddComponent(new WindowSize(size));
            windowEntity.AddComponent(closeCallback);

            world.Submit(new WindowUpdate());
            world.Poll();
        }

        public readonly void Dispose()
        {
            entity.Dispose();
        }

        Query IEntity.GetQuery(World world)
        {
            return new Query(world, RuntimeType.Get<IsWindow>());
        }

        public readonly void BecomeMaximized()
        {
            ref IsWindow component = ref ((Entity)entity).GetComponentRef<IsWindow>();
            component.state = IsWindow.State.Maximized;
        }

        public readonly void BecomeFullscreen()
        {
            ref IsWindow component = ref ((Entity)entity).GetComponentRef<IsWindow>();
            component.state = IsWindow.State.Fullscreen;
        }

        public readonly void BecomeWindowed()
        {
            ref IsWindow component = ref ((Entity)entity).GetComponentRef<IsWindow>();
            component.state = IsWindow.State.Windowed;
        }

        public static implicit operator Destination(Window window)
        {
            return window.entity;
        }

        public static implicit operator Entity(Window window)
        {
            return window.entity;
        }
    }
}
