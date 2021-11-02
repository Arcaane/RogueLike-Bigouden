using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.AI;

public class IABarman : MonoBehaviour
{
    #region Variables
    // Utilities
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    public Transform shootPoint;
    private Vector2 pos;

    // Bezier Param
    public Transform angularPointBezier;
    private LineRenderer _lineRenderer;
    private int numPoints = 20;
    private Vector3[] positions = new Vector3[20];

    // Cocktails Projectiles Parameter
    private float tParam;
    private Vector2 projectilePosition;
    public GameObject cocktail;
    
    
    // Ints
    [SerializeField] public int lifePoint; // Point de vie de l'unité
    [SerializeField] private int shieldPoint; // Point de l'armure de l'unité
    
    // Floats
    [SerializeField] private float detectZone; // Fov
    [SerializeField] private float attackRange; // Portée de l'attaque
    [SerializeField] private float delayAttack; // Delay entre les attack des ennemis
    [SerializeField] private float timeBeforeAggro; // Delay avant que les ennemis aggro le joueur
    [SerializeField] private float movementSpeed; // Vitesse de déplacement de l'unité
    
    // Variable spé Barman
    [SerializeField] private float rangeAoe; // L'unité est stun ?
    [SerializeField] private int damageAoe; // Range de l'aoe du coktail du serveur
    [SerializeField] private int damageAoeBeforeExplosion; // Nombre de balles que tire le shooter
    
    // Bools
    [SerializeField] private bool isPlayerInAggroRange;
    [SerializeField] private bool isPlayerInAttackRange; // Le player est-il en range ?
    [SerializeField] private bool isReadyToShoot; // Peut tirer ?
    [SerializeField] private bool isAggro; // L'unité chase le joueur ?
    [SerializeField] private bool isAttacking; // L'unité attaque ?
    [SerializeField] private bool isStun; // L'unité est stun ?
    [SerializeField] private bool isRdyMove;
    
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = numPoints;
        
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        isAggro = false;
        isRdyMove = false;
        isReadyToShoot = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    // Update is called once per frame
    private void Update()
    {
        angularPointBezier.position = new Vector3((target.position.x + shootPoint.position.x) / 2,
            (target.position.y + shootPoint.position.y) / 2 + 3, 0);
        
        DrawQuadraticCurve();
            
        isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < detectZone;
        isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;
    
        if (!isPlayerInAggroRange && isAggro)
            Patrolling();
        if (isPlayerInAggroRange && isAggro)
            ChasePlayer();
        if ( isPlayerInAggroRange && isPlayerInAttackRange && isAggro )
            Attacking();
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(shootPoint.position, 0.2f);
    }

    #region PatrollingState
    private void Patrolling()
    {
        if (isRdyMove)
        {
            StartCoroutine(ResetPath());
        }
    }
    Vector3 GetNewRandomPosition()
    {
        float x = Random.Range(-3, 4);
        float y = Random.Range(-3, 4);
        pos = new Vector2(x, y);
        Debug.DrawLine(transform.position, pos, Color.white);
        return pos;
    }
    private void GetNewPath()
    {
        pos = GetNewRandomPosition();
        agent.SetDestination(pos);
    }
    IEnumerator ResetPath()
    {
        isRdyMove = false;
        GetNewPath();
        yield return new WaitForSeconds(3f);
        isRdyMove = true;
    }
    #endregion

    #region ChaseState

    private void ChasePlayer()
    { agent.SetDestination(target.position); }
    #endregion
    
    #region AttackState
    private void Attacking()
    {
        agent.SetDestination(transform.position);
        if (isReadyToShoot)
            StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        isReadyToShoot = false;
        // Play an attack animation
    
        int cocktailRand = Random.Range(1, 4);
        // 1 = Dmg (+) / 2 = Dmg (-) / 3 = Vie + aux ennemis

        var projectile = Instantiate(cocktail, shootPoint);
        for (int i = 0; i < positions.Length; i++)
        {
            yield return new WaitForSeconds(0.01f);
            projectile.transform.position = positions[i];
        }

        if (projectile.transform.position == positions[positions.Length - 1])
        {
            projectilePosition = projectile.transform.position;
            BreakProjectile(cocktailRand);
            Destroy(projectile);
        }
        StartCoroutine(ResetShoot());
    }
        
    
    private IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(delayAttack);
        isReadyToShoot = true;
    }
    #endregion
    
    private void WaitToGo()
    {
        isAggro = true;
        isRdyMove = true;
        isReadyToShoot = true;
    }

    public Vector3 CalculateQuadraticBezierCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2) // Calculer la courbe du coktail
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    private void DrawQuadraticCurve()
    {
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float) numPoints;
            positions[i] =
                CalculateQuadraticBezierCurve(t, shootPoint.position, angularPointBezier.position, target.position);
        }
        _lineRenderer.SetPositions(positions);
    }

    #region ProjectileBehaviour

    private void BreakProjectile(int projectileType)
    {
        switch (projectileType)
        {
            case 1:
                ObjectPooler.Instance.SpawnFromPool("ProjectileBarman1", projectilePosition, Quaternion.identity);
                break;
            case 2:
                ObjectPooler.Instance.SpawnFromPool("ProjectileBarman2", projectilePosition, Quaternion.identity);
                break;
            case 3:
                ObjectPooler.Instance.SpawnFromPool("ProjectileBarman3", projectilePosition, Quaternion.identity);
                break;
        }
    }
    #endregion
}