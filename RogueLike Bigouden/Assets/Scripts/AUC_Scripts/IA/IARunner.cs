using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IARunner : MonoBehaviour
{
    public EnnemyData ennemyData;
    
    #region Variables Assignation 

    //Utilities
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    private Rigidbody2D rb;
    private Vector2 pos;
    
    // Bools
    public bool isPlayerInAggroRange;
    public bool isRdyMove;
    public bool isPlayerInAttackRange;
    public bool isReadyToShoot;
    public bool isStun;
    public bool isCharging;
    public bool isRushing;
    public bool isAggro;

    public float dashSpeed = 3f;
    public bool isReadyToDash;
    public bool isDashing;
    private float stunDuration = 1.5f;
    
    [SerializeField]
    public string name // Nom de l'unité
    {
        get { return ennemyData.nameSO; }
        set { ennemyData.nameSO = name; }
    }

    [SerializeField]
    public string description // Description de l'unité
    {
        get { return ennemyData.descriptionSO; }
        set { ennemyData.descriptionSO = description; }
    }

    [SerializeField]
    public int lifePointSO // Point de vie de l'unité
    {
        get { return ennemyData.lifePointSO; }
        set { ennemyData.lifePointSO = lifePointSO; }
    }

    [SerializeField]
    public int shieldPointSO // Point de l'armure de l'unité
    {
        get { return ennemyData.shieldPointSO; }
        set { ennemyData.shieldPointSO = shieldPointSO; }
    }

    [SerializeField]
    public int damage // Nombre de dégats
    {
        get { return ennemyData.damageSO; }
        set { ennemyData.damageSO = damage; }
    }

    [SerializeField]
    public float detectZone // Fov
    {
        get { return ennemyData.detectZoneSO; }
        set { ennemyData.detectZoneSO = detectZone; }
    }

    [SerializeField]
    private float attackRange // Portée de l'attaque
    {
        get { return ennemyData.attackRangeSO; }
        set { ennemyData.attackRangeSO = attackRange; }
    }

    [SerializeField]
    public float delayAttack // Delay entre les attack des ennemis
    {
        get { return ennemyData.delayAttackSO; }
        set { ennemyData.delayAttackSO = delayAttack; }
    }

    [SerializeField]
    public float timeBeforeAggro // Delay avant que les ennemis aggro le joueur
    {
        get { return ennemyData.timeBeforeAggroSO; }
        set { ennemyData.timeBeforeAggroSO = timeBeforeAggro; }
    }
    
    [SerializeField]
    private float movementSpeed // Vitesse de déplacement de l'unité
    {
        get { return ennemyData.movementSpeedSO; }
        set { ennemyData.movementSpeedSO = movementSpeed; }
    }

    [SerializeField]
    public float chargeDuration // MS de la charge du runner
    {
        get { return ennemyData.chargeDurationSO; }
        set { ennemyData.chargeDurationSO = chargeDuration; }
    }

    [SerializeField]
    public float moveSpeedCharge // MS de la charge du runner
    {
        get { return ennemyData.moveSpeedChargeSO; }
        set { ennemyData.moveSpeedChargeSO = moveSpeedCharge; }
    }

    public int lifePoint;
    public int shieldPoint;
    
    private float rushDelay = 3f;
    private float rushingSpeed;
    private Vector2 fwd;
    private LayerMask isPlayer;
    
    #endregion
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        lifePoint = lifePointSO;
        shieldPoint = shieldPointSO;
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = movementSpeed;
        rushingSpeed = movementSpeed * 3;

        isAggro = false;
        isReadyToDash = true;
        isRushing = false;

        isPlayerInAggroRange = false;
        isPlayerInAttackRange = false;
        Debug.Log(detectZone);
        Debug.Log(attackRange);
        Debug.Log(target);
        Debug.Log(movementSpeed);
        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    private void Update()
    {
        isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < detectZone;
        isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;

        if (!isPlayerInAggroRange && isAggro)
            Patrolling();
        if (!isPlayerInAttackRange && isPlayerInAggroRange && isAggro)
            ChasePlayer();
        if (isPlayerInAttackRange && isPlayerInAggroRange && isAggro)
            Attacking();
    }

    #region PatrollingState

    private void Patrolling()
    {
        if (isRdyMove)
            StartCoroutine(ResetPath());
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
        isCharging = true;
        agent.SetDestination(target.position);
        var accelaration = (moveSpeedCharge - movementSpeed) / 3;
        agent.speed += accelaration * Time.deltaTime;
    }
    
    #endregion

    #region AttackState
    private void Attacking()
    {
        isCharging = false;
        agent.SetDestination(transform.position);
        if (isReadyToDash)
        {
            StartCoroutine(nameof(Dash));
        }
    }
    
    IEnumerator Dash()
    {
        isReadyToDash = false;
        rb.velocity = Vector2.zero;
        
        fwd = transform.TransformDirection(target.position.x - rb.transform.position.x, target.position.y - rb.transform.position.y, 0);
        Physics2D.Raycast(transform.position, fwd, attackRange, isPlayer);
        Debug.DrawRay(transform.position, fwd, Color.green);
        
        yield return new WaitForSeconds(0f);
        rb.velocity = fwd.normalized * dashSpeed;
        isDashing = true;
        agent.speed = movementSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        yield return new WaitForSeconds(.5f);
        rb.velocity = Vector2.zero;
        isDashing = false;
        Vector2 dir = Vector2.zero;
        
        yield return new WaitForSeconds(1f);
        isReadyToDash = true;
        agent.speed = movementSpeed;
        StopCoroutine(nameof(Dash));
    }
    
    private IEnumerator TakeObstacle()
    {
        StopCoroutine(nameof(Dash));
        isReadyToDash = false;
        agent.speed = 0;

        Debug.Log("STUN");
        isStun = true;
        
        yield return new WaitForSeconds(stunDuration);
        
        agent.speed = movementSpeed;
        isReadyToDash = true;
        isStun = false;
        Debug.Log("NO MORE STUN");
    }
    
    #endregion
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    
    private void WaitToGo()
    {
        isAggro = true;
        isRdyMove = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(nameof(TakeObstacle));
        }
    }

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
}