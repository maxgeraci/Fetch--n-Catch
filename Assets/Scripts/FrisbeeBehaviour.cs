using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeBehaviour : MonoBehaviour {

    private Vector3 dir;
    public float speed;

    // Use this for initialization
    void Start () {
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    private void FixedUpdate()
    {
        dir = Vector3.forward;

        float amoutToMove = speed * Time.deltaTime;
        transform.Translate(dir * amoutToMove);
    }
}
