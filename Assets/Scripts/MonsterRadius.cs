using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRadius : MonoBehaviour
{
    MonsterController monster;
    BoxCollider2D col;
    public LayerMask itemMask;

    private void Awake()
    {
        monster = FindObjectOfType<MonsterController>();
        col = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hitting object");
        if (monster.curItem == null && (collision.GetComponent<Food>() != null || collision.GetComponent<Toy>()))
        {
            Item i = collision.GetComponent<Item>();
            if (i.grounded) monster.curItem = collision.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Still hitting object");
        if (monster.curItem == null && (collision.GetComponent<Food>() != null || collision.GetComponent<Toy>()))
        {
            Item i = collision.GetComponent<Item>();
            if (i.grounded) monster.curItem = collision.gameObject;
        }
    }

    public void SpoilAllFoodInRoom()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, col.size, 0, itemMask);
        foreach (Collider2D c in cols)
        {
            Food f = c.GetComponent<Food>();
            if (f != null) f.spoiled = true;

            Poop s = c.GetComponent<Poop>();
            if (s != null) s.gameObject.SetActive(false);
        }
    }
}
