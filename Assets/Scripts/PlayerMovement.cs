
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed;

    public float runningSpeed;

    public float turningSpeed;

    private GatherInput gatherInput;

    private Rigidbody rb;

    Camera mainCamera;

    private Vector3 movementVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gatherInput = GetComponent<GatherInput>();

        movementVector = Vector3.zero;

        mainCamera = Camera.main;
        mainCamera.transform.SetParent(transform);
        mainCamera.transform.localPosition = new Vector3(0f, 0.75f, 0f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {
        
        //Debug.Log("MouseX: " + gatherInput.MouseX);

        //gameObject.transform.Rotate(0f, gatherInput.MouseX * turningSpeed, 0f);

    }

    public void ResetPlayerSpeed()
    {
        rb.linearVelocity = Vector3.zero;
    }
}
