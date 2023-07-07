using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float rotationSpeed = 200f;
    public float acceleration = 2f;
    public float friction = 2f;

    private float currentSpeed;
    private float screenHalfWidth;

    private void Start()
    {
        float screenHeight = Camera.main.orthographicSize;
        float screenWidth = screenHeight * Camera.main.aspect;
        screenHalfWidth = screenWidth;
    }

    private void Update()
    {
        float moveAxis = Input.GetAxis("Vertical");
        float rotationAxis = Input.GetAxis("Horizontal");

        //adjust the current speed based on acceleration and friction
        if (moveAxis != 0f)
        {
            currentSpeed += moveAxis * acceleration * Time.deltaTime;
        }
        else
        {
            float deceleration = friction * Mathf.Sign(currentSpeed);
            currentSpeed -= deceleration * Time.deltaTime;

            if (Mathf.Sign(currentSpeed) != Mathf.Sign(deceleration))
            {
                currentSpeed = 0f;
            }
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        //move the car forward/backward
        transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);

        //wrap the car horizontally
        WrapHorizontal();

        //rotate the car
        transform.Rotate(Vector3.forward * -rotationSpeed * rotationAxis * Time.deltaTime);
    }

    private void WrapHorizontal()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition.x > screenHalfWidth)
        {
            currentPosition.x = -screenHalfWidth;
        }
        else if (currentPosition.x < -screenHalfWidth)
        {
            currentPosition.x = screenHalfWidth;
        }
        transform.position = currentPosition;
    }
}
