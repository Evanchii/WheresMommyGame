using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    public NavMeshAgent agent;
    public Transform player, enemy;
    public LayerMask whatIsGround, whatIsPlayer;
    public GameObject vid;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetAttacks;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, isOnCooldown, isDead = false;


    private void Patrolling()
    {
        animator.SetBool("isAtk", false);
        animator.SetBool("isWalking", true);
        if (!walkPointSet)
        {
            animator.SetBool("isWalking", false);
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity, Vector3.up);
            agent.SetDestination(walkPoint);
        }

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
    }

    private void ChasePlayer()
    {
        animator.SetBool("isAtk", false);
        animator.SetBool("isWalking", true);
        transform.rotation = Quaternion.LookRotation(agent.velocity, Vector3.up);
        agent.SetDestination(player.position);
    }

    private IEnumerator AttackPlayer()
    {
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

            GameObject playerMesh = GameObject.FindWithTag("PlayerMesh");
            AudioSource bgm = playerMesh.gameObject.GetComponent<AudioSource>();

            bgm.Stop();

            Touchpad.SetActive(false);
            Joystick.SetActive(false);
            firebtn.SetActive(false);
            interactbtn.SetActive(false);
            vid.SetActive(true);

            isDead = true;
            agent.Move(new Vector3(10, -10, 10));
            enemy.transform.position = new Vector3(10, -10, 10);

            yield return new WaitForSeconds(18f);
            Destroy(player.gameObject, 2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        yield return new WaitForSeconds(5f);
        isOnCooldown = false;
        animator.SetBool("isAtk", false);
    }



    // Start is called before the first frame update
    void Start()
    {
        vid = GameObject.FindWithTag("visibility5");
        vid.SetActive(false);
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        enemy = GameObject.FindWithTag("Enemy").transform;
        agent = GetComponent<NavMeshAgent>();
        isOnCooldown = false;
        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerInSightRange && !playerInAttackRange) Patrolling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInSightRange && playerInAttackRange && !isOnCooldown)
            {
                isOnCooldown = true;
                StartCoroutine(AttackPlayer());
            }
        } else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isAtk", false);

            agent.Move(new Vector3(10, -10, 10));
            enemy.transform.position = new Vector3(10, -10, 10);
        }
    }
}
