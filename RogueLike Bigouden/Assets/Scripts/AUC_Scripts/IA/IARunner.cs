using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IARunner : MonoBehaviour
{
    public EnnemyData ennemyData;

    #region Variables

    //Utilities
    [SerializeField] private Transform target;
    public Transform shootPoint;
    private NavMeshAgent agent;
    private Rigidbody2D rb;

    private bool isPlayerInAggroRange;
    private bool isRdyMove;
    private Vector2 pos;

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
    public int lifePoint // Point de vie de l'unité
    {
        get { return ennemyData.lifePointSO; }
        set { ennemyData.lifePointSO = lifePoint; }
    }

    [SerializeField]
    public int shieldPoint // Point de l'armure de l'unité
    {
        get { return ennemyData.shieldPointSO; }
        set { ennemyData.shieldPointSO = shieldPoint; }
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
    public float attackRange // Portée de l'attaque
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
    public float attackSpeed // Vitesse d'attaque de l'unité
    {
        get { return ennemyData.attackSpeedSO; }
        set { ennemyData.attackSpeedSO = attackSpeed; }
    }

    [SerializeField]
    public float movementSpeed // Vitesse de déplacement de l'unité
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

    [SerializeField]
    public bool isPlayerInAttackRange // Le player est-il en range ?
    {
        get { return ennemyData.isPlayerInAttackRangeSO; }
        set { ennemyData.isPlayerInAttackRangeSO = isPlayerInAttackRange; }
    }

    [SerializeField]
    public bool isReadyToShoot // Peut tirer ?
    {
        get { return ennemyData.isReadyToShootSO; }
        set { ennemyData.isReadyToShootSO = isReadyToShoot; }
    }

    [SerializeField]
    public bool isAggro // L'unité chase le joueur ?
    {
        get { return ennemyData.isAggroSO; }
        set { ennemyData.isAggroSO = isAggro; }
    }

    [SerializeField]
    public bool isCharging // Le runner charge t'il ?
    {
        get { return ennemyData.isChargingSO; }
        set { ennemyData.isChargingSO = isCharging; }
    }

    [SerializeField]
    public bool isAttacking // L'unité attaque ?
    {
        get { return ennemyData.isAttackingSO; }
        set { ennemyData.isAttackingSO = isAttacking; }
    }

    [SerializeField]
    public bool isStun // L'unité est stun ?
    {
        get { return ennemyData.isStunSO; }
        set { ennemyData.isStunSO = isStun; }
    }

    #endregion

    public bool isRushing;
    private bool isReadyToDash;
    private float rushDelay = 3f;
    private float rushingSpeed;
    private float stunDuration = 3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = movementSpeed;
        rushingSpeed = movementSpeed * 3;
        
        
        isAggro = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isReadyToDash = true;
        isRushing = false;
        
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
        agent.SetDestination(target.position);
        var accelaration = (moveSpeedCharge - movementSpeed) / 3;
        agent.speed += accelaration * Time.deltaTime;
    }
    
    #endregion

    #region AttackState
    private void Attacking()
    {
        agent.SetDestination(transform.position);
        if (isReadyToDash) 
            StartCoroutine(nameof(Dash));
    }
    
    IEnumerator Dash()
    {
        isRushing = true;
        agent.speed = rushingSpeed;
        yield return new WaitForSeconds(rushDelay);
        agent.speed = movementSpeed;
        isRushing = false;
    }

    public IEnumerator TakeObstacle()
    {
        StopCoroutine(nameof(Dash));
        isRushing = false;
        agent.speed = 0;
        isStun = true;
        yield return new WaitForSeconds(stunDuration);
        agent.speed = movementSpeed;
        isStun = false;
    }
    
    #endregion
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(shootPoint.position, attackRange / 2);
    }
    
    private void WaitToGo()
    {
        isAggro = true;
    }
}