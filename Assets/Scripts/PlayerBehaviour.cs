using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody rigidBody;

    float timeLeft = 3f;
    bool stopTimer = false;

    int cookieCount;

    public GameObject completedScreen;
    public GameObject tryAgainScreen;

    public Image cookie1;
    public Image cookie2;
    public Image cookie3;

    public Sprite noCookie;
    public Sprite yesCookie;

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

        cookieCount = 0;

        anim["Run"].speed = 2.1f;
        anim["Jump"].speed = 1.7f;

        Time.timeScale = 1;
    }

    void Update()
    {
        dir = Vector3.left;

        if (!stopTimer)
        {
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft < 0)
        {
            onRunning = true;
            canJump = true;
            timeLeft = 1;
            stopTimer = true;
        }

        if (onRunning)
        {
            float amoutToMove = speed * Time.deltaTime;
            transform.Translate(dir * amoutToMove);

            if (onGround)
            {
                rigidBody.GetComponent<Animation>().Play("Run");
            }
        }

        if (onGround && canJump && !IsPointerOverUIObject())
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                rigidBody.velocity += jumpHeight * Vector3.up;
                onGround = false;

                rigidBody.GetComponent<Animation>().Play("Jump");
            }
        }

        if (this.transform.position.y < 0)
        {
            speed = 0;
        }

        if (this.transform.position.y < -3)
        {
            Time.timeScale = 0;
            tryAgainScreen.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(Physics.gravity * rigidBody.mass * 2.5f);
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
            Time.timeScale = 0;
            CookieCounter();
            completedScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            cookieCount++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onRunning = true;
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void CookieCounter()
    {
        if (cookieCount == 0)
        {
            cookie1.sprite = noCookie;
            cookie2.sprite = noCookie;
            cookie3.sprite = noCookie;
        } else if (cookieCount == 1)
        {
            cookie1.sprite = yesCookie;
            cookie2.sprite = noCookie;
            cookie3.sprite = noCookie;
        } else if (cookieCount == 2)
        {
            cookie1.sprite = yesCookie;
            cookie2.sprite = yesCookie;
            cookie3.sprite = noCookie;

        } else if (cookieCount == 3)
        {
            cookie1.sprite = yesCookie;
            cookie2.sprite = yesCookie;
            cookie3.sprite = yesCookie;
        }
    }
}
