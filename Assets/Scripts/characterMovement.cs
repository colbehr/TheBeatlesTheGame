using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    public float jumpHeight;
    public float stepDown;
    public float gravity;
    Vector2 input;
    Animator animator;
    CharacterController characterController;
    Vector3 rootMotion;
    Vector3 velocity;
    bool isJumping;
    public float turn = 15;
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        //lock cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //get x y input from keyboard
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        //set params for animations
        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveZ", input.y);
        //if the character is not moving, let user control where the camera sits
        if (input.x  > .1 || input.y > .1 )
        {
            //otherwise the character turns to where the camera is pointing
            float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turn * Time.deltaTime);
        }
        //if the player hits the jump key, jump
        if (Input.GetAxis("Jump") > 0)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        //if the character is jumping, bring the character back to the ground and play the jump anim
        if (isJumping)
        {
            velocity.y -= gravity * Time.fixedDeltaTime;
            characterController.Move(velocity * Time.fixedDeltaTime);
            isJumping = !characterController.isGrounded;
            animator.SetBool("isJumping", isJumping);
        }
        else
        {
            //otherwise move the character
            characterController.Move(rootMotion + Vector3.down * stepDown);
            rootMotion = Vector3.zero;
        }
    }


    void Jump()
    {
        //dont let the character jump multiple times
        if (!isJumping)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            velocity = animator.velocity;
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }
}
