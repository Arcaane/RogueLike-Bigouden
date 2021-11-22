using System;
using System.Collections;
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
    
    private Vector2 shootPosRef;
    private Rigidbody2D rb;
    private bool isRdyMove;

    private Vector3 lastPos;
    private Vector3 shootDir;
    public Vector3 shootPointPos;
    // Attack Variables 

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
    
    
    public LayerMask isPlayer;
    private Vector3 pos;
    private Vector2 fwd;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();    
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        _detectZone = GetComponent<EnnemyStatsManager>().detectZone;
        _attackRange = GetComponent<EnnemyStatsManager>().attackRange;
        _attackDelay = GetComponent<EnnemyStatsManager>().attackDelay;
        _timeBeforeAggro = GetComponent<EnnemyStatsManager>().timeBeforeAggro;
        _movementSpeed = GetComponent<EnnemyStatsManager>().movementSpeed;
        
        // Parametres de l'agent
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        agent.speed = _movementSpeed;
        
        // Set Bools
        _isAggro = false;
        _isReadyToShoot = true;
        isRdyMove = true;
        
        _isPlayerInAggroRange = false;
        _isPlayerInAttackRange = false;

        Invoke(nameof(WaitToGo), _timeBeforeAggro);
    }

    private void Update()
    {
        _isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < _attackRange;
        _isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < _detectZone;
        
        if (!_isPlayerInAggroRange && !_isPlayerInAttackRange) 
            Patrolling();
        if (_isPlayerInAggroRange && !_isPlayerInAttackRange) 
            ChasePlayer();
        if (_isPlayerInAttackRange && _isPlayerInAggroRange)
            Attacking();
    }

    #region PatrollingState
    private void Patrolling()
    {
        fct();
       // WalkAnimation(agent);
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
        Debug.Log(pos);
        return pos;
    }
    private void GetNewPath()
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
    #endregion

    #region ChaseState
    private void ChasePlayer()
    {
        fct();
        agent.SetDestination(target.position);
        //WalkAnimation(agent);
        _isAttacking = false;
    }
    #endregion

    #region AttackState
    private void Attacking()
    {
        if (_isReadyToShoot) VerifyShoot();
        
        agent.SetDestination(transform.position);
        Debug.DrawRay(transform.position, new Vector3(target.position.x - rb.transform.position.x, target.position.y - rb.transform.position.y -0.17f), Color.green);
    }

    private void VerifyShoot()
    {
        // Raycast pour vérifier si le joueur est en cible 
        fwd = transform.TransformDirection(target.position.x - rb.transform.position.x, target.position.y - rb.transform.position.y -0.17f, 0);

        if (Physics2D.Raycast(transform.position, fwd, _attackRange, isPlayer))
        {
            Debug.Log("Player dans le viseur !");
            Shoot();
        }
    }

    private void Shoot()
    {
        _isReadyToShoot = false;
        StartCoroutine(BulletShoot());
    }

    private const float radiusShootPoint = 0.75f;
    IEnumerator BulletShoot()
    {
        _isAttacking = true;
        shootPointPos = (target.position - transform.position);
        shootPointPos.Normalize();
        
        //AttackAnimation(agent);
        
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position + shootPointPos * radiusShootPoint, Quaternion.identity);
            obj.GetComponent<BulletBehaviour>().GoDirection(shootDir);
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(0.7f);
        _isReadyToShoot = true;
        
    }
    #endregion

    private void fct()
    {
        shootDir = agent.velocity;
        if (shootDir == Vector3.zero) {
            shootDir = lastPos;
        }
        shootDir.Normalize();
    }
    
    private void WaitToGo()
    {
        _isAggro = true;
    }

   /* #region Animations
    public void WalkAnimation(NavMeshAgent agent)
    {
        if (agent.velocity != Vector3.zero)
        {
            shooterAnimator.SetFloat("Vertical", agent.velocity.y);
            shooterAnimator.SetFloat("Horizontal", agent.velocity.x);
            shooterAnimator.SetFloat("Magnitude", agent.velocity.magnitude); 
        }
    }

    public void AttackAnimation(NavMeshAgent agent)
    {
        shooterAnimator.SetFloat("Vertical", fwd.y);
        shooterAnimator.SetFloat("Horizontal", fwd.x);
        shooterAnimator.SetBool("isAttack", _isAttacking);
    }

    #endregion  */
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusShootPoint);
    }
}