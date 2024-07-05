using System;

namespace Windows.Components
{
    public readonly struct LastDeviceUpdateTime
    {
        public readonly TimeSpan value;

        public LastDeviceUpdateTime(TimeSpan value)
        {
            this.value = value;
        }
    }
}
