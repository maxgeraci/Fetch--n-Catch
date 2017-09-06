using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSound : MonoBehaviour {

    public Sprite OffSprite;
    public Sprite OnSprite;
    public Image image;

    private string firstTime;

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
            //Make sound
        } else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            //Make sound
            image.sprite = OnSprite;
        } else if (PlayerPrefs.GetInt("Sound") == 2)
        {
            //Mute sound
            image.sprite = OffSprite;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeImage()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            image.sprite = OffSprite;
            PlayerPrefs.SetInt("Sound", 2);
            //Make sound
        } else if (PlayerPrefs.GetInt("Sound") == 2)
        {
            image.sprite = OnSprite;
            PlayerPrefs.SetInt("Sound", 1);
            //Mute sound
        }
    }
}
