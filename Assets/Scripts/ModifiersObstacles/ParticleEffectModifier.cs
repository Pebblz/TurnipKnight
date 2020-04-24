using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectModifier : PlayerModifier
{
    public ParticleSystem[] particles;
    // Start is called before the first frame update
    public override void activate()
    {
        Instantiate(particles[0], gameObject.transform.position, Quaternion.identity);
    }

    public override void Start()
    {
        particles = Resources.LoadAll<ParticleSystem>("ParticleEffects");
        this.timeout = 0;
    }

    protected override void deactivate()
    {
        return;
    }
}
