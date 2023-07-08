using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartFrog : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool canMove = true;

    public List<Node> path;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Node node in path)
        {
            Debug.Log(node.gridX + " " + node.gridY);
        }
    }
}
