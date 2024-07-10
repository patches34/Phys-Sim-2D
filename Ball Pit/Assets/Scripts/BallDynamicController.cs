using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallDynamicController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_Rigidbody;

    Vector2 direction = Vector2.zero;

    Vector3 deltaPosition;

    [SerializeField]
    float speed = 0f;

    [SerializeField]
    float changeTime = 5f;
    float changeTimer = 0f;

    [SerializeField]
    SimDataSO simData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaPosition = transform.position;
        changeTimer -= Time.deltaTime;

        if (changeTimer < 0f)
        {
            direction = Random.insideUnitCircle;
            direction.Normalize();

            changeTimer = changeTime;
        }



        UpdateDirection();

        CheckForScreenEdge();

        transform.position = deltaPosition;
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

    void UpdateDirection()
    {
        // Grab current velocity  - New
        transform.rotation = Quaternion.LookRotation(Vector3.back, m_Rigidbody.velocity);
    }

    private void FixedUpdate()
    {
        m_Rigidbody.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
