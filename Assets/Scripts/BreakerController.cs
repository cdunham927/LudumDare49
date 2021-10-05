using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerController : MonoBehaviour
{
    //public float powerRequired;
    public float warmup;
    float curWarmup;
    bool mousedOver;
    AudioSource src;
    public GameObject fillBar;
    public float maxScale;
    GameController cont;
    SpriteRenderer rend;
    public Sprite restSprite;
    public Sprite clickSprite;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        cont = FindObjectOfType<GameController>();
        src = GetComponent<AudioSource>();
        maxScale = fillBar.transform.localScale.y;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && mousedOver && !cont.powerOn)
        {
            curWarmup += Time.deltaTime;
        }
        else curWarmup = 0f;

        if (curWarmup > warmup) Spawn();

        fillBar.transform.localScale = new Vector3(fillBar.transform.localScale.x, (curWarmup / warmup) * maxScale, 1);
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
        src.PlayOneShot(src.clip);
        curWarmup = 0;
        FindObjectOfType<GameController>().RestorePower();
        //Instantiate(obj, spawnPos.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}
