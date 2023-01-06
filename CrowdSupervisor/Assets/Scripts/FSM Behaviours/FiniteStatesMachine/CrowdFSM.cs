using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdFSM : FSM
{
    [Header("Idle State Parameters")]
    public float min_wait;
    public float max_wait;

    [Header("Walk State Parameters")]
    public float max_speed_walk;
    public float acceleration_factor;
    public float avoidance_factor;
    public float personal_space;

    [Header("Flee State Parameters")]
    public float max_speed_flee;
    public float evasion_factor;
    public float exit_factor;
    public GameObject exit;

    [Header("Indirect Flee State Parameters")]
    public float follow_factor;

    [Header("Panic State Parameters")]
    public float max_speed_panic;

    [Header("Calming State Parameters")]
    public float max_speed_calming;
    public float degrouping_factor;


    // Sensing Variables
    protected HashSet<GameObject> nearby_objects;
    protected Vector3 current_objective;

    // FSM Parameters
    protected string current_state_name;

    // FSM Script Initialization
    public override void Start(){

        // HashSet of nearby objects
        nearby_objects = new HashSet<GameObject>();

        // Different states of our FSM
        State idle = new IdleState(this);
        State walk = new WalkState(this,max_speed_walk, acceleration_factor, avoidance_factor, personal_space);
        State flee = new FleeState(this,max_speed_flee, evasion_factor, exit_factor);
        State indirect_flee = new IndirectFleeState(this,max_speed_flee, follow_factor, exit_factor);
        State panic = new PanicState(this,max_speed_panic);
        State calming = new CalmingState(this,max_speed_calming,degrouping_factor);

        this.initial_state = walk;
        this.current_state = walk;
        this.states = new List<State> {idle,walk,flee,indirect_flee,panic};

        // Construction of the FSM
        idle.addTransition(new WaitRandomTransition(this,walk,min_wait,max_wait));
        idle.addTransition(new TagNearbyTransition(this,flee,"Perturbator"));
        idle.addTransition(new TriggerSpeedNearbyTransition(this,indirect_flee,1.3f * max_speed_walk));

        walk.addTransition(new AtObjectiveTransition(this,idle,0.25f));
        walk.addTransition(new TagNearbyTransition(this,flee,"Perturbator"));
        walk.addTransition(new TriggerSpeedNearbyTransition(this,indirect_flee,1.3f * max_speed_walk));

        // This transition triggers if the perturbator is nearby continuously during a time frame
        flee.addTransition(new AndSynchronizedTransition(new WaitRandomTransition(this,panic,1.0f,5.0f),new TagNearbyTransition(this,panic,"Perturbator")));
        flee.addTransition(new TagNearbyTransition(this,calming,"Reassure"));
        // This transition triggers if no perturbator is nearby continuously during a time frame
        flee.addTransition(new AndSynchronizedTransition(new WaitRandomTransition(this,calming,5.0f,10.0f),new NotTransition(new TagNearbyTransition(this,idle,"Perturbator"))));

        indirect_flee.addTransition(new TagNearbyTransition(this,calming,"Reassure"));
        indirect_flee.addTransition(new TagNearbyTransition(this,flee,"Perturbator"));
        // This transition triggers if no perturbator is nearby continuously during a time frame
        indirect_flee.addTransition(new AndSynchronizedTransition(new WaitRandomTransition(this,calming,1.0f,5.0f),new NotTransition(new TagNearbyTransition(this,panic,"Perturbator"))));

        panic.addTransition(new TagNearbyTransition(this,calming,"Reassure"));
        // This transition triggers if no perturbator is nearby continuously during a time frame
        panic.addTransition(new AndSynchronizedTransition(new WaitRandomTransition(this,calming,4.0f,8.0f),new NotTransition(new TagNearbyTransition(this,idle,"Perturbator"))));

        calming.addTransition(new WaitRandomTransition(this,walk,5.0f,8.0f));
        calming.addTransition(new TagNearbyTransition(this,flee,"Perturbator"));
        calming.addTransition(new TriggerSpeedNearbyTransition(this,indirect_flee,1.3f * max_speed_walk));


        this.current_state.Start();
    }

    // Nearby Object detection
    public override void OnTriggerEnter(Collider other)
    {
        nearby_objects.Add(other.gameObject);
    }

    public override void OnTriggerExit(Collider other){
        nearby_objects.Remove(other.gameObject);
    }

    public void setObjective(Vector3 target){this.current_objective = target;}
    public Vector3 getObjective(){return this.current_objective;}

    public void setCurrentStateName(string s){this.current_state_name = s;}

    public HashSet<GameObject> getNearbyObjects(){return this.nearby_objects;}

}
