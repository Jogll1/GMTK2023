using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    //score
    public int score;
    private int highScore;

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

    public bool gameStart;
    public bool gameEnd;

    //countdown timer
    public float countdown = 3f;
    public GameObject countdownText;
    public AudioClip countdownTickClip;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnFrogs>();

        //load high score from playerprefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        comboCounter = 0;

        gameStart = false;
        gameEnd = false;

        Array.Reverse(healthIcons);

        StartCoroutine(StartTimer());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString("00000000");
        DisplayHealth();

        if (gameStart)
        {
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

            if (health <= 0 && !gameEnd)
            {
                //game over
                spawnManager.canSpawn = false;

                //set high score if needed
                if (score > highScore)
                {
                    //update the high score value
                    highScore = score;

                    //save the new high score to PlayerPrefs
                    PlayerPrefs.SetInt("HighScore", highScore);
                    PlayerPrefs.Save();
                }

                //spawn game over screen
                GameObject goPanel = Instantiate(gameOverPanel, canvas.transform.position, Quaternion.identity, canvas.transform);
                goPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString("00000000");
                goPanel.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "High Score: " + highScore.ToString("00000000");

                //end game
                gameEnd = true;
            }
        }
    }

    private IEnumerator StartTimer()
    {
        float timeRemaining = countdown;

        while (timeRemaining > 0f)
        {
            int seconds = Mathf.FloorToInt(timeRemaining);
            countdownText.GetComponent<Animator>().SetTrigger("PlayAnim");
            countdownText.GetComponent<TextMeshProUGUI>().text = seconds.ToString();

            GetComponent<AudioSource>().PlayOneShot(countdownTickClip);
            yield return new WaitForSeconds(1f);

            timeRemaining -= 1f;
        }

        // Timer has reached 0
        countdownText.GetComponent<TextMeshProUGUI>().text = "0";
        countdownText.GetComponent<Animator>().SetTrigger("PlayAnim");
        GetComponent<AudioSource>().PlayOneShot(countdownTickClip);
        yield return new WaitForSeconds(1f);
        countdownText.SetActive(false);
        gameStart = true;

        Debug.Log("Game start");
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
