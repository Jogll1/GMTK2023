using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public int score;

    //combo timer
    public float comboTimer = 0.5f;
    public float currentTime;

    public int comboCounter = 0;

    //ui
    public TextMeshProUGUI scoreText;

    //health
    public int health = 5;
    public GameObject[] healthIcons;
    public GameObject gameOverPanel;
    public Canvas canvas;

    private SpawnFrogs spawnManager;

    public bool gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnFrogs>();

        comboCounter = 0;

        gameEnd = false;

        Array.Reverse(healthIcons);
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

        DisplayHealth();

        if (health <= 0 && !gameEnd)
        {
            //game over
            spawnManager.canSpawn = false;

            //spawn game over screen
            GameObject goPanel = Instantiate(gameOverPanel, canvas.transform.position, Quaternion.identity, canvas.transform);
            goPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString("00000000");

            //end game
            gameEnd = true;
        }
    }

    void DisplayHealth()
    {
        health = Mathf.Clamp(health, 0, 5);

        for (int i = 0; i < 5; i++)
        {
            healthIcons[i].SetActive(false);
        }

        for (int i = 0; i < health; i++)
        {
            healthIcons[i].SetActive(true);
        }
    }
}
