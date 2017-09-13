using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string scene;

    public void LoadNextScene()
    {
        Initiate.Fade(scene, Color.white, 3.0f);
        //SceneManager.LoadScene(scene);
    }

    public void LinkURL()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.SpicyTuna");
    }
}
