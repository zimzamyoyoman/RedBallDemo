using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    // Ball Physics Variables
    [SerializeField] float ballSpeed = 1000f;
    [SerializeField] float jumpSpeed = 1000f;
    [SerializeField] float touchInputForceDirection = 10f;

    // Cached References
    private Touch touch;
    private Rigidbody ballRigidBody;
    private float screenWidth;

    // State
    private bool ballTouched = false;
    private bool gameHasStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        // Initialize cached references
        ballRigidBody = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gameHasStarted)
        {
            StartGame();
        }
        if (gameHasStarted)
        {
            GetTouchInput();
            MoveBallAcrossPlatform();
        }
    }

    private void MoveBallAcrossPlatform()
    {
        ballRigidBody.AddForce(0, 0, 1000 * Time.deltaTime);
    }

    private void StartGame()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                gameHasStarted = true;
            }
        }
        
    }

    private void GetTouchInput()
    {
        // Get touch input
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                // When touch starts
                case TouchPhase.Began:
                    // Form raycast from camera to ball
                    Ray raycast = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        // Make ball jump if raycast collides with it
                        if (raycastHit.collider.CompareTag("Ball"))
                        {
                            moveBallVertically(touchInputForceDirection);
                            // change ball state 
                            ballTouched = true;
                        }

                        // Move ball horizontally on the basis of touch position with respect to screen width
                        else
                        {
                            // Move ball right
                            if (touch.position.x > screenWidth / 2)
                            {
                                moveBallHorizontally(touchInputForceDirection);
                            }

                            // Move ball left
                            else if (touch.position.x < screenWidth / 2)
                            {
                                moveBallHorizontally(-touchInputForceDirection);
                            }
                        }
                    }

                    break;

                // When touch ends
                case TouchPhase.Ended:
                    // decrease ball speed on the basis of ball state
                    if (!ballTouched)
                    {
                        ballRigidBody.velocity = Vector3.zero;
                        ballRigidBody.angularVelocity = Vector3.zero;
                    }
                    else
                    {
                        ballRigidBody.velocity *= 0.9f;
                        ballRigidBody.angularVelocity *= 0.9f;
                    }
                    // revert back ball state
                    ballTouched = false;
                    break;
            }
        }
    }

    // Make ball jump
    private void moveBallVertically(float touchInputForceDirection)
    {
        ballRigidBody.AddForce(new Vector3(0, touchInputForceDirection * jumpSpeed * Time.deltaTime, 0));
    }

    // Make ball move horizontally
    private void moveBallHorizontally(float touchInputForceDirection)
    {
        ballRigidBody.AddForce(new Vector3(touchInputForceDirection * ballSpeed * Time.deltaTime, 0, 0));
    }
}
