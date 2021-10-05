using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject[] obj;
    public bool random;
    public int currentSelection;
    public float cooldown;
    public float warmup;
    float curCooldown;
    public GameObject spawnPos;
    bool mousedOver;
    public Sprite restSprite;
    public Sprite clickSprite;
    SpriteRenderer rend;
    public AudioSource src;
    public AudioClip otherClip;
    public float waitTime = 0.125f;
    public SpriteRenderer itemShow;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        src = GetComponent<AudioSource>();

        ChangeSprite();
    }

    private void Update()
    {
        if (curCooldown > 0) curCooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && mousedOver && curCooldown <= 0)
        {
            //Spawn();
            //CancelInvoke();
            if (otherClip) Invoke("OtherClip", waitTime);
            Invoke("Spawn", warmup);
        }
    }

    public void ChangeSprite()
    {
        if (itemShow != null)
        {
            itemShow.sprite = obj[currentSelection].GetComponent<SpriteRenderer>().sprite;
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

    public void OtherClip()
    {
        src.PlayOneShot(otherClip, 0.7f);
    }

    public void Spawn()
    {
        rend.sprite = clickSprite;
        src.PlayOneShot(src.clip);
        Invoke("ResetSprite", 0.1f);
        if (obj.Length > 1)
        {
            if (!random) Instantiate(obj[currentSelection], spawnPos.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            else
            {
                int r = Random.Range(0, obj.Length - 1);
                Instantiate(obj[r], spawnPos.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            }
        }
        curCooldown = cooldown;
        if (obj.Length == 1) Instantiate(obj[0], spawnPos.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}
