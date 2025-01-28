using Rendering;
using Types;
using Worlds;
using Worlds.Tests;

namespace Windows.Tests
{
    public abstract class WindowTests : WorldTests
    {
        static WindowTests()
        {
            TypeRegistry.Load<WindowsTypeBank>();
            TypeRegistry.Load<RenderingTypeBank>();
        }

        protected override Schema CreateSchema()
        {
            Schema schema = base.CreateSchema();
            schema.Load<WindowsSchemaBank>();
            schema.Load<RenderingSchemaBank>();
            return schema;
        }
    }
}