using Simulation;
using System;
using System.Numerics;
using Windows.Components;

namespace Windows
{
    public readonly struct Mouse : IDisposable, IInputDevice
    {
        public readonly Entity entity;

        public readonly bool IsDestroyed => entity.IsDestroyed;
        public readonly Vector2 Position => entity.GetComponent<IsMouse>().Position;

        public readonly Vector2 PositionDelta
        {
            get
            {
                LastMouseState lastState = entity.GetComponent<LastMouseState>();
                Vector2 position = Position;
                Vector2 lastPosition = lastState.value.position;
                return position - lastPosition;
            }
        }

        public readonly Vector2 Scroll => entity.GetComponent<IsMouse>().Scroll;

        public readonly Vector2 ScrollDelta
        {
            get
            {
                LastMouseState lastState = entity.GetComponent<LastMouseState>();
                Vector2 scroll = Scroll;
                Vector2 lastScroll = lastState.value.scroll;
                return scroll - lastScroll;
            }
        }

        public readonly ButtonState LeftButton => this[Button.LeftButton];
        public readonly ButtonState MiddleButton => this[Button.MiddleButton];
        public readonly ButtonState RightButton => this[Button.RightButton];
        public readonly ButtonState ForwardButton => this[Button.ForwardButton];
        public readonly ButtonState BackButton => this[Button.BackButton];

        public readonly ButtonState this[uint index]
        {
            get
            {
                LastMouseState lastState = entity.GetComponent<LastMouseState>();
                IsMouse mouse = entity.GetComponent<IsMouse>();
                return new(lastState.value[(Button)index], mouse.state[(Button)index]);
            }
        }

        public readonly ButtonState this[Button control] => this[(uint)control];

        World IEntity.World => entity.world;
        EntityID IEntity.Value => entity;

        public Mouse()
        {
            throw new InvalidOperationException("Cannot create a mouse without a world.");
        }

        public Mouse(World world, EntityID existingEntity)
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

        public readonly void SetPosition(Vector2 position, TimeSpan timeStamp)
        {
            entity.SetComponent(new LastDeviceUpdateTime(timeStamp));
            ref IsMouse mouse = ref entity.GetComponentRef<IsMouse>();
            mouse.Position = position;
        }

        public readonly void AddScroll(Vector2 scroll, TimeSpan timeStamp)
        {
            entity.SetComponent(new LastDeviceUpdateTime(timeStamp));
            ref IsMouse mouse = ref entity.GetComponentRef<IsMouse>();
            mouse.Scroll = scroll;
        }

        public readonly void SetButtonDown(Button button, bool state, TimeSpan timeStamp)
        {
            entity.SetComponent(new LastDeviceUpdateTime(timeStamp));
            ref IsMouse mouse = ref entity.GetComponentRef<IsMouse>();
            mouse.state[button] = state;
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
