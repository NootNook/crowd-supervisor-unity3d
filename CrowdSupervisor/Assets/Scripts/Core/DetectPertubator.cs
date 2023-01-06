using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPertubator : MonoBehaviour
{
    private HashSet<int> visiblePertubator = new HashSet<int>();
    private HashSet<int> tempVisiblePertubator = new HashSet<int>();

    void Start()
    {
        InvokeRepeating("Check", 0.0f, 0.5f);
    }

    private void Check()
    {
        visiblePertubator = new HashSet<int>(tempVisiblePertubator);
        tempVisiblePertubator.Clear();
    }

    public void call(int id)
    {
        tempVisiblePertubator.Add(id);
    }

    public HashSet<int> getVisiblePertubator()
    {
        return visiblePertubator;
    }
}