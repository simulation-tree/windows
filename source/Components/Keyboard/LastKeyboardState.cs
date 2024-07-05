namespace Windows.Components
{
    public readonly struct LastKeyboardState
    {
        public readonly KeyboardState value;

        public LastKeyboardState(KeyboardState value)
        {
            this.value = value;
        }
    }
}
