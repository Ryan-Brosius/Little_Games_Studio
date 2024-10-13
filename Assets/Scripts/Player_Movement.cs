using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player_Movement : MonoBehaviour

    // We can call upon certain things, like horizontal and vertical under "services" -> "General settings".
    // Horizontal and vertical movement has been determined, in a way, for us by unity, including WASD key inputs.
    // TO DO:
    // Toggle jump - Current item to do. Kirill: I think i did it. Not sure if you had the same in mind. Feel free to change it, but do tell me why so i can learn.
    // Toggle sprint
{   
    //Yo, Kirill is here, I am just kind of looking around and learning. Heads up, I left some questions if you gor time you can answear them, Its fine if you don't
    //P.S I tested the movement a little and I think if we want to keep the jumping you might wanna add some more gravity to the plyer after the jump. rn it feels like jumping on the moon.

    // Something to let the script reference, which should be the player object (capsule) in this case, and a way to rotate the player when turning.
    public Transform player;
    public Transform playerRotation;
    Vector3 playerMovement;

    Rigidbody rb;

    public float maxVelocity;

    bool isGrounded;
    //Kirill: I see you created this float but haven't used it yet. Do you have something in mind for it later or you just ended up not needing it? 
    public float playerForce;
    //Kirill: made a public variable for jumping force so we can adjust it as ne meded in unity editor.
    public float jumpForce;
    //Kirill: made that public boolean so we can toggle jumping in unity editor
    public bool jumpActive = false;
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
        //Kirill: here's where I used those two variables I made. 
        if (Input.GetKey(KeyCode.Space) && isGrounded == true && jumpActive)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);
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
