using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupObstacle : Obstacle
{
    public override void Start()
    {
        this.gameObject.AddComponent<StickyModifier>();
        this.modifiers.Add(this.gameObject.GetComponent<StickyModifier>());
    }
}
