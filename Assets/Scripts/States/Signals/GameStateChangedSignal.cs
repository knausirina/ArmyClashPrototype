using System;

public class GameStateChangedSignal 
{ 
    public Type NewState { get; }
    public GameStateChangedSignal(Type newState) => NewState = newState;
}