using UnityEngine;
using System.Collections;

public class ScaleAndMoveObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToTransform;
    [SerializeField] private Vector3 targetScale = new Vector3(2f, 2f, 2f);
    [SerializeField] private Vector3 targetPosition = new Vector3(5f, 0f, 0f);

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool isTransformed = false;

    void Start()
    {
        // เก็บตำแหน่งและขนาดเดิมตอนเริ่มต้น
        originalScale = objectToTransform.transform.localScale;
        originalPosition = objectToTransform.transform.position;
    }

    public void OnButtonClick()
    {
        // สลับระหว่างสถานะเดิมกับสถานะที่เปลี่ยนแปลง
        if (!isTransformed)
        {
            StartCoroutine(TransformObject(originalScale, originalPosition, targetScale, targetPosition));
            isTransformed = true;
        }
        else
        {
            StartCoroutine(TransformObject(targetScale, targetPosition, originalScale, originalPosition));
            isTransformed = false;
        }
    }

    private IEnumerator TransformObject(Vector3 startScale, Vector3 startPosition, Vector3 endScale, Vector3 endPosition)
    {
        float journeyLength = 1f;
        float distanceCovered = 0f;

        while (distanceCovered < journeyLength)
        {
            distanceCovered += Time.deltaTime;
            float fraction = distanceCovered / journeyLength;

            // Lerp สำหรับขนาด
            objectToTransform.transform.localScale = Vector3.Lerp(startScale, endScale, fraction);

            // Lerp สำหรับตำแหน่ง
            objectToTransform.transform.position = Vector3.Lerp(startPosition, endPosition, fraction);

            yield return null;
        }

        // ปรับค่าสุดท้ายให้ตรงกับเป้าหมาย เพื่อป้องกันความคลาดเคลื่อน
        objectToTransform.transform.localScale = endScale;
        objectToTransform.transform.position = endPosition;
    }
}