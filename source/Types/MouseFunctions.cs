using System;
using System.Numerics;
using Windows;
using Windows.Components;

public static class MouseFunctions
{
    public static Vector2 GetPosition<T>(this T inputDevice) where T : IMouse
    {
        ref IsMouse state = ref inputDevice.GetComponentRef<T, IsMouse>();
        return state.Position;
    }

    public static void SetPosition<T>(this T inputDevice, Vector2 position, TimeSpan timestamp) where T : IMouse
    {
        ref IsMouse state = ref inputDevice.GetComponentRef<T, IsMouse>();
        state.Position = position;

        inputDevice.SetUpdateTime(timestamp);
    }

    public static Vector2 GetScroll<T>(this T inputDevice) where T : IMouse
    {
        ref IsMouse state = ref inputDevice.GetComponentRef<T, IsMouse>();
        return state.Scroll;
    }

    public static void AddScroll<T>(this T inputDevice, Vector2 scroll, TimeSpan timestamp) where T : IMouse
    {
        ref IsMouse state = ref inputDevice.GetComponentRef<T, IsMouse>();
        state.Scroll = scroll;

        inputDevice.SetUpdateTime(timestamp);
    }

    public static bool IsButtonDown<T>(this T inputDevice, uint control) where T : IMouse
    {
        ref IsMouse state = ref inputDevice.GetComponentRef<T, IsMouse>();
        return state.state[control];
    }

    public static void SetButtonDown<T>(this T inputDevice, uint control, bool pressed, TimeSpan timestamp) where T : IMouse
    {
        ref IsMouse state = ref inputDevice.GetComponentRef<T, IsMouse>();
        state.state[control] = pressed;

        inputDevice.SetUpdateTime(timestamp);
    }
}