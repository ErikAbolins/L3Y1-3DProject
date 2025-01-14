using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
   
   public float speed = 10f;
   public float acceleration = 2f;
   public float deceleration = 3f;
   public float steeringAngle = 30f;
   public float brakeForce = 5f;

   public float currentSpeed = 0f;
   private float horizontalInput;
   private float verticalInput;
   
    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0)
        {
            currentSpeed += acceleration * verticalInput * Time.deltaTime;
        }      
        else if (verticalInput > 0)
        {
            currentSpeed -= brakeForce * -verticalInput * Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -speed / 2f, speed);
        
        float steer = horizontalInput * steeringAngle * Time.deltaTime;
        transform.Rotate(0f, steer, 0f);

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }
}
