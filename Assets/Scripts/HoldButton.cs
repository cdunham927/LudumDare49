using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : MonoBehaviour
{
    public GameObject obj;
    public float warmup;
    float curWarmup;
    public GameObject spawnPos;
    bool mousedOver;
    public Sprite restSprite;
    public Sprite clickSprite;
    SpriteRenderer rend;
    public AudioSource src;
    public GameObject fillBar;
    public float maxScale;

    public bool on = true;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        maxScale = fillBar.transform.localScale.y;
        src = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (on)
        {
            if (Input.GetMouseButton(0) && mousedOver)
            {
                curWarmup += Time.deltaTime;
            }
            else curWarmup = 0f;

            if (curWarmup > warmup) Spawn();

            fillBar.transform.localScale = new Vector3(fillBar.transform.localScale.x, (curWarmup / warmup) * maxScale, 1);
        }
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
        Instantiate(obj, spawnPos.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}
