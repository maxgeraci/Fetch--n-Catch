using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel1 : MonoBehaviour {

    public Image star1;
    public Image star2;
    public Image star3;

    // Use this for initialization
    void Start () {
		if (PlayerPrefs.GetInt("StarsLevel1") == 1)
        {
            star1.enabled = true;
            star2.enabled = false;
            star3.enabled = false;
        }
        else if (PlayerPrefs.GetInt("StarsLevel1") == 2)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = false;
        }
        else if (PlayerPrefs.GetInt("StarsLevel1") == 3)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = true;
        } else
        {
            star1.enabled = false;
            star2.enabled = false;
            star3.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
