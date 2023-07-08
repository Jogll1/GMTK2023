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

    public GameObject exhaust;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float screenHeight = Camera.main.orthographicSize;
        float screenWidth = screenHeight * Camera.main.aspect;
        screenHalfWidth = screenWidth;
    }

    private void Update()
    {
        float moveAxis = Input.GetAxis("Vertical");
        float rotationAxis = Input.GetAxis("Horizontal");

        //modify particle system based on speed
        ParticleSystem particles = exhaust.GetComponent<ParticleSystem>();
        var psEmission = particles.emission;
        psEmission.rateOverTime = currentSpeed * 2.5f;

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

        //move the car
        // transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
        Vector2 movement = transform.up * currentSpeed;
        rb.velocity = movement;

        //wrap the car horizontally
        WrapHorizontal();

        //rotate the car
        transform.Rotate(Vector3.forward * -rotationSpeed * rotationAxis * Time.deltaTime);
        // rb.angularVelocity = -rotationSpeed * rotationAxis;
    }

    private void WrapHorizontal()
    {
        //teleport the car if it goes of the side of the screen
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
