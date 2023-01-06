using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Perturbation
{
    private string id {get;}
    private Vector3 position {get;}
    private int type {get;}

    public const int PERTURBATOR = 0;
    public const int PANIC = 1;
    public const int FLEE = 2;
    private static int n_pert = 0;

    public Perturbation(Vector3 position, int type = PERTURBATOR)
    {
        this.id = "Perturbation_" + Perturbation.n_pert;
        Perturbation.n_pert ++;

        this.position = position;
        this.type = type;
    }
}
