using Worlds;

namespace Windows.Components
{
    [Component]
    public readonly struct IsDisplay
    {
        public readonly uint width;
        public readonly uint height;
        public readonly float refreshRate;

        public IsDisplay(uint width, uint height, float refreshRate)
        {
            this.width = width;
            this.height = height;
            this.refreshRate = refreshRate;
        }
    }
}
