using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelloObstacle : Obstacle
{
    public float bounceForce = 4f;
    public override void Start()
    {
        this.gameObject.AddComponent<BouncyModifier>();
        this.gameObject.GetComponent<BouncyModifier>().bounce = bounceForce;
        this.modifiers.Add(this.gameObject.GetComponent<BouncyModifier>());
        
    }


}
