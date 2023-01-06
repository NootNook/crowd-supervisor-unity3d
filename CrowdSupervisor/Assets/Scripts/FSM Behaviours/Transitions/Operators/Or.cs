using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Or Operator for transitions
public class OrTransition : Transition
{
    private Transition A, B;

    public OrTransition(Transition A, Transition B) : base(A.getContext(),A.getTarget())
    {
        this.A = A;
        this.B = B;

        if (A.getTarget() != B.getTarget())
        {
            Debug.Log("Error during Transition Combination : two targets are differents");
        }
        if (A.getContext() != B.getContext())
        {
            Debug.Log("Error during Transition Combination : two contexts are differents");
        }
    }

    public override void init()
    {
        A.init();
        B.init();
    }

    public override bool checkCondition()
    {
        return A.checkCondition() || B.checkCondition();
    }
}
