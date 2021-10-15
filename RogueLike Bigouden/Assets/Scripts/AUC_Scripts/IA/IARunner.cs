using UnityEngine;
using UnityEngine.AI;

public class IARunner : MonoBehaviour
{
    public EnnemyData ennemyData;
    #region Variables
    //Utilities
    [SerializeField] private Transform target;
    public Transform shootPoint;
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    
    [SerializeField] private string name // Nom de l'unité
    {
        get { return ennemyData.nameSO; }
        set { ennemyData.nameSO = name; }
    }
    [SerializeField] private string description // Description de l'unité
    {
        get { return ennemyData.descriptionSO; }
        set { ennemyData.descriptionSO = description; }
    }
    [SerializeField] int lifePoint // Point de vie de l'unité
    {
        get { return ennemyData.lifePointSO; }
        set { ennemyData.lifePointSO = lifePoint; }
    }
    [SerializeField] int shieldPoint // Point de l'armure de l'unité
    {
        get { return ennemyData.shieldPointSO; }
        set { ennemyData.shieldPointSO = shieldPoint; }
    }
    [SerializeField] int damage // Nombre de dégats
    {
        get { return ennemyData.damageSO; }
        set { ennemyData.damageSO = damage; }
    }
    [SerializeField] int detectZone // Fov
    {
        get { return ennemyData.detectZoneSO; }
        set { ennemyData.detectZoneSO = detectZone; }
    }
    [SerializeField] float attackRange // Portée de l'attaque
    {
        get { return ennemyData.attackRangeSO; }
        set { ennemyData.attackRangeSO = attackRange; }
    }
    [SerializeField] float delayAttack // Delay entre les attack des ennemis
    {
        get { return ennemyData.delayAttackSO; }
        set { ennemyData.delayAttackSO = delayAttack; }
    }
    [SerializeField] float timeBeforeAggro // Delay avant que les ennemis aggro le joueur
    {
        get { return ennemyData.timeBeforeAggroSO; }
        set { ennemyData.timeBeforeAggroSO = timeBeforeAggro; }
    }
    [SerializeField] float attackSpeed // Vitesse d'attaque de l'unité
    {
        get { return ennemyData.attackSpeedSO; }
        set { ennemyData.attackSpeedSO = attackSpeed; }
    }
    [SerializeField] float movementSpeed // Vitesse de déplacement de l'unité
    {
        get { return ennemyData.movementSpeedSO; }
        set { ennemyData.movementSpeedSO = movementSpeed; }
    }
    [SerializeField] float chargeDuration // MS de la charge du runner
    {
        get { return ennemyData.chargeDurationSO; }
        set { ennemyData.chargeDurationSO = chargeDuration; }
    }
    [SerializeField] float moveSpeedCharge // MS de la charge du runner
    {
        get { return ennemyData.moveSpeedChargeSO; }
        set { ennemyData.moveSpeedChargeSO = moveSpeedCharge; }
    }
    [SerializeField] bool isPlayerInAttackRange // Le player est-il en range ?
    {
        get { return ennemyData.isPlayerInAttackRangeSO; }
        set { ennemyData.isPlayerInAttackRangeSO = isPlayerInAttackRange; }
    }
    [SerializeField] bool isReadyToShoot // Peut tirer ?
    {
        get { return ennemyData.isReadyToShootSO; }
        set { ennemyData.isReadyToShootSO = isReadyToShoot; }
    }
    [SerializeField] bool isAggro // L'unité chase le joueur ?
    {
        get { return ennemyData.isAggroSO; }
        set { ennemyData.isAggroSO = isAggro; }
    }
    [SerializeField] bool isCharging  // Le runner charge t'il ?
    {
        get { return ennemyData.isChargingSO; }
        set { ennemyData.isChargingSO = isCharging; }
    }
    [SerializeField] bool isAttacking // L'unité attaque ?
    {
        get { return ennemyData.isAttackingSO; }
        set { ennemyData.isAttackingSO = isAttacking; }
    }
    [SerializeField] bool isStun // L'unité est stun ?
    {
        get { return ennemyData.isStunSO; }
        set { ennemyData.isStunSO = isStun; }
    }
    #endregion
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        isAggro = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isReadyToShoot = true;
        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    private void Update()
    {
        isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;

        if (!isPlayerInAttackRange && isAggro)
            ChasePlayer();
        if (isPlayerInAttackRange && isAggro)
            Attacking();
    }

    private void FixedUpdate()
    {
        var lookdir = target.position - rb.transform.position;
        var angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(shootPoint.position, attackRange / 2);
    }

    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        if (isReadyToShoot) Shoot();
    }

    private void Shoot()
    {
        isReadyToShoot = false;
        // Play an attack animation
        // Detect if player in range
        var hitEnemies = Physics2D.OverlapCircleAll(shootPoint.position, attackRange / 2);
        // Damage if true
        foreach (var hittenObj in hitEnemies) Debug.Log("We hit " + hittenObj.name);
        Invoke(nameof(ResetShoot), delayAttack);
    }

    private void ResetShoot()
    {
        isReadyToShoot = true;
    }

    private void WaitToGo()
    {
        isAggro = true;
    }
}