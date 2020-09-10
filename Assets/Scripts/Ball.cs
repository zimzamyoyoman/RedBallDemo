using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] float ballSpeed = 1000f;
    [SerializeField] float touchInputForceDirection = 10f;

    private Touch touch;
    private Rigidbody ballRigidBody;
    private float screenWidth;


    // Start is called before the first frame update
    void Start()
    {
        ballRigidBody = ball.GetComponent<Rigidbody>();
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
                    if (touch.position.x > screenWidth / 2)
                    {
                        moveBall(touchInputForceDirection);
                    }

                    if (touch.position.x < screenWidth / 2)
                    {
                        moveBall(-touchInputForceDirection);
                    }
                    break;

                case TouchPhase.Ended:
                    ballRigidBody.velocity = Vector3.zero;
                    ballRigidBody.angularVelocity = Vector3.zero;
                    break;
            }
        }
    }

    private void moveBall(float touchInputForceDirection)
    {
        ballRigidBody.AddForce(new Vector3(touchInputForceDirection * ballSpeed * Time.deltaTime, 0, 0));
    }
}
