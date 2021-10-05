using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShower : MonoBehaviour
{
    public float warmup;
    float curWarmup;
    //public GameObject spawnPos;
    bool mousedOver;
    MonsterController monster;
    public Sprite restSprite;
    public Sprite clickSprite;
    SpriteRenderer rend;
    public AudioSource src;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        monster = FindObjectOfType<MonsterController>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && mousedOver)
        {
            curWarmup += Time.deltaTime;
        }

        if (curWarmup > warmup) Spawn();
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

    public void ResetSprite()
    {
        rend.sprite = restSprite;
    }

    public void Spawn()
    {
        rend.sprite = clickSprite;
        Invoke("ResetSprite", 1f);
        //src.PlayOneShot(src.clip);
        curWarmup = 0;
        monster.PowerShower();
    }
}
