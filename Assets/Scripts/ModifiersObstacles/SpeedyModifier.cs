using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyModifier : PlayerModifier
{
    // Start is called before the first frame update
    public override void Start()
    {
        this.timeout = 1f;
    }
    public override void activate()
    {
        GameObject player = this.GetPlayer();
        float newSpeed = player.GetComponent<PlayerScript>().defaultSpeed * 4;
        player.GetComponent<PlayerScript>().speed = newSpeed;
    }

    protected override void deactivate()
    {
        GameObject player = this.GetPlayer();
        float newSpeed = player.GetComponent<PlayerScript>().defaultSpeed;
        player.GetComponent<PlayerScript>().speed = newSpeed;
    }

    
    


}
