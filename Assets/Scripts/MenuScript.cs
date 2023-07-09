using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public int highScore;
    public GameObject highScoreText;

    private void Start()
    {
        //load high score from playerprefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Start is called before the first frame update
    void Update()
    {
        //load high score from playerprefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        highScoreText.GetComponent<TextMeshProUGUI>().text = "High Score: " + highScore.ToString("00000000");
    }

    public static void ClearPlayerPrefs()
    {
        //clear player prefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs cleared.");
    }
}
