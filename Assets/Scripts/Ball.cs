using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [SerializeField] float ballSpeed = 1000f;
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
        addPhysicsRaycaster();
    }

    // Update is called once per frame
    void Update()
    {
        GetTouchInput();
    }

    void addPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
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
                            Debug.Log("Ball clicked");
                        }

                        else
                        {
                            if (touch.position.x > screenWidth / 2)
                            {
                                moveBall(touchInputForceDirection);
                            }

                            else if (touch.position.x < screenWidth / 2)
                            {
                                moveBall(-touchInputForceDirection);
                            }
                        }
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

    /*public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name=="Ball")
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
            ballTouched = true;
        }

    }*/
}
