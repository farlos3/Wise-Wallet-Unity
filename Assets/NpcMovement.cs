using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{

    private AnimatedWalk animatedWalk;

    void Start()
    {
        animatedWalk = GetComponent<AnimatedWalk>();
        if (animatedWalk == null)
        {
            Debug.LogError("ไม่พบ AnimatedWalk component!");
        }

        // ทำให้ NPC ไม่แสดงผลตั้งแต่เริ่ม
        gameObject.SetActive(true);
    }
}
