using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Walking State, intended for a CrowdFSM context
public class WalkState : State
{
    private Vector3 acceleration;
    private float acceleration_factor, max_speed;
    private float avoidance_factor, personal_space ;

    public WalkState(CrowdFSM context, float acceleration_factor, float max_speed, float avoidance_factor, float personal_space ) : base(context)
    {
        this.acceleration_factor = acceleration_factor;
        this.max_speed = max_speed;
        this.avoidance_factor =  avoidance_factor;
        this.personal_space = personal_space;
        this.acceleration = new Vector3(0,0,0);
    }

    public override void Start()
    {
        // Setting context new move objective
        Vector3 objective = new Vector3(Random.Range(-45.0f, 45.0f), 1.01f, Random.Range(-45.0f, 45.0f));
        ((CrowdFSM)context).setObjective(objective);

        // Looking at new objective
        context.transform.rotation = Quaternion.LookRotation(objective - context.transform.position);

        // Change color when starting
        context.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

        // Communicating State entry
        ((CrowdFSM)context).setCurrentStateName("walk");
        Debug.Log(context.name +" New state : walk");

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
                float factor = ForcesComputation.InverseSquareForce(toObject.magnitude, avoidance_factor, personal_space);
                acceleration += Vector3.Normalize(toObject) * factor;

                // Counting the number of nearby crowd agents
                number_of_crowds += 1;
            }
        }

        // Drawing sum of avoidance forces
        Debug.DrawLine(context.transform.position,context.transform.position + acceleration, Color.red);

        // Computing and drawing objective force
        Vector3 objective_force =  Vector3.Normalize(((CrowdFSM)context).getObjective() - context.transform.position);
        objective_force *= ForcesComputation.LinearForce(objective_force.magnitude, acceleration_factor) * (0.5f + 0.5f / Mathf.Sqrt(number_of_crowds));
        Debug.DrawLine(context.transform.position,context.transform.position + objective_force, Color.green);
        acceleration += objective_force;
    }

    public override void FixedUpdate()
    {
        // On applique la force calculée
        context.GetComponent<Rigidbody>().AddForce(acceleration);

        // On oriente l'agent vers son objectif
        context.transform.rotation = Quaternion.LookRotation(context.GetComponent<Rigidbody>().velocity);

        // On limite la vitesse
        float speed = context.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed > max_speed){context.GetComponent<Rigidbody>().velocity *= max_speed/speed;}
    }
}
