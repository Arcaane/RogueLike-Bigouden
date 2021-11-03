using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IACac : MonoBehaviour
{
    #region Variables
    
    // Utilities
    [SerializeField] 
    private Transform target;
    
    [SerializeField] 
    private NavMeshAgent agent;
    
    [SerializeField]
    private Transform hitPoint;
    
    private Rigidbody2D rb;
    private Vector2 pos;
    
    // Bools 
    public bool isPlayerInAggroRange;
    public bool isPlayerInAttackRange;
    public bool isRdyMove;
    public bool isReadyToHit;
    public bool isAggro;
    
    // Int 
    public int lifePoint;
    public int shieldPoint;
    public int damageDealt;

    // Float
    public float detectZone;
    public float attackRange;
    public float timeBeforeAggro;
    public float delayAttack;
    public float movementSpeed;
    public float hitRadius;

    private Vector2 fwd;
    public LayerMask isPlayer;
    private List<Vector2> pathList;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = movementSpeed;

        isAggro = false;
        isRdyMove = false;
        isPlayerInAggroRange = false;
        isPlayerInAttackRange = false;
        
        isReadyToHit = true;
        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < detectZone;
        isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;

        if (!isPlayerInAggroRange && isAggro)
            Patrolling();
        if (!isPlayerInAttackRange && isPlayerInAggroRange && isAggro)
            ChasePlayer();
        if(isPlayerInAggroRange && isPlayerInAttackRange && isAggro)
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
        if (isReadyToHit)
        {
            Hit();
        }
    }

    private void Hit()
    {
        isReadyToHit = false;
        
        // Play attack animation
        
        // Detect player 
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(hitPoint.position, hitRadius, isPlayer);
        
        // Damage them
        foreach (var player in hitPlayers)
        {
            Debug.Log("Player hit" + player.name);
        }
        
        StartCoroutine(ResetHit());
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(delayAttack);
        isReadyToHit = true;
    }
    #endregion

    private void WaitToGo()
    {
        isAggro = true;
        isRdyMove = true;
        isReadyToHit = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
    }
    
    #region Damage Gestion
    public void TakeDamage(int damage)
    {
        if (shieldPoint > 0)
        {
            shieldPoint -= damage;
            if (shieldPoint < 0)
                shieldPoint = 0;
        }
        else
            lifePoint -= damage;
        
        if (lifePoint <= 0)
            Death();
    }

    private void Death()
    {
        // Play Death Animation
        Destroy(gameObject);
    }
    #endregion
}
