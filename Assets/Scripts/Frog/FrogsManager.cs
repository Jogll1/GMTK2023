using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogsManager : MonoBehaviour
{
    public GameObject[] frogs;
    public Pathfinding pathfinding;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        frogs = GameObject.FindGameObjectsWithTag("Frog");
    }

    // Update is called once per frame
    void Update()
    {
        //set each frogs path
        foreach (GameObject frog in frogs)
        {
            SmartFrog smartFrog = frog.GetComponent<SmartFrog>();

            smartFrog.path = pathfinding.FindPath(smartFrog.transform.position, smartFrog.goal.position);
        }
    }
}
