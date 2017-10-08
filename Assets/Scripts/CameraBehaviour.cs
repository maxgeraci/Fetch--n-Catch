using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    private GameObject player;
    public GameObject playerCustom1;
    public GameObject playerCustom2;
    public GameObject playerCustom3;
    public GameObject playerCustom4;
    public GameObject playerCustom5;
    public GameObject playerCustom6;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("Spikey1") == 2)
        {
            player = playerCustom1;
            playerCustom1.SetActive(true);
        } else if (PlayerPrefs.GetInt("Spikey2") == 2)
        {
            player = playerCustom2;
            playerCustom2.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Spikey3") == 2)
        {
            player = playerCustom3;
            playerCustom3.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Spikey4") == 2)
        {
            player = playerCustom4;
            playerCustom4.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Spikey5") == 2)
        {
            player = playerCustom5;
            playerCustom5.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Spikey6") == 2)
        {
            player = playerCustom6;
            playerCustom6.SetActive(true);
        }

        offset = transform.position - player.transform.position;

        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
