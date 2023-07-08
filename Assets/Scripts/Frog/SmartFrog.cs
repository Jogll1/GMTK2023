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
    private float currentMoveTimer;

    //rigidbody
    private Rigidbody2D rb;

    //screen sizes
    private float halfScreenWidth;
    private float halfScreenHeight;

    //design
    public GameObject design;

    //death effects
    public GameObject deathText;
    public GameObject squish;
    public GameObject scoreSound;

    private ScoreManager scoreManager;

    bool died = false;

    public AudioClip jumpClip;
    public bool playedSound;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = GameObject.FindWithTag("Pathfinding").GetComponent<Pathfinding>();
        scoreManager = GameObject.FindWithTag("ScoreManager").GetComponent<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();

        moveTarget.parent = null;

        died = false;

        //set canMove
        canMove = true;

        //set timer
        currentMoveTimer = Random.Range(moveTimer - 0.1f, moveTimer + 0.15f);

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
            if (!playedSound)
            {
                GetComponent<AudioSource>().PlayOneShot(jumpClip);
                playedSound = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, moveSpeed * Time.deltaTime);
        }

        //if at target
        if (Vector3.Distance(transform.position, moveTarget.position) <= .05f)
        {
            playedSound = false;
            //reset timer once at target
            currentMoveTimer = Random.Range(moveTimer - 0.1f, moveTimer + 0.15f);

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

                moveTarget.position = UpdateTargetPos(moveTarget.position, h, v);
            }
            else
            {
                //move towards goal
                int h = 0;
                int v = 0;
                int ran = Random.Range(0, 2); //bit of random
                float distX = goal.transform.position.x - transform.position.x;
                float distY = goal.transform.position.y - transform.position.y;
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

        UpdateDesign(h, v);
        return updatedTargetPos;
    }

    private void UpdateDesign(int h, int v)
    {
        if (h == 1)
        {
            design.transform.eulerAngles = new Vector3(0f, 0f, -90f);
        }
        else if (h == -1)
        {
            design.transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        else if (v == -1)
        {
            design.transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else
        {
            design.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
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
        if (other.tag == "Car")
        {
            //die
            //set score
            if (!died)
            {
                scoreManager.currentTime = scoreManager.comboTimer;
                scoreManager.comboCounter++;
                int scoreToAdd = scoreManager.comboCounter * 100;
                Debug.Log(scoreToAdd);

                GameObject deathTextGO = Instantiate(deathText, gameObject.transform.position, Quaternion.identity);
                deathTextGO.GetComponentInChildren<TextMesh>().text = scoreToAdd.ToString();
                deathTextGO.transform.localScale = new Vector3(scoreManager.comboCounter * 0.4f + 0.6f, scoreManager.comboCounter * 0.4f + 0.6f, 1f);
                scoreManager.score += scoreToAdd;
                Instantiate(squish, gameObject.transform.position, Quaternion.identity);
                Instantiate(scoreSound, gameObject.transform.position, Quaternion.identity);

                died = true;
            }

            Destroy(moveTarget.gameObject);
            Destroy(gameObject);
        }
        else if (other.tag == "Goal")
        {
            //at goal
            transform.position = goal.transform.position;
        }
    }
}
