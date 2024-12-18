using Rendering;
using Rendering.Components;
using System;
using System.Numerics;
using Unmanaged;
using Windows.Components;
using Windows.Functions;
using Worlds;

namespace Windows
{
    public readonly struct Window : IWindow, IEquatable<Window>
    {
        private readonly Destination destination;

        public ref Vector2 Position
        {
            get
            {
                ref WindowTransform transform = ref destination.AsEntity().GetComponent<WindowTransform>();
                return ref transform.position;
            }
        }

        public ref Vector2 Size
        {
            get
            {
                ref WindowTransform transform = ref destination.AsEntity().GetComponent<WindowTransform>();
                return ref transform.size;
            }
        }

        public readonly WindowCloseCallback CloseCallback
        {
            get
            {
                return destination.AsEntity().GetComponent<IsWindow>().closeCallback;
            }
        }

        public readonly bool IsResizable
        {
            get
            {
                IsWindow component = destination.AsEntity().GetComponent<IsWindow>();
                return component.IsResizable;
            }
            set
            {
                ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
                component.IsResizable = value;
            }
        }

        public readonly bool IsTransparent
        {
            get
            {
                IsWindow component = destination.AsEntity().GetComponent<IsWindow>();
                return component.IsTransparent;
            }
            set
            {
                ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
                component.IsTransparent = value;
            }
        }

        public readonly bool IsBorderless
        {
            get
            {
                IsWindow component = destination.AsEntity().GetComponent<IsWindow>();
                return component.IsBorderless;
            }
            set
            {
                ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
                component.IsBorderless = value;
            }
        }

        public readonly bool AlwaysOnTop
        {
            get
            {
                IsWindow component = destination.AsEntity().GetComponent<IsWindow>();
                return component.AlwaysOnTop;
            }
            set
            {
                ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
                component.AlwaysOnTop = value;
            }
        }

        public readonly bool IsFullscreen
        {
            get
            {
                IsWindow component = destination.AsEntity().GetComponent<IsWindow>();
                return component.state == IsWindow.State.Fullscreen;
            }
        }

        public readonly bool IsMinimized
        {
            get
            {
                IsWindow component = destination.AsEntity().GetComponent<IsWindow>();
                return component.IsMinimized;
            }
            set
            {
                ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
                component.IsMinimized = value;
            }
        }

        public readonly bool IsMaximized
        {
            get
            {
                IsWindow component = destination.AsEntity().GetComponent<IsWindow>();
                return component.state == IsWindow.State.Maximized;
            }
            set
            {
                ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
                component.state = value ? IsWindow.State.Maximized : IsWindow.State.Windowed;
            }
        }

        public readonly (uint width, uint height, uint refreshRate) Display
        {
            get
            {
                World world = destination.GetWorld();
                IsWindow component = destination.AsEntity().GetComponent<IsWindow>();
                rint displayReference = component.displayReference;
                uint displayEntity = destination.GetReference(displayReference);
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

        public readonly IsWindow.State State => destination.AsEntity().GetComponent<IsWindow>().state;

        readonly World IEntity.World => destination.GetWorld();
        readonly uint IEntity.Value => destination.GetEntityValue();
        readonly Definition IEntity.Definition => new Definition().AddComponentType<IsWindow>();

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
            Entity destination = new Entity<IsDestination, IsWindow, WindowTransform>(world, new IsDestination(size, renderer), new IsWindow(title, closeCallback), new WindowTransform(position, size)).AsEntity();
            destination.CreateArray<DestinationExtension>();
            this.destination = destination.As<Destination>();
        }

        public Window(World world, USpan<char> title, Vector2 position, Vector2 size, USpan<char> renderer, WindowCloseCallback closeCallback)
            : this(world, new FixedString(title), position, size, new FixedString(renderer), closeCallback)
        {
        }

        public readonly void Dispose()
        {
            destination.Dispose();
        }

        public readonly override string ToString()
        {
            return destination.ToString();
        }

        public readonly void BecomeMaximized()
        {
            ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
            component.state = IsWindow.State.Maximized;
        }

        public readonly void BecomeFullscreen()
        {
            ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
            component.state = IsWindow.State.Fullscreen;
        }

        public readonly void BecomeWindowed()
        {
            ref IsWindow component = ref destination.AsEntity().GetComponent<IsWindow>();
            component.state = IsWindow.State.Windowed;
        }

        public readonly override bool Equals(object? obj)
        {
            return obj is Window window && Equals(window);
        }

        public readonly bool Equals(Window other)
        {
            return destination.Equals(other.destination);
        }

        public readonly override int GetHashCode()
        {
            return destination.GetHashCode();
        }

        public static implicit operator Destination(Window window)
        {
            return window.destination;
        }

        public static implicit operator Entity(Window window)
        {
            return window.AsEntity();
        }

        public static bool operator ==(Window left, Window right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Window left, Window right)
        {
            return !(left == right);
        }
    }
}
