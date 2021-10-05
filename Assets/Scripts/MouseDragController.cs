using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDragController : MonoBehaviour
{
    public Rigidbody2D selectedObject;
    Vector3 offset;
    Vector3 mousePosition;

    public float maxSpeed = 10;
    Vector2 mouseForce;
    Vector3 lastPosition;

    public LayerMask itemMask;

    public Image inventoryHighlight;
    public Color highlightColor;

    private void Awake()
    {
        inventoryHighlight.color = Color.white;
    }

    void LateUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (selectedObject)
        {
            inventoryHighlight.color = highlightColor;
            mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
            mouseForce = Vector2.ClampMagnitude(mouseForce, maxSpeed);
            lastPosition = mousePosition;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition, itemMask);

            if (targetObject)
            {
                targetObject.isTrigger = true;
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            inventoryHighlight.color = Color.white;
            selectedObject.velocity = Vector2.zero;
            selectedObject.GetComponent<Collider2D>().isTrigger = false;
            selectedObject.AddForce(mouseForce, ForceMode2D.Impulse);
            selectedObject = null;
        }
    }

    void FixedUpdate()
    {
        if (selectedObject)
        {
            selectedObject.MovePosition(mousePosition + offset);
        }
    }
}
