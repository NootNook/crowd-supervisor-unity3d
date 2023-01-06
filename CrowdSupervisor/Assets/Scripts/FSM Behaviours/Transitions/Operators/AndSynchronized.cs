using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// And Operator for transitions with reinitialization if one is false
public class AndSynchronizedTransition : Transition
{
    private Transition A, B;

    public AndSynchronizedTransition(Transition A, Transition B) : base(A.getContext(),A.getTarget())
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
        if (!A.checkCondition())
        {
            B.init();
        }
        if (!B.checkCondition())
        {
            A.init();
        }
        return A.checkCondition() && B.checkCondition();
    }
}
