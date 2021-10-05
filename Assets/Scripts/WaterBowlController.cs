using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBowlController : MonoBehaviour
{
    public bool filled = false;
    public Sprite emptySpr;
    public Sprite filledSpr;
    SpriteRenderer rend;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        rend.sprite = (filled) ? filledSpr : emptySpr;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<WaterBottle>() != null)
        {
            if (!filled)
            {
                collision.gameObject.SetActive(false);
                filled = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<WaterBottle>() != null)
        {
            if (!filled)
            {
                collision.gameObject.SetActive(false);
                filled = true;
            }
        }
    }
}
