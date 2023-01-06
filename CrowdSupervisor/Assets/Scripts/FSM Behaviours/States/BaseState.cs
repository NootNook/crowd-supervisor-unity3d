using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract Base Class Of Behaviour States of the FSM
public abstract class State
{
    // Agent data
    protected FSM context;

    // Transitions Data
    protected List<Transition> transitions;
    public void addTransition(Transition t){this.transitions.Add(t);}

    //Default Constructor
    public State(FSM context){this.context = context; transitions = new List<Transition>();}

    // Check if we need to change state
    public State checkTransitions(){
        foreach(Transition t in this.transitions){
            if(t.checkCondition()){
                this.End();
                t.getTarget().Start();
                return t.getTarget();
            }
        }
        return this;
    }

    public virtual void Start(){}
    public virtual void End(){}

    public virtual void FixedUpdate(){}
    public virtual void Update(){}
    public virtual void LateUpdate(){}

    public virtual void OnTriggerEnter(Collider c){}
    public virtual void OnTriggerStay(Collider c){}
    public virtual void OnTriggerExit(Collider c){}

    public virtual void OnCollisionEnter(Collision c){}
    public virtual void OnCollisionStay(Collision c){}
    public virtual void OnCollisionExit(Collision c){}
}
