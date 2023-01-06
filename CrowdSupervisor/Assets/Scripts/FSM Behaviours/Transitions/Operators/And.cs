using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// And Operator for transitions
public class AndTransition : Transition
{
    private Transition A, B;

    public AndTransition(Transition A, Transition B) : base(A.getContext(),A.getTarget())
    {
        this.A = A;
        this.B = B;

        if (A.getTarget() != B.getTarget())
        {
            Debug.Log("Error during Transition Combination : two targets are differents");
        }
        this.target = A.getTarget();
    }

    public override void init()
    {
        A.init();
        B.init();
    }

    public override bool checkCondition()
    {
        return A.checkCondition() && B.checkCondition();
    }
}
