using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpHeight;
    public float DistanceToMaxHeight;
    public float SpeedHorizontal;
    public float PressTimeToMaxJump;

    public float WallSlideSpeed = 1;
    public ContactFilter2D Filter;

    private Rigidbody2D rigidbody;
    private CollisionDetection collisionDetection;
    private float lastVelocityY;
    private float jumpStartedTime;

    bool IsWallSliding => collisionDetection.IsTouchingFront;

    [Header("Jumping Parameters")]
    public bool IsJumping = false;
    public int MaxJumps = 2;
    public int RemainingJumps;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();

        RemainingJumps = MaxJumps;
    }
    void FixedUpdate()
    {
        if (IsPeakReached()) TweakGravity();

        if (IsWallSliding) SetWallSlide();

        if (collisionDetection.IsGrounded) RemainingJumps = MaxJumps;
    }

    private void Update()
    {
        if (IsJumping && (Time.time - jumpStartedTime) >= PressTimeToMaxJump)
        {
            OnJumpFinished();
        }
    }

    private bool IsPeakReached()
    {
        bool reached = ((lastVelocityY * rigidbody.linearVelocity.y) < 0);
        lastVelocityY = rigidbody.linearVelocity.y;

        return reached;
    }

    private void TweakGravity()
    {
        rigidbody.gravityScale *= 1.2f;
    }

    private void SetWallSlide()
    {
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x,
            Mathf.Max(rigidbody.linearVelocity.y, -WallSlideSpeed));
    }

    public void OnJumpStarted()
    {
        if (collisionDetection.IsGrounded || RemainingJumps > 0)
        {
            SetGravity();
            var velocity = new Vector2(rigidbody.linearVelocity.x, GetJumpForce());
            rigidbody.linearVelocity = velocity;
            jumpStartedTime = Time.time;

            IsJumping = true;
        }
    }
    public void OnJumpFinished()
    {
        if (IsJumping) {
            float fractionOfTimePressed = 1 / Mathf.Clamp01((Time.time - jumpStartedTime) / PressTimeToMaxJump);
            rigidbody.gravityScale *= fractionOfTimePressed;

            IsJumping = false;
            RemainingJumps--;
        }
    }

    private void SetGravity()
    {
        var grav = 2 * JumpHeight * (SpeedHorizontal * SpeedHorizontal) / (DistanceToMaxHeight * DistanceToMaxHeight);
        rigidbody.gravityScale = grav / 9.81f;
    }

    private float GetJumpForce()
    {
        return 2 * JumpHeight * SpeedHorizontal / DistanceToMaxHeight;
    }
}
