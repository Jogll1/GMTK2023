using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartFrog : MonoBehaviour
{
    private Pathfinding pathfinding;

    public float moveSpeed = 5f;
    public bool canMove = true;

    public List<Node> path = new List<Node>();

    public Transform goal;

    public Transform moveTarget;

    //timer
    public float moveTimer = 0.5f;
    public float currentMoveTimer;

    //rigidbody
    private Rigidbody2D rb;

    //screen sizes
    private float halfScreenWidth;
    private float halfScreenHeight;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GameObject.FindWithTag("Pathfinding").GetComponent<Pathfinding>();
        rb = GetComponent<Rigidbody2D>();

        moveTarget.parent = null;

        //set canMove
        canMove = true;

        //set timer
        currentMoveTimer = moveTimer;

        //screen sizes
        halfScreenHeight = Camera.main.orthographicSize;
        halfScreenWidth = halfScreenHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        //change timer
        currentMoveTimer -= Time.deltaTime;

        if (currentMoveTimer <= 0 && canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);
        }

        //if at target
        if (Vector3.Distance(transform.position, moveTarget.position) <= .05f)
        {
            //reset timer once at target
            currentMoveTimer = moveTimer;

            //get next move from path list  
            //convert pos to nodes
            //set path
            path = pathfinding.FindPath(transform.position, goal.position);
            //get own node pos
            Node myNode = pathfinding.grid.NodeFromWorldPos(transform.position);
            if (path.Count > 0) //when frog has nowhere to go path is null
            {
                int h = path[0].gridX - myNode.gridX;
                int v = path[0].gridY - myNode.gridY;

                Debug.Log(h + " " + v);

                moveTarget.position = UpdateTargetPos(moveTarget.position, h, v);
            }
        }
    }

    private Vector3 UpdateTargetPos(Vector3 targetPos, int h, int v)
    {
        Vector3 updatedTargetPos = targetPos;

        if (Mathf.Abs(h) == 1f)
        {
            //constrain screen size
            if (updatedTargetPos.x + h <= halfScreenWidth && updatedTargetPos.x + h >= -halfScreenWidth)
            {
                updatedTargetPos.x += h;
            }
        }

        if (Mathf.Abs(v) == 1f)
        {
            //constrain screen size
            if (updatedTargetPos.y + v <= halfScreenHeight && updatedTargetPos.y + v >= -halfScreenHeight)
            {
                updatedTargetPos.y += v;
            }
        }

        return updatedTargetPos;
    }

    public void LogPath()
    {
        Debug.Log("Logging: ");
        foreach (Node node in path)
        {
            Debug.Log(node.gridX + " " + node.gridY);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Car" && canMove)
        {
            //die
            Destroy(gameObject);
        }
        else if (other.tag == "Goal")
        {
            //at goal
            transform.position = goal.transform.position;
            canMove = false;
        }
    }
}
