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
        public readonly ref ASCIIText256 Title => ref GetComponent<IsWindow>().title;
        public readonly WindowState State => GetComponent<IsWindow>().windowState;
        public readonly ref Vector4 Region => ref As<Destination>().Region;
        public readonly ref Vector4 ClearColor => ref As<Destination>().ClearColor;
        public readonly ref ASCIIText256 RendererLabel => ref As<Destination>().RendererLabel;

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
                return component.windowState == WindowState.Fullscreen;
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
                return component.windowState == WindowState.Maximized;
            }
            set
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                component.windowState = value ? WindowState.Maximized : WindowState.Windowed;
            }
        }

        public readonly ref CursorState CursorState
        {
            get
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                return ref component.cursorState;
            }
        }

        /// <summary>
        /// The area where the cursor is allowed to move.
        /// <para>
        /// If set to <see cref="Vector4.Zero"/>, the cursor will be allowed to move anywhere.
        /// </para>
        /// </summary>
        public readonly Vector4 CursorArea
        {
            get
            {
                IsWindow component = GetComponent<IsWindow>();
                return component.cursorArea;
            }
            set
            {
                ref IsWindow component = ref GetComponent<IsWindow>();
                component.cursorArea = value;
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

        public Window(World world, ASCIIText256 title, Vector2 position, Vector2 size, ASCIIText256 renderer, WindowCloseCallback closeCallback)
        {
            this.world = world;
            value = world.CreateEntity(new IsDestination(size, renderer), new IsWindow(title, closeCallback), new WindowTransform(position, size));
            CreateArray<DestinationExtension>();
        }

        public Window(World world, USpan<char> title, Vector2 position, Vector2 size, USpan<char> renderer, WindowCloseCallback closeCallback)
            : this(world, new ASCIIText256(title), position, size, new ASCIIText256(renderer), closeCallback)
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
            component.windowState = WindowState.Maximized;
        }

        public readonly void BecomeFullscreen()
        {
            ref IsWindow component = ref GetComponent<IsWindow>();
            component.windowState = WindowState.Fullscreen;
        }

        public readonly void BecomeWindowed()
        {
            ref IsWindow component = ref GetComponent<IsWindow>();
            component.windowState = WindowState.Windowed;
        }

        public readonly bool TryGetSurfaceInUse(out MemoryAddress surface)
        {
            return As<Destination>().TryGetSurfaceInUse(out surface);
        }

        public readonly bool TryGetRendererInstanceInUse(out MemoryAddress renderer)
        {
            return As<Destination>().TryGetRendererInstanceInUse(out renderer);
        }

        public static implicit operator Destination(Window window)
        {
            return window.As<Destination>();
        }
    }
}