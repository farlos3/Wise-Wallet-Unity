using UnityEngine;

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
}