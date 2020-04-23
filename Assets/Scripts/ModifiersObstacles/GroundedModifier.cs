using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedModifier : PlayerModifier
{
    public override void activate()
    {
        GameObject player = this.GetPlayer();
        player.GetComponent<PlayerScript>().grounded = true;
    }

    public override void Start()
    {
        this.timeout = 0f;
    }

    protected override void deactivate()
    {
        return;
    }
}
