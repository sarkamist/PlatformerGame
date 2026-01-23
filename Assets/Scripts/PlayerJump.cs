using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float JumpHeight;
    public float DistanceToMaxHeight;
    public float SpeedHorizontal;
    public float PressTimeToMaxJump;

    public float WallSlideSpeed = 1;
    public ContactFilter2D filter;

    private Rigidbody2D rigidbody;
    private CollisionDetection collisionDetection;
    private float lastVelocityY;
    private float jumpStartedTime;
    bool IsWallSliding => collisionDetection.IsTouchingFront;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();
    }
    void FixedUpdate()
    {
        if (IsPeakReached()) TweakGravity();

        if (IsWallSliding) SetWallSlide();
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
        // Modify player linear velocity on wall sliding
        //rigidbody.gravityScale = 0.8f;
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x,
            Mathf.Max(rigidbody.linearVelocity.y, -WallSlideSpeed));
    }

    public void OnJumpStarted()
    {
        if (collisionDetection.IsGrounded)
        {
            SetGravity();
            var velocity = new Vector2(rigidbody.linearVelocity.x, GetJumpForce());
            rigidbody.linearVelocity = velocity;
            jumpStartedTime = Time.time;
        }
    }
    public void OnJumpFinished()
    {
        float fractionOfTimePressed = 1 / Mathf.Clamp01((Time.time - jumpStartedTime) / PressTimeToMaxJump);
        rigidbody.gravityScale *= fractionOfTimePressed;
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
