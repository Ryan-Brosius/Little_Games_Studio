using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player_Movement : MonoBehaviour

    // We can call upon certain things, like horizontal and vertical under "services" -> "General settings".
    // Horizontal and vertical movement has been determined, in a way, for us by unity, including WASD key inputs.
    // TO DO:
    // Toggle jump - Current item to do.
    // Toggle sprint
    // 
{
    // Something to let the script reference, which should be the player object (capsule) in this case, and a way to rotate the player when turning.
    public Transform player;
    public Transform playerRotation;
    Vector3 playerMovement;

    Rigidbody rb;

    public float maxVelocity;

    bool isGrounded;

    public float playerForce;

    // Set player speed for how fast the player is. Adjustable by increasing or decreasing for faster or slower speeds respectively.
    public float playerSpeed = 3f;

    // Some variables for player input. Forward and backward (obvious), and horizontal for left and right movement.
    private float verticalMovement;
    private float horizontalMovement;


    // Start is called before the first frame update
    void Start()
    {
        // Add a rigid body component to the player.
        rb = GetComponent<Rigidbody>();
        // we do not want the player to fall over during movement.
        rb.freezeRotation = true;

    }


    // Update is called once per frame
    void Update()
    {
        // Similar to the mouse, we ge the built in unity axes and assign them so we can move our player along the axes and according to
        // the camera orientation.
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        // This bit of code calculates the movement direction for the player, according to the current orientation of the player / camera.
        playerMovement = playerRotation.forward * verticalMovement + playerRotation.right * horizontalMovement;
        playerMovement.y = 0f;

        rb.AddRelativeForce(playerMovement.normalized * playerSpeed * 3f, ForceMode.Force);

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector3.up * 80f, ForceMode.Force);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
       if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
