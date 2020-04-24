using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathModifier : PlayerModifier
{
    // Start is called before the first frame update
    public override void Start()
    {
        timeout = 0;
    }

    public override void activate()
    {
        GameObject player = this.GetPlayer();        
        player.GetComponent<PlayerScript>().isDead = true;
    }

    protected override void deactivate()
    {
        return;
    }
}
