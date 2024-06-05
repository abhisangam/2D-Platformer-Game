using System;
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

    [SerializeField]
    private float gravityMultiplier = 1.0f;

    [SerializeField]
    private PlayerHealthManager playerHealthManager;

    public bool isGrounded { get; private set; }

    //This really doesn't belong here
    [SerializeField] private ScoreManager scoreManager;

    // Start is called before the first frame update

    private bool isCrouching = false;
    private bool isJumping = false;

    private Rigidbody2D playerRigidBody;

    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;
    private float verticalVelocity = 0.0f;



    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody2D>(); 

        playerHealthManager.OnPlayerDead += OnPlayerDead;
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        Debug.Log("Velocity: " + verticalVelocity);
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
    }

    void MovePlayer(float horizontal, float vertical)
    {
        Vector3 displacement = Vector3.zero;
        if (horizontal < 0.0f)
        {
            displacement.x = -runSpeed * Time.deltaTime;
            //playerRigidBody.MovePosition(transform.position + new Vector3(-runSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        else if (horizontal > 0.0f)
        {
            displacement.x = runSpeed * Time.deltaTime;
            //playerRigidBody.MovePosition(transform.position + new Vector3(runSpeed * Time.deltaTime, 0.0f, 0.0f));
        }

        if (verticalVelocity <= 0.0f && !isGrounded)
        {
            isJumping = true;
        }

        if (isJumping)//in air
        {
            if (isGrounded && (verticalVelocity <= 0.0f)) //if touches ground coming down
            {
                isJumping = false;
            }
            else
            {
                verticalVelocity += Physics2D.gravity.y * Time.fixedDeltaTime * gravityMultiplier;
            }
        }

        if (vertical > 0.0f && isGrounded && !isJumping)
        {
            //playerRigidBody.AddForce(new Vector2(0.0f, jumpSpeed), ForceMode2D.Impulse);
            verticalVelocity = jumpSpeed;
            animator.SetTrigger("Jump");    
            isJumping = true;
        }

        displacement.y = verticalVelocity * Time.fixedDeltaTime;

        playerRigidBody.MovePosition(transform.position + displacement);
    }

    internal void CollectKey()
    {
        scoreManager.IncrementScore(10);
    }

    internal void PlayerInjured()
    {
        playerHealthManager.RegisterPlayerInjury();
    }

    private void OnPlayerDead()
    {
        animator.SetTrigger("Death");
        verticalVelocity = jumpSpeed;
        isJumping = true;
        playerHealthManager.OnPlayerDead -= OnPlayerDead;
        this.enabled = false;
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();

        MovePlayer(horizontalInput, verticalInput);
        PlayPlayerMovementAnimations(horizontalInput, verticalInput);
    }

    bool IsGrounded()
    {
        bool groundCheckResult = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 100, LayerMask.GetMask("Platform"));
        if (hit.collider != null)
        {
            // Calculate the distance from the surface
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            if (distance < 0.05f && (Vector2.Dot(hit.normal, Vector2.up) > 0.8f))
            {
                groundCheckResult = true;
            }
            else
            {
                groundCheckResult = false;
            }
        }

        return groundCheckResult;
    }
}
