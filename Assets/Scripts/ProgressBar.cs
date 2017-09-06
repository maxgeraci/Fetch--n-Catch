using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
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

    // Use this for initialization
    void Start()
    {
        totalDistance = platform.GetComponent<Collider>().bounds.size.z;
        totalBallDistance = progressBar.GetComponent<Collider>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (frisbee.transform.position.z > 0)
        {
            frisbeePercentagePosition = frisbee.transform.position.z / totalDistance * 100;
            frisbeeBall.transform.position = new Vector2((frisbeePercentagePosition / 100 * totalBallDistance) + (progressBar.transform.position.x - totalBallDistance / 2), frisbeeBall.transform.position.y);
        }

        if (frisbee.transform.position.z > 14.9)
        {
            playerPercentagePosition = player.transform.position.z / totalDistance * 100;
            playerBall.transform.position = new Vector2((playerPercentagePosition / 100 * totalBallDistance) + (progressBar.transform.position.x - totalBallDistance / 2), playerBall.transform.position.y);
        }
    }
}
