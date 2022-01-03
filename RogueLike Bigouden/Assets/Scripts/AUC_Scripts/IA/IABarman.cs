using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using static TimeManager;

public class IABarman : MonoBehaviour
{
    #region Variables Barman Assignation
    // Utilities
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private Vector2 pos;
    private Vector3 shootPointPos;

    // Bezier Param
    private Vector2 angularPointBezier;
    private LineRenderer _lineRenderer;
    private int numPoints = 20;
    private Vector3[] positions = new Vector3[20];

    // Cocktails Projectiles Parameter
    private float tParam;
    private Vector2 projectilePosition;
    public GameObject cocktail;
    public float testSpeed = 0.03f;
    private Vector2 playerTransform;
    
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

    // Anims
    [SerializeField] private Animator barmanAnimator;
    private bool _isAttack;
    private bool _isWalk;
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
        
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _lineRenderer.positionCount = numPoints;
        agent.speed = _movementSpeed;
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Set bools
        _isAttack = false;
        _isWalk = false;        
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
        DrawQuadraticCurve();
            
        _isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < _detectZone;
        _isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < _attackRange;
    
        if (!_isPlayerInAggroRange && _isAggro)
            Patrolling();
        if (_isPlayerInAggroRange && _isAggro)
            ChasePlayer();
        if ( _isPlayerInAggroRange && _isPlayerInAttackRange && _isAggro )
            Attacking();
        
        //Animations(agent);
        if (agent.velocity.x <= 0.1f && agent.velocity.y <= 0.1f)
        {
            _isWalk = false;
        }
        else { _isWalk = true; }
    }

    private void FixedUpdate()
    {
        Animations(agent);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(shootPointPos, 0.2f);
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
        _isWalk = true; // Anim
        _isAttack = false; // Anim
    }
    IEnumerator ResetPath()
    {
        _isRdyMove = false;
        GetNewPath();
        yield return new WaitForSeconds(3f);
        _isWalk = false; // Anim
        _isAttack = false; // Anim
        _isRdyMove = true;
    }
    #endregion

    #region ChaseState

    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
        _isWalk = true; // Anim
        _isAttack = false; // Anim
    }
    #endregion
    
    #region AttackState

    private const float radiusShootPoint = 0.5f;
    private const float upTofitPlayer = 0.18f;
    private void Attacking()
    {
        _isWalk = false; // Anim

        if (_isReadyToShoot)
        {
            _isAttack = true;
            StartCoroutine(Shoot());
        }
        agent.SetDestination(transform.position);
        Debug.DrawRay(transform.position, new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y + upTofitPlayer), Color.green);
    }
    
    private IEnumerator Shoot()
    {
        playerTransform = target.position;
        _isReadyToShoot = false;
        
        shootPointPos = (target.position - transform.position);
        shootPointPos.Normalize();
        
        angularPointBezier = new Vector3((target.position.x + shootPointPos.x) / 2,
            (target.position.y + shootPointPos.y) / 2 + 1.5f, 0);
        
        
        int cocktailRand = Random.Range(1, 4);
        // 1 = Dmg (+) / 2 = Dmg (-) / 3 = Vie + aux ennemis

        var projectile = Instantiate(cocktail, transform.position + shootPointPos * radiusShootPoint, Quaternion.identity);
        for (int i = 0; i < positions.Length; i++)
        {
            yield return new WaitForSeconds(testSpeed);
            projectile.transform.position = positions[i];
        }

        if (projectile.transform.position == positions[positions.Length - 1])
        {
            projectilePosition = projectile.transform.position;
            BreakProjectile(cocktailRand);
            projectile.SetActive(false);
        }

        _isAttack = false;
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
            positions[i] = CalculateQuadraticBezierCurve(t, transform.position + shootPointPos * radiusShootPoint, angularPointBezier, new Vector2(playerTransform.x, playerTransform.y + upTofitPlayer));
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

    #region Anims
    private void Animations(NavMeshAgent agent)
    {
        if (_isAttack)
        {
            barmanAnimator.SetFloat("Horizontal", shootPointPos.x);
            barmanAnimator.SetFloat("Vertical", shootPointPos.y + upTofitPlayer);
            barmanAnimator.SetBool("isAttack", _isAttack);
            barmanAnimator.SetBool("isWalk", _isWalk);
        }
        else
        {
            barmanAnimator.SetFloat("Horizontal", agent.velocity.x);
            barmanAnimator.SetFloat("Vertical", agent.velocity.y);
            barmanAnimator.SetBool("isAttack", _isAttack);
            barmanAnimator.SetBool("isWalk", _isWalk);
        }
        
        Debug.Log("is attack " + _isAttack);           
        Debug.Log("is Walk " + _isWalk);
        
    }
    #endregion
}