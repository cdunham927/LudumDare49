using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public abstract class Action
{
    public float timeToComplete;
    float curTime;
    public bool isFinished = false;
    [Space]
    [Header("Distance to object stuff")]
    public float distanceToInteract = 3;
    public float distance;
    public GameObject targetObj;

    MonsterController monster;

    void Awake()
    {
        //monster = MonoBehaviour.FindObjectOfType<MonsterController>();
    }

    public virtual void Start()
    {
        monster = MonoBehaviour.FindObjectOfType<MonsterController>();
        curTime = timeToComplete;
    }

    public virtual void Update() 
    {
        if (curTime > 0 && (targetObj != null && distance <= distanceToInteract)) curTime -= Time.deltaTime;
        if (curTime > 0 && targetObj == null) curTime -= Time.deltaTime;

        if (curTime <= 0) FinishAction();

        if (targetObj != null)
        {
            distance = Vector2.Distance(monster.transform.position, targetObj.transform.position);
            //Debug.Log("Distance: " + distance);
            if (distance > distanceToInteract)
            {
                //If target is on our right
                if (targetObj.transform.position.x > monster.transform.position.x)
                {
                    monster.Move(1);
                }
                else monster.Move(-1);
            }
        }
    }

    public virtual void FinishAction()
    {
        isFinished = true;
    }
}
