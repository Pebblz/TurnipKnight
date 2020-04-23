using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObstacle : Obstacle
{
    public override void Start()
    {
        this.gameObject.AddComponent<GroundedModifier>();
        this.modifiers.Add(this.gameObject.GetComponent<GroundedModifier>());
    }
}
