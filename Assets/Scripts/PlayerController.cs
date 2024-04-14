using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(playerSpeed));

        Vector3 scale = transform.localScale;
        if(playerSpeed < 0.0f)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if(playerSpeed > 0.0f)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}
