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

    float timeLeft = 6f;
    //float boostTime = 3f;
    bool stopTimer;

    int cookieCount;

    public GameObject completedScreen;
    public GameObject tryAgainScreen;

    public Image cookie1;
    public Image cookie2;
    public Image cookie3;

    public Sprite noCookie;
    public Sprite yesCookie;
    public string nextLevel;

    private Vector3 dir;
    public float speed;
    public float oldSpeed;
    public float newSpeed;
    public float jumpHeight;
    private bool onGround;
    private bool onRunning;
    private bool canJump;
    public GameObject frisbee;
    public Animation anim;

    //private bool boost;
    private bool gameOver;

    public Text countDown;
    public int countDownInt;

    public AudioClip cookieSound;
    public AudioClip finishSound;
    public AudioClip tryAgainSound;

    private float startTouchPosition;
    private float endTouchPosition;
    private float startClickPosition;
    private float endClickPosition;

    void Start()
    {
        rigidBody = transform.GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();

        dir = Vector3.zero;
        onGround = true;
        onRunning = false;
        canJump = false;
        //boost = false;

        stopTimer = false;
        gameOver = false;

        oldSpeed = 8;

        cookieCount = 0;
        this.gameObject.AddComponent<AudioSource>();

        anim["Run"].speed = 2f;
        anim["Jump"].speed = 1.7f;

        Time.timeScale = 1;
    }

    void Update()
    {
        dir = Vector3.left;

        if (!stopTimer)
        {
            timeLeft -= Time.deltaTime;
            countDownInt = (int)timeLeft;
            countDown.text = countDownInt.ToString();
        }
        if (timeLeft < 1)
        {
            onRunning = true;
            canJump = true;
            timeLeft = 1;
            stopTimer = true;
            countDown.enabled = false;
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
            if (Input.GetMouseButtonDown(0) && newSpeed < 3)
            {
                newSpeed += 0.3f;
                speed = oldSpeed + newSpeed;
                anim["Run"].speed += 0.075f;
            }

            if (newSpeed > 0)
            {
                newSpeed -= 0.015f;
                speed = oldSpeed + newSpeed;
                anim["Run"].speed -= 0.00375f;
            }
        }

        if (onGround && canJump && !IsPointerOverUIObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                startClickPosition = Input.mousePosition.y;
            }
            if (Input.GetMouseButtonUp(0))
            {
                endClickPosition = Input.mousePosition.y;

                if (endClickPosition - startClickPosition > Screen.height / 5)
                {
                    rigidBody.velocity += jumpHeight * Vector3.up;
                    rigidBody.GetComponent<Animation>().Play("Jump");
                    onGround = false;

                    //if (!boost)
                    //{
                    //    speed = 9.5f;
                    //    anim["Run"].speed = 2.5f;
                    //}
                    //else
                    //{
                    //    speed = 10.4f;
                    //    anim["Run"].speed = 3.5f;
                    //}
                }
            }

                for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    startTouchPosition = touch.position.y;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    endTouchPosition = touch.position.y;
                    if (endTouchPosition - startTouchPosition > Screen.height / 5)
                    {
                        rigidBody.velocity += jumpHeight * Vector3.up;
                        rigidBody.GetComponent<Animation>().Play("Jump");
                        onGround = false;

                        //if (!boost)
                        //{
                        //    speed = 9.5f;
                        //    anim["Run"].speed = 2.5f;
                        //}
                        //else
                        //{
                        //    speed = 10.4f;
                        //    anim["Run"].speed = 3.5f;
                        //}
                    }
                }

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
            if (!gameOver)
            {
                this.GetComponent<AudioSource>().clip = tryAgainSound;
                this.GetComponent<AudioSource>().volume = 0.7f;
                this.GetComponent<AudioSource>().Play();
                gameOver = true;
            }
        }

        //if (boost)
        //{
        //    boostTime -= Time.deltaTime;
        //}

        //if (boostTime < 0)
        //{
        //    boost = false;
        //    boostTime = 3f;
        //    speed = 9.5f;
        //    anim["Run"].speed = 2.5f;
        //}
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
            //if (!boost)
            //{
            //    speed = 9.5f;
            //    anim["Run"].speed = 2.5f;
            //}
            //else
            //{
            //    speed = 10.4f;
            //    anim["Run"].speed = 3.5f;
            //}

            if (collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                if (contact.normal.z < 0)
                {
                    if (!onGround)
                    {
                        speed = 0;
                    } else
                    {
                        speed = oldSpeed + newSpeed;
                    }
                    onRunning = false;
                    this.GetComponent<Animation>().Play("Idle");
                }
            }
        }

        if (collision.gameObject.CompareTag("Frisbee"))
        {
            this.GetComponent<AudioSource>().clip = finishSound;
            this.GetComponent<AudioSource>().volume = 0.4f;
            this.GetComponent<AudioSource>().Play();
            Time.timeScale = 0;
            CookieCounter();
            PlayerPrefs.SetInt(nextLevel, 2);
            completedScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            this.GetComponent<AudioSource>().clip = cookieSound;
            this.GetComponent<AudioSource>().volume = 0.6f;
            this.GetComponent<AudioSource>().Play();
            cookieCount++;
        }

        //if (other.gameObject.CompareTag("Boost"))
        //{
        //    Destroy(other.gameObject);
        //    this.GetComponent<AudioSource>().clip = cookieSound;
        //    this.GetComponent<AudioSource>().volume = 0.6f;
        //    this.GetComponent<AudioSource>().Play();
        //    boost = true;
        //    speed = 10.4f;
        //    anim["Run"].speed = 3.5f;
        //}
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
