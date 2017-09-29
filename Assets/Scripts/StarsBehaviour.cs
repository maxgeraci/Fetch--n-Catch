using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Stars"))
        {
            PlayerPrefs.SetInt("Stars", 0);
        }

        if (!PlayerPrefs.HasKey("StarsLevel1"))
        {
            PlayerPrefs.SetInt("StarsLevel1", 0);
            PlayerPrefs.SetInt("StarsLevel2", 0);
            PlayerPrefs.SetInt("StarsLevel3", 0);
            PlayerPrefs.SetInt("StarsLevel4", 0);
            PlayerPrefs.SetInt("StarsLevel5", 0);
            PlayerPrefs.SetInt("StarsLevel6", 0);
            PlayerPrefs.SetInt("StarsLevel7", 0);
            PlayerPrefs.SetInt("StarsLevel8", 0);
            PlayerPrefs.SetInt("StarsLevel9", 0);
            PlayerPrefs.SetInt("StarsLevel10", 0);
            PlayerPrefs.SetInt("StarsLevel11", 0);
            PlayerPrefs.SetInt("StarsLevel12", 0);
            PlayerPrefs.SetInt("StarsLevel13", 0);
            PlayerPrefs.SetInt("StarsLevel14", 0);
            PlayerPrefs.SetInt("StarsLevel15", 0);
            PlayerPrefs.SetInt("StarsLevel16", 0);
            PlayerPrefs.SetInt("StarsLevel17", 0);
            PlayerPrefs.SetInt("StarsLevel18", 0);
            PlayerPrefs.SetInt("StarsLevel19", 0);
            PlayerPrefs.SetInt("StarsLevel20", 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
