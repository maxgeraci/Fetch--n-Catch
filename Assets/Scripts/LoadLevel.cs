using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadLevel : MonoBehaviour {

    public Sprite unLocked;
    public Sprite locked;
    public Image image;
    public string level;

    public Image star1;
    public Image star2;
    public Image star3;

    public string starsLevel;

    // Use this for initialization
    void Start () {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey(level))
        {
            if (level == "Level1")
            {
                PlayerPrefs.SetInt(level, 2);
                image.sprite = unLocked;
            } else
            {
                PlayerPrefs.SetInt(level, 1);
                image.sprite = locked;
            }
        }
        else if (PlayerPrefs.GetInt(level) == 1 || PlayerPrefs.GetInt(level) == 0)
        {
            image.sprite = locked;
            image.GetComponent<EventTrigger>().enabled = false;
        }
        else if (PlayerPrefs.GetInt(level) == 2)
        {
            image.sprite = unLocked;
            image.GetComponent<EventTrigger>().enabled = true;
        }


        if (PlayerPrefs.GetInt(starsLevel) == 1)
        {
            star1.enabled = true;
            star2.enabled = false;
            star3.enabled = false;
        }
        else if (PlayerPrefs.GetInt(starsLevel) == 2)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = false;
        }
        else if (PlayerPrefs.GetInt(starsLevel) == 3)
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

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(level);
    }
}
