using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected List<PlayerModifier> modifiers = new List<PlayerModifier>();
    public abstract void Start();

    /// <summary>
    /// Override this method to add additional effects to the object
    /// </summary>
    public virtual void AdditionalEffects() 
    {
        return;
    }


    private void OnTriggerEnter(Collider collision)
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

}
