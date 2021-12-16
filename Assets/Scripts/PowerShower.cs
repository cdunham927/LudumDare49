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
    Vector3 lastMousePos;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        monster = FindObjectOfType<MonsterController>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && mousedOver)
        {
            InvokeRepeating("CheckMouse", 0.01f, 0.1f);
            if (Input.mousePosition.y < lastMousePos.y) curWarmup += Time.deltaTime;
        }

        if (curWarmup >= warmup) Spawn();
    }

    void CheckMouse()
    {
        lastMousePos = Input.mousePosition;
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
        //CancelInvoke();
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
