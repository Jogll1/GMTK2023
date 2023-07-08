using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogsManager : MonoBehaviour
{
    public GameObject[] frogs;

    // Start is called before the first frame update
    void Start()
    {
        frogs = GameObject.FindGameObjectsWithTag("Frog");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
