using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchSound : MonoBehaviour {

    public Sprite OffSprite;
    public Sprite OnSprite;
    public Image image;

	// Use this for initialization
	void Start () {
        //Playerpreferences
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeImage()
    {
        if (image.sprite == OnSprite)
        {
            image.sprite = OffSprite;
        } else
        {
            image.sprite = OnSprite;
        }
    }
}
