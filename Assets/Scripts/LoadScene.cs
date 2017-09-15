using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string scene;

    public void LoadNextScene()
    {
        //if(scene == "Maps" || scene == "Levels Park")
        //{
        //    Initiate.Fade(scene, Color.white, 4.0f);
        //}
        //else
        //{
        //    SceneManager.LoadScene(scene);
        //}

        Time.timeScale = 1;
        //SceneManager.LoadScene(scene);
        Initiate.Fade(scene, Color.white, 3.0f);
    }

    public void LinkURL()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.SpicyTuna");
    }
}
