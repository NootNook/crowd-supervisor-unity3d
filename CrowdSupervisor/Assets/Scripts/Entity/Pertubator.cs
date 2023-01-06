using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pertubator : MonoBehaviour
{
    public DetectPertubator detectPertubator;

    void OnWillRenderObject()
    {
        string name = Camera.current.name;
        if (name.StartsWith("Camera"))
        {
            string[] extract = name.Split("_");
            int id = int.Parse(extract[1]);
            detectPertubator.call(id);
        }
    }
}
