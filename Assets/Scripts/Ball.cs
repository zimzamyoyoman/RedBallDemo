using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [SerializeField] float ballSpeed = 1000f;
    [SerializeField] float jumpSpeed = 1000f;
    [SerializeField] float touchInputForceDirection = 10f;

    private Touch touch;
    private Rigidbody ballRigidBody;
    private float screenWidth;
    private bool ballTouched = false;


    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        GetTouchInput();
    }

    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray raycast = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        if (raycastHit.collider.CompareTag("Ball"))
                        {
                            moveBallVertically(touchInputForceDirection);
                            ballTouched = true;
                        }

                        else
                        {
                            if (touch.position.x > screenWidth / 2)
                            {
                                moveBallHorizontally(touchInputForceDirection);
                            }

                            else if (touch.position.x < screenWidth / 2)
                            {
                                moveBallHorizontally(-touchInputForceDirection);
                            }
                        }
                    }

                    break;

                case TouchPhase.Ended:
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
                    ballTouched = false;
                    break;
            }
        }
    }

    private void moveBallVertically(float touchInputForceDirection)
    {
        ballRigidBody.AddForce(new Vector3(0, touchInputForceDirection * jumpSpeed * Time.deltaTime, 0));
    }

    private void moveBallHorizontally(float touchInputForceDirection)
    {
        ballRigidBody.AddForce(new Vector3(touchInputForceDirection * ballSpeed * Time.deltaTime, 0, 0));
    }
}
