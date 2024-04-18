using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float runSpeed;

    // Start is called before the first frame update

    private bool isCrouching = false;
    private bool isJumping = false;

    private Rigidbody2D playerRigidBody;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        MovePlayer(horizontalInput, verticalInput);
        PlayPlayerMovementAnimations(horizontalInput, verticalInput);

    }

    private void PlayPlayerMovementAnimations(float horizontal, float vertical)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 scale = transform.localScale;
        if (horizontal < 0.0f)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0.0f)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                animator.SetBool("Crouch", true);
                isCrouching = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", false);
            isCrouching = false;
        }

        if (vertical > 0.0f)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }

    void MovePlayer(float horizontal, float vertical)
    {
        if (horizontal < 0.0f)
        {
            transform.position = transform.position + new Vector3(-runSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else if (horizontal > 0.0f)
        {
            transform.position = transform.position + new Vector3(runSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        if (vertical > 0.0f)
        {
            playerRigidBody.AddForce(new Vector2(0.0f, jumpSpeed), ForceMode2D.Force);
        }
        
    }
}