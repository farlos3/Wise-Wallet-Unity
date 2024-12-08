using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public string[] desiredItems; // รายการสินค้าที่ NPC ต้องการ
    public int numberOfItems; // จำนวนสินค้าที่ต้องการ

    private int itemsSold = 0; // จำนวนสินค้าที่ขายได้
    private bool isShopping = false;

    public void StartShopping()
    {
        if (!isShopping)
        {
            isShopping = true;
            numberOfItems = Random.Range(1, 4); // NPC ต้องการสินค้าจำนวน 1-3 อย่าง
            Debug.Log("NPC wants " + numberOfItems + " items.");
        }
    }

    public void SellItem(string itemName)
    {
        if (isShopping && itemsSold < numberOfItems && System.Array.Exists(desiredItems, item => item == itemName))
        {
            itemsSold++;
            Debug.Log("Item sold: " + itemName + ". Total sold: " + itemsSold);

            if (itemsSold >= numberOfItems)
            {
                Debug.Log("NPC is satisfied!");
                EndShopping();
            }
        }
        else
        {
            Debug.Log("Item not desired or already satisfied.");
        }
    }

    public void EndShopping()
    {
        isShopping = false;
        itemsSold = 0;
        Debug.Log("Shopping session ended.");
    }
}

