using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool grounded;
    public LayerMask groundMask;
    SpriteRenderer rend;
    Collider2D col;
    public float dist = 1f;
    public bool added = false;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public virtual void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (rend.size.y / 2f)), Vector2.down, dist, groundMask);
        grounded = (hit.collider != null && col.isTrigger == false);
        if (Application.isEditor) Debug.DrawLine(new Vector2(transform.position.x, transform.position.y - (rend.size.y / 2f)), (Vector2)transform.position + Vector2.down);
    }
}
