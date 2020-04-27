using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyModifier : PlayerModifier
{
    public float bounce = 4f;

    public override void Start()
    {
        this.timeout = 0.1f;
    }
    public override void activate()
    {
        GameObject Player = this.GetPlayer();
        Vector3 bounceForce = new Vector3(0f, this.bounce, 0f);
        Player.GetComponent<PlayerScript>().rigidbody.AddForce(bounceForce, ForceMode.Impulse);
        Player.GetComponent<PlayerScript>().grounded = false;
        Player.GetComponent<PlayerScript>().anim.SetBool("jump", true);
        GetComponent<Animator>().SetBool("jello", true);
    }


    //no cleanup needed for bounce functionality
    protected override void deactivate()
    {
        return;
    }


}
