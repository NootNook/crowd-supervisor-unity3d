using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wait transition
public class WaitTransition : Transition
{
    private float t0;
    private float delta_t;

    public WaitTransition(FSM context, State target, float time) : base(context,target)
    {
        this.t0 = Time.time;
        this.delta_t = time;
    }

    public override void init(){ this.t0 = Time.time;}

    public override bool checkCondition()
    {
        if (Time.time - this.t0 > this.delta_t){return true;}
        return false;
    }
}
