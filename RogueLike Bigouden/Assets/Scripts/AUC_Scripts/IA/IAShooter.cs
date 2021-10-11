using UnityEngine;
using UnityEngine.AI;

public class IAShooter : MonoBehaviour
{
    //Utilities
    [SerializeField] private Transform target;
    public bool playerInAttackRange, readyToShoot, playerAggro;

    // Tweakable Values
    public float attackRange = 5f;
    public float shootForce;
    public float timeToResetShoot = .6f;
    public float timeBeforeAggro = .5f;
    public Transform shootPoint;
    private NavMeshAgent agent;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        playerAggro = false;
        readyToShoot = true;
        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    private void Update()
    {
        playerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;

        if (!playerInAttackRange && playerAggro)
            ChasePlayer();
        if (playerInAttackRange && playerAggro)
            Attacking();
    }

    private void FixedUpdate()
    {
        var lookdir = target.position - rb.transform.position;
        var angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        if (readyToShoot) Shoot();
    }

    private void Shoot()
    {
        shootForce = Random.Range(2.3f, 2.7f);
        readyToShoot = false;
        // Play an attack animation
        var ball = ObjectPooler.Instance.SpawnFromPool("Projectile", shootPoint.position, Quaternion.identity);
        var rbball = ball.GetComponent<Rigidbody2D>();
        rbball.AddForce(shootPoint.right * shootForce, ForceMode2D.Impulse);
        rbball.rotation = rb.rotation;
        Invoke(nameof(ResetShoot), timeToResetShoot);
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    public void LookPlayer(Transform targetTransform, Transform launcherTransform)
    {
        Vector2 lookdir = targetTransform.position - launcherTransform.position;
        var angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void WaitToGo()
    {
        playerAggro = true;
    }
}