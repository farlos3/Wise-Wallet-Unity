using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0, 1)]
    public float smoothTime;

    public Vector3 positionOffset;

    // ขอบเขตของกล้อง
    public Vector2 minBounds; // พิกัดต่ำสุดของกล้อง (เช่น Bottom-left)
    public Vector2 maxBounds; // พิกัดสูงสุดของกล้อง (เช่น Top-right)

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        // ตำแหน่งเป้าหมายของกล้อง
        Vector3 targetPosition = target.position + positionOffset;

        // ใช้ SmoothDamp ในการเคลื่อนกล้อง
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // จำกัดตำแหน่งกล้องให้อยู่ในขอบเขตที่กำหนด
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

        // อัปเดตตำแหน่งของกล้อง
        transform.position = smoothedPosition;
    }

    private void OnDrawGizmosSelected()
    {
        // วาดกรอบขอบเขตของกล้องใน Scene View เพื่อให้เห็นภาพ
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(minBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, maxBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, maxBounds.y, 0), new Vector3(maxBounds.x, minBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, minBounds.y, 0), new Vector3(minBounds.x, minBounds.y, 0));
    }
}
