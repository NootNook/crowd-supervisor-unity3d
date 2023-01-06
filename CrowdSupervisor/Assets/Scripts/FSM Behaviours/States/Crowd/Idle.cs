using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Idle State, intended for a CrowdFSM context
public class IdleState : State
{
    public IdleState(CrowdFSM context) : base(context){}

    public override void Start(){

        // Change color when starting
        context.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        // Communicating State entry
        ((CrowdFSM)context).setCurrentStateName("idle");
        Debug.Log(context.name +" New state : idle");

        // Reinitializing Transitions
        foreach (Transition t in transitions){ t.init();}
    }

    public override void FixedUpdate()
    {
        // Setting speed to 0
        context.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
