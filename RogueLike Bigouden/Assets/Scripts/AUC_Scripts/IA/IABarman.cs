using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABarman : MonoBehaviour
{
    #region MyRegion
    // Utilities
    public EnnemyData ennemyData;
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    public Transform shootPoint;
    
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
    [SerializeField] int damageAoe // Range de l'aoe du coktail du serveur
    {
        get { return ennemyData.damageAoeSO; }
        set { ennemyData.damageAoeSO = damageAoe; }
    }
    [SerializeField] int damageAoeBeforeExplosion // Nombre de balles que tire le shooter
    {
        get { return ennemyData.damageAoeBeforeExplosionSO; }
        set { ennemyData.damageAoeBeforeExplosionSO = damageAoeBeforeExplosion; }
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

    [SerializeField] Vector2 bulletSpeed // L'unité est stun ?
    {
        get { return ennemyData.bulletSpeedSO; }
        set { ennemyData.bulletSpeedSO = bulletSpeed; }
    }
    [SerializeField] float rangeAoe // L'unité est stun ?
    {
        get { return ennemyData.rangeAoeSO; }
        set { ennemyData.rangeAoeSO = rangeAoe; }
    }
    
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        isAggro = false;
        isReadyToShoot = true;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    // Update is called once per frame
    private void Update()
    {
        isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;

        if (isPlayerInAttackRange && isAggro)
            Attacking();
    }

    private void FixedUpdate()
    {
        var lookdir = target.position - rb.transform.position;
        var angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void OnDrawGizmos()
    {
        //Draw the parabola by sample a few times
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Attacking()
    {
        if (isReadyToShoot)
            Shoot();
    }

    private void Shoot()
    {
        var bulletVelocity = Random.Range(bulletSpeed.x, bulletSpeed.y);
        isReadyToShoot = false;
        // Play an attack animation
        
        var projectile =
            ObjectPooler.Instance.SpawnFromPool("ProjectileBarman", shootPoint.position, Quaternion.identity);
        var rbProjectile = projectile.GetComponent<Rigidbody2D>();
        
        rbProjectile.rotation = rb.rotation;
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