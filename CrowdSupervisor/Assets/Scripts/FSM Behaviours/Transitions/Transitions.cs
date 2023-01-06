using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Condition Base Class
public abstract class Transition
{
    protected State target;
    protected FSM context;

    protected Transition(FSM context, State target)
    {
        this.context = context;
        this.target = target;
    }

    public virtual void init(){}

    public abstract bool checkCondition();
    public State getTarget(){return this.target;}
    public FSM getContext(){return this.context;}
}
