using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObstacle : Obstacle
{
    public override void Start()
    {
        this.gameObject.AddComponent<CollectableModifier>();
        this.gameObject.AddComponent<ParticleEffectModifier>();
        modifiers.Add(this.gameObject.GetComponent<CollectableModifier>());
        modifiers.Add(this.gameObject.GetComponent<ParticleEffectModifier>());
    }

    public override void AdditionalEffects()
    {
        Destroy(this.gameObject);
    }
}
