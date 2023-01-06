using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TagNearby transition, intended to be used in a CrowdFSM
public class TriggerSpeedNearbyTransition : Transition
{
    private float speed;

    public TriggerSpeedNearbyTransition(CrowdFSM context, State target, float speed) : base(context,target)
    {
        this.speed = speed;
    }

    public override bool checkCondition()
    {
        foreach (GameObject obj in ((CrowdFSM)context).getNearbyObjects())
        {
            Rigidbody r = obj.GetComponent<Rigidbody>();
            if (r){
                if (r.velocity.magnitude > speed)
                    return true;
            }
        }
        return false;
    }
}
