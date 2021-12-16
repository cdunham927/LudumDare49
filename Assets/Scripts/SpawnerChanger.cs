using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerChanger : MonoBehaviour
{
    public SpawnItem spawn;
    public bool forward = true;
    bool mousedOver = false;
    public Sprite restSprite;
    public Sprite clickSprite;
    SpriteRenderer rend;
    public AudioSource src;

    public bool on = true;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mousedOver && on) Change();
    }

    public void Change()
    {
        src.PlayOneShot(src.clip);
        rend.sprite = clickSprite;
        Invoke("ResetSprite", 0.1f);
        if (forward) spawn.currentSelection = (spawn.currentSelection + 1) % spawn.obj.Length;
        else
        {
            if (spawn.currentSelection > 0) spawn.currentSelection -= 1;
            else spawn.currentSelection = spawn.obj.Length - 1;
        }

        /*
        if (forward && spawn.currentSelection < spawn.obj.Length - 1) spawn.currentSelection += 1;
        else spawn.currentSelection = 0;

        if (!forward && spawn.currentSelection > 0) spawn.currentSelection += 1;
        else spawn.currentSelection = spawn.obj.Length - 1;
        */

        spawn.ChangeSprite();
    }

    public void ResetSprite()
    {
        rend.sprite = restSprite;
    }

    private void OnMouseEnter()
    {
        mousedOver = true;
    }

    private void OnMouseOver()
    {
        mousedOver = true;
    }

    private void OnMouseExit()
    {
        mousedOver = false;
    }
}
