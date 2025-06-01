namespace Windows.Components
{
    public struct IsDisplay
    {
        public uint width;
        public uint height;
        public float refreshRate;

        public IsDisplay(uint width, uint height, float refreshRate)
        {
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
        }
    }
}
