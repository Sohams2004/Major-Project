using UnityEngine;

public enum States
{
    Running,
    Success,
    Failure
}

public abstract class Node
{
    protected States state;

    public abstract States RunState();
}
