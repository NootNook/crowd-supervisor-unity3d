using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Panic State
public class PanicState : State
{
    private Vector3 direction;
    private float max_speed;

    public PanicState(CrowdFSM context, float max_speed) : base(context)
    {
        direction = new Vector3(0,0,0);
        this.max_speed = max_speed;
    }

    public override void Start(){

        // Change color when starting
        context.GetComponent<Renderer>().material.SetColor("_Color", Color.black);

        // Communicating State entry
        ((CrowdFSM)context).setCurrentStateName("panic");
        Debug.Log(context.name + " New state : panic");

        //context.tag = "Perturbator";

        // Setting random panic direction
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)) * max_speed;

        // Reinitializing Transitions
        foreach (Transition t in transitions){ t.init();}
    }

    public override void Update()
    {
        // Computing directionnal force to stay in area
        Vector3 next = context.transform.position + direction;
        if (next.x > 25 || next.x < -25 || next.z > 25 || next.z < -25)
        {
            direction = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)) * max_speed;
        }
    }

    public override void FixedUpdate()
    {
        // On applique la force calculée
        context.GetComponent<Rigidbody>().AddForce(direction);

        // On oriente l'agent vers son objectif
        context.transform.rotation = Quaternion.LookRotation(context.GetComponent<Rigidbody>().velocity);

        // On limite la vitesse
        float speed = context.GetComponent<Rigidbody>().velocity.magnitude;
        if (speed > max_speed){context.GetComponent<Rigidbody>().velocity *= max_speed/speed;}
    }

    public override void End(){} // context.tag = "Crowd";}
}
