using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    public string foodName;
    public float decayRate;
    public bool spoiled = false;
    public float refillAmt;
    public float spoiledRefillAmt;
    public float shitMod = 5f;

    public override void Update()
    {
        base.Update();
    }
}
