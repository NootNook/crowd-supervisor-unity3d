using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Perturbate State, intended for a CrowdFSM context
public class PerturbateState : State
{
    private float max_speed, attraction_force;
    private Vector3 acceleration;

    public PerturbateState(CrowdFSM context, float max_speed, float attraction_force) : base(context)
    {
        this.max_speed = max_speed;
        this.attraction_force = attraction_force;
    }

    public override void Start(){

        // Change color when starting
        context.GetComponent<Renderer>().material.SetColor("_Color", Color.black);

        // Communicating State entry
        ((CrowdFSM)context).setCurrentStateName("perturbating");
        Debug.Log(context.name +" New state : perturbating");

        // Reinitializing Transitions
        foreach (Transition t in transitions){ t.init();}

        // Setting tag to Perturbator
        context.gameObject.tag = "Perturbator";
    }

    public override void Update()
    {
        // Computing attraction of other crowd members
        acceleration = new Vector3(0,0,0);
        int number_of_crowds = 1;
        foreach(GameObject obj in ((CrowdFSM)context).getNearbyObjects())
        {
            if (obj.tag == "Crowd")
            {
                // Computing avoidance force to this crowd agent
                Vector3 toObject = obj.transform.position - context.transform.position;
                float factor = ForcesComputation.ConstantForce(attraction_force);
                acceleration += Vector3.Normalize(toObject) * factor;

                // Counting the number of nearby crowd agents
                number_of_crowds += 1;
            }
        }
        // Drawing sum of avoidance forces
        Debug.DrawLine(context.transform.position,context.transform.position + acceleration, Color.green);
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

    public override void End()
    {
        // Setting tag to Crowd
        context.gameObject.tag = "Crowd";
    }
}
