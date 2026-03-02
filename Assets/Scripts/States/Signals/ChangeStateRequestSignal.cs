using System;

public class ChangeStateRequestSignal
{
    public Type TargetState { get; }
    private ChangeStateRequestSignal(Type target) => TargetState = target;

    public static ChangeStateRequestSignal Create<T>() where T : IState
    {
        return new ChangeStateRequestSignal(typeof(T));
    }
}