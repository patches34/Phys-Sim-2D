using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    PhysicsObject physicsObject;

    Vector2 direction = Vector2.zero;

    [SerializeField]
    float speed = 0f;

    [SerializeField]
    float changeTime = 5f;
    float changeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        changeTimer -= Time.deltaTime;

        if (changeTimer < 0f)
        {
            direction = Random.insideUnitCircle;
            direction.Normalize();

            changeTimer = changeTime;
        }
        physicsObject.ApplyForce(direction * speed);
    }
}
