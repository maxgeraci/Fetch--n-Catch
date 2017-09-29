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

    float timeLeft = 4f;
    float startTime;
    //float boostTime = 3f;
    bool stopTimer;
    bool stopTimerUI;

    int cookieCount;
    string niceTime;

    public GameObject completedScreen;
    public GameObject tryAgainScreen;

    public int starTime1;
    public int starTime2;
    public int starTime3;

    public Image star1;
    public Image star2;
    public Image star3;

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

    private bool boost;
    private bool gameOver;

    public Text countDown;
    private int countDownInt;

    public Text endTime;

    public Text timer;
    private int timerInt;

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
        boost = false;

        stopTimer = false;
        stopTimerUI = true;
        gameOver = false;

        oldSpeed = 0;

        cookieCount = 0;
        this.gameObject.AddComponent<AudioSource>();

        anim["Run"].speed = 0f;
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
            stopTimerUI = false;
            countDown.enabled = false;
        }

        if (!stopTimerUI)
        {
            startTime += Time.deltaTime;
            timerInt = (int)startTime;
            float minutes = Mathf.Floor(timerInt / 60F);
            float seconds = Mathf.Floor(timerInt - minutes * 60);
            niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timer.text = niceTime;
        }

        if (onRunning)
        {
            float amoutToMove = speed * Time.deltaTime;
            transform.Translate(dir * amoutToMove);

            if (onGround && speed > 0)
            {
                rigidBody.GetComponent<Animation>().Play("Run");
            } else if (onGround && speed <= 0)
            {
                rigidBody.GetComponent<Animation>().Play("Idle");
            }
        }

        if (speed > 0)
        {
            newSpeed -= 0.06f;
            speed = oldSpeed + newSpeed;
        } else
        {
            speed = 0;
        }

        anim["Run"].speed = speed * 0.23f;

        if (this.transform.position.y < 0)
        {
            speed = 0;
        }

        if (this.transform.position.y < -3)
        {
            if (!gameOver)
            {
                this.GetComponent<AudioSource>().clip = tryAgainSound;
                this.GetComponent<AudioSource>().volume = 0.7f;
                this.GetComponent<AudioSource>().Play();
                gameOver = true;
            }
            gameObject.SetActive(false);
            frisbee.SetActive(false);
            tryAgainScreen.SetActive(true);
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

    public void Run()
    {
        if (onGround && canJump && speed < 11)
        {
            newSpeed += 1.5f;
            speed = oldSpeed + newSpeed;
        }
    }

    public void Jump()
    {
        if (onGround && canJump)
        {
            rigidBody.velocity += jumpHeight * Vector3.up;
            rigidBody.GetComponent<Animation>().Play("Jump");
            onGround = false;

            if (!boost)
            {
                speed = 9.5f;
                anim["Run"].speed = 2.5f;
            }
            else
            {
                speed = 10.4f;
                anim["Run"].speed = 3.5f;
            }
        }
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
                    } else if(onGround)
                    {
                        speed = oldSpeed + newSpeed;
                    } else if(onGround && speed <= 0)
                    {
                        speed = 3;
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
            stopTimerUI = true;
            endTime.text = niceTime;
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
        if (timerInt < starTime3)
        {
            Debug.Log("3 sterren");
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = true;
        }
        else if (timerInt < starTime2)
        {
            Debug.Log("2 sterren");
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = false;
        }
        else if (timerInt < starTime1)
        {
            Debug.Log("1 ster");
            star1.enabled = true;
            star2.enabled = false;
            star3.enabled = false;
        }
        else
        {
            Debug.Log("Geen sterren");
            star1.enabled = false;
            star2.enabled = false;
            star3.enabled = false;
        }
    }
}
