using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerturbatorFSM : CrowdFSM
{
    [Header("Perturbate State Parameters")]
    public float max_speed_perturbate;
    public float attraction_factor;
    public float min_time_perturbate;
    public float max_time_perturbate;

    // FSM Script Initialization
    public override void Start(){

        // HashSet of nearby objects
        nearby_objects = new HashSet<GameObject>();

        // Different states of our FSM
        State idle = new IdleState(this);
        State walk = new WalkState(this,max_speed_walk, acceleration_factor, avoidance_factor, personal_space);
        State perturbate = new PerturbateState(this,max_speed_perturbate, attraction_factor);

        this.initial_state = walk;
        this.current_state = walk;
        this.states = new List<State> {idle,walk,perturbate};

        // Construction of the FSM
        idle.addTransition(new WaitRandomTransition(this,walk,min_wait,max_wait));
        idle.addTransition(new RandomTransition(this,perturbate,0.0005f));

        walk.addTransition(new AtObjectiveTransition(this,idle,0.25f));
        walk.addTransition(new RandomTransition(this,perturbate,0.0005f));

        perturbate.addTransition(new WaitRandomTransition(this,idle,min_time_perturbate,max_time_perturbate));


        this.current_state.Start();
    }

}
