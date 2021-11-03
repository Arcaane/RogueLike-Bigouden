using UnityEngine;
using UnityEngine.AI;

public class EnnemyIA : MonoBehaviour
{
    private void Awake()
    {
        readyToShoot = true;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        // Check for player in range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange && !playerInSightRange)
            Patrolling();
        if (!playerInAttackRange && playerInSightRange)
            ChasePlayer();
        if (playerInSightRange && playerInAttackRange)
            Attacking();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void Patrolling()
    {
        if (!walkPointSet)
            SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        var distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        var randomY = Random.Range(-walkPointRange, walkPointRange);
        var randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (readyToShoot)
            EnnemyShoot();
    }

    private void EnnemyShoot()
    {
        readyToShoot = false;
        ObjectPooler.Instance.SpawnFromPool("Bullet", shootPoint.position, Quaternion.identity);
        Invoke(nameof(ResetShoot), timeBetweenAttacks);
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    #region Variables

    // Utility
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Patroling
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;


    // States
    public float sightRange = 2f, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    // Attacktype
    public float timeBetweenAttacks = .5f; // Temps du reset
    private bool readyToShoot; // Bool pour le reset
    private Transform shootPoint;
    private GameObject bulletPrefab;

    #endregion
}