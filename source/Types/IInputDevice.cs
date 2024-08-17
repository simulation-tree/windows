using Simulation;

namespace Windows
{
    public interface IInputDevice : IEntity
    {
        ButtonState GetButtonState<C>(C control) where C : unmanaged;
    }
}
