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
        float newSpeed = player.GetComponent<PlayerScript>().defaultSpeed / 2;
        player.GetComponent<PlayerScript>().speed = newSpeed;
    }

    protected override void deactivate()
    {
        GameObject player = this.GetPlayer();
        float newSpeed = player.GetComponent<PlayerScript>().defaultSpeed * 2;
        player.GetComponent<PlayerScript>().speed = newSpeed;
    }



}
