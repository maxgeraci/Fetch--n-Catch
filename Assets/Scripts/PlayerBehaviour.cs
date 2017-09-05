using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody rigidBody;

    private Vector3 dir;
    public float speed;
    public float jumpHeight;
    private bool onGround;
    private bool onRunning;
    private bool canJump;
    public GameObject frisbee;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody>();

        dir = Vector3.zero;
        onGround = true;
        onRunning = false;
        canJump = false;
    }

    void FixedUpdate()
    {
        if (onRunning)
        {
            float amoutToMove = speed * Time.deltaTime;
            transform.Translate(dir * amoutToMove);

            //Running animation
        }

        rigidBody.AddForce(Physics.gravity * rigidBody.mass * 2.0f);

        if (onGround && canJump)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                rigidBody.velocity += jumpHeight * Vector3.up;
                onGround = false;

                //Jump animation
            }
        }
    }

    void Update()
    {
        dir = Vector3.left;

        if (frisbee.transform.position.z < -9.9)
        {
            if (frisbee.transform.position.z > -10)
            {
                onRunning = true;
                canJump = true;
            }
        }

        //if (onRunning)
        //{
        //    float amoutToMove = speed * Time.deltaTime;
        //    transform.Translate(dir * amoutToMove);
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if (contact.normal.y > 0)
                {
                    onGround = true;
                }
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            speed = 7;
            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if (contact.normal.z < 0)
                {
                    speed = 1.5f;
                    onRunning = false;
                }

            }
        }

        if (collision.gameObject.CompareTag("Frisbee"))
        {
            Debug.Log("You win!");
            Time.timeScale = 0;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onRunning = true;
        }
    }
}
