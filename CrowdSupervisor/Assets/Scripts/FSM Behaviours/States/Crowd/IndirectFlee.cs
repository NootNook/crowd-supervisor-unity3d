using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fleeing state
public class IndirectFleeState : State
{
    private Vector3 acceleration, speed_shift;
    private float max_speed, follow_factor, exit_factor;

    public IndirectFleeState(CrowdFSM context, float max_speed, float follow_factor, float exit_factor) : base(context)
    {
        this.max_speed = max_speed;
        this.follow_factor = follow_factor;
        this.exit_factor = exit_factor;
        acceleration = new Vector3(0,0,0);
        speed_shift = new Vector3(0,0,0);
    }

    public override void Start(){

        // Change color when starting
        context.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);

        // Communicating State entry
        ((CrowdFSM)context).setCurrentStateName("indirect_flee");
        Debug.Log(context.name + " New state : indirect_flee");

        // Reinitializing Transitions
        foreach (Transition t in transitions){ t.init();}
    }

    public override void Update(){

        speed_shift = new Vector3(0,0,0);

        // Following fleeing agents
        float total = 0;
        foreach(GameObject obj in ((CrowdFSM)context).getNearbyObjects())
        {
            if( obj.tag == "Crowd")
            {
                Vector3 other_speed = obj.GetComponent<Rigidbody>().velocity;
                if (other_speed.magnitude > context.GetComponent<Rigidbody>().velocity.magnitude)
                {
                    speed_shift += other_speed;
                    total += 1;
                }
            }
        }

        if(total > 0)
            speed_shift /= total;

        // Drawing sum of following speeds
        Debug.DrawLine(context.transform.position,context.transform.position + speed_shift,Color.red);

        // Computing and drawing objective force
        acceleration =  Vector3.Normalize(((CrowdFSM)context).exit.transform.position - context.transform.position);
        acceleration *= ForcesComputation.LinearForce(acceleration.magnitude, exit_factor);
        Debug.DrawLine(context.transform.position,context.transform.position + acceleration,Color.green);
    }

    public override void FixedUpdate()
    {
        // On applique la force calculée
        context.GetComponent<Rigidbody>().AddForce(acceleration);

        // On redresse la vitesse
        context.GetComponent<Rigidbody>().velocity = context.GetComponent<Rigidbody>().velocity * (1 - follow_factor) + follow_factor * speed_shift;

        // On oriente l'agent vers son déplacement
        context.transform.rotation = Quaternion.LookRotation(context.GetComponent<Rigidbody>().velocity);

        // On limite la vitesse
        float speed = context.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed > max_speed){context.GetComponent<Rigidbody>().velocity *= max_speed/speed;}
    }
}
