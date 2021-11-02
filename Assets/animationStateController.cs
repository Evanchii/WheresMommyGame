using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

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

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, isOnCooldown, isDead;


    private void Patrolling()
    {
        animator.SetBool("isAtk", false);
        animator.SetBool("isWalking", true);
        Debug.Log("Patrolling");
        if (!walkPointSet)
        {
            animator.SetBool("isWalking", false);
            SearchWalkPoint();
        }

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

        walkPoint = new Vector3(transform.position.x + randomX, 0.1f, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
        Debug.Log("wps-swp: " + walkPointSet);
    }

    private void ChasePlayer()
    {
        animator.SetBool("isAtk", false);
        animator.SetBool("isWalking", true);
        agent.SetDestination(player.position);
    }

    private IEnumerator AttackPlayer()
    {
        Debug.Log("Attack");
        isOnCooldown = true;
        agent.SetDestination(transform.position);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAtk", true);
        AnimatorStateInfo ASI = animator.GetCurrentAnimatorStateInfo(0);
        if (Random.Range(0, 10) > 4)
        {
            GameObject Touchpad = GameObject.FindWithTag("visibility1"),
                Joystick = GameObject.FindWithTag("visibility2"),
                firebtn = GameObject.FindWithTag("visibility3"),
                interactbtn = GameObject.FindWithTag("visibility4");
            VideoPlayer vid = GameObject.FindWithTag("visibility5").GetComponent<VideoPlayer>();

            Touchpad.SetActive(false);
            Joystick.SetActive(false);
            firebtn.SetActive(false);
            interactbtn.SetActive(false);

            Debug.Log("You DEAD!");
            //Destroy(player.gameObject);

            //play cutscene death
            //vid.gameObject.SetActive(true);
            Debug.Log("VIDEO PLAY DONE!");
            isDead = true;
        }

        yield return new WaitForSeconds(5f);
        isOnCooldown = false;
        animator.SetBool("isAtk", false);
    }



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        isOnCooldown = false;
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        Debug.Log("sight" + playerInSightRange);
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange && !isOnCooldown && !isDead)
        {
            isOnCooldown = true;
            StartCoroutine(AttackPlayer());
        }
    }
}
