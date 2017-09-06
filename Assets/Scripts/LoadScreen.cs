using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreen : MonoBehaviour {

    public GameObject firstScreen;
    public GameObject secondScreen;

	public void LoadNextScreen()
    {
        secondScreen.SetActive(true);
        firstScreen.SetActive(false);
    }
}
