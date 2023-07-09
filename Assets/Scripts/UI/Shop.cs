using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject canvas;

    //cars
    public int currentCarSelected = 0;
    public Sprite[] carSprites;
    public Image demoCar;
    public GameObject specialCarButton;

    //frogs
    public int currentFrogSelected = 0;
    public Sprite[] frogSprites;
    public Image demoFrog;
    public GameObject[] frogButtons;

    // Start is called before the first frame update
    void Start()
    {
        currentCarSelected = PlayerPrefs.GetInt("CurrentCarIndex", 0);
        demoCar.sprite = carSprites[currentCarSelected];

        currentFrogSelected = PlayerPrefs.GetInt("CurrentFrogIndex", 0);
        if (currentFrogSelected == 0)
        {
            demoFrog.gameObject.SetActive(false);
            demoFrog.sprite = frogSprites[currentFrogSelected];
        }
        else
        {
            demoFrog.gameObject.SetActive(true);
            demoFrog.sprite = frogSprites[currentFrogSelected];
        }

        // specialCarButton.transform.GetChild(0).GetComponent<Image>().color = new Color(128f, 128f, 128f);
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.GetComponent<MenuScript>().highScore >= 10000)
        {
            //unlock locked car
            specialCarButton.GetComponent<Button>().interactable = true;
            specialCarButton.GetComponent<ButtonSounds>().playSounds = true;
            specialCarButton.transform.GetChild(0).GetComponent<Image>().color = Color.white;
            specialCarButton.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            //lock locked car
            specialCarButton.GetComponent<Button>().interactable = false;
            specialCarButton.GetComponent<ButtonSounds>().playSounds = false;
            specialCarButton.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
            specialCarButton.transform.GetChild(1).gameObject.SetActive(true);
        }

        foreach (GameObject button in frogButtons)
        {
            if (canvas.GetComponent<MenuScript>().highScore >= int.Parse(button.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text))
            {
                //unlock locked button
                button.GetComponent<Button>().interactable = true;
                button.GetComponent<ButtonSounds>().playSounds = true;
                button.transform.GetChild(0).GetComponent<Image>().color = Color.white;
                button.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                //lock locked button
                button.GetComponent<Button>().interactable = false;
                button.GetComponent<ButtonSounds>().playSounds = false;
                specialCarButton.transform.GetChild(0).GetComponent<Image>().color = Color.gray;
                button.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateCarDesign(int index)
    {
        demoCar.sprite = carSprites[index];
        currentCarSelected = index;

        //store to playerPrefs
        PlayerPrefs.SetInt("CurrentCarIndex", currentCarSelected);
        PlayerPrefs.Save();
    }

    public void UpdateFrogDesign(int index)
    {
        currentFrogSelected = index;
        if (currentFrogSelected == 0)
        {
            demoFrog.gameObject.SetActive(false);
            demoFrog.sprite = frogSprites[currentFrogSelected];
        }
        else
        {
            demoFrog.gameObject.SetActive(true);
            demoFrog.sprite = frogSprites[currentFrogSelected];
        }

        //store to playerPrefs
        PlayerPrefs.SetInt("CurrentFrogIndex", currentFrogSelected);
        PlayerPrefs.Save();
    }
}
