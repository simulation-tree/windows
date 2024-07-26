using System;
using Windows;
using Windows.Components;

public static class InputDeviceFunctions
{
    public static void SetUpdateTime<T>(this T inputDevice, TimeSpan timestamp) where T : IInputDevice
    {
        ref LastDeviceUpdateTime lastUpdateTime = ref inputDevice.GetComponentRef<T, LastDeviceUpdateTime>();
        lastUpdateTime = new(timestamp);
    }

    public static ButtonState GetButtonState<T>(this T inputDevice, uint control) where T : IInputDevice
    {
        if (typeof(T) == typeof(IKeyboard))
        {
            KeyboardState state = inputDevice.GetComponent<T, KeyboardState>();
            KeyboardState lastState = inputDevice.GetComponent<T, LastKeyboardState>().value;
            return new ButtonState(state[control], lastState[control]);
        }
        else if (typeof(T) == typeof(IMouse))
        {
            MouseState state = inputDevice.GetComponent<T, MouseState>();
            MouseState lastState = inputDevice.GetComponent<T, LastMouseState>().value;
            return new ButtonState(state[control], lastState[control]);
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public static bool IsPressed<T>(this T inputDevice, uint control) where T : IInputDevice
    {
        if (typeof(T) == typeof(IKeyboard))
        {
            KeyboardState state = inputDevice.GetComponent<T, KeyboardState>();
            return state[control];
        }
        else if (typeof(T) == typeof(IMouse))
        {
            MouseState state = inputDevice.GetComponent<T, MouseState>();
            return state[control];
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    public static bool WasPressed<T>(this T inputDevice, uint control) where T : IInputDevice
    {
        ButtonState buttonState = inputDevice.GetButtonState(control);
        return buttonState.WasPressed;
    }

    public static bool WasReleased<T>(this T inputDevice, uint control) where T : IInputDevice
    {
        ButtonState buttonState = inputDevice.GetButtonState(control);
        return buttonState.WasReleased;
    }
}
