using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDesign : MonoBehaviour
{
    private int currentCarSelected = 0;
    public Sprite[] carSprites;

    // Start is called before the first frame update
    void Start()
    {
        currentCarSelected = PlayerPrefs.GetInt("CurrentCarIndex", 0);
        GetComponent<SpriteRenderer>().sprite = carSprites[currentCarSelected];
    }
}
