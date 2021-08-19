using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// class that controls whole game
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool isRun;
    public bool gameEnd;

    [Header("UI")]
    public GameObject tutorialPanel;
    public GameObject completePanel;
    public GameObject failPanel;
    public Text levelNumber;

    [Header("Levels")]
    public GameObject[] levels;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        gameEnd = false;

        // instance of this class

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        //instantiate level

        Instantiate(levels[PlayerPrefs.GetInt("level", 0)]);
        levelNumber.text = "Level " + PlayerPrefs.GetInt("levelNumber", 1);

        tutorialPanel.SetActive(true);
        isRun = false;

    }


    // called when the user touch the screen 
    public void Play()
    {
        isRun = true;
        tutorialPanel.SetActive(false);
    }


    // called when the player loses the game
    public void Lose()
    {
        gameEnd = true;
        failPanel.SetActive(true);
    }

    // called when the player jumps on win
    public void Jump()
    {
        gameEnd = true;
    }

    // called when the player reach the helicopter
    public void Helicopter()
    {
        completePanel.SetActive(true);
    }


    // called when the player click next button
    public void Next()
    {
        if (PlayerPrefs.GetInt("level", 0) == levels.Length - 1)
        {
            PlayerPrefs.SetInt("level", 0);
        }
        else
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 0) + 1);
        }
        PlayerPrefs.SetInt("levelNumber", PlayerPrefs.GetInt("levelNumber", 1) + 1);
        SceneManager.LoadScene(0);
    }

    // called when the player click reload button
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

}
