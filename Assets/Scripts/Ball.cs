using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float ballSpeed = 50f;
    [SerializeField] float touchInputForceDirection = 1f;

    private Touch touch;
    private Rigidbody ballRigidBody;
    private float screenWidth;


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

            if (touch.position.x > screenWidth / 2)
            {
                Debug.Log(screenWidth / 2);
                moveBall(touchInputForceDirection);
            }

            if (touch.position.x < screenWidth / 2)
            {
                moveBall(-touchInputForceDirection);
            }
        }
    }

    private void moveBall(float touchInputForceDirection)
    {
        ballRigidBody.AddForce(new Vector3(touchInputForceDirection * ballSpeed * Time.deltaTime, 0, 0));
    }
}
