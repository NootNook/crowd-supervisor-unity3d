using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{

    // FSM Constants
    protected List<State> states;
    protected State initial_state;

    // FSM Variables
    protected State current_state;

    public virtual void Start(){current_state.Start();}

    public virtual void Update(){current_state.Update();}
    public virtual void FixedUpdate(){current_state.FixedUpdate();}
    public virtual void LateUpdate(){current_state.LateUpdate(); current_state = current_state.checkTransitions();}

    public virtual void OnTriggerEnter(Collider c){current_state.OnTriggerEnter(c);}
    public virtual void OnTriggerStay(Collider c){current_state.OnTriggerStay(c);}
    public virtual void OnTriggerExit(Collider c){current_state.OnTriggerExit(c);}

    public virtual void OnCollisionEnter(Collision c){current_state.OnCollisionEnter(c);}
    public virtual void OnCollisionStay(Collision c){current_state.OnCollisionEnter(c);}
    public virtual void OnCollisionExit(Collision c){current_state.OnCollisionEnter(c);}

    public virtual void Restart(){this.current_state = this.initial_state;}

}
