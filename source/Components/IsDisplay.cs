using Worlds;

namespace Windows.Components
{
    [Component]
    public readonly struct IsDisplay
    {
        public readonly uint width;
        public readonly uint height;
        public readonly uint refreshRate;

        public IsDisplay(uint width, uint height, uint refreshRate)
        {
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
        }
    }
}
