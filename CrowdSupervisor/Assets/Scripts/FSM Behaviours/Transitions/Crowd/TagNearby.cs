using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TagNearby transition, intended to be used in a CrowdFSM
public class TagNearbyTransition : Transition
{
    private string tag_to_find;

    public TagNearbyTransition(CrowdFSM context, State target, string tag_to_find) : base(context,target)
    {
        this.tag_to_find = tag_to_find;
    }

    public override bool checkCondition()
    {
        foreach (GameObject obj in ((CrowdFSM)context).getNearbyObjects())
        {
            if (obj.tag == tag_to_find)
                return true;
        }
        return false;
    }
}
