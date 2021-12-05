using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using Random = UnityEngine.Random;

public class IAShooter : MonoBehaviour
{
    #region Variables Shooter Assignation
    //Utilities
    [SerializeField] private Transform target;
    [SerializeField] private  NavMeshAgent agent;
    [SerializeField] private Animator shooterAnimator;
    
    private Rigidbody2D rb;
    private bool isRdyMove;

    private Vector3 lastPos;
    public Vector3 shootPointPos;

    private EnnemyStatsManager shooterEnnemy;
    // Attack Variables 
    [SerializeField] private float _detectZone;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _timeBeforeAggro;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float tooClose = 1.1f;
    
    [SerializeField] private bool _isPlayerInAggroRange;
    [SerializeField] private bool _isPlayerInAttackRange;
    [SerializeField] private bool _isReadyToShoot;
    [SerializeField] private bool _isAggro;
    // Ennemy State
    [SerializeField] private bool _isAttack;
    [SerializeField] private bool _isWalk;

    [SerializeField] private bool isSpot;
    
    
    public LayerMask isPlayer;
    private Vector3 pos;
    private Vector2 fwd;

    private Vector2 enemyPos;
    private Vector2 targetPos;
    public Vector2 agentVelocity; // Debug Velocity
    
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        tooClose = 0.82f;
        _detectZone = GetComponent<EnnemyStatsManager>().detectZone;
        _attackRange = GetComponent<EnnemyStatsManager>().attackRange;
        _attackDelay = GetComponent<EnnemyStatsManager>().attackDelay;
        _timeBeforeAggro = GetComponent<EnnemyStatsManager>().timeBeforeAggro;
        _movementSpeed = GetComponent<EnnemyStatsManager>().movementSpeed;
        isSpot = false;
        
        // Parametres de l'agent
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        agent.speed = _movementSpeed;
        
        // Set Bools
        _isAggro = false;
        _isReadyToShoot = true;
        isRdyMove = true;
        _isAttack = false;
        _isWalk = false;
        
        _isPlayerInAggroRange = false;
        _isPlayerInAttackRange = false;

        Invoke(nameof(WaitToGo), _timeBeforeAggro);
    }

    private void Update()
    {
        enemyPos = transform.position;
        targetPos = target.position;
        
        _isPlayerInAttackRange = Vector2.Distance(enemyPos, targetPos) < _attackRange;
        _isPlayerInAggroRange = Vector2.Distance(enemyPos, targetPos) < _detectZone;
        bool _isPlayerTooClose = Vector2.Distance(enemyPos, targetPos) < tooClose;
        
        if (_isPlayerTooClose)
        {
            Flee();
            _isPlayerInAttackRange = false;
            _isPlayerInAggroRange = false;
        }
        if (!_isPlayerInAggroRange && !_isPlayerInAttackRange && !_isPlayerTooClose) 
            Patrolling();
        if (_isPlayerInAggroRange && !_isPlayerInAttackRange) 
            ChasePlayer();
        if (_isPlayerInAttackRange && _isPlayerInAggroRange)
            Attacking();
        
            

        agentVelocity = agent.velocity;
        agentVelocity.Normalize();
        Animations(agent);

        if (agent.velocity.x <= 0.1f && agent.velocity.y <= 0.1f)
        {
            _isWalk = false;
        }
        else { _isWalk = true; }
    }

    #region PatrollingState
    private void Patrolling()
    {
        if (isRdyMove)
            StartCoroutine(ResetPath());
    }
    Vector3 GetNewRandomPosition()
    {
        float x = Random.Range(-3, 4);
        float y = Random.Range(-3, 4);
        pos = new Vector2(x, y);
        Debug.Log(pos);
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
        isRdyMove = false;
        GetNewPath();
        yield return new WaitForSeconds(3f);
        _isWalk = false; // Anim
        _isAttack = false; // Anim
        isRdyMove = true;
    }
    #endregion

    #region ChaseState
    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
        _isWalk = true; // Anim
        _isAttack = false; // Anim
        
        if(!isSpot)
            SpottedPlayer();

    }
    #endregion

    #region AttackState
    private void Attacking()
    {
        _isWalk = false; // Anim
        
        if (_isReadyToShoot)
        {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(enemyPos, _attackRange, isPlayer);
            if (hitPlayers != null)
            {
                Shoot();
            }
        }
        agent.SetDestination(enemyPos);
        Debug.DrawRay(enemyPos, new Vector3(targetPos.x - rb.transform.position.x, targetPos.y - rb.transform.position.y -0.17f), Color.green);
    }
    
    private void Shoot()
    {
        _isReadyToShoot = false;
        _isWalk = false; // Anim
        _isAttack = true;
        StartCoroutine(BulletShoot());
    }

    private const float radiusShootPoint = 0.75f;
    private const float upTofitPlayer = 0.18f;
    IEnumerator BulletShoot()
    {
        
        for (int i = 0; i < 5; i++)
        {
            if (!_isWalk)
            {
                shootPointPos = (target.position - transform.position);
                shootPointPos.Normalize();
                yield return new WaitForSeconds(0.2f);
                GameObject obj = ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position + shootPointPos * radiusShootPoint, Quaternion.identity);
                obj.GetComponent<BulletBehaviour>().GoDirection(new Vector2(shootPointPos.x, shootPointPos.y + upTofitPlayer), 6f); // Direction puis Speed des balles
            }

            yield return new WaitForSeconds(0.05f);
        }

        _isAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _isReadyToShoot = true;
    }
    #endregion

    #region FleeState
    private void Flee()
    {
        _isAttack = false;
        _isWalk = true;
        
        StartCoroutine(ResetPath());;
    }
    #endregion
    
    private void WaitToGo()
    {
        _isAggro = true;
    }

    private void Animations(NavMeshAgent agent)
    {
        if (_isAttack)
        {
            shooterAnimator.SetFloat("Horizontal", shootPointPos.x);
            shooterAnimator.SetFloat("Vertical", shootPointPos.y + upTofitPlayer);
            shooterAnimator.SetBool("isAttack", _isAttack);
            shooterAnimator.SetBool("isWalk", _isWalk);
        }
        else
        {
            shooterAnimator.SetFloat("Horizontal", agent.velocity.x);
            shooterAnimator.SetFloat("Vertical", agent.velocity.y);
            shooterAnimator.SetBool("isAttack", _isAttack);
            shooterAnimator.SetBool("isWalk", _isWalk);
        }
        
        Debug.Log("is attack " + _isAttack);           
        Debug.Log("is Walk " + _isWalk);
        
    }

    private void SpottedPlayer()
    {
        isSpot = true;
        if (isSpot)
        {
            _detectZone *= 2;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusShootPoint);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, tooClose);
    }
}