using Rendering;
using Simulation;
using System;
using System.Numerics;
using Unmanaged;
using Windows.Components;

namespace Windows
{
    public readonly struct Window : IEntity
    {
        public readonly Destination destination;

        public readonly Vector2 Position
        {
            get
            {
                ref WindowPosition windowPosition = ref destination.entity.GetComponentRef<WindowPosition>();
                return windowPosition.value;
            }
            set
            {
                ref WindowPosition windowPosition = ref destination.entity.GetComponentRef<WindowPosition>();
                windowPosition = new(value);
            }
        }

        public readonly Vector2 Size
        {
            get
            {
                ref WindowSize windowSize = ref destination.entity.GetComponentRef<WindowSize>();
                return windowSize.value;
            }
            set
            {
                ref WindowSize windowSize = ref destination.entity.GetComponentRef<WindowSize>();
                windowSize = new(value);
            }
        }

        public readonly bool IsResizable
        {
            get
            {
                ref IsWindow component = ref destination.entity.GetComponentRef<IsWindow>();
                return component.IsResizable;
            }
            set
            {
                ref IsWindow component = ref destination.entity.GetComponentRef<IsWindow>();
                component.IsResizable = value;
            }
        }

        public readonly bool IsBorderless
        {
            get
            {
                IsWindow component = destination.entity.GetComponentRef<IsWindow>();
                return component.IsBorderless;
            }
            set
            {
                ref IsWindow component = ref destination.entity.GetComponentRef<IsWindow>();
                component.IsBorderless = value;
            }
        }

        public readonly bool IsFullscreen
        {
            get
            {
                IsWindow component = destination.entity.GetComponentRef<IsWindow>();
                return component.state == IsWindow.State.Fullscreen;
            }
        }

        public readonly bool IsMinimized
        {
            get
            {
                IsWindow component = destination.entity.GetComponentRef<IsWindow>();
                return component.IsMinimized;
            }
            set
            {
                ref IsWindow component = ref destination.entity.GetComponentRef<IsWindow>();
                component.IsMinimized = value;
            }
        }

        public readonly bool IsMaximized
        {
            get
            {
                IsWindow component = destination.entity.GetComponentRef<IsWindow>();
                return component.state == IsWindow.State.Maximized;
            }
            set
            {
                ref IsWindow component = ref destination.entity.GetComponentRef<IsWindow>();
                component.state = value ? IsWindow.State.Maximized : IsWindow.State.Windowed;
            }
        }

        public readonly (uint width, uint height, uint refreshRate) Display
        {
            get
            {
                World world = destination.entity.world;
                IsWindow component = destination.entity.GetComponentRef<IsWindow>();
                rint displayReference = component.displayReference;
                uint displayEntity = destination.entity.GetReference(displayReference);
                if (displayEntity == default)
                {
                    return default;
                }
                else
                {
                    IsDisplay displayComponent = world.GetComponent<IsDisplay>(displayEntity);
                    return (displayComponent.width, displayComponent.height, displayComponent.refreshRate);
                }
            }
        }

        readonly World IEntity.World => destination.entity.world;
        readonly uint IEntity.Value => destination.entity.value;
        readonly Definition IEntity.Definition => new([RuntimeType.Get<IsWindow>()], []);

#if NET
        [Obsolete("Default constructor not available", true)]
        public Window()
        {
            throw new InvalidOperationException("Cannot create a window without a world.");
        }
#endif

        public Window(World world, uint existingEntity)
        {
            destination = new(world, existingEntity);
        }

        public Window(World world, FixedString title, Vector2 position, Vector2 size, FixedString renderer, WindowCloseCallback closeCallback)
        {
            destination = new(world, size, renderer);
            destination.entity.AddComponent(new IsWindow(title));
            destination.entity.AddComponent(new WindowPosition(position));
            destination.entity.AddComponent(new WindowSize(size));
            destination.entity.AddComponent(closeCallback);
        }

        public Window(World world, USpan<char> title, Vector2 position, Vector2 size, USpan<char> renderer, WindowCloseCallback closeCallback)
            : this(world, new FixedString(title), position, size, new FixedString(renderer), closeCallback)
        {
        }

        public readonly void BecomeMaximized()
        {
            ref IsWindow component = ref destination.entity.GetComponentRef<IsWindow>();
            component.state = IsWindow.State.Maximized;
        }

        public readonly void BecomeFullscreen()
        {
            ref IsWindow component = ref destination.entity.GetComponentRef<IsWindow>();
            component.state = IsWindow.State.Fullscreen;
        }

        public readonly void BecomeWindowed()
        {
            ref IsWindow component = ref destination.entity.GetComponentRef<IsWindow>();
            component.state = IsWindow.State.Windowed;
        }
    }
}
