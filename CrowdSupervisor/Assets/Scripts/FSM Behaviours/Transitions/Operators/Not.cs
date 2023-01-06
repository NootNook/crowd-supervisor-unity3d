using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Or Operator for transitions
public class NotTransition : Transition
{
    private Transition A;

    public NotTransition(Transition A) : base(A.getContext(),A.getTarget())
    {
        this.A = A;
        this.target = A.getTarget();
    }

    public override void init()
    {
        A.init();
    }

    public override bool checkCondition()
    {
        return !A.checkCondition() ;
    }
}
