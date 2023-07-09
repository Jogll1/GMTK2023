using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDesign : MonoBehaviour
{
    public GameObject costume;
    public Sprite[] frogSprites;
    public int currentCostume;

    // Start is called before the first frame update
    void Start()
    {
        currentCostume = PlayerPrefs.GetInt("CurrentFrogIndex", 0);
        costume.GetComponent<SpriteRenderer>().sprite = frogSprites[currentCostume];
    }

}
