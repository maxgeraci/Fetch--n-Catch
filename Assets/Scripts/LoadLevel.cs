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
        else if (PlayerPrefs.GetInt(level) == 1)
        {
            image.sprite = locked;
            image.GetComponent<EventTrigger>().enabled = false;
        }
        else if (PlayerPrefs.GetInt(level) == 2)
        {
            image.sprite = unLocked;
            image.GetComponent<EventTrigger>().enabled = true;
        }

        //Debug.Log(level + ": " + PlayerPrefs.GetInt(level));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(level);
    }
}
