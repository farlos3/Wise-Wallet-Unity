using UnityEngine;
using System.Collections.Generic;


public class OwnerShopItem
{
    public string ItemName { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }

    public OwnerShopItem(string itemName, float price, int quantity)
    {
        ItemName = itemName;
        Price = price;
        Quantity = quantity;
    }
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AnimatedWalk))]
public class Character_Control : MonoBehaviour
{
    public float speed = 8.0f;
    public float jumpForce = 10.0f;
    public LayerMask groundLayer;

    private new Rigidbody2D rigidbody;
    private Vector3 startingPosition;
    private bool isGrounded;
    private AnimatedWalk animatedWalk;
    private bool isMoving = false;
    public float TotalMoney { get; private set; }
    public int Hearts { get; private set; }
    public List<string> Inventory { get; private set; }
    public float Expenses { get; private set; }

    // Move Part
    private void Awake()
    {
        this.animatedWalk = GetComponent<AnimatedWalk>();
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;

        if (this.animatedWalk == null)
        {
            Debug.LogError("AnimatedWalk component is missing!");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.transform.position = this.startingPosition;
        this.rigidbody.velocity = Vector2.zero;
        this.enabled = true;
        if (this.animatedWalk != null)
        {
            this.animatedWalk.Restart();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
    }

    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isCurrentlyMoving = !Mathf.Approximately(moveInput, 0f);

        // ตรวจสอบการเปลี่ยนสถานะการเคลื่อนที่
        if (isCurrentlyMoving != isMoving)
        {
            isMoving = isCurrentlyMoving;
            if (this.animatedWalk != null)
            {
                this.animatedWalk.enabled = true;
                if (isMoving)
                {
                    // เปลี่ยนเป็นแอนิเมชันเดิน
                    this.animatedWalk.ChangeAnimation(this.animatedWalk.walkSprites);
                }
                else
                {
                    // เปลี่ยนเป็นแอนิเมชันนิ่ง
                    this.animatedWalk.ChangeAnimation(this.animatedWalk.idleSprites);
                }
            }
        }

        if (isMoving)
        {
            // อัปเดตทิศทาง
            float direction = Mathf.Sign(moveInput);
            transform.localScale = new Vector3(0.7f * direction, 0.7f, 0.7f);
        }

        // อัปเดตความเร็ว
        Vector2 velocity = this.rigidbody.velocity;
        velocity.x = moveInput * this.speed;
        this.rigidbody.velocity = velocity;
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.rigidbody.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckGrounded()
    {
        Vector2 position = this.transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, this.groundLayer);
        isGrounded = hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Button")) // ตรวจสอบว่าชนกับปุ่มที่มี Tag "Button"
        {
            Debug.Log("Character activated the button by walking through!");
            ButtonAction buttonAction = other.GetComponent<ButtonAction>();
            if (buttonAction != null)
            {
                buttonAction.Activate(); // เรียกฟังก์ชันทำงานของปุ่ม
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ตรวจสอบว่าตัวละครออกจากปุ่ม
        if (collision.CompareTag("Button"))
        {
            ButtonAction buttonAction = collision.GetComponent<ButtonAction>();
            if (buttonAction != null)
            {
                buttonAction.Deactivate(); // ปิดเอฟเฟกต์เมื่อออกจากปุ่ม
            }
        }
    }

}
