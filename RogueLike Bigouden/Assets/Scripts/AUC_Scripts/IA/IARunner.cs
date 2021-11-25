using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IARunner : MonoBehaviour
{
    public EnnemyData ennemyData;
    
    #region Runner Variables Assignation

    //Utilities
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    private Rigidbody2D rb;
    private Vector2 fwd;
    public LayerMask isPlayer;
    private Vector2 pos;

    //Anims
    [SerializeField] private Animator runnerAnimator;
    private bool _isAttack;
    private bool _isWalk;
    
    //Int
    [SerializeField] private int _damageDealt;
    
    //Float
    [SerializeField] private float _detectZone; // Fov
    [SerializeField] private float _attackRange; // Portée de l'attaque
    [SerializeField] private float _timeBeforeAggro; // Delay avant que les ennemis aggro le joueur
    [SerializeField] private float _movementSpeed;

    // Bools
    [SerializeField] private bool _isPlayerInAggroRange;
    [SerializeField] private bool _isRdyMove;
    [SerializeField] private bool _isPlayerInAttackRange;
    [SerializeField] private bool _isAggro;
    [SerializeField] private bool _isDashing;
    
    [SerializeField] private bool _isCharging;
    [SerializeField] private bool _isRushing;
    
    [SerializeField] private float _dashSpeed;
    [SerializeField] private Vector2 shootPointPos;
    [SerializeField] private bool _isReadyToDash;
    [SerializeField] private float _stunDuration;
    [SerializeField] private float _rushDelay;
    [SerializeField] private float _moveSpeedCharge;
    [SerializeField] private bool isSpot;
    [SerializeField] private bool _isStun;
    
    #endregion
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        _damageDealt = GetComponent<EnnemyStatsManager>().damageDealt;
        _detectZone = GetComponent<EnnemyStatsManager>().detectZone;
        _attackRange = GetComponent<EnnemyStatsManager>().attackRange;
        _timeBeforeAggro = GetComponent<EnnemyStatsManager>().timeBeforeAggro;
        _movementSpeed = GetComponent<EnnemyStatsManager>().movementSpeed;
        _dashSpeed = GetComponent<EnnemyStatsManager>().dashSpeed;
        _stunDuration = GetComponent<EnnemyStatsManager>().stunDuration;
        _rushDelay = GetComponent<EnnemyStatsManager>().rushDelay;
        _moveSpeedCharge = _movementSpeed * 3;
        isSpot = false;
        
        // Set bools 
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = _movementSpeed;
        _isAttack = false;
        _isWalk = false;
        _isAggro = false;
        _isReadyToDash = true;
        _isRushing = false;
        _isCharging = false;
        _isPlayerInAggroRange = false;
        _isPlayerInAttackRange = false;
        Invoke(nameof(WaitToGo), _timeBeforeAggro);
    }

    private void Update()
    {
        _isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < _detectZone;
        _isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < _attackRange;

        if (!_isPlayerInAggroRange && _isAggro)
            Patrolling();
        if (!_isPlayerInAttackRange && _isPlayerInAggroRange && _isAggro)
            ChasePlayer();
        if (_isPlayerInAttackRange && _isPlayerInAggroRange && _isAggro)
            Attacking();
        
        //Animations(agent);
        if (agent.velocity.x <= 0.1f && agent.velocity.y <= 0.1f)
        {
            _isWalk = false;
        }
        else { _isWalk = true; }
    }

    #region PatrollingState

    private void Patrolling()
    {
        if (_isRdyMove)
            StartCoroutine(ResetPath());
    }
    
    private void GetNewPath()
    {
        Vector2 pos = GetNewRandomPosition();
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
    private void ChasePlayer()
    {
        _isCharging = true;
        agent.SetDestination(target.position);
        _isWalk = true; // Anim
        _isAttack = false; // Anim
        var accelaration = (_moveSpeedCharge - _movementSpeed) / 3;
        agent.speed += accelaration * Time.deltaTime;
        
        if(!isSpot)
            SpottedPlayer();
    }
    #endregion

    #region AttackState
    private void Attacking()
    {
        _isWalk = false;
        _isCharging = false;
        agent.SetDestination(transform.position);
        if (_isReadyToDash)
        {
            StartCoroutine(nameof(Dash));
            _isAttack = true;
        }
    }
    
    private const float upTofitPlayer = 0.1f;
    IEnumerator Dash()
    {
        // Prepare Variables
        _isReadyToDash = false;
        rb.velocity = Vector2.zero;
        shootPointPos = (target.position - transform.position);
        shootPointPos.Normalize();
        
        // Dash
        yield return new WaitForSeconds(0.2f);
        _isDashing = true;
        rb.velocity =  new Vector2(shootPointPos.x, shootPointPos.y + upTofitPlayer) * _dashSpeed;
       
        // StopDash
        yield return new WaitForSeconds(.6f);
        rb.velocity = Vector2.zero;
        _isDashing = false;
        agent.speed = _movementSpeed;
        _isAttack = false;
        
        //Wait next dash
        yield return new WaitForSeconds(_rushDelay);
        _isReadyToDash = true;
    }
    
    private IEnumerator TakeObstacle()
    {
        StopCoroutine(nameof(Dash));
        _isReadyToDash = false;
        agent.speed = 0;

        Debug.Log("STUN");
        _isStun = true;
        
        yield return new WaitForSeconds(_stunDuration);
        
        agent.speed = _movementSpeed; 
        _isReadyToDash = true;
        _isStun = false;
        Debug.Log("NO MORE STUN");
    }
    
    #endregion
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
    
    private void WaitToGo()
    {
        _isAggro = true;
        _isRdyMove = true;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle") && _isDashing)
        {
            StartCoroutine(nameof(TakeObstacle));
        }
        
        if (other.gameObject.CompareTag("Player") && _isDashing)
        {
            other.gameObject.GetComponent<PlayerStatsManager>().TakeDamage(_damageDealt);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    
    private void SpottedPlayer()
    {
        isSpot = true;
        if (isSpot)
        {
            _detectZone *= 2;
        }
    }
    
    private void Animations(NavMeshAgent agent)
    {
        if (_isAttack)
        {
            runnerAnimator.SetFloat("Horizontal", shootPointPos.x);
            runnerAnimator.SetFloat("Vertical", shootPointPos.y + upTofitPlayer);
            runnerAnimator.SetBool("isAttack", _isAttack);
            runnerAnimator.SetBool("isWalk", _isWalk);
        }
        else
        {
            runnerAnimator.SetFloat("Horizontal", agent.velocity.x);
            runnerAnimator.SetFloat("Vertical", agent.velocity.y);
            runnerAnimator.SetBool("isAttack", _isAttack);
            runnerAnimator.SetBool("isWalk", _isWalk);
        }
        
        Debug.Log("is attack " + _isAttack);           
        Debug.Log("is Walk " + _isWalk);
    }
    
}