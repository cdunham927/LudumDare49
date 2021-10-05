using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : Action
{
    Toy toy;

    public Play(Toy t, float ttc, float dist, GameObject target)
    {
        toy = t;
        timeToComplete = ttc;
        distanceToInteract = dist;
        targetObj = target;
    }

    public override void FinishAction()
    {
        MonsterController monster = MonoBehaviour.FindObjectOfType<MonsterController>();
        monster.GotToy(toy);

        base.FinishAction();
    }
}
