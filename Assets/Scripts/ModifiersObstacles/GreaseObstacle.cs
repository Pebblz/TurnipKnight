using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaseObstacle : Obstacle
{
    public override void Start()
    {
        this.gameObject.AddComponent<SpeedyModifier>();
        this.modifiers.Add(this.gameObject.GetComponent<SpeedyModifier>());
    }
}
