﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseScreen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResumeOnClick()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
