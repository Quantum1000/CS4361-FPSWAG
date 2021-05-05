// Temporary character movement script to test terrain
// this is for real now lol

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float gravity = 13f;
    [SerializeField] [Range(0f, 0.5f)] float moveSmoothTime = 0.3f;
    //[SerializeField] [Range(0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    Vector3 eulers;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 eulers = transform.rotation.eulerAngles;
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {
        // Rewritten as by Sean. Got rid of the smoothing for now, keeping both inputs together as a vector was probably a good idea.
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        // Changed the code to keep track of which way the camera should be facing, update that with the delta, and then assign these rotations to the correct objects.
        eulers = eulers + new Vector3(-mouseDelta.y, mouseDelta.x, 0);
        eulers.x = Mathf.Clamp(eulers.x, -90, 90);
        transform.localEulerAngles = Vector3.Scale(eulers, Vector3.up);
        playerCamera.localEulerAngles = Vector3.Scale(eulers, Vector3.right);

    }

    void UpdateMovement()
    {

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // This is good enough on a keyboard, but when using a controller, this does not allow fine speed control. A different solution would be needed.
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0f;

        velocityY -= gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

    }
}
