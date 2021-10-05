using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : Action
{
    public Drink(float ttc, float dist, GameObject target)
    {
        timeToComplete = ttc;
        distanceToInteract = dist;
        targetObj = target;
    }

    public override void FinishAction()
    {
        MonsterController monster = MonoBehaviour.FindObjectOfType<MonsterController>();
        monster.Drink();

        base.FinishAction();
    }
}
