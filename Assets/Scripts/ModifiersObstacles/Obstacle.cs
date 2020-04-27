using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected List<PlayerModifier> modifiers = new List<PlayerModifier>();
    public GameObject player;
    public abstract void Start();

    /// <summary>
    /// Override this method to add additional effects to the object
    /// </summary>
    public virtual void AdditionalEffects() 
    {
        return;
    }

    public virtual void Update()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");

        if (this.transform.position.x <=  player.transform.position.x - Screen.width)
        {
            Destroy(this.gameObject);
        }
    }


    public virtual void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "PLAYER")
        {
             for(int i = 0; i < modifiers.Count; i++)
            {
                modifiers[i].activate();
                modifiers[i].isAcivated = true;
            }
            AdditionalEffects();
        }
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PLAYER")
        {
            for (int i = 0; i < modifiers.Count; i++)
            {
                modifiers[i].activate();
                modifiers[i].isAcivated = true;
            }
            AdditionalEffects();
        }
    }

}
