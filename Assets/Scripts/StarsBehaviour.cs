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

        int totalStars = PlayerPrefs.GetInt("StarsLevel1") + PlayerPrefs.GetInt("StarsLevel2") + PlayerPrefs.GetInt("StarsLevel3") + PlayerPrefs.GetInt("StarsLevel4") + PlayerPrefs.GetInt("StarsLevel5") + PlayerPrefs.GetInt("StarsLevel6") + PlayerPrefs.GetInt("StarsLevel7") + PlayerPrefs.GetInt("StarsLevel8") + PlayerPrefs.GetInt("StarsLevel9") + PlayerPrefs.GetInt("StarsLevel10") + PlayerPrefs.GetInt("StarsLevel11") + PlayerPrefs.GetInt("StarsLevel12") + PlayerPrefs.GetInt("StarsLevel13") + PlayerPrefs.GetInt("StarsLevel14") + PlayerPrefs.GetInt("StarsLevel15") + PlayerPrefs.GetInt("StarsLevel16") + PlayerPrefs.GetInt("StarsLevel17") + PlayerPrefs.GetInt("StarsLevel18") + PlayerPrefs.GetInt("StarsLevel19") + PlayerPrefs.GetInt("StarsLevel20");

        PlayerPrefs.SetInt("Stars", totalStars);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
