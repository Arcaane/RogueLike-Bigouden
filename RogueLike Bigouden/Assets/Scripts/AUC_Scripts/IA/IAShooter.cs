using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class IAShooter : MonoBehaviour
{
    
    #region Variables
    //Utilities
    public EnnemyData ennemyData;
    [SerializeField] private Transform target;
    public Transform shootPoint;
    public NavMeshAgent agent;
    private Rigidbody2D rb;
    public Animator shooterAnimator;
    private Vector2 shootPosRef;

    // Bools
    [SerializeField] private bool isPlayerInAggroRange;
    [SerializeField] private bool isPlayerInAttackRange;
    [SerializeField] private bool isReadyToShoot;
    [SerializeField] private bool isAggro;
    public LayerMask isPlayer;

    [SerializeField] string name // Nom de l'unité
    {
        get { return ennemyData.nameSO; }
        set { ennemyData.nameSO = name; }
    }
    [SerializeField] string description // Description de l'unité
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
    [SerializeField] float detectZone // Fov
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
    
    [SerializeField] bool isStun // L'unité est stun ?
    {
        get { return ennemyData.isStunSO; }
        set { ennemyData.isStunSO = isStun; }
    }
    #endregion

    private bool isLock;
    private bool isRdyMove;
    private Vector3 pos;
    public float magnitude;
    public bool isAttack;
    private Vector2 fwd;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();    
    }

    private void Start()
    {
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        agent.speed = movementSpeed;
        
        isLock = false;
        isAggro = false;
        isReadyToShoot = true;
        isRdyMove = true;
        
        isPlayerInAggroRange = false;
        isPlayerInAttackRange = false;
        Debug.Log(detectZone);
        Debug.Log(attackRange);

        Invoke(nameof(WaitToGo), timeBeforeAggro);
    }

    private void Update()
    {
        magnitude = agent.velocity.magnitude;
        var lookdir = target.position - rb.transform.position;
        var angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        
        isPlayerInAttackRange = Vector2.Distance(transform.position, target.position) < attackRange;
        isPlayerInAggroRange = Vector2.Distance(transform.position, target.position) < detectZone;
        
        if (!isPlayerInAggroRange && !isPlayerInAttackRange) 
            Patrolling();
        if (isPlayerInAggroRange && !isPlayerInAttackRange) 
            ChasePlayer();
        if (isPlayerInAttackRange && isPlayerInAggroRange) 
            Attacking();
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectZone);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(shootPoint.position, 0.2f);
    }
    

    private void Patrolling()
    {
        WalkAnimation(agent);
        if (isRdyMove)
        {
            StartCoroutine(ResetPath());
        }
    }
    
    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
        WalkAnimation(agent);
        isLock = true;
        isAttack = false;
    }

    private void Attacking()
    {
        if (!isReadyToShoot)
        {
            isAttack = true;
        }
        agent.SetDestination(transform.position);
        Debug.DrawRay(transform.position, new Vector3(target.position.x - rb.transform.position.x, target.position.y - rb.transform.position.y), Color.green);
        if (isReadyToShoot) VerifyShoot();
    }

    private void VerifyShoot()
    {
        // Raycast pour vérifier si le joueur est en cible 
        fwd = transform.TransformDirection(target.position.x - rb.transform.position.x, target.position.y - rb.transform.position.y, 0);

        if (Physics2D.Raycast(transform.position, fwd, attackRange, isPlayer))
        {
            Debug.Log("Player dans le viseur !");
            Shoot();
        }
    }

    private void Shoot()
    {
        isReadyToShoot = false;
        StartCoroutine(BulletShoot());
    }
    
    IEnumerator BulletShoot()
    {
        Vector2 fwd = transform.TransformDirection(target.position.x, target.position.y, 0);
        float bulletVelocity = Random.Range(bulletSpeed.x, bulletSpeed.y);
        
        AttackAnimation(agent);
        
        var lookdir = target.position - rb.transform.position;
        var angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg;
        
        var ball = ObjectPooler.Instance.SpawnFromPool("Bullet", new Vector3(agent.transform.position.x, agent.transform.position.y), Quaternion.identity);
        ball.transform.position = Vector2.MoveTowards(ball.transform.position, fwd, bulletVelocity * Time.deltaTime); 
        yield return new WaitForSeconds(1f);
        isReadyToShoot = true;
    }
    
    Vector3 GetNewRandomPosition()
    {
        float x = Random.Range(-3, 4);
        float y = Random.Range(-3, 4);
        pos = new Vector2(x, y);
        Debug.Log(pos);
        return pos;
    }
    
    private void WaitToGo()
    {
        Debug.Log(isAggro);
        isAggro = true;
        Debug.Log(isAggro);
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
        shooterAnimator.SetBool("isAttack", isAttack);
    }
}