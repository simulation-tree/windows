using System;

namespace Windows.Components
{
    public struct LastDeviceUpdateTime
    {
        public TimeSpan value;

        public LastDeviceUpdateTime(TimeSpan value)
        {
            this.value = value;
        }
    }
}
