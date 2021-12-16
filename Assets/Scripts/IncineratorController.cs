using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncineratorController : MonoBehaviour
{
    bool mousedOver;
    //public Sprite restSprite;
    //public Sprite clickSprite;
    SpriteRenderer rend;
    public AudioSource src;
    //public SpriteRenderer itemShow;

    MouseDragController drag;

    private void Awake()
    {
        drag = FindObjectOfType<MouseDragController>();
        //rend = GetComponent<SpriteRenderer>();
        //src = GetComponent<AudioSource>();

        //ChangeSprite();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && mousedOver && drag.selectedObject != null)
        {
            //Debug.Log("Destroy object");
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mousedOver = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        mousedOver = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        mousedOver = false;
    }

    public bool MousedOver()
    {
        return mousedOver;
    }

    public void ChangeSprite()
    {
        //if (itemShow != null)
        //{
        //    itemShow.sprite = obj[currentSelection].GetComponent<SpriteRenderer>().sprite;
        //}
    }

    private void OnMouseEnter()
    {
        //mousedOver = true;
    }

    private void OnMouseOver()
    {
        //mousedOver = true;
    }

    private void OnMouseExit()
    {
        //mousedOver = false;
    }

    public void ResetSprite()
    {
        //rend.sprite = restSprite;
    }

    public void Despawn()
    {
        //rend.sprite = clickSprite;
        //src.PlayOneShot(src.clip);
        //Invoke("ResetSprite", 0.1f);

        if (drag.selectedObject.gameObject.GetComponent<Poop>() != null) FindObjectOfType<MonsterController>().CleanShit();
        Destroy(drag.selectedObject.gameObject);
        drag.selectedObject = null;
    }
}
