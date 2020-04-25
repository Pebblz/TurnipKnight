using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkObstacle : Obstacle
{
    // Start is called before the first frame update

    public override void Start()
    {
        this.gameObject.AddComponent<DeathModifier>();

        this.modifiers.Add(this.gameObject.GetComponent<DeathModifier>());        
    }

    public void Update()
    {
        if(GameObject.FindGameObjectWithTag("PLAYER").transform.position.x > this.transform.position.x - this.transform.localScale.x)
        {
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, -0.10f, 0f), ForceMode.Impulse);
        }
    }

    public override void AdditionalEffects()
    {
        GameManager.soundSource.pitch = Random.Range(0.8f, 1.2f);
        GameManager.soundSource.PlayOneShot(GameObject.Find("Player").GetComponent<PlayerScript>().deathSound);
        Destroy(this.gameObject);
    }
}
