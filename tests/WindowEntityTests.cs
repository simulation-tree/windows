using System.Numerics;
using Unmanaged;
using Worlds;

namespace Windows.Tests
{
    public class WindowEntityTests : WindowTests
    {
        [Test]
        public void VerifyCompliance()
        {
            using World world = CreateWorld();
            Vector2 initialPosition = new(100, 500);
            Vector2 initialSize = new(800, 600);
            FixedString renderer = "test renderer";
            FixedString title = "Test Window";
            Window window = new(world, title, initialPosition, initialSize, renderer, default);
            Assert.That(window.IsCompliant, Is.True);
            Assert.That(window.Position, Is.EqualTo(initialPosition));
            Assert.That(window.Size, Is.EqualTo(initialSize));
            Assert.That(window.RendererLabel.ToString(), Is.EqualTo(renderer.ToString()));
            Assert.That(window.Title.ToString(), Is.EqualTo(title.ToString()));

            window.Position.X += 100;

            Assert.That(window.Position, Is.EqualTo(new Vector2(200, 500)));
        }
    }
}