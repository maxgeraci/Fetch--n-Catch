using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpikeyOutfits : MonoBehaviour {

    public Sprite locked;
    public Sprite unLocked;
    public Sprite selected;
    public Image image;
    public string spikeyOutfit;

    public Image spikey1;
    public Image spikey2;
    public Image spikey3;
    public Image spikey4;
    public Image spikey5;
    public Image spikey6;

    // Use this for initialization
    void Start () {
		if (PlayerPrefs.GetInt(spikeyOutfit) == 0)
        {
            image.sprite = locked;
        } else if (PlayerPrefs.GetInt(spikeyOutfit) == 1)
        {
            image.sprite = unLocked;
        } else if (PlayerPrefs.GetInt(spikeyOutfit) == 2)
        {
            image.sprite = selected;
        }
	}

    public void ClickOnOutfit()
    {
        if (PlayerPrefs.GetInt(spikeyOutfit) == 1)
        {
            if (PlayerPrefs.GetInt("Spikey1") == 2)
            {
                PlayerPrefs.SetInt("Spikey1", 1);
                spikey1.sprite = unLocked;
            }

            if (PlayerPrefs.GetInt("Spikey2") == 2)
            {
                PlayerPrefs.SetInt("Spikey2", 1);
                spikey2.sprite = unLocked;
            }

            if (PlayerPrefs.GetInt("Spikey3") == 2)
            {
                PlayerPrefs.SetInt("Spikey3", 1);
                spikey3.sprite = unLocked;
            }

            if (PlayerPrefs.GetInt("Spikey4") == 2)
            {
                PlayerPrefs.SetInt("Spikey4", 1);
                spikey4.sprite = unLocked;
            }

            if (PlayerPrefs.GetInt("Spikey5") == 2)
            {
                PlayerPrefs.SetInt("Spikey5", 1);
                spikey5.sprite = unLocked;
            }

            if (PlayerPrefs.GetInt("Spikey6") == 2)
            {
                PlayerPrefs.SetInt("Spikey6", 1);
                spikey6.sprite = unLocked;
            }

            image.sprite = selected;
            PlayerPrefs.SetInt(spikeyOutfit, 2);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
