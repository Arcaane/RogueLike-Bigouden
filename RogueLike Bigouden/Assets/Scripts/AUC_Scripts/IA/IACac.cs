using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IACac : MonoBehaviour
{
    #region Cac Variables Assignation
    
    // Utilities
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform hitPoint;
    [SerializeField] private bool isRdyMove;
    private Vector2 pos;
    private Vector2 fwd;
    public LayerMask isPlayer;
    
    [SerializeField] private int _damageDealt;
    [SerializeField] private float _detectZone;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _timeBeforeAggro;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private bool _isPlayerInAggroRange;
    [SerializeField] private bool _isPlayerInAttackRange;
    [SerializeField] private bool _isReadyToShoot;
    [SerializeField] private bool _isAggro;
    [SerializeField] private bool _isAttacking;
    public float hitRadius;
    
    #endregion

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        _damageDealt = GetComponent<EnnemyStatsManager>().damageDealt;
        _detectZone = GetComponent<EnnemyStatsManager>().detectZone;
        _attackRange = GetComponent<EnnemyStatsManager>().attackRange;
        _attackDelay = GetComponent<EnnemyStatsManager>().attackDelay;
        _timeBeforeAggro = GetComponent<EnnemyStatsManager>().timeBeforeAggro;
        _movementSpeed = GetComponent<EnnemyStatsManager>().movementSpeed;
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = _movementSpeed;

        _isAggro = false;
        isRdyMove = false;
        _isPlayerInAggroRange = false;
        _isPlayerInAttackRange = false;
        _isAttacking = false;
        _isReadyToShoot = true;
        Invoke(nameof(WaitToGo), _timeBeforeAggro);
    }

    // Update is called once per frame
    void Update()
    {
        _isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < _detectZone;
        _isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < _attackRange;

        if (!_isPlayerInAggroRange && _isAggro)
            Patrolling();
        if (!_isPlayerInAttackRange && _isPlayerInAggroRange && _isAggro)
            ChasePlayer();
        if(_isPlayerInAggroRange && _isPlayerInAttackRange && _isAggro)
            Attacking();
    }

    #region PatrollingState
    
    void Patrolling()
    {
        if (isRdyMove)
            StartCoroutine(ResetPath());
    }
    
    void GetNewPath()
    { 
        Vector2 pos = GetNewRandomPosition(); 
        agent.SetDestination(pos);
    }

    IEnumerator ResetPath()
    {
        isRdyMove = false;
        GetNewPath();
        yield return new WaitForSeconds(3f);
        isRdyMove = true;
    }
        
    Vector3 GetNewRandomPosition()
    { 
        float x = Random.Range(-3, 4); 
        float y = Random.Range(-3, 4); 
        pos = new Vector2(x, y);
        Debug.Log(pos); 
        return pos;
    }
    #endregion

    #region ChaseState
    void ChasePlayer()
    {
        agent.SetDestination(target.position);
    }
    
    #endregion

    #region AttackState

    void Attacking()
    {
        agent.SetDestination(transform.position);
        if (_isReadyToShoot)
        {
            Hit();
        }
    }

    private void Hit()
    {
        _isReadyToShoot = false;
        
        // Play attack animation
        
        // Detect player 
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(hitPoint.position, hitRadius, isPlayer);
        
        // Damage them
        foreach (var player in hitPlayers)
        {
            player.GetComponent<PlayerStatsManager>().TakeDamage(_damageDealt);
            Debug.Log("Player: " + player.name + " just hit!");
        }
        
        StartCoroutine(ResetHit());
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(_attackDelay);
        _isReadyToShoot = true;
    }
    #endregion

    private void WaitToGo()
    {
        _isAggro = true;
        isRdyMove = true;
        _isReadyToShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
    }
}
