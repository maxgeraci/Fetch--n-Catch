using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Stars"))
        {
            PlayerPrefs.SetInt("Stars", 0);

            PlayerPrefs.SetInt("Spikey1", 2);
            PlayerPrefs.SetInt("Spikey2", 0);
            PlayerPrefs.SetInt("Spikey3", 0);
            PlayerPrefs.SetInt("Spikey4", 0);
            PlayerPrefs.SetInt("Spikey5", 0);
            PlayerPrefs.SetInt("Spikey6", 0);
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
        }

        int totalStars = PlayerPrefs.GetInt("StarsLevel1") + PlayerPrefs.GetInt("StarsLevel2") + PlayerPrefs.GetInt("StarsLevel3") + PlayerPrefs.GetInt("StarsLevel4") + PlayerPrefs.GetInt("StarsLevel5") + PlayerPrefs.GetInt("StarsLevel6") + PlayerPrefs.GetInt("StarsLevel7") + PlayerPrefs.GetInt("StarsLevel8") + PlayerPrefs.GetInt("StarsLevel9") + PlayerPrefs.GetInt("StarsLevel10");

        PlayerPrefs.SetInt("Stars", totalStars);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
