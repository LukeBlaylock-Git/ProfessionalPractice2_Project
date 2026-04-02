using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator Anim;

    [Header("Movement")]
    public float MaxSpeed = 8f;         // top speed
    public float Acceleration = 55f;    // ramp up
    public float Deceleration = 110f;   // slow down when no input
    public float AirControl = 0.6f;     // less control in air (0..1)

    [Header("Jump")]
    public float JumpForce = 10f;
    public float JumpMultiplier = 0.5f; // lower = shorter tap jump

    [Header("Ground Check")]
    public Transform GroundCheck;
    public float GroundRadius = 0.15f;
    public LayerMask GroundMask;

    private Rigidbody2D RB;

    private bool isGrounded;
    private bool JumpPressed;
    private bool JumpReleased;
    private float MoveInput;

    void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        RB.interpolation = RigidbodyInterpolation2D.Interpolate;
        RB.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    void Update()
    {
        MoveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            JumpPressed = true;

        if (Input.GetButtonUp("Jump"))
            JumpReleased = true;
    }

    void FixedUpdate()
    {
        if (GroundCheck != null)
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, GroundMask);

        if (MoveInput != 0f)
            RB.WakeUp();

        // Horizontal movement (acceleration/deceleration)
        float TargetSpeed = MoveInput * MaxSpeed;
        float AccelRate = (Mathf.Abs(TargetSpeed) > 0.01f) ? Acceleration : Deceleration;

        if (!isGrounded)
            AccelRate *= AirControl;

        Anim.SetFloat("Speed", Mathf.Abs(RB.linearVelocity.x));
        Anim.SetBool("Grounded", isGrounded);
        Anim.SetFloat("YVelocity", RB.linearVelocity.y);

        float NewX = Mathf.MoveTowards(RB.linearVelocity.x, TargetSpeed, AccelRate * Time.fixedDeltaTime);
        RB.linearVelocity = new Vector2(NewX, RB.linearVelocity.y);
        Anim.SetFloat("Speed", Mathf.Abs(RB.linearVelocity.x));

        bool JumpedThisFrame = false;
        if (JumpPressed && isGrounded)
        {
            RB.linearVelocity = new Vector2(RB.linearVelocity.x, JumpForce);
            FindObjectOfType<AudioManager>().Play("PlayerJump");
            JumpedThisFrame = true;
        }

        // Variable jump cut (tap = shorter jump)
        if (JumpReleased && RB.linearVelocity.y > 0f)
        {
            RB.linearVelocity = new Vector2(RB.linearVelocity.x, RB.linearVelocity.y * JumpMultiplier);
        }

        JumpPressed = false;

        //animatio
        if (Anim != null)
        {
            float SpeedAbs = Mathf.Abs(RB.linearVelocity.x);

            Anim.SetFloat("Speed", SpeedAbs);
            Anim.SetBool("Grounded", isGrounded);
            Anim.SetFloat("YVelocity", RB.linearVelocity.y);

            if (SpeedAbs > 0.1f)
                Anim.SetTrigger("Move");

            if (JumpedThisFrame)
            {
                Anim.SetTrigger("Jump");
            }
        }

        JumpReleased = false;

        
    }

    private void OnDrawGizmosSelected()
    {
        if (GroundCheck == null) return;
        Gizmos.DrawWireSphere(GroundCheck.position, GroundRadius);
    }
}