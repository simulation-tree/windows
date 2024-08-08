using System.Numerics;
using Windows;
using Windows.Components;

public static class WindowFunctions
{
    public static Vector2 GetPosition<T>(this T window) where T : IWindow
    {
        ref WindowPosition windowPosition = ref window.GetComponentRef<T, WindowPosition>();
        return windowPosition.value;
    }

    public static Vector2 GetSize<T>(this T window) where T : IWindow
    {
        ref WindowSize windowSize = ref window.GetComponentRef<T, WindowSize>();
        return windowSize.value;
    }

    public static void SetPosition<T>(this T window, Vector2 position) where T : IWindow
    {
        ref WindowPosition windowPosition = ref window.GetComponentRef<T, WindowPosition>();
        windowPosition = new(position);
    }

    public static void SetSize<T>(this T window, Vector2 size) where T : IWindow
    {
        ref WindowSize windowSize = ref window.GetComponentRef<T, WindowSize>();
        windowSize = new(size);
    }

    public static bool IsResizable<T>(this T window) where T : IWindow
    {
        IsWindow component = window.GetComponent<T, IsWindow>();
        return component.IsResizable;
    }

    public static void SetResizable<T>(this T window, bool resizable) where T : IWindow
    {
        ref IsWindow component = ref window.GetComponentRef<T, IsWindow>();
        component.IsResizable = resizable;
    }

    public static bool IsBorderless<T>(this T window) where T : IWindow
    {
        IsWindow component = window.GetComponent<T, IsWindow>();
        return component.IsBorderless;
    }

    public static void SetBorderless<T>(this T window, bool borderless) where T : IWindow
    {
        ref IsWindow component = ref window.GetComponentRef<T, IsWindow>();
        component.IsBorderless = borderless;
    }

    public static bool IsFullscreen<T>(this T window) where T : IWindow
    {
        IsWindow component = window.GetComponent<T, IsWindow>();
        return component.state == IsWindow.State.Fullscreen;
    }

    public static bool IsMinimized<T>(this T window) where T : IWindow
    {
        IsWindow component = window.GetComponent<T, IsWindow>();
        return component.IsMinimized;
    }

    public static void SetMinimized<T>(this T window, bool minimized) where T : IWindow
    {
        ref IsWindow component = ref window.GetComponentRef<T, IsWindow>();
        component.IsMinimized = minimized;
    }

    public static bool IsMaximized<T>(this T window) where T : IWindow
    {
        IsWindow component = window.GetComponent<T, IsWindow>();
        return component.state == IsWindow.State.Maximized;
    }

    public static void BecomeWindowed<T>(this T window) where T : IWindow
    {
        ref IsWindow component = ref window.GetComponentRef<T, IsWindow>();
        component.state = IsWindow.State.Windowed;
    }

    public static void BecomeMaximized<T>(this T window) where T : IWindow
    {
        ref IsWindow component = ref window.GetComponentRef<T, IsWindow>();
        component.state = IsWindow.State.Maximized;
    }

    public static void BecomeFullscreen<T>(this T window) where T : IWindow
    {
        ref IsWindow component = ref window.GetComponentRef<T, IsWindow>();
        component.state = IsWindow.State.Fullscreen;
    }
}