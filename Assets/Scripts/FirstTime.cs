using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstTime : MonoBehaviour {

    public GameObject screen;

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("isFirstTime"))
        {
            screen.SetActive(true);
            PlayerPrefs.SetInt("isFirstTime", 1);

        } else
        {
            SceneManager.LoadScene("Menu");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
