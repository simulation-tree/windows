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

    public unsafe static ButtonState GetButtonState<T, C>(this T inputDevice, C control) where T : IInputDevice where C : unmanaged
    {
        uint controlIndex = *(uint*)&control;
        Type type = typeof(T);

        //todo: ugly switch that theoretically doesnt need to exist, due to different components per device type
        if (typeof(IKeyboard).IsAssignableFrom(type))
        {
            KeyboardState state = inputDevice.GetComponent<T, IsKeyboard>().state;
            KeyboardState lastState = inputDevice.GetComponent<T, LastKeyboardState>().value;
            return new ButtonState(state[controlIndex], lastState[controlIndex]);
        }
        else if (typeof(IMouse).IsAssignableFrom(type))
        {
            MouseState state = inputDevice.GetComponent<T, IsMouse>().state;
            MouseState lastState = inputDevice.GetComponent<T, LastMouseState>().value;
            return new ButtonState(state[controlIndex], lastState[controlIndex]);
        }
        else
        {
            throw new NotSupportedException($"Input device type {type} is not supported");
        }
    }

    public unsafe static bool IsPressed<T, C>(this T inputDevice, C control) where T : IInputDevice where C : unmanaged
    {
        uint controlIndex = *(uint*)&control;
        Type type = typeof(T);
        if (typeof(IKeyboard).IsAssignableFrom(type))
        {
            KeyboardState state = inputDevice.GetComponent<T, IsKeyboard>().state;
            return state[controlIndex];
        }
        else if (typeof(IMouse).IsAssignableFrom(type))
        {
            MouseState state = inputDevice.GetComponent<T, IsMouse>().state;
            return state[controlIndex];
        }
        else
        {
            throw new NotSupportedException($"Input device type {type} is not supported");
        }
    }

    public static bool WasPressed<T, C>(this T inputDevice, C control) where T : IInputDevice where C : unmanaged
    {
        ButtonState buttonState = inputDevice.GetButtonState(control);
        return buttonState.WasPressed;
    }

    public static bool WasReleased<T, C>(this T inputDevice, C control) where T : IInputDevice where C : unmanaged
    {
        ButtonState buttonState = inputDevice.GetButtonState(control);
        return buttonState.WasReleased;
    }
}
