using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Idle State, intended for a CrowdFSM context
public class CalmingState : State
{
    private float max_speed, spacing_force;
    private Vector3 acceleration;

    public CalmingState(CrowdFSM context, float max_speed, float spacing_force) : base(context)
    {
        this.max_speed = max_speed;
        this.spacing_force = spacing_force;
    }

    public override void Start(){

        // Change color when starting
        context.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);

        // Communicating State entry
        ((CrowdFSM)context).setCurrentStateName("calming");
        Debug.Log(context.name +" New state : calming");

        // Reinitializing Transitions
        foreach (Transition t in transitions){ t.init();}
    }

    public override void Update()
    {
        // Computing avoidance of other crowd members
        acceleration = new Vector3(0,0,0);
        int number_of_crowds = 1;
        foreach(GameObject obj in ((CrowdFSM)context).getNearbyObjects())
        {
            if (obj.tag == "Crowd")
            {
                // Computing avoidance force to this crowd agent
                Vector3 toObject = context.transform.position  - obj.transform.position;
                float factor = ForcesComputation.InverseLinearForce(toObject.magnitude, spacing_force);
                acceleration += Vector3.Normalize(toObject) * factor;

                // Counting the number of nearby crowd agents
                number_of_crowds += 1;
            }
        }
        // Drawing sum of avoidance forces
        Debug.DrawLine(context.transform.position,context.transform.position + acceleration, Color.red);
        acceleration /= number_of_crowds;
    }

    public override void FixedUpdate()
    {
        context.GetComponent<Rigidbody>().AddForce(acceleration);

        // On oriente l'agent vers son objectif
        context.transform.rotation = Quaternion.LookRotation(context.GetComponent<Rigidbody>().velocity);

        // On limite la vitesse
        float speed = context.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed > max_speed){context.GetComponent<Rigidbody>().velocity *= max_speed/speed;}
    }
}
