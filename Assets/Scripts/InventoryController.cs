using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Inventory things")]
    public Stack<Item> inventory = new Stack<Item>();
    public Text inventoryText;
    public int maxSlots = 5;

    public MouseDragController drag;
    public bool onInventory;

    void Awake()
    {
        drag = FindObjectOfType<MouseDragController>();
    }

    /*public void OnPointerClick(PointerEventData eventData)
    {

    }*/

    void Update()
    {
        if (inventoryText != null) inventoryText.text = ("Inventory: " + inventory.Count + "/" + maxSlots.ToString() + " Items");

        if (Input.GetMouseButtonUp(0))
        {
            //Put an item into the inventory
            if (onInventory && inventory.Count < maxSlots)
            {
                //Debug.Log("Trying to put in inventory: " + drag.selectedObject.gameObject);
                Item obj = drag.selectedObject.GetComponent<Item>();
                if (obj != null)
                {
                    obj.gameObject.SetActive(false);
                    inventory.Push(obj);
                    //Debug.Log("Drop an item into inventory");
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Drag an item out of the inventory
            if (onInventory && inventory.Count > 0)
            {
                Item obj = inventory.Pop();
                obj.GetComponent<Collider2D>().isTrigger = false;
                obj.gameObject.SetActive(true);
                drag.selectedObject = obj.GetComponent<Rigidbody2D>();
                //Debug.Log("Taking an item out of the inventory");
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Drag an item out of the inventory
        if (onInventory && inventory.Count > 0)
        {

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //You are no longer moused over the inventory
        onInventory = false;
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        //You are now moused over the inventory
        onInventory = true;
    }
}
