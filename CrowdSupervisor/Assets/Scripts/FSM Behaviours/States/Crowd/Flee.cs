using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fleeing state
public class FleeState : State
{
    private Vector3 acceleration;
    private float max_speed, evasion_factor, exit_factor;

    public FleeState(CrowdFSM context, float max_speed, float evasion_factor, float exit_factor) : base(context)
    {
        this.max_speed = max_speed;
        this.evasion_factor = evasion_factor;
        this.exit_factor = exit_factor;
        acceleration = new Vector3(0,0,0);
    }

    public override void Start(){

        // Change color when starting
        context.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        // Communicating State entry
        ((CrowdFSM)context).setCurrentStateName("flee");
        Debug.Log(context.name + " New state : flee");

        // Reinitializing Transitions
        foreach (Transition t in transitions){ t.init();}
    }

    public override void Update(){

        // Computing avoidance of other perturbator agents
        acceleration = new Vector3(0,0,0);
        foreach(GameObject obj in ((CrowdFSM)context).getNearbyObjects())
        {
            if( obj.tag == "Perturbator")
            {
                // Computing avoidance force to this perturbator agent
                Vector3 toPerturbator = context.transform.position  - obj.transform.position;
                float factor = ForcesComputation.InverseSquareForce(toPerturbator.magnitude, evasion_factor);
                acceleration += Vector3.Normalize(toPerturbator) * factor;
            }
        }

        // Drawing sum of avoidance forces
        Debug.DrawLine(context.transform.position,context.transform.position + acceleration,Color.red);

        // Computing and drawing objective force
        Vector3 exit_force =  Vector3.Normalize(((CrowdFSM)context).exit.transform.position - context.transform.position);
        exit_force *= ForcesComputation.LinearForce(exit_force.magnitude, exit_factor);
        Debug.DrawLine(context.transform.position,context.transform.position + exit_force,Color.green);
        acceleration += exit_force;
    }

    public override void FixedUpdate()
    {
        // On applique la force calculée
        context.GetComponent<Rigidbody>().AddForce(acceleration);

        // On oriente l'agent vers son déplacement
        context.transform.rotation = Quaternion.LookRotation(context.GetComponent<Rigidbody>().velocity);

        // On limite la vitesse
        float speed = context.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed > max_speed){context.GetComponent<Rigidbody>().velocity *= max_speed/speed;}
    }
}
