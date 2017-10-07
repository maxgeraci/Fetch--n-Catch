using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("isFirstTime"))
        {
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
