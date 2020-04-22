using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyModifier : PlayerModifier
{

    private void Start()
    {
        this.timeout = 3.0f;
  
    }



    public override void activate()
    {
        GameObject player = this.GetPlayer();
        player.GetComponent<PlayerScript>().speed /= 2;
    }

    protected override void deactivate()
    {
        GameObject player = this.GetPlayer();
        player.GetComponent<PlayerScript>().speed *= 2;
    }



}
