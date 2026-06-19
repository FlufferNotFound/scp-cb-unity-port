
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed;

    public float runningSpeed;

    private GatherInput gatherInput;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gatherInput = GetComponent<GatherInput>();
    }

    private void Update()
    {

        Move();
    }


    private void Move()
    {   //Forward/backward
        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,  
            rb.linearVelocity.y,
            gatherInput.Moving * walkingSpeed);

        //Left/right
        rb.linearVelocity = new Vector3(
            gatherInput.Strafing * walkingSpeed,
            rb.linearVelocity.y,
            rb.linearVelocity.z
        );
    }

    public void ResetPlayerSpeed()
    {
        rb.linearVelocity = Vector3.zero;
    }
}
