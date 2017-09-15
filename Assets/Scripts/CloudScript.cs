using UnityEngine;
using System.Collections;

public class CloudScript : MonoBehaviour
{
    //Set these variables to whatever you want the slowest and fastest speed for the clouds to be, through the inspector.
    //If you don't want clouds to have randomized speed, just set both of these to the same number.
    //For Example, I have these set to 2 and 5
    public float minSpeed;
    public float maxSpeed;

    float speed;
    public bool beginCloud;

    void Start()
    {
        //Set Cloud Movement Speed, and Position to random values within range defined above
        speed = Random.Range(minSpeed, maxSpeed);
        if (!beginCloud)
        {
            transform.position = new Vector3(Random.Range(-150, -200), Random.Range(-70, 70), transform.position.z);
        } else
        {
            transform.position = new Vector3(Random.Range(-100, 100), Random.Range(-70, 70), transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Translates the cloud to the right at the speed that is selected
        transform.Translate(speed * Time.deltaTime, 0, 0);
        //If cloud is off Screen, Destroy it.
        if (transform.position.x > 150)
        {
            Destroy(gameObject);
        }
    }
}