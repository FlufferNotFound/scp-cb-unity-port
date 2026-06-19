
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed;

    public float runningSpeed;

    private GatherInput gatherInput;

    private Rigidbody rb;

    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        mainCamera.transform.SetParent(transform);
        mainCamera.transform.localPosition = new Vector3(0f, 0.75f, 0f);

        //Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;


        rb = GetComponent<Rigidbody>();
        gatherInput = GetComponent<GatherInput>();
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {

        switch (gatherInput.IsSprinting)
        {
            case true:
                //Forward/backward
                rb.linearVelocity = new Vector3(
                    rb.linearVelocity.x,
                    rb.linearVelocity.y,
                    gatherInput.MovingDirection * runningSpeed);

                //Left/right
                rb.linearVelocity = new Vector3(
                    gatherInput.StrafingDirection * runningSpeed,
                    rb.linearVelocity.y,
                    rb.linearVelocity.z
                );
                break;

            case false:
                //Forward/backward
                rb.linearVelocity = new Vector3(
                    rb.linearVelocity.x,
                    rb.linearVelocity.y,
                    gatherInput.MovingDirection * walkingSpeed);

                //Left/right
                rb.linearVelocity = new Vector3(
                    gatherInput.StrafingDirection * walkingSpeed,
                    rb.linearVelocity.y,
                    rb.linearVelocity.z
                );
                break;
        }

        gameObject.transform.Rotate(0f, gatherInput.MouseX, 0f);
        mainCamera.transform.Rotate(-gatherInput.MouseY, 0f, 0f);
    }

    public void ResetPlayerSpeed()
    {
        rb.linearVelocity = Vector3.zero;
    }
}
