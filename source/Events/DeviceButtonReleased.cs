using Simulation;
using Unmanaged;

namespace Windows.Events
{
    public readonly struct DeviceButtonReleased
    {
        public readonly eint device;
        public readonly RuntimeType deviceType;
        public readonly uint control;

        private DeviceButtonReleased(eint device, RuntimeType deviceType, uint control)
        {
            this.device = device;
            this.deviceType = deviceType;
            this.control = control;
        }

        public static DeviceButtonReleased Create<T>(eint device, uint control) where T : unmanaged, IInputDevice
        {
            return new(device, RuntimeType.Get<T>(), control);
        }

        public static DeviceButtonReleased Create<T>(T device, uint control) where T : unmanaged, IInputDevice
        {
            return new(device.Value, RuntimeType.Get<T>(), control);
        }
    }
}
