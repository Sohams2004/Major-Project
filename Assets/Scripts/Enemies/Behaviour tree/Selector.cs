using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Selector : Node
{
    public List<Node> children = new List<Node>();

    public Selector(List<Node> children)
    {
        this.children = children;
    }

    public override States RunState()
    {
        foreach (Node child in children)
        {
            switch(child.RunState())
            {
                case States.Running:
                    state = States.Running;
                    return States.Running;

                case States.Success:
                    state = States.Success;
                    return States.Success;

                case States.Failure:
                    break;

                default:
                    break;
            }
        }
        state = States.Failure;
        return state;
    }
}
