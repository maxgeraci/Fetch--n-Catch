﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public AudioClip clickSound;

    // Use this for initialization
    void Start()
    {
        this.gameObject.AddComponent<AudioSource>();

        totalDistance = platform.GetComponent<Collider>().bounds.size.z;
        totalBallDistance = progressBar.GetComponent<Collider>().bounds.size.x;
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
            Time.timeScale = 0;
            tryAgainScreen.SetActive(true);
        }
    }

    public void PauseOnClick()
    {
        this.GetComponent<AudioSource>().clip = clickSound;
        this.GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }
}