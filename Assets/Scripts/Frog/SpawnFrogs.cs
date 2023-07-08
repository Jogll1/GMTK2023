using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFrogs : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform[] goals;
    public List<GameObject> frogs = new List<GameObject>();
    public GameObject frogPrefab;

    public Transform frogsParent;

    public int maxFrogs;

    //spawn timer
    public float spawnDelayTimer = 0.5f;
    private float currentTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = Random.Range(spawnDelayTimer - 0.1f, spawnDelayTimer + 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (frogs.Count < maxFrogs)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0)
            {
                GameObject frog = Instantiate(frogPrefab, spawnPoints[Random.Range(0, frogs.Count)].position, Quaternion.identity, frogsParent);
                SmartFrog smartFrog = frog.GetComponent<SmartFrog>();
                smartFrog.goal = goals[Random.Range(0, goals.Length)].transform;

                frogs.Add(frog);

                currentTimer = Random.Range(spawnDelayTimer - 0.1f, spawnDelayTimer + 0.2f);
            }
        }
        else
        {
            currentTimer = spawnDelayTimer;
        }

        for (int i = 0; i < frogs.Count; i++)
        {
            if (frogs[i] == null)
            {
                frogs.Remove(frogs[i]);
            }
        }
    }
}
