using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableModifier : PlayerModifier
{
    // Start is called before the first frame update
    public override void activate()
    {
        GameObject player = this.GetPlayer();
        player.GetComponent<PlayerScript>().score += 100;
    }

    public override void Start()
    {
        this.timeout = 0;
    }

    protected override void deactivate()
    {
        return;
    }
}
