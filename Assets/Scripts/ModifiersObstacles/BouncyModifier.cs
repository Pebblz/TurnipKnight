using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyModifier : PlayerModifier
{


    public override void Start()
    {
        this.timeout = 0.1f;
    }
    public override void activate()
    {
        GameObject Player = this.GetPlayer();
        Vector3 bounceForce = new Vector3(0, 4, 0);
        Player.GetComponent<PlayerScript>().rigidbody.AddForce(bounceForce, ForceMode.Impulse);
        Player.GetComponent<PlayerScript>().grounded = false;


    }


    //no cleanup needed for bounce functionality
    protected override void deactivate()
    {
        return;
    }


}
