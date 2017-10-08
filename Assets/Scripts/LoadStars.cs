using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadStars : MonoBehaviour {

    public Text stars;
    public Text stars2;

	// Use this for initialization
	void Start () {
        int amountStars = PlayerPrefs.GetInt("Stars");
        stars.text = amountStars.ToString();

        if (stars2 != null)
        {
            stars2.text = amountStars.ToString();
        }

        if (amountStars >= 5)
        {
            if (PlayerPrefs.GetInt("Spikey2") != 2)
            {
                PlayerPrefs.SetInt("Spikey2", 1);
            }
        }

        if (amountStars >= 10)
        {
            if (PlayerPrefs.GetInt("Spikey3") != 2)
            {
                PlayerPrefs.SetInt("Spikey3", 1);
            }
        }

        if (amountStars >= 15)
        {
            if (PlayerPrefs.GetInt("Spikey4") != 2)
            {
                PlayerPrefs.SetInt("Spikey4", 1);
            }
        }

        if (amountStars >= 20)
        {
            if (PlayerPrefs.GetInt("Spikey5") != 2)
            {
                PlayerPrefs.SetInt("Spikey5", 1);
            }
        } 
        
        if (amountStars >= 25)
        {
            if (PlayerPrefs.GetInt("Spikey6") != 2)
            {
                PlayerPrefs.SetInt("Spikey6", 1);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
