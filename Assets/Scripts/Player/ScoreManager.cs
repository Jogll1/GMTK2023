using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;

    //combo timer
    public float comboTimer = 0.5f;
    public float currentTime;

    public int comboCounter = 0;

    //ui
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        comboCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString("00000000");

        if (currentTime <= 0)
        {
            currentTime = 0;
            comboCounter = 0;
        }
        else
        {
            //if timer started
            currentTime -= Time.deltaTime;

        }
    }
}
