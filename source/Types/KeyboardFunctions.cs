using System;
using Windows;
using Windows.Components;

public static class KeyboardFunctions
{
    public static KeyboardState GetState<T>(this T inputDevice) where T : IKeyboard
    {
        ref IsKeyboard state = ref inputDevice.GetComponentRef<T, IsKeyboard>();
        return state.state;
    }

    public static KeyboardState GetLastState<T>(this T inputDevice) where T : IKeyboard
    {
        ref LastKeyboardState lastState = ref inputDevice.GetComponentRef<T, LastKeyboardState>();
        return lastState.value;
    }

    public static void SetKeyDown<T>(this T inputDevice, uint control, bool pressed, TimeSpan timestamp) where T : IKeyboard
    {
        ref IsKeyboard state = ref inputDevice.GetComponentRef<T, IsKeyboard>();
        state.state.SetKeyDown(control, pressed);

        inputDevice.SetUpdateTime(timestamp);
    }
}
