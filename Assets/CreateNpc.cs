using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNpc : MonoBehaviour
{
    public GameObject npcPrefab; // Prefab ของ NPC
    public GameObject targetGameObject; // GameObject ที่ใช้กำหนดตำแหน่งให้ NPC

    public void CreateNpcAtGameObject()
    {
        if (targetGameObject != null)
        {
            // สร้าง NPC ที่ตำแหน่งของ targetGameObject
            Instantiate(npcPrefab, targetGameObject.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("ไม่พบ GameObject ที่กำหนดตำแหน่ง");
        }
    }
}