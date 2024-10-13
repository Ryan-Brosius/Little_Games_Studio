using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class FP_camera : MonoBehaviour
{
    // We already moved the camera to the top of the player. Also, we set the camera to be a child
    // of the player. Because this is first person, it needs some modifications instead of just
    // attaching the camera as a child to the player.


    // Allows us to attach the actual player to the script, allowing the camera to know
    // which object to follow.
    public Transform player;

    // Values for the camera rotation speed, as well as values for getting the x and y axis of the mouse for movement.
    public float camera_y_rotation_speed = 2f;
    public float camera_x_rotation_speed = 2f;
    private float x_axis;
    private float y_axis;

    // Start is called before the first frame update
    void Start()
    {
        // Mostly QOL stuff. The first line hides the cursor so it isnt visible during gameplay.
        // Cursor.lockstate "locks" the cursor into the game window, so you can't click anything outside the game window.
        // Hitting escape unlocks and unhides the cursor.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        // We get the mouse input via input.GteAxis (we name it x and y), and multiply that by the 
        // respective speeds. Can be increased or descreased for faster or slower "sensitivity".
        // Also, turns out we do negatives for Y axis because of needing negative angles to look up and down.
        x_axis += Input.GetAxis("Mouse X") * camera_x_rotation_speed;
        y_axis -= Input.GetAxis("Mouse Y") * camera_y_rotation_speed;


        // Clamp y axis so it does not spin in a circle, and simulates a real person looking up and down.
        y_axis=Mathf.Clamp(y_axis, -80f, 80f);


        // Moves the camera based on the y and x axis.
        transform.eulerAngles = new Vector3(y_axis, x_axis, 0.0f);
    }
}
