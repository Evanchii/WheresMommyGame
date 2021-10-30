using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    /*private void Awake() 
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }*/

    private void Patrolling()
    {
        animator.SetBool("isAtk", false);
        animator.SetBool("isWalking", true);
        Debug.Log("Patrolling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
        Debug.Log("wps-swp: " + walkPointSet);
    }

    private void ChasePlayer()
    {
        animator.SetBool("isAtk", false);
        animator.SetBool("isWalking", true);
        //Debug.Log("Chase");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAtk", true);
        //Debug.Log("Attack");
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform.position);
        //Debug.Log(player.transform.position.x + " " + player.position.y + " " + player.transform.position.z);

        if (!alreadyAttacked)
        {
            //insert attack

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        Debug.Log("sight" + playerInSightRange);
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }
}
