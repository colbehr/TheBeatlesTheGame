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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveZ", input.y);

        if (Input.GetAxis("Jump") > 0)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        if (isJumping)
        {
            velocity.y -= gravity * Time.fixedDeltaTime;
            characterController.Move(velocity * Time.fixedDeltaTime);
            isJumping = !characterController.isGrounded;
            animator.SetBool("isJumping", isJumping);

        }
        else
        {
            characterController.Move(rootMotion + Vector3.down * stepDown);
            rootMotion = Vector3.zero;
        }
    }


    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            velocity = animator.velocity;
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }
}
