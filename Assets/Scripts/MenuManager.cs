using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    public GameObject gamePlayObj;
    public GameObject playPanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;

    public Button playBtn;
    public Button restartBtn;
    public Button backBtn;

    public GameObject vfx;

    void Awake()
    {
        instance = this;
    }

    void Start () {
        playBtn.interactable = true;
        playPanel.SetActive(true);
	}
	
    public void PlayGame()
    {
        playBtn.interactable = false;
        if (Player.instance == null)
        {
            playPanel.GetComponent<Animator>().SetBool("IsOn", true);
            gamePanel.SetActive(true);
            gamePlayObj.SetActive(true);
        }
        else
        {
            Player.instance.Restart();
        }
    }

    public void BackToMenu()
    {
        
        gameOverPanel.GetComponent<Animator>().SetBool("IsOn", true);
        playPanel.GetComponent<Animator>().SetBool("IsOn", false);
        playBtn.interactable = true;
        gamePlayObj.SetActive(false);
        gamePanel.SetActive(false);
    }
}
