﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelloObstacle : Obstacle
{
    public override void Start()
    {
        this.gameObject.AddComponent<BouncyModifier>();
        this.modifiers.Add(this.gameObject.GetComponent<BouncyModifier>());
        
    }


}
