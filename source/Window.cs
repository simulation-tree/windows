using Rendering;
using Rendering.Components;
using System.Numerics;
using Unmanaged;
using Windows.Components;
using Windows.Functions;
using Worlds;

namespace Windows
{
    public readonly partial struct Window : IEntity
    {
        public readonly ref Vector2 Position => ref GetComponent<WindowTransform>().position;
        public readonly ref Vector2 Size => ref GetComponent<WindowTransform>().size;
        public readonly WindowCloseCallback CloseCallback => GetComponent<IsWindow>().closeCallback;
        public readonly ref FixedString Title => ref GetComponent<IsWindow>().title;
        public readonly IsWindow.State State => GetComponent<IsWindow>().state;
        public readonly ref Vector4 Region => ref As<Destination>().Region;
        public readonly ref Vector4 ClearColor => ref As<Destination>().ClearColor;
        public readonly ref FixedString RendererLabel => ref As<Destination>().RendererLabel;

        public readonly bool IsResizable
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                return component.IsResizable;
            }
            set
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                component.IsResizable = value;
            }
        }

        public readonly bool IsTransparent
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                return component.IsTransparent;
            }
            set
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                component.IsTransparent = value;
            }
        }

        public readonly bool IsBorderless
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                return component.IsBorderless;
            }
            set
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                component.IsBorderless = value;
            }
        }

        public readonly bool AlwaysOnTop
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                return component.AlwaysOnTop;
            }
            set
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                component.AlwaysOnTop = value;
            }
        }

        public readonly bool IsFullscreen
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                return component.state == IsWindow.State.Fullscreen;
            }
        }

        public readonly bool IsMinimized
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                return component.IsMinimized;
            }
            set
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                component.IsMinimized = value;
            }
        }

        public readonly bool IsMaximized
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                return component.state == IsWindow.State.Maximized;
            }
            set
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                component.state = value ? IsWindow.State.Maximized : IsWindow.State.Windowed;
            }
        }

        public readonly Display Display
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                if (TryGetReference(component.displayReference, out uint displayEntity))
                {
                    return new Entity(world, displayEntity).As<Display>();
                }
                else
                {
                    return default;
                }
            }
        }

        public Window(World world, FixedString title, Vector2 position, Vector2 size, FixedString renderer, WindowCloseCallback closeCallback)
        {
            this.world = world;
            value = world.CreateEntity(new IsDestination(size, renderer), new IsWindow(title, closeCallback), new WindowTransform(position, size));
            CreateArray<DestinationExtension>();
        }

        public Window(World world, USpan<char> title, Vector2 position, Vector2 size, USpan<char> renderer, WindowCloseCallback closeCallback)
            : this(world, new FixedString(title), position, size, new FixedString(renderer), closeCallback)
        {
        }

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsWindow>();
        }

        public readonly override string ToString()
        {
            return value.ToString();
        }

        public readonly void BecomeMaximized()
        {
            ref IsWindow component = ref GetComponent<IsWindow>();
            component.state = IsWindow.State.Maximized;
        }

        public readonly void BecomeFullscreen()
        {
            ref IsWindow component = ref GetComponent<IsWindow>();
            component.state = IsWindow.State.Fullscreen;
        }

        public readonly void BecomeWindowed()
        {
            ref IsWindow component = ref GetComponent<IsWindow>();
            component.state = IsWindow.State.Windowed;
        }

        public readonly bool TryGetSurfaceInUse(out Allocation surface)
        {
            return As<Destination>().TryGetSurfaceInUse(out surface);
        }

        public readonly bool TryGetRendererInstanceInUse(out Allocation renderer)
        {
            return As<Destination>().TryGetRendererInstanceInUse(out renderer);
        }

        public static implicit operator Destination(Window window)
        {
            return window.As<Destination>();
        }
    }
}