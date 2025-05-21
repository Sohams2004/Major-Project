using UnityEngine;
using System.Collections.Generic;
public class Sequence : Node
{
    public List<Node> children = new List<Node>();

    public bool isChildRunning;

    public Sequence(List<Node> children)
    {
        this.children = children;
    }

    public override States RunState()
    {
        foreach (Node child in children)
        {
            switch (child.RunState())
            {
                case States.Running:
                    state = States.Running;
                    return States.Running;

                case States.Success:
                    break;

                case States.Failure:
                    state = States.Failure;
                    return States.Failure;

                default:
                    break;
            }
        }

        state = isChildRunning ? States.Running : States.Success;
        return state;
    }
}
