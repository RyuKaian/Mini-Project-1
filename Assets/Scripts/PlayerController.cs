using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audios;
    public float speed = 5;
    public float jumpImpulse = 10;
    private bool grounded = false;
    public int boostMeter = 0;
    public float invincibilityTimer;
    public float gameTimer = 60f;
    public float score;

    public bool playing = true;
    public bool gameOver;
    public bool InvinMusic;

    public AudioClip BlueSphere;
    public AudioClip Bomb;
    public AudioClip Coin;
    public AudioClip IronBall;


    float MAX_SWIPE_TIME = 0.5f;
    float MIN_SWIPE_DISTANCE = 0.17f;
    Vector2 startPos;
    float startTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audios = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playing)
        {
            if (gameTimer < 0)
                gameOver = true;

            float _speed = speed;
            score = transform.position.z + 19;

            gameTimer -= Time.deltaTime;
            invincibilityTimer -= Time.deltaTime;

            if (Invincible())
                _speed *= 2;

            if (!grounded)
                _speed /= 1.5f;

            if (Input.touches.Length > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    startPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
                    startTime = Time.time;
                }
                if (t.phase == TouchPhase.Ended)
                {
                    if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
                        return;
                    Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
                    Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);
                    if (swipe.magnitude < MIN_SWIPE_DISTANCE)
                        return;
                    if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                    {
                        if (swipe.x > 0 && transform.position.x < 2)
                            transform.position += new Vector3(3, 0, 0);
                        else if (swipe.x < 0 && transform.position.x > -2)
                            transform.position += new Vector3(-3, 0, 0);
                    }
                    else
                        if (swipe.y > 0 && grounded)
                            rb.AddForce(new Vector3(0, jumpImpulse, 0), ForceMode.Impulse);
                }
            }

            if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && transform.position.x < 2)
                transform.position += new Vector3(3, 0, 0);

            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && transform.position.x > -2)
                transform.position += new Vector3(-3, 0, 0);

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
                rb.AddForce(new Vector3(0, jumpImpulse, 0), ForceMode.Impulse);

            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BlueSphere"))
        {
            Destroy(other.gameObject);
            if (!Invincible())
            {
                boostMeter += 10;
                audios.PlayOneShot(BlueSphere);
            }
            if (boostMeter == 50)
            {
                InvinMusic = true;
                boostMeter = 0;
                invincibilityTimer = 5;
            }
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            audios.PlayOneShot(Coin);
            Destroy(other.gameObject);
            gameTimer += 2f;
        }
        if (other.gameObject.CompareTag("Bomb"))
        {
            if (!Invincible())
            {
                audios.PlayOneShot(Bomb);
                gameOver = true;
            }
            else
                Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("IronBall"))
        {
            audios.PlayOneShot(IronBall);
            Destroy(other.gameObject);

            if (!Invincible())
                gameTimer -= 10f;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            grounded = true;
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
            grounded = false;
    }
    
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOver");
    }

    public bool Invincible()
    {
        if (invincibilityTimer < 0)
            return false;
        else
            return true;
    }
}

