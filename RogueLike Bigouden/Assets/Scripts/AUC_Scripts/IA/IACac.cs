using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking.Match;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class IACac : MonoBehaviour
{
    #region Cac Variables Assignation
    
    // Utilities
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool isRdyMove;
    [SerializeField] private Animator cacAnimator;
    private Vector2 pos;
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
    [SerializeField] private bool isSpot;
    public float hitRadius;
    
    // Anims States
    private bool _isWalk;
    private bool _isAttack;

    private Vector3 shootPointPos;
    
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

        _isAttack = false;
        _isWalk = false;
        
        _isAggro = false;
        isRdyMove = false;
        _isPlayerInAggroRange = false;
        _isPlayerInAttackRange = false;
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
        
        Animations(agent);

        if (agent.velocity.x <= 0.1f && agent.velocity.y <= 0.1f)
        {
            _isWalk = false;
        }
        else { _isWalk = true; }
    }

    private void FixedUpdate()
    {
        //agent.speed = _movementSpeed * TimeManager._timeManager.CustomDeltaTimeEnnemy;
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
        _isWalk = true; // Anim
        _isAttack = false; // Anim
        
        if(!isSpot)
            SpottedPlayer();
    }
    
    #endregion

    #region AttackState

    void Attacking()
    {
        _isWalk = false;
        if (_isReadyToShoot)
        {
            Collider2D[] detectPlayer = Physics2D.OverlapCircleAll(transform.position, _attackRange, isPlayer);
            if (detectPlayer != null)
            {
                GoHit();
                _isAttack = true;
                _isReadyToShoot = false;
            }
        }
        agent.SetDestination(transform.position);
        Debug.DrawRay(transform.position, new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y + upTofitPlayer), Color.green);
    }
    private const float radiusShootPoint = 0.75f;
    private const float upTofitPlayer = 0.5f;
    private void GoHit()
    {
        _isWalk = false; // Anim
        StartCoroutine(Hit());
    }
    private bool paire = true;
    IEnumerator Hit()
    {
        shootPointPos = (target.position - transform.position);
        shootPointPos.Normalize();
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position + shootPointPos * radiusShootPoint, hitRadius, isPlayer);
        foreach (var _player in hitPlayers)
        {
            if (paire)
            {
                paire = false;
                _player.GetComponent<PlayerStatsManager>().TakeDamage(_damageDealt);
                Debug.Log("Player Hit : " + _player.name + " & receive : " + _damageDealt + " damage !");
            }
            else
            {
                paire = true;
            }
        }
        
        _isAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _isReadyToShoot = true;
    }
    #endregion

    #region Anims
    private void Animations(NavMeshAgent agent)
    {
        if (_isAttack)
        {
            cacAnimator.SetFloat("Horizontal", shootPointPos.x);
            cacAnimator.SetFloat("Vertical", shootPointPos.y + upTofitPlayer);
            cacAnimator.SetBool("isAttack", _isAttack);
            cacAnimator.SetBool("isWalk", _isWalk);
        }
        else
        {
            cacAnimator.SetFloat("Vertical", agent.velocity.y);
            cacAnimator.SetFloat("Horizontal", agent.velocity.x);
            cacAnimator.SetBool("isAttack", _isAttack);
            cacAnimator.SetBool("isWalk", _isWalk);
        }
    }
    #endregion
    
    private void SpottedPlayer()
    {
        isSpot = true;
        if (isSpot)
        {
            _detectZone *= 2;
        }
    }
    
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
        Gizmos.DrawWireSphere(transform.position + shootPointPos * radiusShootPoint, hitRadius);
    }
}
