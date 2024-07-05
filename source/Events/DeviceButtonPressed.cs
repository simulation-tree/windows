using Simulation;
using Unmanaged;

namespace Windows.Events
{
    public readonly struct DeviceButtonPressed
    {
        public readonly EntityID device;
        public readonly RuntimeType deviceType;
        public readonly uint control;

        private DeviceButtonPressed(EntityID device, RuntimeType deviceType, uint control)
        {
            this.device = device;
            this.deviceType = deviceType;
            this.control = control;
        }

        public static DeviceButtonPressed Create<T>(EntityID device, uint control) where T : unmanaged, IInputDevice
        {
            return new(device, RuntimeType.Get<T>(), control);
        }

        public static DeviceButtonPressed Create<T>(T device, uint control) where T : unmanaged, IInputDevice
        {
            return new(device.Value, RuntimeType.Get<T>(), control);
        }
    }
}
