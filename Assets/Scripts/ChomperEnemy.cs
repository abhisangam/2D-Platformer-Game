using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChomperEnemy : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 1.0f;
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] private PlayerController playerController;

    private Animator animator;

    private int currentPatrolPointIndex;

    private List<Vector3> patrolPointsGlobal;

    [SerializeField]
    private float attackRate; // attacks per second
    private float attackTimer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        patrolPointsGlobal = new List<Vector3>();
        for(int i = 0; i < patrolPoints.Count; i++)
        {
            patrolPointsGlobal.Add(new Vector3(patrolPoints[i].position.x, patrolPoints[i].position.y, patrolPoints[i].position.z));
        }

        if(patrolPoints.Count > 1)
        {
            animator.SetBool("IsWalking", true);
        }

        attackTimer = 1.0f / attackRate;
    }

    // Update is called once per frame
    void Update()
    {
        //      Debug.Log(patrolPointsGlobal[currentPatrolPointIndex]);
        if (patrolPoints.Count > 1)
        {
            float distance = Vector3.Distance(patrolPointsGlobal[currentPatrolPointIndex], transform.position);
            //Debug.Log("Distance: " + distance);
            if (distance < patrolSpeed * Time.deltaTime)
            {
                currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Count;
            }
            MoveTowardsPatrolPoint();
        }

        attackTimer -= Time.deltaTime;
        if(attackTimer < 0f) attackTimer = 0f;
    }

    void MoveTowardsPatrolPoint()
    {
        Vector3 direction = patrolPointsGlobal[currentPatrolPointIndex] - transform.position;
        direction.Normalize();
        if(direction.x < 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        transform.position += direction * Time.deltaTime * patrolSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            
            playerController.PlayerInjured();
            attackTimer = 1.0f / attackRate;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (attackTimer <= 0)
            {
                playerController.PlayerInjured();
                attackTimer = 1.0f / attackRate;
            }
        }
    }
}
