using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TimeManager;

public class PlayerAttribut : MonoBehaviour
{
    [SerializeField] private TimeManager timerManager;

    [Header("Value Update In Background")]
    //Timer Value for the Delay.
    [SerializeField]
    private float _timerDash;

    [SerializeField] private float _timerBetweenDash;
    [SerializeField] private float _timerAttack;
    [SerializeField] private float _timerDelayAttack;
    [SerializeField] private float _timerDodgeEffect;
    [SerializeField] private float _delayProjectile;

    [Header("Component Rigidbody")] [SerializeField]
    private Rigidbody2D rb;

    [Header("Utiliser le Clavier ?")] [SerializeField]
    private bool useVibration;


    [Header("Vitesse du joueur")]
    //Vitesse de déplacement du joueur.
    public float speed = 5;


    [Header("Etat du dash")]
    //Check Si le player est a déjà Dash ou si le joueur est en train de Dash.

    //Can be delete for the Final Build
    [SerializeField]
    private bool useRBDash;

    [SerializeField] public float dashSpeedRB = 5;

    //float--------------------
    public float dashSpeed = 5;

    public float durationDash = 1f;
    public float durationCooldownDash = 1f;

    //int--------------------
    public int dashCount = 3;

    //bool--------------------
    public bool isDash;
    public bool canDash;

    //private value for Dash------------------
    [SerializeField] private float dashCounter;

    [Header("Player Attack X/Y")] [SerializeField]
    private GameObject splinePivot;

    [SerializeField] private Transform offsetAttackXY;

    [SerializeField] public AttackSystemSpline attackSpline;
    [SerializeField] public ProjectilePath attackPath;

    //float-------------------
    [SerializeField] public float valueBeforeResetAttack;
    [SerializeField] public float attackType;
    [SerializeField] public float attackMovingSpeed;

    //bool--------------------
    public bool isAttacking;
    public bool launchFirstAttack;
    public bool launchSecondAttack;
    public bool launchAOEAttack;
    public float delayBeforeResetAttack = 1f;
    public float delayForSecondAttack = 4f;

    [Header("Player Attack Projectile")]
    //Object Projectile
    [SerializeField]
    private GameObject projectileObj;

    //Projectile Attack Position
    [SerializeField] private Transform projectileAttack;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float speedProjectile;

    //Offset of the end Position
    [SerializeField] private float offsetEndPosProjectile;

    //Projectile is launch
    [SerializeField] public bool launchProjectile;


    [Header("Dogdge Ability")]
    //float-----------------------------------
    [SerializeField]
    private float radiusDodge;

    [SerializeField] private float durationEffect;
    [SerializeField] private float speedModification;

    ////bool-----------------------------------
    [SerializeField] public bool useDodgeAbility;
    [SerializeField] private bool dodgeAbility;
    [SerializeField] private bool bulletIn;

    [Header("Animation et Sprite Renderer Joueur")] [SerializeField]
    public SpriteRenderer playerMesh;

    [SerializeField] private Animator animatorPlayer;

    [Header("Collision")] [SerializeField] private LayerMask m_maskCanape;
    [SerializeField] private bool m_isColliding;
    [SerializeField] private Vector3 lastVelocity;


    [Header("FeedBack (Vibrations, etc)")]
    //Script permettant d'ajouter des FeedBack dans le jeu.
    [SerializeField]
    private PlayerFeedBack playerFeedBack;


    // Private Valor use just for this script----------------------------------
    [SerializeField] private PlayerStatsManager _playerStatsManager;
    [SerializeField] private ProjectilePath _attackPath;

    //float
    private float _durationResetDash;
    private float _smallMovementFloat;


    //int
    private int _dashCount;
    private int _dashCountMax;

    //Vector3
    //Permet de relier ces vecteurs au Joystick dans le InputHandler.
    Vector2 movementInput, lookAxis;
    Vector3 _lastPosition;
    Vector3 _lastPositionForRotor;
    [SerializeField] Vector3 _directionNormalized;
    [SerializeField] Vector3 _look;
    [SerializeField] Vector3 _objPosition;

    [HideInInspector] public Vector3 _move;

    //Bools
    private bool _readyToAttackX;
    private bool _readyToAttackY;
    private bool _readyToAttackB;
    private bool _isDashing;
    private bool _readyToDash;
    private bool _onButter;

    //End of Private Valor ----------------------------------------------------

    [Header("Debug Input")] [SerializeField]
    private bool _launchDebug;

    [SerializeField] private Vector3 positionStick;
    [SerializeField] private List<GameObject> elementOfTextMeshPro;

    private const float dashIntValue = 1.666667f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerStatsManager = GetComponent<PlayerStatsManager>();
        //_isDashing = _playerStatsManager.isDashing;
        canDash = true;

        //Projectile-------------------
        projectileObj.SetActive(false);
        _delayProjectile = projectileObj.GetComponent<ProjectilePlayer>()._delayIncrementation;
    }

    private void Start()
    {
        if (!_launchDebug && elementOfTextMeshPro.Count > 0)
        {
            for (int i = 0; i < elementOfTextMeshPro.Count; i++)
            {
                elementOfTextMeshPro[i].SetActive(false);
            }
        }

        _objPosition = projectileObj.gameObject.transform.position;
        _objPosition.y = splinePivot.transform.position.y + offsetEndPosProjectile;
        projectileAttack.position = _objPosition;
    }

    public void Move()
    {
        transform.Translate(_move * speed * CustomDeltaTimeAttack);
    }


    #region AnimatorProcess

    public void Animation()
    {
        bool moving = lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0 && lookAxis != Vector2.zero;

        SetJoystickValue(moving);
        animatorPlayer.SetFloat("Magnitude", movementInput.magnitude);

        if (launchFirstAttack)
        {
            SetJoystickValue(moving);
            SetAttackValue(true);
        }
        else if (launchSecondAttack)
        {
            SetJoystickValue(moving);
            SetAttackValue(attack2: true);
        }
        else if (launchAOEAttack)
        {
            SetJoystickValue(moving);
            SetAttackValue(attack3: true);
        }
        else
        {
            SetAttackValue();
        }
    }

    void SetJoystickValue(bool moving)
    {
        if (moving)
        {
            animatorPlayer.SetFloat("Horizontal", lookAxis.x);
            animatorPlayer.SetFloat("Vertical", lookAxis.y);
        }
        else if (movementInput != Vector2.zero)
        {
            animatorPlayer.SetFloat("Horizontal", movementInput.x);
            animatorPlayer.SetFloat("Vertical", movementInput.y);
        }
    }

    void SetAttackValue(bool attack1 = false, bool attack2 = false, bool attack3 = false)
    {
        animatorPlayer.SetBool("AttackX1", attack1);
        animatorPlayer.SetBool("AttackX2", attack2);
        animatorPlayer.SetBool("AttackY", attack3);
    }

    #endregion AnimatorProcess

    void Attack(bool look)
    {
        switch (look && lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 ||
                lookAxis.y < 0 && lookAxis != Vector2.zero && !isAttacking)
        {
            case true:
                float rotationXObjLook = (Mathf.Atan2(lookAxis.y, lookAxis.x) * Mathf.Rad2Deg) - 90f;
                splinePivot.transform.rotation = Quaternion.AngleAxis(rotationXObjLook, Vector3.forward);
                break;
            case false:
                if (movementInput != Vector2.zero)
                {
                    float rotationXObjMove = (Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg - 90f);
                    splinePivot.transform.rotation = Quaternion.AngleAxis(rotationXObjMove, Vector3.forward);
                }

                break;
        }
    }

    public void SetInputVector(Vector2 direction, bool look)
    {
        switch (look)
        {
            case true:
                lookAxis = direction;
                Attack(true);
                break;
            case false:
                movementInput = direction;
                Attack(false);
                break;
        }
    }

    #region DashAttribut

    public void Dash()
    {
        canDash = true;
        dashCounter++;
        if (dashCounter >= 1)
        {
            isDash = true;
            if (dashCounter >= dashCount)
            {
                dashCounter = dashCount;
                canDash = false;
            }
        }

        if (canDash)
        {
            StartDash();
        }
    }

    void StartDash()
    {
        if (useRBDash)
        {
            rb.velocity = Vector2.zero;
            LaunchDash();
            StartCoroutine(DashWaitCorou());
        }
        else if (!useRBDash)
        {
            DashTP(dashSpeed);
        }
    }


    void DashWait()
    {
        if (useVibration)
        {
            playerFeedBack.MovingRumble(playerFeedBack.vibrationForce);
        }

        _timerBetweenDash += CustomDeltaTimeAttack;
        if (durationDash >= _timerBetweenDash)
        {
            _timerBetweenDash = 0;
            if (useVibration)
            {
                playerFeedBack.MovingRumble(playerFeedBack.vibrationForce);
            }
        }
    }

    void LaunchDash()
    {
        Vector2 velocity = Vector2.zero;
        Vector2 dir = _lastPosition;
        velocity += dir.normalized * (dashSpeedRB * dashIntValue);
        rb.velocity = velocity;
        StartCoroutine(DashWaitCorou());
    }

    IEnumerator DashWaitCorou()
    {
        //playerFeedBack.MovingRumble(CheckPosition(_lastPositionForRotor));
        if (useVibration)
        {
            playerFeedBack.MovingRumble(playerFeedBack.vibrationForce);
        }

        _isDashing = true;
        yield return new WaitForSeconds(durationDash / 2);
        if (useVibration)
        {
            playerFeedBack.MovingRumble(Vector2.zero);
        }

        rb.velocity = Vector2.zero;
        _isDashing = false;
    }

    public void DashTP(float speed)
    {
        Vector2 dir = _lastPosition;
        rb.AddForce(dir * (speed * 100));
    }

    public void SmallMovementAttack()
    {
        Vector2 dir = _lastPosition;
        transform.position =
            Vector2.Lerp(transform.position, dir + (Vector2) offsetAttackXY.position, _smallMovementFloat);
        Debug.Log(_smallMovementFloat);
    }

    public void ResetSmallMovement()
    {
        _smallMovementFloat = attackMovingSpeed * CustomDeltaTimeAttack;
        if (_smallMovementFloat > 1)
        {
            _smallMovementFloat = 0;
        }
    }

    #endregion DashAttribut

    private void Update()
    {
        if (isDash || isAttacking)
        {
            ResetSmallMovement();
            DashWait();
            Reset();
        }

        switch (attackType)
        {
            case 1:
                ResetAttackX(0);
                break;
            case 2:
                ResetAttackX(1);
                break;
        }

        if (launchProjectile)
        {
            LaunchProjectile();
        }
        else if (!launchProjectile)
        {
            ResetLaunchProjectile();
        }

        if (m_isColliding)
        {
            lastVelocity = rb.velocity;
        }
    }


    public void FixedUpdate()
    {
        SaveLastPosition();
        _attackPath.Path();
        _attackPath.OnMovement(attackSpline.arrayVector[0].pointAttack);
        Move();
        Animation();

        if (_launchDebug)
        {
            //DebugUI();
        }
    }

    public void AttackTypeX()
    {
        if (attackType < 2)
        {
            attackType++;
            isAttacking = true;
            attackPath.launchAttack = true;
            launchFirstAttack = true;
            launchSecondAttack = false;
            if (attackType >= 2 && delayForSecondAttack >= _timerAttack)
            {
                attackType = 2;
                attackPath.launchSecondAttack = true;
                launchFirstAttack = false;
                launchSecondAttack = true;
            }
        }
    }

    void ResetAttackX(int attack)
    {
        _timerDelayAttack += CustomDeltaTimeAttack;
        if (_timerDelayAttack >= valueBeforeResetAttack)
        {
            _timerDelayAttack = 0f;
            if (attack == 0)
            {
                launchFirstAttack = false;
            }

            if (attack == 1)
            {
                launchFirstAttack = false;
                launchSecondAttack = false;
            }
        }
    }


    public void Reset()
    {
        if (isDash)
        {
            _timerDash += CustomDeltaTimeAttack;
            if (_timerDash >= durationCooldownDash)
            {
                canDash = true;
                isDash = false;
                dashCounter = 0f;
                _timerDash = 0f;
            }
        }

        if (isAttacking)
        {
            _timerAttack += CustomDeltaTimeAttack;
            if (_timerAttack >= _playerStatsManager.firstAttackReset.x ||
                _timerAttack <= _playerStatsManager.firstAttackReset.y)
            {
                animatorPlayer.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                if (_timerAttack > _playerStatsManager.firstAttackReset.y)
                {
                    animatorPlayer.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (_timerAttack >= delayBeforeResetAttack)
            {
                attackType = 0;
                isAttacking = false;
                attackPath.launchAttack = false;
                attackPath.launchSecondAttack = false;
                attackPath.progress = 0f;
                _timerAttack = 0f;
            }
        }
    }

    public void SaveLastPosition()
    {
        Vector3 move = new Vector3(movementInput.x, movementInput.y, 0);
        Vector3 look = new Vector3(lookAxis.x, lookAxis.y, 0);
        _move = move;
        _look = look;
        if (movementInput.x != 0 || movementInput.y != 0)
        {
            _lastPosition = _move.normalized;
            _lastPositionForRotor = _lastPosition;
        }

        if (lookAxis.x != 0 || lookAxis.y != 0)
        {
            _lastPosition = _look.normalized;
            _lastPositionForRotor = _lastPosition;
        }
    }


    #region Projectile

    public void LaunchProjectile()
    {
        projectileObj.SetActive(true);
        _delayProjectile += speedProjectile * CustomDeltaTimeAttack;
        Vector2 posPlayer = transform.position;
        _objPosition = Vector2.Lerp(posPlayer, projectileAttack.position, animationCurve.Evaluate(_delayProjectile));
        projectileObj.transform.position = _objPosition;
        if (_delayProjectile >= 1)
        {
            _delayProjectile = 1;
            launchProjectile = false;
            projectileObj.SetActive(false);
        }
    }

    void ResetLaunchProjectile()
    {
        _delayProjectile = 0;
        projectileObj.transform.position = transform.position;
    }

    #endregion

    Vector3 CheckPosition(Vector3 direction)
    {
        float puissance = 5f;
        Vector3 newDirection = direction.normalized;
        _directionNormalized = newDirection;
        float durationRotor = 1f;
        if (direction.y > 0)
        {
            if (direction.x > 0 && direction.y > direction.x)
            {
                direction = new Vector3(newDirection.y, newDirection.x, durationRotor);
            }

            else if (direction.x < 0 && direction.y < direction.x)
            {
                newDirection.x *= -1;

                direction = new Vector3(newDirection.y, newDirection.x, durationRotor);
            }
        }
        else if (direction.y < 0)
        {
            newDirection.y *= -1;
            if (direction.x > 0 && direction.y > direction.x)
            {
                direction = new Vector3(newDirection.x, newDirection.y, durationRotor);
            }
            else if (direction.x < 0 && direction.y < direction.x)
            {
                newDirection.x *= -1;
                direction = new Vector3(newDirection.x, newDirection.y, durationRotor);
            }
        }

        return direction;
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == 18)
        {
            m_isColliding = true;
            /*
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collider.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 0f);
            Debug.Log("Hello");
            */
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        m_isColliding = false;
        Debug.Log("Au revoir");
    }

    /*public void DebugUI()
    {
        ////For Stick Only
        Image pointColor = elementOfTextMeshPro[0].GetComponent<Image>();

        ////For Data Only
        TextMeshProUGUI axisCoord = elementOfTextMeshPro[1].GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI isDashingText = elementOfTextMeshPro[2].GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI dashCountText = elementOfTextMeshPro[3].GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI timeBeforeDashText = elementOfTextMeshPro[4].GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI isAttackingText = elementOfTextMeshPro[5].GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI numberOfAttackText = elementOfTextMeshPro[6].GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI timeBeforeAttack = elementOfTextMeshPro[7].GetComponent<TextMeshProUGUI>();

        pointColor.color = Color.white;
        switch (lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0 && lookAxis != Vector2.zero)
        {
            case true:
                elementOfTextMeshPro[0].transform.localPosition = new Vector3(
                    positionStick.x + lookAxis.x * 50,
                    positionStick.y + lookAxis.y * 50,
                    0);
                break;
            case false:
                if (movementInput != Vector2.zero || lookAxis != Vector2.zero)
                {
                    elementOfTextMeshPro[0].transform.localPosition = new Vector3(
                        positionStick.x + movementInput.x * 50,
                        positionStick.y + movementInput.y * 50,
                        0);
                }

                break;
        }


        //Dash-------------------------------------------
        if (dashCount == 0)
        {
            pointColor.color = Color.white;
            dashCountText.color = Color.white;
        }

        if (isDash)
        {
            pointColor.color = dashCounter >= dashCount ? Color.red : Color.green;
            dashCountText.color = dashCounter >= dashCount ? Color.red : Color.green;
            timeBeforeDashText.color = dashCount >= 3 ? Color.red : Color.green;
        }
        else
        {
            dashCountText.color = Color.green;
            timeBeforeDashText.color = Color.green;
        }

        if (isAttacking)
        {
            isAttackingText.color = Color.blue;
            timeBeforeAttack.color = attackType >= 2 ? Color.red : Color.green;
            numberOfAttackText.color = attackType >= 2 ? Color.red : Color.green;
        }
        else
        {
            isAttackingText.color = Color.red;
            timeBeforeAttack.color = Color.green;
            numberOfAttackText.color = Color.green;
        }


        ////Text------------------------------------------
        dashCountText.text = "Dash Count : " + dashCounter;
        isDashingText.text = "Is Dashing: " + isDash;
        timeBeforeDashText.text =
            "Time Before Next Dash: " + Mathf.Round((durationCooldownDash - _timerDash) * 10) * 0.1;
        numberOfAttackText.text = "Number Of Attack " + (2 - attackType);
        isAttackingText.text = "Is Attacking : " + isAttacking;
        timeBeforeAttack.text = "Time Before Reset Attack : " +
                                Mathf.Round((delayBeforeResetAttack - _timerAttack) * 10) * 0.1;
        axisCoord.text = "X : " + Mathf.Round(movementInput.x * 10) * 0.1 + "  " + "Y : " +
                         Mathf.Round(movementInput.y * 10) * 0.1;
    }
    */

    public void DodgeAttack()
    {
        useDodgeAbility = true;
        Vector3 playerPos = transform.position;
        Vector3 dodgeRadius = new Vector3(playerPos.x * radiusDodge, playerPos.y * radiusDodge, 0);
        float distPlayerRadius = Vector3.Distance(playerPos, dodgeRadius);
        speed *= speedModification;
        _timerDodgeEffect += CustomDeltaTimeAttack;
        if (_timerAttack >= durationEffect)
        {
            useDodgeAbility = false;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        //float objPos = _objPosition.x > _objPosition.y ?_objPosition.x : _objPosition.y;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, (_lastPosition.normalized * dashSpeed) / 2);
        Gizmos.DrawWireSphere(splinePivot.transform.position, offsetEndPosProjectile);
    }
}

[System.Serializable]
public class TimeManager
{
    private static float timeRalenti;
    public const float DurationRalenti = 2f; // in real time ! Float to changer for the duration of the Slow Down

    public static bool isRalenti =>
        Time.time - timeRalenti < DurationRalenti; // did we ralenti more than 0.5f seconds ago ? so we are slown Down

    public static float CustomDeltaTimeEnnemy =>
        isRalenti
            ? Time.deltaTime * speedRalentiEnnemy
            : Time.deltaTime; // is ralenti => delta Time * 0.3f ( on ralenti la speed * 0.3f sinon full speed )


    public static float CustomDeltaTimeAttack =>
        isRalenti
            ? Time.deltaTime * speedRalentiProj
            : Time.deltaTime; // is ralenti => delta Time * 0.3f ( on ralenti la speed * 0.3f sinon full speed )

    public const float speedRalentiEnnemy = 0.1f;
    public const float speedRalentiProj = 0.1f;
    public const float stopTime = 0f;

    public static void SlowDownGame()
    {
        timeRalenti = Time.time;
    }
}