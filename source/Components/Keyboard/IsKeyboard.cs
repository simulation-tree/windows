namespace Windows.Components
{
    public struct IsKeyboard
    {
        public KeyboardState state;

        public IsKeyboard(KeyboardState state)
        {
            this.state = state;
        }
    }
}
