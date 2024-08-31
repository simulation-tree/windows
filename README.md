# Windows
Abstraction library for windowing on standalone platforms.

### Example
```cs
using World world = new World();
Window mainWindow = new(world, "The Window", new(100, 100), new(800, 600), "vulkan", new(&WindowCloseRequested));
mainWindow.IsFullscreen = true;
(uint width, uint height, uint refreshRate) display = mainWindow.Display;

[UnmanagedCallersOnly]
static void WindowCloseRequested(World world, uint windowEntity)
{
    world.DestroyEntity(windowEntity);
}
```