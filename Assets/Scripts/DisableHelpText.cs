using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHelpText : MonoBehaviour
{
    public float waitTime = 3f;
    public GameObject obj;

    private void Awake()
    {
        Invoke("Disable", waitTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) Disable();
    }

    public void Disable()
    {
        obj.SetActive(false);
    }
}
