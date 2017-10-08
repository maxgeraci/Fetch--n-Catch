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
    bool stopTimer;
    bool stopTimerUI;

    bool tapTimerBool;
    float tapTimer = 5f;

    string niceTime;

    public Image bar;
    private float barDistance;
    public Image star;

    private int stars;

    public GameObject completedScreen;
    public GameObject tryAgainScreen;
    public GameObject tapScreen;

    public Image video;

    public int starTime;

    public Image star1;
    public Image star2;
    public Image star3;

    public string nextLevel;
    public string levelStars;

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

    bool slowdown;

    private bool boost;
    private bool gameOver;

    public Text countDown;

    public Text endTime;

    public Text timer;
    private int timerInt;

    public AudioClip boostSound;
    public AudioClip slowDownSound;
    public AudioClip quickTimeSound;
    public AudioClip tryAgainSound;
    public AudioClip jumpSound;
    public AudioClip countDownSound;
    public AudioClip countDownGoSound;
    public AudioClip levelCompleteSound;

    private float startTouchPosition;
    private float endTouchPosition;
    private float startClickPosition;
    private float endClickPosition;

    void Start()
    {
        video.enabled = true;
        rigidBody = transform.GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();

        dir = Vector3.zero;
        onGround = true;
        onRunning = false;
        canJump = false;
        boost = false;

        slowdown = false;

        stopTimer = false;
        stopTimerUI = true;
        gameOver = false;
        tapTimerBool = false;

        stars = 0;

        oldSpeed = 0;

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
            countDown.text = "GO!";

            if (timeLeft < 4 && timeLeft > 3.9)
            {
                this.GetComponent<AudioSource>().clip = countDownSound;
                this.GetComponent<AudioSource>().volume = 0.1f;
                this.GetComponent<AudioSource>().Play();
            }

            if (timeLeft < 3 && timeLeft > 2.9)
            {
                this.GetComponent<AudioSource>().clip = countDownSound;
                this.GetComponent<AudioSource>().volume = 0.1f;
                this.GetComponent<AudioSource>().Play();
            }

            if (timeLeft < 2 && timeLeft > 1.9)
            {
                this.GetComponent<AudioSource>().clip = countDownSound;
                this.GetComponent<AudioSource>().volume = 0.1f;
                this.GetComponent<AudioSource>().Play();
            }

            if (timeLeft < 1 && timeLeft > 0.9)
            {
                this.GetComponent<AudioSource>().clip = countDownGoSound;
                this.GetComponent<AudioSource>().volume = 0.2f;
                this.GetComponent<AudioSource>().Play();
            }
        }
        if (timeLeft < 1)
        {
            video.enabled = false;
            onRunning = true;
            canJump = true;
            stopTimerUI = false;
        }

        if (timeLeft < 0.5)
        {
            timeLeft = 1;
            stopTimer = true;
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

        if (speed > 0 && !slowdown)
        {
            newSpeed -= 0.06f; ;
            speed = oldSpeed + newSpeed;
        } else if (speed > 0 && slowdown)
        {
            newSpeed -= 0.06f; ;
            speed = oldSpeed + newSpeed;
            slowdown = false;
        }
        else
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
                this.GetComponent<AudioSource>().volume = 0.3f;
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

        if (tapTimerBool)
        {
            tapTimer -= Time.deltaTime;
        }

        if (tapTimer < 0)
        {
            tapTimerBool = false;
            tapTimer = 1;
            tapScreen.SetActive(false);
            StarCounter();
            completedScreen.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.AddForce(Physics.gravity * rigidBody.mass * 2.5f);
    }

    public void TapForStar()
    {
        if (this.isActiveAndEnabled)
        {
            star.transform.position = new Vector2(star.transform.position.x + (bar.GetComponent<Collider>().bounds.size.x * 0.03f), star.transform.position.y);

            if (star.transform.position.x > Screen.width / 2 + bar.GetComponent<Collider>().bounds.size.x / 2)
            {
                tapTimerBool = false;
                tapScreen.SetActive(false);
                stars += 1;
                StarCounter();
                completedScreen.SetActive(true);
            }
        }
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
            this.GetComponent<AudioSource>().clip = jumpSound;
            this.GetComponent<AudioSource>().volume = 0.1f;
            this.GetComponent<AudioSource>().Play();

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

        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
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
            this.GetComponent<AudioSource>().clip = quickTimeSound;
            this.GetComponent<AudioSource>().volume = 0.1f;
            this.GetComponent<AudioSource>().Play();
            stopTimerUI = true;
            endTime.text = niceTime;
            frisbee.SetActive(false);
            PlayerPrefs.SetInt(nextLevel, 2);
            stars += 1;

            if (timerInt < starTime)
            {
                stars += 1;
            }
            tapScreen.SetActive(true);
            tapTimerBool = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boost"))
        {
            Destroy(other.gameObject);
            this.GetComponent<AudioSource>().clip = boostSound;
            this.GetComponent<AudioSource>().volume = 0.1f;
            this.GetComponent<AudioSource>().Play();
            boost = true;
            newSpeed += 5;
        }

        if (other.gameObject.CompareTag("Slowdown"))
        {
            Destroy(other.gameObject);
            this.GetComponent<AudioSource>().clip = slowDownSound;
            this.GetComponent<AudioSource>().volume = 0.1f;
            this.GetComponent<AudioSource>().Play();
            slowdown = true;
            if (newSpeed - 5 > 1)
            {
                newSpeed -= 5;
            }
            else if (newSpeed - 5 <= 1 && newSpeed - 5 > 0)
            {
                newSpeed -= 4;
            } else if (newSpeed - 5 <= 0 && newSpeed - 5 > -1)
            {
                newSpeed -= 3;
            }
            else if (newSpeed - 5 <= -1 && newSpeed - 5 > -2)
            {
                newSpeed -= 2;
            }
            else if (newSpeed - 5 <= -2 && newSpeed - 5 > -3)
            {
                newSpeed -= 1;
            }
            else
            {
                newSpeed -= 0;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            onRunning = true;
        }
    }

    public void StarCounter()
    {
        if (stars == 1)
        {
            star1.enabled = true;
            star2.enabled = false;
            star3.enabled = false;

            if (PlayerPrefs.GetInt(levelStars) == 0 || PlayerPrefs.GetInt(levelStars) == 1 || PlayerPrefs.GetInt(levelStars) == 2)
            {
                PlayerPrefs.SetInt(levelStars, 1);
            }
        } else if (stars == 2)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = false;

            if (PlayerPrefs.GetInt(levelStars) == 0 || PlayerPrefs.GetInt(levelStars) == 1)
            {
                PlayerPrefs.SetInt(levelStars, 2);
            }
        }
        else if (stars == 3)
        {
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = true;

            PlayerPrefs.SetInt(levelStars, 3);
        }

        int totalStars = PlayerPrefs.GetInt("StarsLevel1") + PlayerPrefs.GetInt("StarsLevel2") + PlayerPrefs.GetInt("StarsLevel3") + PlayerPrefs.GetInt("StarsLevel4") + PlayerPrefs.GetInt("StarsLevel5") + PlayerPrefs.GetInt("StarsLevel6") + PlayerPrefs.GetInt("StarsLevel7") + PlayerPrefs.GetInt("StarsLevel8") + PlayerPrefs.GetInt("StarsLevel9") + PlayerPrefs.GetInt("StarsLevel10") + PlayerPrefs.GetInt("StarsLevel11") + PlayerPrefs.GetInt("StarsLevel12") + PlayerPrefs.GetInt("StarsLevel13") + PlayerPrefs.GetInt("StarsLevel14") + PlayerPrefs.GetInt("StarsLevel15") + PlayerPrefs.GetInt("StarsLevel16") + PlayerPrefs.GetInt("StarsLevel17") + PlayerPrefs.GetInt("StarsLevel18") + PlayerPrefs.GetInt("StarsLevel19") + PlayerPrefs.GetInt("StarsLevel20");

        PlayerPrefs.SetInt("Stars", totalStars);
    }
}
