using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CloudManager : MonoBehaviour
{
    //Set this variable to your Cloud Prefab through the Inspector
    public Image cloudPrefab1;
    public Image cloudPrefab2;
    public Image cloudPrefab3;
    public GameObject canvas;

    //Set this variable to how often you want the Cloud Manager to make clouds in seconds.
    //For Example, I have this set to 2
    public float delay;

    //If you ever need the clouds to stop spawning, set this variable to false, by doing: CloudManagerScript.spawnClouds = false;
    public static bool spawnClouds = true;

    // Use this for initialization
    void Start()
    {
        //Begin SpawnClouds Coroutine
        StartCoroutine(SpawnClouds());
    }

    IEnumerator SpawnClouds()
    {
        //This will always run
        while (true)
        {
            //Only spawn clouds if the boolean spawnClouds is true
            while (spawnClouds)
            {
                //Instantiate Cloud Prefab and then wait for specified delay, and then repeat
                Image cloud1 = Instantiate(cloudPrefab1) as Image;
                Image cloud2 = Instantiate(cloudPrefab2) as Image;
                Image cloud3 = Instantiate(cloudPrefab3) as Image;
                cloud1.transform.SetParent(canvas.transform, false);
                cloud2.transform.SetParent(canvas.transform, false);
                cloud3.transform.SetParent(canvas.transform, false);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}