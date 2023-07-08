using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAStar : MonoBehaviour
{
    //screen sizes for grid
    private float halfScreenWidth;
    private float halfScreenHeight;

    // Start is called before the first frame update
    void Start()
    {
        //screen sizes
        halfScreenHeight = Camera.main.orthographicSize;
        halfScreenWidth = halfScreenHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
