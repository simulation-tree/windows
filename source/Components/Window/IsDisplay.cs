namespace Windows.Components
{
    public struct IsDisplay
    {
        public uint width;
        public uint height;
        public uint refreshRate;

        public IsDisplay(uint width, uint height, uint refreshRate)
        {
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
        }
    }
}
