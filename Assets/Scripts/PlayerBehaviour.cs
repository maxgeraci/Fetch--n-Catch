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
    public Animation anim;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();

        dir = Vector3.zero;
        onGround = true;
        onRunning = false;
        canJump = false;

        anim["Run"].speed = 2.0f;
        anim["Jump"].speed = 1.5f;
    }

    void FixedUpdate()
    {
        if (onRunning)
        {
            float amoutToMove = speed * Time.deltaTime;
            transform.Translate(dir * amoutToMove);

            if (onGround)
            {
                rigidBody.GetComponent<Animation>().Play("Run");
            }
        }

        rigidBody.AddForce(Physics.gravity * rigidBody.mass * 2.0f);

        if (onGround && canJump)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                rigidBody.velocity += jumpHeight * Vector3.up;
                onGround = false;

                rigidBody.GetComponent<Animation>().Play("Jump");
            }
        }
    }

    void Update()
    {
        dir = Vector3.left;

        if (frisbee.transform.position.z > 14.9)
        {
            if (frisbee.transform.position.z < 15)
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
                    this.GetComponent<Animation>().Play("Idle");
                }

            }
        }

        if (collision.gameObject.CompareTag("Frisbee"))
        {
            Debug.Log("You win!");
            SceneManager.LoadScene("Level1");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
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
