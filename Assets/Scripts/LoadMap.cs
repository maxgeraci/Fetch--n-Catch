using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadMap : MonoBehaviour {

    public Sprite unLocked;
    public Sprite locked;
    public Image image;

    // Use this for initialization
    void Start ()
    {
		if (PlayerPrefs.GetInt("Level6") == 2)
        {
            image.sprite = unLocked;
            image.GetComponent<EventTrigger>().enabled = true;
        } else if (PlayerPrefs.GetInt("Level6") == 1 || PlayerPrefs.GetInt("Level6") == 0)
        {
            image.GetComponent<EventTrigger>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
