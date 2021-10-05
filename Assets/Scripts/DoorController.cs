using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    CameraController cam;
    public bool right = true;

    private void Awake()
    {
        cam = FindObjectOfType<CameraController>();
    }

    private void OnMouseDown()
    {
        if (!right && cam.curTarget > 0) cam.curTarget--;
        else if (right && cam.curTarget < cam.targets.Length - 1) cam.curTarget++;
    }
}
