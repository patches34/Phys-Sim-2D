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
    
    [SerializeField]
    float rotationOffset = 0f;

    public WorldManager worldManager;

    // Start is called before the first frame update
    void Start()
    {
        deltaPosition = Vector2.zero;
    }

    void FixedUpdate()
    {
        // Calculate the velocity for this frame - New
        velocity += acceleration * Time.fixedDeltaTime;

        UpdateDirection();

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        deltaPosition = transform.position + (velocity * Time.fixedDeltaTime);

        worldManager.CheckForScreenEdge(ref deltaPosition);      

        rBody2D.MovePosition(deltaPosition);

        // Zero out acceleration - New
        acceleration = Vector3.zero;
    }

    //  Force Methods
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    void UpdateDirection()
    {
        // Grab current direction from velocity  - New
        direction = velocity.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x);

        transform.rotation = Quaternion.Euler(0f, 0f, (angle * Mathf.Rad2Deg) + rotationOffset);
    }
}
