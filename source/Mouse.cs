using Simulation;
using System;
using System.Numerics;
using Unmanaged;
using Windows.Components;

namespace Windows
{
    public readonly struct Mouse : IMouse, IDisposable
    {
        private readonly InputDevice device;

        public readonly Vector2 Position
        {
            get
            {
                ref IsMouse state = ref ((Entity)device).GetComponent<IsMouse>();
                return state.Position;
            }
        }

        public readonly Vector2 Scroll
        {
            get
            {
                ref IsMouse state = ref ((Entity)device).GetComponent<IsMouse>();
                return state.Scroll;
            }
        }

        World IEntity.World => (Entity)device;
        eint IEntity.Value => (Entity)device;

        public Mouse()
        {
            throw new InvalidOperationException("Cannot create a mouse without a world.");
        }

        public Mouse(World world, eint existingEntity)
        {
            device = new(world, existingEntity);
        }

        public Mouse(World world)
        {
            device = new(world);
            Entity entity = device;
            entity.AddComponent(new IsMouse());
            entity.AddComponent(new LastMouseState());
        }

        public readonly void Dispose()
        {
            device.Dispose();
        }

        Query IEntity.GetQuery(World world)
        {
            return new Query(world, RuntimeType.Get<IsMouse>());
        }

        public readonly void SetPosition(Vector2 position, TimeSpan timestamp)
        {
            ref IsMouse state = ref ((Entity)device).GetComponent<IsMouse>();
            state.Position = position;

            device.SetUpdateTime(timestamp);
        }

        public readonly void AddScroll(Vector2 scroll, TimeSpan timestamp)
        {
            ref IsMouse state = ref ((Entity)device).GetComponent<IsMouse>();
            state.Scroll = scroll;

            device.SetUpdateTime(timestamp);
        }

        public readonly bool IsButtonDown(uint control)
        {
            ref IsMouse state = ref ((Entity)device).GetComponent<IsMouse>();
            return state.state[control];
        }

        public readonly void SetButtonDown(uint control, bool pressed, TimeSpan timestamp)
        {
            ref IsMouse state = ref ((Entity)device).GetComponent<IsMouse>();
            state.state[control] = pressed;

            device.SetUpdateTime(timestamp);
        }

        readonly unsafe ButtonState IInputDevice.GetButtonState<C>(C control)
        {
            uint controlIndex = *(uint*)&control;
            MouseState state = ((Entity)device).GetComponent<IsMouse>().state;
            MouseState lastState = ((Entity)device).GetComponent<LastMouseState>().value;
            return new ButtonState(state[controlIndex], lastState[controlIndex]);
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
