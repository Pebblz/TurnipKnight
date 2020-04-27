using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkObstacle : Obstacle
{
    // Start is called before the first frame update
    Vector3 startPos;
    private bool needsToReset = false;
    private bool fallen = false;
    public override void Start()
    {
        startPos = this.transform.position;
        this.gameObject.AddComponent<DeathModifier>();
        this.gameObject.AddComponent<GroundedModifier>();
        this.modifiers.Add(this.gameObject.GetComponent<DeathModifier>());        
        this.modifiers.Add(this.gameObject.GetComponent<GroundedModifier>());
    }

    public void Update()
    {
        if(GameObject.FindGameObjectWithTag("PLAYER").transform.position.x > this.transform.position.x - this.transform.localScale.x && !needsToReset && !fallen)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -5f, 0f), ForceMode.Impulse);
            fallen = true;
        }


        if (needsToReset)
        {
            moveUpToTop();
        }
    }

    public void moveUpToTop()
    {
        float s = 2 * Time.deltaTime;
        if(s + this.transform.position.y > startPos.y)
        {
            needsToReset = false;
        }
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + s, this.transform.position.z);
    }

    public override void AdditionalEffects()
    {
        GameManager.soundSource.pitch = Random.Range(0.8f, 1.2f);
        GameManager.soundSource.PlayOneShot(GameObject.Find("Player").GetComponent<PlayerScript>().deathSound);
        
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PLAYER")
        {
            modifiers[1].activate();
            modifiers[1].isAcivated = true;
        }

        if(collision.gameObject.tag == "FLOOR")
        {
            this.needsToReset = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
    
        
    }
    public override void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "PLAYER")
        {
            modifiers[0].activate();
            modifiers[0].isAcivated = true;
            AdditionalEffects();
        }

    }
}
