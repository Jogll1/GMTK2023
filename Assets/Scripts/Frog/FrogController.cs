using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform targetPos;

    private bool canMove = true;

    //detect when close to the car
    public float detectionRadius = 3f;

    //timer
    public float moveTimer = 0.5f;
    private float currentMoveTimer;

    //goal and car
    public GameObject player;
    public GameObject goal;

    //rigidbody
    private Rigidbody2D rb;

    //screen sizes
    private float halfScreenWidth;
    private float halfScreenHeight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        targetPos.parent = null;

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
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
        }

        //if at target
        if (Vector3.Distance(transform.position, targetPos.position) <= .05f)
        {
            //reset timer once at target
            currentMoveTimer = moveTimer;

            //distance between frog and car
            float distFromCar = Vector3.Distance(transform.position, player.transform.position);

            int h = 0;
            int v = 0;

            //if far from car, move towards goal, else move away from it
            if (distFromCar > detectionRadius + 1.5 || Vector3.Distance(transform.position, goal.transform.position) <= 3f)
            {
                Debug.Log("moving towards goal");
                //moving towards goal
                //distance between frog and goal
                float distX = goal.transform.position.x - transform.position.x;
                float distY = goal.transform.position.y - transform.position.y;

                int ran = Random.Range(0, 2); //bit of random

                if (ran == 0)
                {
                    if (Mathf.Abs(distX) > 0.05f)
                    {
                        h = (int)Mathf.Sign(distX);
                    }
                }
                else
                {
                    if (Mathf.Abs(distY) > 0.05f)
                    {
                        v = (int)Mathf.Sign(distY);
                    }
                }

                //move target
                targetPos.position = UpdateTargetPos(targetPos.position, h, v);
            }
            else if (distFromCar > detectionRadius)
            {
                Debug.Log("in range");
                //moving away from car
                //distance between frog and car
                float distX = player.transform.position.x - transform.position.x;
                float distY = player.transform.position.y - transform.position.y;

                int ran = Random.Range(0, 2); //bit of random

                if (ran == 0)
                {
                    if (Mathf.Abs(distX) > 0.05f)
                    {
                        h = -(int)Mathf.Sign(distX);
                    }
                }
                else
                {
                    if (Mathf.Abs(distY) > 0.05f)
                    {
                        v = -(int)Mathf.Sign(distY);
                    }
                }

                //move target
                targetPos.position = UpdateTargetPos(targetPos.position, h, v);
            }
            else
            {
                Debug.Log("moving away");
                //moving away
                //moving around car radius
                //distance between frog and car
                float distX = player.transform.position.x - transform.position.x;
                float distY = player.transform.position.y - transform.position.y;

                int ran = Random.Range(0, 2); //bit of random

                if (ran == 0)
                {
                    if (Mathf.Abs(distY) > 0.05f)
                    {
                        v = -(int)Mathf.Sign(distY);
                    }

                    if (Vector3.Distance(transform.position + new Vector3(1f, 0f, 0f), player.transform.position) > detectionRadius)
                    {
                        v = 1;
                    }
                    else if (Vector3.Distance(transform.position + new Vector3(-1f, 0f, 0f), player.transform.position) > detectionRadius)
                    {
                        v = -1;
                    }
                }
                else
                {
                    if (Mathf.Abs(distX) > 0.05f)
                    {
                        h = -(int)Mathf.Sign(distX);
                    }

                    if (Vector3.Distance(transform.position + new Vector3(0f, 1f, 0f), player.transform.position) > detectionRadius)
                    {
                        h = 1;
                    }
                    else if (Vector3.Distance(transform.position + new Vector3(0f, -1f, 0f), player.transform.position) > detectionRadius)
                    {
                        h = -1;
                    }
                }

                if (h == 0) v = 1;

                //move target
                targetPos.position = UpdateTargetPos(targetPos.position, h, v);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Car")
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
