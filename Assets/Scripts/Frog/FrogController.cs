using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform targetPos;

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

        if (currentMoveTimer <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
        }

        //if at target
        if (Vector3.Distance(transform.position, targetPos.position) <= .05f)
        {
            //reset timer once at target
            currentMoveTimer = moveTimer;

            // int ran = Random.Range(0, 2);
            // int h = (ran == 0) ? Random.Range(-1, 2) : 0;
            // int v = (ran == 1) ? Random.Range(-1, 2) : 0;

            //moving towards goal
            int h = 0;
            int v = 0;
            //distance between frog and goal
            float distX = goal.transform.position.x - transform.position.x;
            float distY = goal.transform.position.y - transform.position.y;
            Debug.Log(distX + " " + distY);

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

            targetPos.position = UpdateTargetPos(targetPos.position, h, v);
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
    }
}
