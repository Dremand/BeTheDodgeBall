using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    private List<Transform> paths = new List<Transform>();      //array to hold the positions of all the children 

    void Awake()
    {
        //populate array 
        for (int i = 0; i < transform.childCount; i++)
        {
            paths.Add(transform.GetChild(i).transform);
        }
    }

    //Getter for other objects to reference
    public Transform[] GetPaths()
    {
        if (paths.Count > 0)
        {
            return paths.ToArray();
        }
        return null;
    }
}