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

    [SerializeField]
    SimDataSO simData;

    // Start is called before the first frame update
    void Start()
    {
        deltaPosition = Vector2.zero;
    }

    void Update ()
    {
        // Calculate the velocity for this frame - New
        velocity += acceleration * Time.fixedDeltaTime;

        UpdateDirection();

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        deltaPosition += (velocity * Time.deltaTime);        

        // Zero out acceleration - New
        acceleration = Vector3.zero;
    }

    void FixedUpdate()
    {
        deltaPosition += transform.position;

        CheckForScreenEdge();

        rBody2D.MovePosition(deltaPosition);

        deltaPosition = Vector3.zero;
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
        transform.rotation = Quaternion.LookRotation(Vector3.back, direction);

        //float angle = Mathf.Atan2(direction.y, direction.x);

        //transform.rotation = Quaternion.Euler(0f, 0f, (angle * Mathf.Rad2Deg) + rotationOffset);
    }

    public void CheckForScreenEdge()
    {
        if (deltaPosition.x > simData.screenSizeX)
        {
            deltaPosition.x = -simData.screenSizeX;
        }
        else if (deltaPosition.x < -simData.screenSizeX)
        {
            deltaPosition.x = simData.screenSizeX;
        }

        if (deltaPosition.y > simData.screenSizeY)
        {
            deltaPosition.y = -simData.screenSizeY;
        }
        else if (deltaPosition.y < -simData.screenSizeY)
        {
            deltaPosition.y = simData.screenSizeY;
        }
    }
}
