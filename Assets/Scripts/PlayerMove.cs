using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;

    Rigidbody2D rigidbody;
    private float horizontalDir; // Horizontal move direction value [-1, 1]
    private float lastDir;
    public bool IsMoving;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 velocity = rigidbody.linearVelocity;
        velocity.x = horizontalDir * Speed;
        rigidbody.linearVelocity = velocity;
    }

    // NOTE: InputSystem: "move" action becomes "OnMove" method
    void OnMove(InputValue value)
    {
        // Read value from control, the type depends on what
        // type of controls the action is bound to
        var inputVal = value.Get<Vector2>();
        if (inputVal != null && inputVal.x != 0)
        {
            horizontalDir = inputVal.x;
            lastDir = inputVal.x;
        }

        if (IsMoving == false && lastDir != 0)
        {
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
    }
}
