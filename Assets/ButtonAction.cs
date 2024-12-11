using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public GameObject effect; // วัตถุที่เป็นเอฟเฟกต์เมื่อปุ่มทำงาน
    public AudioClip buttonSound; // เสียงเอฟเฟกต์เมื่อปุ่มถูกชน
    private AudioSource audioSource;
    private Vector3 originalScale; // เก็บขนาดเดิมของ GameObject ที่เราจะขยาย
    public GameObject objectToScale; // ตัวแปรที่สามารถกำหนด GameObject ที่ต้องการขยาย

    private void Start()
    {
        // ตรวจสอบว่า objectToScale ถูกตั้งค่าหรือไม่
        if (objectToScale == null)
        {
            Debug.LogWarning("Please assign a GameObject to scale in the Inspector.");
        }

        // ตั้งค่า AudioSource สำหรับเสียง
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && buttonSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // เก็บขนาดเดิมของ GameObject ที่เราจะขยาย
        if (objectToScale != null)
        {
            originalScale = objectToScale.transform.localScale;
        }
    }

    public void Activate()
    {
        Debug.Log("Button has been activated!");

        // ตรวจสอบว่ามีการตั้งค่า objectToScale หรือไม่
        if (objectToScale != null)
        {
            // ขยายขนาดของ GameObject
            objectToScale.transform.localScale = originalScale * 1.5f; // ขยายให้ใหญ่ขึ้น 1.5 เท่า
        }

        // แสดงเอฟเฟกต์ (ถ้ามี)
        if (effect != null)
        {
            effect.SetActive(true);
        }

        // เล่นเสียง (ถ้ามี)
        if (buttonSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
    }

    public void Deactivate()
    {
        Debug.Log("Button has been deactivated!");

        // ตรวจสอบว่ามีการตั้งค่า objectToScale หรือไม่
        if (objectToScale != null)
        {
            // คืนขนาดของ GameObject กลับสู่ขนาดเดิม
            objectToScale.transform.localScale = originalScale;
        }

        // ซ่อนเอฟเฟกต์ (ถ้ามี)
        if (effect != null)
        {
            effect.SetActive(false);
        }
    }
}
