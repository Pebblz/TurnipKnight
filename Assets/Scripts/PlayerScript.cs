using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{

    public bool grounded = true;
    public float speed;
    public float MaxSpeed;
    public int score = 0;
    public float defaultSpeed;
    public Rigidbody rigidbody;
    public float timer;
    private float timerStartValue;
    public float accel;
    public float capSpeed;
    public bool dead = false;
    public float fixSuperBoostTimer = 5f;
    private float defaultSuperBoostTimer;
    public AudioClip scoreSound;
    public AudioClip deathSound;
    
    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
        grounded = true;
        rigidbody = GetComponent<Rigidbody>();
        timerStartValue = timer;
        capSpeed = MaxSpeed + 10;
        defaultSuperBoostTimer = fixSuperBoostTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead())
        {

            transform.Translate(new Vector3(speed * Time.deltaTime, 0f, 0f));


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
        }

        else
        {
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
        rigidbody.AddForce(new Vector3(0f, 2, 0f), ForceMode.Impulse);
        grounded = false;
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
