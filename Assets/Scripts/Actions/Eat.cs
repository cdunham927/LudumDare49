using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : Action
{
    Food food;
    public Eat(Food fd, float ttc, float dist, GameObject target)
    {
        food = fd;
        timeToComplete = ttc;
        distanceToInteract = dist;
        targetObj = target;
    }

    public override void FinishAction()
    {
        MonsterController monster = MonoBehaviour.FindObjectOfType<MonsterController>();
        monster.Eat(food);

        base.FinishAction();
    }
}
