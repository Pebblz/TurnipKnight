using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerModifier : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeout = 0f;
    public bool isAcivated = false;


    public GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("PLAYER");
    }
    
    public void coolDown()
    {
        if (isAcivated)
        {
            
            if (timeout <= 0)
            {
                isAcivated = false;
                deactivate();
            }
            else if (timeout > 0)
            {
                timeout -= Time.deltaTime;
            }
        }
    }
    private void FixedUpdate()
    {
        this.coolDown();  
    }
    /// <summary>
    /// This function performs any mathmatical mutations to the player when
    /// the player comes in contact has
    /// i.e slowing speed, 
    /// </summary>
    public abstract void activate();

    /// <summary>
    /// gets called after activate returns false. Does any cleanup that mutated the player
    /// Basically the undo action of the activate function
    /// </summary>
    protected abstract void deactivate();


}
