using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaitRandomTransition : WaitTransition
{
    public WaitRandomTransition(FSM agent, State target, float min_time, float max_time)
     : base(agent,target,Random.Range(min_time,max_time)){}
}
