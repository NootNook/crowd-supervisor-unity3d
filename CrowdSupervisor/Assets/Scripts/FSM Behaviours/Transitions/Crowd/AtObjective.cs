using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AtObjective transition
public class AtObjectiveTransition : Transition
{
    private float threshold;

    public AtObjectiveTransition(CrowdFSM context, State target, float threshold) : base(context,target)
    {
        this.threshold = threshold;
    }

    public override bool checkCondition()
    {
        return (((CrowdFSM)context).getObjective() - context.transform.position).magnitude < threshold;
    }
}
