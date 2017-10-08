using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject tryAgainScreen;

    public GameObject platform;
    public GameObject progressBar;

    public GameObject player;
    public GameObject playerBall;
    public GameObject frisbee;
    public GameObject frisbeeBall;

    public GameObject playerCustom1;
    public GameObject playerCustom2;
    public GameObject playerCustom3;
    public GameObject playerCustom4;
    public GameObject playerCustom5;
    public GameObject playerCustom6;

    private float playerSpeed;
    private float playerBallSpeed;
    private float frisbeeSpeed;
    private float frisbeeBallSpeed;

    private float totalDistance;
    private float totalBallDistance;

    private float playerPosition;
    private float playerPercentagePosition;
    private float frisbeePosition;
    private float frisbeePercentagePosition;

    private bool gameOver;

    //public AudioClip clickSound;
    public AudioClip tryAgainSound;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("Spikey1") == 2)
        {
            player = playerCustom1;
        }
        else if (PlayerPrefs.GetInt("Spikey2") == 2)
        {
            player = playerCustom2;
        }
        else if (PlayerPrefs.GetInt("Spikey3") == 2)
        {
            player = playerCustom3;
        }
        else if (PlayerPrefs.GetInt("Spikey4") == 2)
        {
            player = playerCustom4;
        }
        else if (PlayerPrefs.GetInt("Spikey5") == 2)
        {
            player = playerCustom5;
        }
        else if (PlayerPrefs.GetInt("Spikey6") == 2)
        {
            player = playerCustom6;
        }

        this.gameObject.AddComponent<AudioSource>();

        totalDistance = platform.GetComponent<Collider>().bounds.size.z;
        totalBallDistance = progressBar.GetComponent<Collider>().bounds.size.x;

        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (frisbee.transform.position.z > 0)
        {
            frisbeePercentagePosition = frisbee.transform.position.z / totalDistance * 100;
            frisbeeBall.transform.position = new Vector2((frisbeePercentagePosition / 100 * totalBallDistance) + (progressBar.transform.position.x - totalBallDistance / 2), frisbeeBall.transform.position.y);
        }

        if (player.transform.position.z > 0.83)
        {
            playerPercentagePosition = player.transform.position.z / totalDistance * 100;
            playerBall.transform.position = new Vector2((playerPercentagePosition / 100 * totalBallDistance) + (progressBar.transform.position.x - totalBallDistance / 2), playerBall.transform.position.y);
        }

        if (frisbee.transform.position.z > totalDistance)
        {
            if (!gameOver)
            {
                this.GetComponent<AudioSource>().clip = tryAgainSound;
                this.GetComponent<AudioSource>().volume = 0.3f;
                this.GetComponent<AudioSource>().Play();
                gameOver = true;
            }
            player.SetActive(false);
            frisbee.SetActive(false);
            tryAgainScreen.SetActive(true);
        }
    }

    public void PauseOnClick()
    {
        //this.GetComponent<AudioSource>().clip = clickSound;
        //this.GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }
}
