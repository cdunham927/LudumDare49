using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnlyOneItem : MonoBehaviour
{
    public GameObject[] obj;
    public float cooldown;
    public float warmup;
    float curCooldown;
    public GameObject spawnPos;
    bool mousedOver;
    public bool alreadySpawned = false;
    public Sprite restSprite;
    public Sprite clickSprite;
    SpriteRenderer rend;
    public AudioSource src;
    public Collider2D col;

    public bool on = true;

    private void Awake()
    {
        src = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        alreadySpawned = false;
    }
    
    private void Update()
    {
        if (on)
        {
            if (curCooldown > 0) curCooldown -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && mousedOver && curCooldown <= 0 && !alreadySpawned)
            {
                //Spawn();
                //CancelInvoke();
                Invoke("Spawn", warmup);
            }

            if (alreadySpawned)
            {
                col.enabled = true;
                rend.sprite = clickSprite;
            }
            else
            {
                col.enabled = false;
                rend.sprite = restSprite;
            }
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
    
    public void Spawn()
    {
        col.enabled = true;
        src.PlayOneShot(src.clip);
        curCooldown = cooldown;
        alreadySpawned = true;
        Instantiate(obj[Random.Range(0, obj.Length)], spawnPos.transform.position, Quaternion.identity);
    }
}
