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
    
    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
        grounded = true;
        rigidbody = GetComponent<Rigidbody>();
        timerStartValue = timer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0f,0f));
       
        if (isTouched() && grounded)
        {
            jump();
           

        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {

            jump();
        }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            speed += accel;
            defaultSpeed = speed;
            timer = timerStartValue;
        }
        print(speed);

    }
    
    public bool isTouched()
    {
        return (Input.touchCount > 0);
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
