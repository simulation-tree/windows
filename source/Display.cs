using Windows.Components;
using Worlds;

namespace Windows
{
    public readonly partial struct Display : IEntity
    {
        public readonly uint Width => GetComponent<IsDisplay>().width;
        public readonly uint Height => GetComponent<IsDisplay>().height;
        public readonly float RefreshRate => GetComponent<IsDisplay>().refreshRate;

        public readonly (uint width, uint height) Size
        {
            get
            {
                IsDisplay component = GetComponent<IsDisplay>();
                return (component.width, component.height);
            }
        }

        public Display(World world, uint width, uint height, float refreshRate)
        {
            this.world = world;
            value = world.CreateEntity(new IsDisplay(width, height, refreshRate));
        }

        readonly void IEntity.Describe(ref Archetype archetype)
        {
            archetype.AddComponentType<IsDisplay>();
        }
    }
}