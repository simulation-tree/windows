using Simulation;
using System;
using System.Numerics;
using Unmanaged;
using Windows.Components;
using Windows.Events;

namespace Windows
{
    public readonly struct Window : IDisposable
    {
        public readonly Entity entity;

        public readonly bool IsDestroyed => entity.IsDestroyed;

        public readonly FixedString Title
        {
            get => entity.GetComponent<IsWindow>().title;
            set => entity.GetComponentRef<IsWindow>().title = value;
        }

        public readonly int X
        {
            get => Position.x;
            set
            {
                ref WindowPosition position = ref entity.GetComponentRef<WindowPosition>();
                position.x = value;
            }
        }

        public readonly int Y
        {
            get => Position.y;
            set
            {
                ref WindowPosition position = ref entity.GetComponentRef<WindowPosition>();
                position.y = value;
            }
        }

        public readonly uint Width
        {
            get => Size.width;
            set
            {
                ref WindowSize scale = ref entity.GetComponentRef<WindowSize>();
                scale.width = value;
            }
        }

        public readonly uint Height
        {
            get => Size.height;
            set
            {
                ref WindowSize scale = ref entity.GetComponentRef<WindowSize>();
                scale.height = value;
            }
        }

        public readonly WindowPosition Position
        {
            get => entity.GetComponent<WindowPosition>();
            set
            {
                ref WindowPosition position = ref entity.GetComponentRef<WindowPosition>();
                position = value;
            }
        }

        public readonly WindowSize Size
        {
            get => entity.GetComponent<WindowSize>();
            set
            {
                ref WindowSize size = ref entity.GetComponentRef<WindowSize>();
                size = value;
            }
        }

        public readonly bool IsBorderless
        {
            get => entity.GetComponent<IsWindow>().IsBorderless;
            set => entity.GetComponentRef<IsWindow>().IsBorderless = value;
        }

        public readonly bool IsResizable
        {
            get => entity.GetComponent<IsWindow>().IsResizable;
            set => entity.GetComponentRef<IsWindow>().IsResizable = value;
        }

        public readonly bool IsHidden
        {
            get => entity.GetComponent<IsWindow>().IsHidden;
            set => entity.GetComponentRef<IsWindow>().IsHidden = value;
        }

        public readonly IsWindow.State State
        {
            get => entity.GetComponent<IsWindow>().state;
            set => entity.GetComponentRef<IsWindow>().state = value;
        }

        public readonly bool IsMaximized => State == IsWindow.State.Maximized;
        public readonly bool IsFullscreen => State == IsWindow.State.Fullscreen;

        public Window()
        {
            throw new InvalidOperationException("Cannot create a window without a world.");
        }

        public Window(World world, EntityID existingEntity)
        {
            entity = new(world, existingEntity);
        }

        public Window(World world, ReadOnlySpan<char> title, Vector2 position, Vector2 size, WindowCloseCallback closeCallback)
        {
            entity = new(world);
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

        public readonly void BecomeFullscreen()
        {
            ref IsWindow window = ref entity.GetComponentRef<IsWindow>();
            window.state = IsWindow.State.Fullscreen;
        }

        public readonly void BecomeMaximized()
        {
            if (!IsResizable)
            {
                throw new InvalidOperationException("Cannot maximize a non-resizable window.");
            }

            ref IsWindow window = ref entity.GetComponentRef<IsWindow>();
            window.state = IsWindow.State.Maximized;
        }

        public readonly void BecomeWindowed()
        {
            ref IsWindow window = ref entity.GetComponentRef<IsWindow>();
            window.state = IsWindow.State.Windowed;
        }
    }
}
