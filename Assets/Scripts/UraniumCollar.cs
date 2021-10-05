using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UraniumCollar : MonoBehaviour
{
    MonsterController monster;

    private void Awake()
    {
        monster = FindObjectOfType<MonsterController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Uranium>() != null)
        {
            collision.gameObject.SetActive(false);
            monster.AddUranium();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Uranium>() != null)
        {
            collision.gameObject.SetActive(false);
            monster.AddUranium();
        }
    }
}
