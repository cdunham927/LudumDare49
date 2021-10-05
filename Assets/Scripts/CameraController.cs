using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] targets;
    [HideInInspector]
    public int curTarget = 2;
    public float followSpd;

    private void Awake()
    {
        //Monster room is in the middle of the array
        curTarget = 2;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targets[curTarget].transform.position.x, targets[curTarget].transform.position.y, -10f), Time.deltaTime * followSpd);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (curTarget > 0) curTarget--;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (curTarget < targets.Length - 1) curTarget++;
        }
    }
}
