using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABarman : MonoBehaviour
{
    // Utilities
    [SerializeField] private Transform target;
    [SerializeField] private bool playerInAttackRange, readyToShoot, playerAggro;

    // Tweakable Values
    public float attackRange = 1.5f;
    public float timeToResetShoot = 1f;
    public float timeBeforeAggro = .5f;
    public float height; // Hauteur de la parabole
    public Transform shootPoint;

    public int _numberOfElements = 10; // Number of elements should draw in path of parabola
    public Transform _moveableObject; // Objet to move on path
    private readonly List<GameObject> _trajectoryElementsContainer = new List<GameObject>();
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    private float shootForce;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerAggro = false;
        readyToShoot = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    // Update is called once per frame
    private void Update()
    {
        playerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;

        if (playerInAttackRange && playerAggro)
            Attacking();

        float distributionTime = 0;
        for (float i = 1; i <= _numberOfElements; i++)
        {
            distributionTime++;
            var currentPosition =
                SampleParabola(transform.position, target.position, height, i / _numberOfElements);
            _trajectoryElementsContainer[(int) i - 1].transform.position =
                new Vector3(currentPosition.x, currentPosition.y, 0);

            var nextPosition = SampleParabola(transform.position, target.position, height,
                (i + 1) / _numberOfElements);
            var angleInR = Mathf.Atan2(nextPosition.y - currentPosition.y, nextPosition.x - currentPosition.x);
            _trajectoryElementsContainer[(int) i - 1].transform.eulerAngles =
                new Vector3(0, 0, Mathf.Rad2Deg * angleInR - 90);
        }

        if (_moveableObject)
            //Shows how to animate something following a parabola
            _moveableObject.position = SampleParabola(transform.position, target.position, height, Time.time % 1);
    }

    private void FixedUpdate()
    {
        var lookdir = target.position - rb.transform.position;
        var angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnDrawGizmos()
    {
        //Draw the parabola by sample a few times
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
        float count = 20;
        var lastP = transform.position;
        for (float i = 0; i < count + 1; i++)
        {
            var p = SampleParabola(transform.position, target.position, height, i / count);
            Gizmos.color = i % 2 == 0 ? Color.blue : Color.green;
            Gizmos.DrawLine(lastP, p);
            lastP = p;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Attacking()
    {
        if (readyToShoot)
            Shoot();
    }

    private void Shoot()
    {
        shootForce = Random.Range(1.5f, 2.1f);
        height = Random.Range(1, 2);
        readyToShoot = false;
        // Play an attack animation
        var projectile =
            ObjectPooler.Instance.SpawnFromPool("ProjectileBarman", shootPoint.position, Quaternion.identity);
        var rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.position = SampleParabola(transform.position, target.position, height, 0.7f);
        rbProjectile.rotation = rb.rotation;
        Invoke(nameof(ResetShoot), timeToResetShoot);
    }

    private Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        var parabolicT = t * 2 - 1;
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            var travelDirection = end - start;
            var result = start + t * travelDirection;
            result.y += (-parabolicT * parabolicT + 1) * height;
            return result;
        }
        else
        {
            var travelDirection = end - start;
            var levelDirection = end - new Vector3(start.x, end.y, start.z);
            var right = Vector3.Cross(travelDirection, levelDirection);
            var up = Vector3.Cross(right, travelDirection);
            if (end.y > start.y)
                up = -up;
            var result = start + t * travelDirection;
            result += (-parabolicT * parabolicT + 1) * height * up.normalized;
            return result;
        }
    }

    private void ResetShoot()
    {
        readyToShoot = true;
    }

    private void WaitToGo()
    {
        playerAggro = true;
    }
}