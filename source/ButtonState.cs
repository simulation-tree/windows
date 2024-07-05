namespace Windows
{
    public readonly struct ButtonState
    {
        private readonly State state;

        public readonly bool WasPressed => state == State.WasPressed;
        public readonly bool Held => state == State.Held;
        public readonly bool WasReleased => state == State.WasReleased;
        public readonly bool IsPressed => state == State.Held || state == State.WasPressed;

        public ButtonState(State state)
        {
            this.state = state;
        }

        public ButtonState(bool previousState, bool currentState)
        {
            if (previousState && currentState)
            {
                state = State.Held;
            }
            else if (!previousState && currentState)
            {
                state = State.WasPressed;
            }
            else if (previousState && !currentState)
            {
                state = State.WasReleased;
            }
            else
            {
                state = State.Released;
            }
        }

        public enum State : byte
        {
            Released,
            WasPressed,
            Held,
            WasReleased
        }
    }
}
