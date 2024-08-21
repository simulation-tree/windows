using Rendering;
using Simulation;
using System;
using System.Numerics;
using Unmanaged;
using Windows.Components;

namespace Windows
{
    public readonly struct Window : IEntity, IDisposable
    {
        private readonly Destination destination;

        public readonly bool IsDestroyed => destination.IsDestroyed;

        public readonly Vector2 Position
        {
            get
            {
                ref WindowPosition windowPosition = ref ((Entity)destination).GetComponent<WindowPosition>();
                return windowPosition.value;
            }
            set
            {
                ref WindowPosition windowPosition = ref ((Entity)destination).GetComponent<WindowPosition>();
                windowPosition = new(value);
            }
        }

        public readonly Vector2 Size
        {
            get
            {
                ref WindowSize windowSize = ref ((Entity)destination).GetComponent<WindowSize>();
                return windowSize.value;
            }
            set
            {
                ref WindowSize windowSize = ref ((Entity)destination).GetComponent<WindowSize>();
                windowSize = new(value);
            }
        }

        public readonly bool IsResizable
        {
            get
            {
                ref IsWindow component = ref ((Entity)destination).GetComponent<IsWindow>();
                return component.IsResizable;
            }
            set
            {
                ref IsWindow component = ref ((Entity)destination).GetComponent<IsWindow>();
                component.IsResizable = value;
            }
        }

        public readonly bool IsBorderless
        {
            get
            {
                IsWindow component = ((Entity)destination).GetComponent<IsWindow>();
                return component.IsBorderless;
            }
            set
            {
                ref IsWindow component = ref ((Entity)destination).GetComponent<IsWindow>();
                component.IsBorderless = value;
            }
        }

        public readonly bool IsFullscreen
        {
            get
            {
                IsWindow component = ((Entity)destination).GetComponent<IsWindow>();
                return component.state == IsWindow.State.Fullscreen;
            }
        }

        public readonly bool IsMinimized
        {
            get
            {
                IsWindow component = ((Entity)destination).GetComponent<IsWindow>();
                return component.IsMinimized;
            }
            set
            {
                ref IsWindow component = ref ((Entity)destination).GetComponent<IsWindow>();
                component.IsMinimized = value;
            }
        }

        public readonly bool IsMaximized
        {
            get
            {
                IsWindow component = ((Entity)destination).GetComponent<IsWindow>();
                return component.state == IsWindow.State.Maximized;
            }
            set
            {
                ref IsWindow component = ref ((Entity)destination).GetComponent<IsWindow>();
                component.state = value ? IsWindow.State.Maximized : IsWindow.State.Windowed;
            }
        }

        World IEntity.World => (Entity)destination;
        eint IEntity.Value => (Entity)destination;

#if NET
        [Obsolete("Default constructor not available", true)]
        public Window()
        {
            throw new InvalidOperationException("Cannot create a window without a world.");
        }
#endif

        public Window(World world, eint existingEntity)
        {
            destination = new(world, existingEntity);
        }

        public Window(World world, ReadOnlySpan<char> title, Vector2 position, Vector2 size, FixedString renderer, WindowCloseCallback closeCallback)
        {
            destination = new(world, size, renderer);
            Entity entity = destination;
            entity.AddComponent(new IsWindow(title));
            entity.AddComponent(new WindowPosition(position));
            entity.AddComponent(new WindowSize(size));
            entity.AddComponent(closeCallback);
        }

        public readonly void Dispose()
        {
            destination.Dispose();
        }

        Query IEntity.GetQuery(World world)
        {
            return new Query(world, RuntimeType.Get<IsWindow>());
        }

        public readonly void BecomeMaximized()
        {
            ref IsWindow component = ref ((Entity)destination).GetComponent<IsWindow>();
            component.state = IsWindow.State.Maximized;
        }

        public readonly void BecomeFullscreen()
        {
            ref IsWindow component = ref ((Entity)destination).GetComponent<IsWindow>();
            component.state = IsWindow.State.Fullscreen;
        }

        public readonly void BecomeWindowed()
        {
            ref IsWindow component = ref ((Entity)destination).GetComponent<IsWindow>();
            component.state = IsWindow.State.Windowed;
        }

        public static implicit operator Destination(Window window)
        {
            return window.destination;
        }

        public static implicit operator Entity(Window window)
        {
            return window.destination;
        }
    }
}
