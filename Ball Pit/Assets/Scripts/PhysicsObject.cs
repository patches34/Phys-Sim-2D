using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PhysicsObject : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rBody2D;

    Vector3 deltaPosition, direction, velocity, acceleration;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    public Vector3 Velocity { get { return velocity; } }

    [SerializeField]
    float mass = 1f, maxSpeed = 1f;

    public float MaxSpeed
    {
        get { return maxSpeed; }
    }

    //  things that might go away
    Vector2 screenSize = Vector2.zero;
    [SerializeField]
    float rotationOffset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        deltaPosition = Vector2.zero;

        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y * Camera.main.aspect;
    }

    void FixedUpdate()
    {
        // Calculate the velocity for this frame - New
        velocity += acceleration * Time.fixedDeltaTime;

        UpdateDirection();

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        deltaPosition = transform.position + (velocity * Time.fixedDeltaTime);

        CheckForScreenEdge();      

        rBody2D.MovePosition(deltaPosition);

        // Zero out acceleration - New
        acceleration = Vector3.zero;
    }


    //  Force Methods
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    void CheckForScreenEdge()
    {
        if (deltaPosition.x > screenSize.x)
        {
            deltaPosition.x = -screenSize.x;
        }
        else if(deltaPosition.x < -screenSize.x)
        {
            deltaPosition.x = screenSize.x;
        }

        if (deltaPosition.y > screenSize.y)
        {
            deltaPosition.y = -screenSize.y;
        }
        else if (deltaPosition.y < -screenSize.y)
        {
            deltaPosition.y = screenSize.y;
        }
    }

    void UpdateDirection()
    {
        // Grab current direction from velocity  - New
        direction = velocity.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x);

        transform.rotation = Quaternion.Euler(0f, 0f, (angle * Mathf.Rad2Deg) + rotationOffset);
    }
}
