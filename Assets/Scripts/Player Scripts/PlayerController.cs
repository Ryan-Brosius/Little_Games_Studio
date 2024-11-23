using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    [SerializeField]
    private Camera cam;

    [Header("Mouse Settings")]
    [SerializeField]
    private float sensitivity = 250f;
    [SerializeField]
    private float xSenMultiplier = 1f, ySenMultiplier = 1.3f;
    private float xRotation, yRotation;

    [Header("Movement Settings")]
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float runSpeed = 10f;
    [SerializeField]
    private float acceleration = 90f;
    private float targetSpeed;
    private float currentSpeed;
    private Vector3 moveDirection = Vector3.zero;

    [Header("Jumping Settings")]
    [SerializeField]
    private bool canJump = true;
    [SerializeField]
    private float jumpHeight = 4.0f;

    [Header("Gravity Settings")]
    [SerializeField]
    private float gravity = 35f;
    private float previousY;

    [Header("Status Flags")]
    public bool isMoving, isRunning, isGrounded;

    [Header("Audio Settings")]
    [SerializeField] private string[] footstepClips;
    [SerializeField]
    private float footstepInterval = 0.5f;
    private float footstepTimer;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        cc.enabled = true;

        xRotation = transform.rotation.eulerAngles.y;
        yRotation = transform.rotation.eulerAngles.x;

        previousY = transform.position.y;

        footstepTimer = footstepInterval;
    }

    // Update is called once per frame
    void Update()
    {
        updateCameraMovement();
        updateMovement();
        UpdateFootsteps();
    }

    void updateCameraMovement()
    {
        xRotation += Input.GetAxis("Mouse X") * (sensitivity * Time.deltaTime) * xSenMultiplier;
        yRotation -= Input.GetAxis("Mouse Y") * (sensitivity * Time.deltaTime) * ySenMultiplier;

        yRotation = Mathf.Clamp(yRotation, -80.0f, 80.0f);

        transform.localRotation = Quaternion.Euler(0.0f, xRotation, 0.0f);
        cam.transform.localRotation = Quaternion.Euler(yRotation, 0.0f, 0.0f);
    }

    void updateMovement()
    {
        isGrounded = cc.isGrounded;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 desiredDirection = (transform.forward * vertical + transform.right * horizontal).normalized;

        targetSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (desiredDirection.magnitude > 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0;
        }

        isRunning = currentSpeed > walkSpeed;

        moveDirection.x = desiredDirection.x * currentSpeed;
        moveDirection.z = desiredDirection.z * currentSpeed;

        if (isGrounded)
        {
            moveDirection.y = -2f;
        }

        if (canJump && Input.GetButtonDown("Jump") && isGrounded)
        {
            moveDirection.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        // Checks to see if players head has hit the ceiling as they jumped
        //if (!isGrounded && previousY == transform.position.y)
        //{
            //moveDirection.y = 0.0f;
        //}
        previousY = transform.position.y;

        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
        isMoving = cc.velocity.sqrMagnitude > 0.0f ? true : false;
    }

    void UpdateFootsteps()
    {
        if (isMoving && isGrounded)
        {
            footstepTimer -= isRunning ? Time.deltaTime * 1.8f : Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                PlayFootstepSound();
                footstepTimer = footstepInterval;
            }
        }
    }

    void PlayFootstepSound()
    {
        if (footstepClips.Length > 0)
        {
            if (GetCurrentSurface() == SurfaceType.Surface.Grass)
            {
                int index = Random.Range(3, footstepClips.Length);
                AudioManager.Instance.getSound(footstepClips[index]).source.pitch = 1.4f + Random.Range(-0.1f, 0.1f);
                AudioManager.Instance.Play(footstepClips[index]);
            } else
            {
                int index = Random.Range(0, 3);
                AudioManager.Instance.getSound(footstepClips[index]).source.pitch = 1f + Random.Range(-0.2f, 0.2f);
                AudioManager.Instance.Play(footstepClips[index]);
            }
        }
    }

    SurfaceType.Surface GetCurrentSurface()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, cc.height / 2 + 0.1f))
        {
            SurfaceType surfaceType = hit.collider.GetComponent<SurfaceType>();
            if (surfaceType != null)
            {
                return surfaceType.surfaceType;
            }
        }
        return SurfaceType.Surface.Default;
    }

}