using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.AI;

public class IABarman : MonoBehaviour
{
    #region Variables Barman Assignation
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
    
    // Floats
    [SerializeField] private float _detectZone; // Fov
    [SerializeField] private float _attackRange; // Portée de l'attaque
    [SerializeField] private float _delayAttack; // Delay entre les attack des ennemis
    [SerializeField] private float _timeBeforeAggro; // Delay avant que les ennemis aggro le joueur
    [SerializeField] private float _movementSpeed; // Vitesse de déplacement de l'unité
    
    // Variable spé Barman
    [SerializeField] private float _rangeAoe; // 
    [SerializeField] private int _damageAoe; // Range de l'aoe du coktail du serveur
    [SerializeField] private int _damageAoeAfterExplosion; // Nombre de balles que tire le shooter
    
    // Bools
    [SerializeField] private bool _isPlayerInAggroRange;
    [SerializeField] private bool _isPlayerInAttackRange; // Le player est-il en range ?
    [SerializeField] private bool _isReadyToShoot; // Peut tirer ?
    [SerializeField] private bool _isAggro; // L'unité chase le joueur ?
    [SerializeField] private bool _isAttacking; // L'unité attaque ?
    [SerializeField] private bool _isRdyMove;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _detectZone = GetComponent<EnnemyStatsManager>().detectZone;
        _attackRange = GetComponent<EnnemyStatsManager>().attackRange;
        _delayAttack = GetComponent<EnnemyStatsManager>().attackDelay;
        _timeBeforeAggro = GetComponent<EnnemyStatsManager>().timeBeforeAggro;
        _movementSpeed = GetComponent<EnnemyStatsManager>().movementSpeed;
        _rangeAoe = GetComponent<EnnemyStatsManager>().rangeAoe;
        _damageAoe = GetComponent<EnnemyStatsManager>().damageAoe;
        _damageAoeAfterExplosion = GetComponent<EnnemyStatsManager>().damageAoeAfterExplosion;
        
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = numPoints;
        agent.speed = _movementSpeed;
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        _isAggro = false;
        _isRdyMove = false;
        _isReadyToShoot = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Invoke(nameof(WaitToGo), _timeBeforeAggro);
    }

    // Update is called once per frame
    private void Update()
    {
        angularPointBezier.position = new Vector3((target.position.x + shootPoint.position.x) / 2,
            (target.position.y + shootPoint.position.y) / 2 + 3, 0);
        
        DrawQuadraticCurve();
            
        _isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < _detectZone;
        _isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < _attackRange;
    
        if (!_isPlayerInAggroRange && _isAggro)
            Patrolling();
        if (_isPlayerInAggroRange && _isAggro)
            ChasePlayer();
        if ( _isPlayerInAggroRange && _isPlayerInAttackRange && _isAggro )
            Attacking();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(shootPoint.position, 0.2f);
    }

    #region PatrollingState
    private void Patrolling()
    {
        if (_isRdyMove)
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
        _isRdyMove = false;
        GetNewPath();
        yield return new WaitForSeconds(3f);
        _isRdyMove = true;
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
        if (_isReadyToShoot)
            StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        _isReadyToShoot = false;
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
        yield return new WaitForSeconds(_delayAttack);
        _isReadyToShoot = true;
    }
    #endregion
    
    private void WaitToGo()
    {
        _isAggro = true;
        _isRdyMove = true;
        _isReadyToShoot = true;
    }

    

    #region ProjectileBehaviour
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