using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Random Transitions
public class RandomTransition : Transition
{
    private float probability;

    public RandomTransition(FSM context, State target, float probability) : base(context,target)
    {
        this.probability = probability;
    }

    public override bool checkCondition(){
        float rand = Random.Range(0.0f,1.0f);
        return  rand < probability;
    }
}
