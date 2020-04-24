using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyModifier : PlayerModifier
{
    private bool started = false;
    public override void Start()
    {
        this.timeout = 3.0f;
  
    }


    public override void coolDown()
    {
        if (isPlayerGrounded() || started)
        {
            started = true;
            base.coolDown();
        }
        
    }

    public bool isPlayerGrounded()
    {
        GameObject player = GetPlayer();
        return player.GetComponent<PlayerScript>().grounded;
    }
    public override void activate()
    {
        GameObject player = this.GetPlayer();
        float newSpeed = player.GetComponent<PlayerScript>().defaultSpeed / 2;
        player.GetComponent<PlayerScript>().speed = newSpeed;
    }

    protected override void deactivate()
    {
        GameObject player = this.GetPlayer();
        float newSpeed = player.GetComponent<PlayerScript>().defaultSpeed;
        player.GetComponent<PlayerScript>().speed = newSpeed;
        this.started = false;
    }



}
