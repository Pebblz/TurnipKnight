using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObstacle : Obstacle
{
    public override void Start()
    {
        this.gameObject.AddComponent<CollectableModifier>();
        modifiers.Add(this.gameObject.GetComponent<CollectableModifier>());
    }

    public override void AdditionalEffects()
    {
        Destroy(this.gameObject);
    }
}
