namespace Windows.Components
{
    public readonly struct LastMouseState
    {
        public readonly MouseState value;

        public LastMouseState(MouseState value)
        {
            this.value = value;
        }
    }
}
