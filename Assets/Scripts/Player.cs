using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Joystick joystick;

    [SerializeField]
    float speed = 1000f;

    float moveHorizontal;
    float moveVertical;

    Rigidbody rb;

    public Text scoreTxt;
    public Text finalScoreTxt;
    public Text bestScoreTxt;
    int score,highScore;

    int noOfCube;
    int streakRed, streakBlue;
    string hitTag;


    void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
    }
	
    void Update()
    {
        #if UNITY_EDITOR
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        if (moveHorizontal > 0.2)
        {
            rb.AddForce(Vector3.right * speed * Time.deltaTime);
        }
        if (moveHorizontal < -0.2)
        {
            rb.AddForce(Vector3.left * speed * Time.deltaTime);
        }
        if (moveVertical > 0.2)
        {
            rb.AddForce(Vector3.forward * speed * Time.deltaTime);
        }
        if (moveVertical < -0.2)
        {
            rb.AddForce(Vector3.back * speed * Time.deltaTime);
        }
        #endif

        rb.velocity = new Vector3(joystick.Horizontal * speed, 0, joystick.Vertical * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Red" || other.tag == "Blue")
        {

            var vfx = Instantiate(MenuManager.instance.vfx, other.transform.position, Quaternion.identity);
            Destroy(vfx, 1f);

            hitTag = other.tag;
            UpdateScore(); 
            Destroy(other.gameObject);
            noOfCube++;

            if (noOfCube == 10)
            {
                GameOver();
            }
        }
    }

    void UpdateScore()
    {
        if (hitTag == "Red")
        {
            streakBlue = 0;
            streakRed++;
            if (streakRed > 1)
            {
                score += 20 * streakRed;
            }
            else
            {
                score += 20;
                streakRed = 1;
            }
        }
        else if (hitTag == "Blue")
        {
            streakRed = 0;
            streakBlue++;
            if (streakBlue > 1)
            {
                score += 15 * streakBlue;
            }
            else
            {
                score += 15;
                streakBlue = 1;
            }
        }
       
        scoreTxt.text = "Score : " + score;
    }

    public void GameOver()
    {
        rb.velocity = Vector3.zero;
        DeletePreviousObject();

        MenuManager.instance.gamePanel.SetActive(false);
        MenuManager.instance.restartBtn.interactable = true;
        MenuManager.instance.backBtn.interactable = true;
        MenuManager.instance.gameOverPanel.SetActive(true);
        MenuManager.instance.gameOverPanel.GetComponent<Animator>().SetBool("IsOn", false);

        finalScoreTxt.text = "Congratulations you gained " + score + " scores";
        if (highScore < score)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            bestScoreTxt.text = ">Best Score< " + PlayerPrefs.GetInt("highScore");
        }
       
    }

    public void Restart()
    {
        MenuManager.instance.gamePlayObj.SetActive(true);
        Spawner.instance.SpawnCubes();
        MenuManager.instance.gamePanel.SetActive(true);
        MenuManager.instance.restartBtn.interactable = false;
        MenuManager.instance.backBtn.interactable = false;
        MenuManager.instance.gameOverPanel.GetComponent<Animator>().SetBool("IsOn", true);
        MenuManager.instance.playBtn.interactable = false;
        MenuManager.instance.playPanel.GetComponent<Animator>().SetBool("IsOn", true);
       
        ResetValues();
    }

    void ResetValues()
    {
        score = 0;
        noOfCube = 0;
        scoreTxt.text = "Score : " + score;
        TimeManager.totalTime = 1;
        TimeManager.secondsLeft = TimeManager.totalTime * 60;
        TimeManager.instance.timerTxt.text = "1:00";
        TimeManager.instance.isTimeUp = false; 

        joystick.handle.anchoredPosition = Vector2.zero;
    }

    void DeletePreviousObject()
    {
        GameObject[] RedTagObject;
        RedTagObject = GameObject.FindGameObjectsWithTag("Red");
        if (RedTagObject != null)
        {
            foreach (GameObject redCube in RedTagObject)
            {
                Destroy(redCube);
            }
        }

        GameObject[] BlueTagObject;
        BlueTagObject = GameObject.FindGameObjectsWithTag("Blue");

        if (BlueTagObject != null)
        {
            foreach (GameObject blueCube in BlueTagObject)
            {
                Destroy(blueCube);
            }
        }
    }
}
