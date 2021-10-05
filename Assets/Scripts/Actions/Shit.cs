using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shit : Action
{
    public Shit(float ttc)
    {
        timeToComplete = ttc;
    }

    public override void FinishAction()
    {
        MonsterController monster = MonoBehaviour.FindObjectOfType<MonsterController>();
        monster.Shit();

        base.FinishAction();
    }
}
