using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform targetPos;

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

            //random moves for now
            int ran = Random.Range(0, 2);
            int h = (ran == 0) ? Random.Range(-1, 2) : 0;
            int v = (ran == 1) ? Random.Range(-1, 2) : 0;

            Vector3 updatedTargetPos = targetPos.position;

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

            targetPos.position = updatedTargetPos;
        }
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
