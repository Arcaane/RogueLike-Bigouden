using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class IAShooter : MonoBehaviour
{
    
    #region Variables
    //Utilities
    public EnnemyData ennemyData;
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
    [SerializeField] Vector2 bulletSpread // Random sur la trajectoire des balles (Faible)
    {
        get { return ennemyData.bulletSpreadSO; }
        set { ennemyData.bulletSpreadSO = bulletSpread; }
    }
    [SerializeField] Vector2 bulletSpeed // Random sur la vitesse des balles (Faible) 
    {
        get { return ennemyData.bulletSpeedSO; }
        set { ennemyData.bulletSpeedSO = bulletSpeed; }
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
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        isAggro = false;
        isReadyToShoot = true;
        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    private void Update()
    {
        isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;

        if (!isPlayerInAttackRange && isAggro)
            ChasePlayer();
        if (isAggro && isAggro)
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
        float bulletVelocity = Random.Range(bulletSpeed.x, bulletSpeed.y);
        isReadyToShoot = false;
        
        // Play an attack animation
        
        var ball = ObjectPooler.Instance.SpawnFromPool("Projectile", shootPoint.position, Quaternion.identity);
        var rbball = ball.GetComponent<Rigidbody2D>();
        rbball.AddForce(shootPoint.right * bulletVelocity, ForceMode2D.Impulse);
        rbball.rotation = rb.rotation;
        
        Invoke(nameof(ResetShoot), delayAttack);
    }

    private void ResetShoot()
    {
        isReadyToShoot = true;
    }

    public void LookPlayer(Transform targetTransform, Transform launcherTransform)
    {
        Vector2 lookdir = targetTransform.position - launcherTransform.position;
        var angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void WaitToGo()
    {
        isAggro = true;
    }
}