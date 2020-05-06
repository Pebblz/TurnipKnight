using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{

    public bool grounded = true;
    public float speed;
    public float MaxSpeed;
    public int score = 0;
    public float defaultSpeed;
    public Rigidbody rigidbody;
    public float timer;
    public float deathTimer;
    private float timerStartValue;
    public float accel;
    public float capSpeed;
    public bool dead = false;
    public float fixSuperBoostTimer = 5f;
    private float defaultSuperBoostTimer;
    public AudioClip scoreSound;
    public AudioClip deathSound;
    public Animator anim;
    public bool checkedHighScore = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
        grounded = true;
        rigidbody = GetComponent<Rigidbody>();
        timerStartValue = timer;
        capSpeed = MaxSpeed + 10;
        defaultSuperBoostTimer = fixSuperBoostTimer;
        anim = GetComponent<Animator>();
        deathTimer = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead())
        {

            transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));


            if (isTouched() && grounded)
            {
                jump();
                

            }
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                jump();
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (defaultSpeed != MaxSpeed)
                {
                    defaultSpeed += accel;
                }
                timer = timerStartValue;
            }

            if (speed > capSpeed)
            {
                speed = capSpeed;

            }

            if (speed == capSpeed)
            {
                fixSuperBoostTimer -= Time.deltaTime;
            }


            if (fixSuperBoostTimer <= 0)
            {
                speed = defaultSpeed;
                fixSuperBoostTimer = defaultSuperBoostTimer;
            }

            if (gameObject.transform.position.y <= -10)
            {
                dead = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }

            if (grounded)
            {
                anim.SetBool("jump", false);
            }
        }


        else
        {
            deathTimer -= Time.deltaTime;
            anim.SetBool("death", true);

            if (deathTimer <= 0 || gameObject.transform.position.y <= -10)
            {
                GameObject.Find("Main Camera").GetComponent<CameraScript>().positionBiasX = 0;

                if(!checkedHighScore)
                {
                    int highScore = GameManager.GetHighScore();
                    if (score > highScore)
                    {
                        Debug.Log("new Highscore");
                        GameManager.SaveHighScore(score);
                        checkedHighScore = true;
                    }
                }
        
                GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + score;
                GameObject.Find("GameOverCanvas").GetComponent<Canvas>().enabled = true;
            }

            //play death animation
            //freeze posion or turn off gravity after player falls off bottom of screen 
        }
    }
    
    public bool isTouched()
    {
        return (Input.touchCount > 0);
    }

    public bool isDead()
    {
        return dead;
    }


    public void jump()
    {
        rigidbody.AddForce(new Vector3(0f, 3, 0f), ForceMode.Impulse);
        grounded = false;
        anim.SetBool("jump", true);
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        //landing after jump
        if (collision.gameObject.tag == "FLOOR")
        {
            grounded = true;
        }
    }
}
