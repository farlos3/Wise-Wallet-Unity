using UnityEngine;
using System.Collections;

public class NpcMovement : MonoBehaviour
{
    public Camera mainCamera;
    public float moveSpeed = 2f;  // ความเร็วในการเดิน
    public float closeDistance = 1.0f; // ระยะห่างที่ NPC หยุด
    private Vector3 targetPosition;
    private bool isMoving = false;
    private AnimatedWalk animatedWalk; // ตัวแปรที่ใช้ควบคุมการเปลี่ยนแอนิเมชัน
    public GameObject player; // ผู้เล่นหลัก

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // ใช้ Camera หลักถ้าไม่ได้กำหนด
        }

        // หาค่า AnimatedWalk ที่เกี่ยวข้องกับ NPC นี้
        animatedWalk = GetComponent<AnimatedWalk>();

        // ตรวจสอบว่ามีการตั้งค่า AnimatedWalk
        if (animatedWalk == null)
        {
            Debug.LogError("AnimatedWalk component not found!");
        }

        // ซ่อน NPC ตอนเริ่มเกม
        gameObject.SetActive(false); // NPC ซ่อนไว้ไม่ให้แสดงผล
    }

    // เริ่มเดินจากหลังผู้เล่น
    public void StartWalking()
    {
        // ทำให้ NPC แสดงผล
        gameObject.SetActive(true);

        if (mainCamera == null || player == null)
        {
            Debug.LogError("Main camera or player not assigned!");
            return;
        }

        // กำหนดตำแหน่งเริ่มต้นให้ NPC อยู่หลังผู้เล่น
        Vector3 startPosition = player.transform.position + new Vector3(2f, 0f, 0f); // NPC จะอยู่หลังผู้เล่น (ตำแหน่งตามแกน X)

        // กำหนดตำแหน่งเป้าหมาย (เช่น ร้าน)
        Vector3 shopPosition = new Vector3(10f, 0f, 0f); // ตัวอย่างตำแหน่งของร้าน

        // กำหนดตำแหน่งเริ่มต้นให้ NPC
        transform.position = startPosition;

        // เริ่มเดินไปที่ตำแหน่งของร้าน
        StartCoroutine(MoveNpc(shopPosition));
    }

    // Coroutine ที่ใช้ในการเดินของ NPC ไปยังเป้าหมาย
    private IEnumerator MoveNpc(Vector3 target)
    {
        isMoving = true;

        // เริ่มแอนิเมชันการเดิน
        if (animatedWalk != null)
        {
            animatedWalk.ChangeAnimation(animatedWalk.walkSprites);
        }

        // เดินไปที่ตำแหน่งเป้าหมาย
        while (Vector3.Distance(transform.position, target) > closeDistance)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            yield return null;
        }

        // เมื่อเดินถึงร้านแล้ว ให้หยุดเดินและเปลี่ยนแอนิเมชันเป็นนิ่ง
        isMoving = false;
        if (animatedWalk != null)
        {
            animatedWalk.ChangeAnimation(animatedWalk.idleSprites);
        }

        Debug.Log("NPC has finished moving.");
    }
}
