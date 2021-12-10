using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using static TimeManager;

public class PlayerAttribut : MonoBehaviour
{
    private TimeManager timerManager;

    [Header("Component Stats Manager")] 
    [SerializeField] private PlayerStatsManager _playerStatsManager;
    [SerializeField] private PlayerInput_Final _playerInput;
    [SerializeField] private Inventory _playerInventory;

    [Header("Value Update In Background")]
    //Timer Value for the Delay.
    [SerializeField]
    private float _timerDash;

    [SerializeField] private float _timerBetweenDash;
    [SerializeField] private float _timerAttack;
    [SerializeField] private float _timerUltimate;
    [SerializeField] private float _timerCamera;

    [Header("Component Rigidbody")] [SerializeField]
    private Color colorReset;

    [SerializeField] private Rigidbody2D rb;

    [Header("Utiliser le Clavier ?")] [SerializeField]
    private bool useVibration;

    [Header("Bounce Collider")] [SerializeField]
    private float _bounceCount;

    [SerializeField] private float bounceForce;
    [SerializeField] private bool isBounce;
    [SerializeField] private Vector3 lastVelocity;

    [Header("Etat du dash")]
    //Check Si le player est a déjà Dash ou si le joueur est en train de Dash.

    //Can be delete for the Final Build
    [SerializeField]
    private bool useRBDash;

    [SerializeField] public float dashSpeedRB = 5;

    //float--------------------
    [SerializeField] float _dashSpeed;

    public float durationDash = 1f;

    //private value for Dash------------------
    [SerializeField] private float dashCounter;

    [Header("Player Attack X/Y")] [SerializeField]
    private SpriteRenderer spriteRendererFrame;

    [SerializeField] private Color perfectFrameColor;
    [SerializeField] private GameObject splinePivot;

    [SerializeField] public AttackSystemSpline attackSpline;
    [SerializeField] public ProjectilePath attackPath;

    //float-------------------
    [SerializeField] public float attackType;
    [SerializeField] public float attackMovingSpeed;
    public bool launchAOEAttack;

    //Valor for detecting a hit
    private int isHurt;
    private Vector3 targetPos;

    [Space(10)] [Header("Player Attack Projectile")]
    //Object Projectile
    // [SerializeField] private Transform projectileObj;
    private Vector3 shootPointPos;

    //Projectile Attack Position
    [SerializeField] private GameObject AttackProjectile;
    [SerializeField] public GameObject launchProjectileFeedback;
    [SerializeField] private float damageProjectile;
    [SerializeField] public float delayProjectile;
    [SerializeField] public float delayProjectileReduction;
    [SerializeField] public float p_delay;

    //Offset of the end Position
    [SerializeField] private float offsetEndPosProjectile;


    [Space(10)]
    [Header("Player Ultimate")]
    //float--------------------
    [SerializeField]
    private GameObject ultBulletSpawner;

    [SerializeField] private float ultDuration;

    //bool--------------------
    [SerializeField] private bool isUlting;

    [Header("Boolean pour dialogue et Item")] 
    [SerializeField] public bool canTakeItem;
    [SerializeField] public bool canTalk;
    [SerializeField] public bool canSkipDialogue;


    [Header("Dogdge Ability")]
    //float-----------------------------------
    [SerializeField]
    private float radiusDodge;

    [SerializeField] private float durationEffect;
    [SerializeField] private float speedModification;
    [SerializeField] private float _timerDodgeEffect;

    ////bool-----------------------------------
    [SerializeField] public bool useDodgeAbility;
    [SerializeField] private bool dodgeAbility;
    [SerializeField] private bool bulletIn;

    [Header("Animation et Sprite Renderer Joueur")] 
    [SerializeField] public SpriteRenderer playerMesh;
    [SerializeField] private Animator animatorPlayer;


    [Header("FeedBack (Vibrations, etc)")]
    //Script permettant d'ajouter des FeedBack dans le jeu.
    [SerializeField]
    private PlayerFeedBack playerFeedBack;

    [SerializeField] private Transform posCam;

    [SerializeField] private Camera cam;
    [SerializeField] private float _timerBeforeResetPosCamera;
    [SerializeField] Vector2 limitationArray;

    [Space(10)]

    // Private Valor use just for this script----------------------------------
    [SerializeField]
    private ProjectilePath _attackPath;

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

    [HideInInspector] public Vector3 _move;

    //Bools
    private bool _readyToAttackX;
    private bool _readyToAttackY;
    private bool _readyToAttackB;
    public bool _isDashing;
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
        _playerInput = GetComponent<PlayerInput_Final>();
        rb = GetComponent<Rigidbody2D>();
        _playerStatsManager = GetComponent<PlayerStatsManager>();
        cam = _playerInput.GetComponent<Camera>();
        ultBulletSpawner.SetActive(false);

        //_isDashing = _playerStatsManager.isDashing;
        _playerStatsManager.readyToDash = true;
        _playerStatsManager.readyToAttackX = true;
        _playerStatsManager.readyToAttackY = true;
        _playerStatsManager.readyToAttackB = true;

        Player_FeedBack.fb_instance.p_attribut = this;
        Player_FeedBack.fb_instance.p_transform = transform;
    }

    private void Start()
    {
        //posCam = PlayerFeedBack.instance.pivotCam;
        if (!_launchDebug && elementOfTextMeshPro.Count > 0)
        {
            for (int i = 0; i < elementOfTextMeshPro.Count; i++)
            {
                elementOfTextMeshPro[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        _attackPath.Path();
        if (_playerStatsManager.isDashing || _playerStatsManager.isAttackingX)
        {
            ResetSmallMovement();
            DetectAttackCamera();
            DashWait();
            Reset();
            DodgeAbility_T();
        }

        if (_playerStatsManager.isAttackB && _playerStatsManager.readyToAttackB)
        {
            MovementProjectile();
        }

        if (isUlting)
        {
            UltimateDelay();
        }

        if (!_playerStatsManager.readyToAttackB)
        {
            ResetLaunchProjectile();
        }

        if (_playerStatsManager.readyToAttackB && !_playerStatsManager.isAttackB)
        {
            shootPointPos = (_lastPosition);
            shootPointPos.Normalize();
            float angle = Mathf.Atan2(shootPointPos.y, shootPointPos.x) * Mathf.Rad2Deg;
            launchProjectileFeedback.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (launchProjectileFeedback.activeSelf)
        {
            _playerStatsManager.movementSpeed = 0.5f;
        }
        else
        {
            _playerStatsManager.movementSpeed = 5f;
        }

        //Stock l'ancienne Velocity
        lastVelocity = rb.velocity;
    }


    public void FixedUpdate()
    {
        SaveLastPosition();
        _attackPath.OnMovement(attackSpline.arrayVector[0].pointAttack);
        Move();

        Animation();
        SmoothJoystickCamera();
        //MoveCameraRightStick();

        if (_launchDebug)
        {
            //DebugUI();
        }
    }

    public void Move()
    {
        if (!isUlting)
        {
            if (_playerStatsManager.isAttackFirstX || _playerStatsManager.isAttackSecondX)
            {
                transform.Translate(_move * _playerStatsManager.movementSpeed * (1.3f) *
                                    _timeManager.CustomDeltaTimePlayer);
            }
            else
                transform.Translate(_move * _playerStatsManager.movementSpeed * _timeManager.CustomDeltaTimePlayer);
        }
    }

    #region AnimatorProcess

    public void Animation()
    {
        bool moving = lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0 && lookAxis != Vector2.zero;

        SetJoystickValue(moving);
        animatorPlayer.SetFloat("Magnitude", movementInput.magnitude);

        if (_playerStatsManager.isAttackFirstX)
        {
            SetJoystickValue(moving);
            SetAttackValue(true);
        }
        else if (_playerStatsManager.isAttackSecondX && !_playerStatsManager.isAttackFirstX)

        {
            SetJoystickValue(moving);
            SetAttackValue(attack2: true);
            attackPath.launchSecondAttack = true;
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
                lookAxis.y < 0 && lookAxis != Vector2.zero && !_playerStatsManager.isAttackingX)
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
        _playerStatsManager.readyToDash = true;
        dashCounter++;
        if (dashCounter >= 1)
        {
            _playerStatsManager.isDashing = true;
            if (dashCounter >= _playerStatsManager.dashCounter)
            {
                dashCounter = _playerStatsManager.dashCounter;
                _playerStatsManager.readyToDash = false;
            }
        }

        if (_playerStatsManager.readyToDash && !isUlting)
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
            DashTP(_dashSpeed);
        }
    }


    void DashWait()
    {
        if (useVibration)
        {
            playerFeedBack.MovingRumble(playerFeedBack.vibrationForce);
        }

        _timerBetweenDash += _timeManager.CustomDeltaTimePlayer;
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
        spriteRendererFrame.material.SetFloat("_DiffuseIntensity", 10);
        Vector2 velocity = Vector2.zero;
        Vector2 dir = _lastPosition;
        velocity += dir.normalized * (_playerStatsManager.dashSpeed * dashIntValue);
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

        _playerStatsManager.isDashing = true;
        yield return new WaitForSeconds(_playerStatsManager.dashDuration / 2);
        if (useVibration)
        {
            playerFeedBack.MovingRumble(Vector2.zero);
        }

        spriteRendererFrame.material.SetFloat("_DiffuseIntensity", 1);
        rb.velocity = Vector2.zero;
        _playerStatsManager.isDashing = false;
    }

    public void DashTP(float speed)
    {
        Vector2 dir = _lastPosition;
        rb.AddForce(dir * (speed * 100));
    }


    public void ResetSmallMovement()
    {
        _smallMovementFloat = attackMovingSpeed * _timeManager.CustomDeltaTimePlayer;
        if (_smallMovementFloat > 1)
        {
            _smallMovementFloat = 0;
        }
    }

    #endregion DashAttribut

    #region AttackAttribute

    public void AttackTypeX()
    {
        _playerStatsManager.isAttackingX = true;
        attackType++;
        if (attackType == 1)
        {
            attackPath.launchFirstAttack = true;
            _playerStatsManager.isAttackFirstX = true;
            animatorPlayer.speed = _timeManager.m_speedRalentiPlayer;
        }

        if (attackType == 2 &&
            _timerAttack > _playerStatsManager.firstAttackReset.x &&
            _timerAttack < _playerStatsManager.firstAttackReset.y + 0.2f)
        {
            attackType = 2;
            _playerStatsManager.isAttackFirstX = false;
            _playerStatsManager.isAttackSecondX = true;
        }
    }

    public void Reset()
    {
        if (_playerStatsManager.isDashing)
        {
            _timerDash += _timeManager.CustomDeltaTimePlayer;
            if (_timerDash >= _playerStatsManager.dashCooldown)
            {
                _playerStatsManager.readyToDash = true;
                _playerStatsManager.isDashing = false;
                dashCounter = 0f;
                _timerDash = 0f;
            }
        }

        if (_playerStatsManager.isAttackingX)
        {
            _timerAttack += _timeManager.CustomDeltaTimePlayer;
            if (_timerAttack > _playerStatsManager.firstAttackReset.x &&
                _timerAttack < (_playerStatsManager.firstAttackReset.y))
            {
                spriteRendererFrame.material.color = perfectFrameColor;
                spriteRendererFrame.color = perfectFrameColor;
            }

            if (_timerAttack > _playerStatsManager.firstAttackReset.y)
            {
                spriteRendererFrame.material.color = colorReset;
                spriteRendererFrame.color = colorReset;
            }

            if (_playerStatsManager.isAttackFirstX && _timerAttack >= _playerStatsManager.firstAttackReset.x + 0.2f)
            {
                attackType = 0;
                _playerStatsManager.isAttackingX = false;
                _playerStatsManager.isAttackFirstX = false;
                attackPath.launchFirstAttack = false;
                attackPath.progress = 0f;
                _timerAttack = 0f;
            }

            if (_playerStatsManager.isAttackSecondX)
            {
                if (_timerAttack >= (_playerStatsManager.firstAttackReset.y + 0.2f))
                {
                    attackType = 0;
                    _playerStatsManager.isAttackingX = false;
                    _playerStatsManager.isAttackFirstX = false;
                    _playerStatsManager.isAttackSecondX = false;
                    attackPath.launchFirstAttack = false;
                    attackPath.launchSecondAttack = false;
                    attackPath.progress = 0f;
                    _timerAttack = 0f;
                }
            }
        }
    }

    #endregion

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

    private const float radiusShootPoint = 0.9f;

    public void MovementProjectile()
    {
        damageProjectile = PlayerStatsManager.playerStatsInstance.damageProjectile;
        _playerStatsManager.isAttackB = false;
        p_delay = _playerStatsManager.attackCdB;
        _playerStatsManager.readyToAttackB = false;

        GameObject obj = Instantiate(AttackProjectile, transform.position + shootPointPos * radiusShootPoint,
            Quaternion.identity);
        obj.GetComponent<ProjectilePlayer>()
            .GoDirection(new Vector2(shootPointPos.x, shootPointPos.y), 7f, 2,
                damageProjectile); // Direction puis Speed des balles
        Destroy(obj, delayProjectile);

        launchProjectileFeedback.SetActive(false);
    }

    private void ResetLaunchProjectile()
    {
        _playerStatsManager.readyToAttackB = false;
        Debug.Log("pls sir");
        p_delay -= Time.deltaTime;
        if (p_delay <= 0)
        {
            p_delay = 0;
            _playerStatsManager.readyToAttackB = true;
        }
    }

    #endregion

    #region Ultimate

    //Launch the function to activate Ultimate
    public void LaunchUltimate()
    {
        isUlting = true;
        ultBulletSpawner.SetActive(true);
    }

    //Ultimate Delay when he is Activate.
    public void UltimateDelay()
    {
        ultDuration = _playerStatsManager.actualUltPoint;
        if (ultDuration > _playerStatsManager.ultDuration)
        {
            ultDuration = (ultDuration / 2) / 10;
            _timerUltimate += _timeManager.CustomDeltaTimePlayer;

            movementInput = Vector2.zero;

            if (_timerUltimate >= ultDuration)
            {
                _timerUltimate = 0;
                ultBulletSpawner.SetActive(false);
                isUlting = false;
                _playerStatsManager.actualUltPoint = 0;
            }
        }
        else
        {
            ultDuration = 0;
            _timerUltimate += _timeManager.CustomDeltaTimePlayer;
            if (_timerUltimate >= ultDuration)
            {
                _timerUltimate = 0;
                ultBulletSpawner.SetActive(false);
                isUlting = false;
                _playerStatsManager.actualUltPoint = 0;
            }
        }
    }

    //Capacity bar of the Ultimate.
    public void UltimateBar()
    {
    }

    #endregion

    #region CameraController

    public void DetectAttackCamera()
    {
        //Camera Shake
        isHurt = attackPath.GetComponent<ProjectilePath>().projectile.GetComponent<ApplyAttack>().isDetect;
        targetPos = attackPath.GetComponent<ProjectilePath>().projectile.GetComponent<ApplyAttack>().posTarget;
        if (isHurt == 1)
        {
            //playerFeedBack.MoveCameraInput(isHurt, transform.position, targetPos, 10);
            //ResetPosCam();
        }
    }

    public void ResetPosCam()
    {
        _timerCamera += _timeManager.CustomDeltaTimePlayer;
        if (_timerCamera >= _timerBeforeResetPosCamera)
        {
            _timerCamera = 0;
            playerFeedBack.CameraMovement(targetPos, transform.position, 10);
        }
    }

    public void SmoothJoystickCamera()
    {
        Vector3 aim = new Vector3(lookAxis.x, lookAxis.y, 0.0f);
        Vector3 lookAxisNor = lookAxis.normalized;
        Vector3 target;
        if (_playerInput.kbMouse)
        {
            target = posCam.position + aim.normalized;
        }
        else
        {
            target = posCam.position + aim.normalized * 10;
        }

        float roundAxis = 0;
        Vector2 posObj = transform.position.normalized;
        roundAxis = Vector2.Distance(posObj, lookAxisNor);
        roundAxis = roundAxis / 5;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target, (roundAxis <= 0 ? -roundAxis : roundAxis));
        posCam.position = new Vector2(
            Mathf.Clamp(smoothedPosition.x, -limitationArray.x, limitationArray.x),
            Mathf.Clamp(smoothedPosition.y, -limitationArray.y, limitationArray.y));
    }

    #endregion

    public void DodgeAttack()
    {
        useDodgeAbility = true;
        Vector3 playerPos = transform.position;
        Vector3 dodgeRadius = new Vector3(playerPos.x * radiusDodge, playerPos.y * radiusDodge, 0);
        float distPlayerRadius = Vector3.Distance(playerPos, dodgeRadius);
        _playerStatsManager.movementSpeed *= speedModification;
        _timerDodgeEffect += _timeManager.CustomDeltaTimePlayer;
        if (_timerAttack >= durationEffect)
        {
            useDodgeAbility = false;
        }
    }

    //Bounce Without Physics Material;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sofa"))
        {
            BounceSofa(other);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sofa"))
        {
            BounceSofa(other);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isBounce = false;
    }

    void BounceSofa(Collision2D obj)
    {
        float speed = lastVelocity.magnitude * bounceForce;
        Vector3 direction = Vector3.Reflect(lastVelocity.normalized, obj.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 0f);

        //For Projectile Only.
        /*
        _bounceCount--;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        */

        isBounce = true;
    }


    [SerializeField] private float radiusBeforeDash;
    [SerializeField] List<Transform> target;
    private Vector3 posPlayer;

    public void DodgeAbility_T()
    {
        foreach (Transform obj in target)
        {
            Vector3 objPos = obj.position;
            float range = Vector2.Distance(transform.position, objPos);
            if (range <= 0)
            {
                range *= -1;
            }

            if (range < radiusBeforeDash)
            {
                TimeManager._timeManager.SlowDownGame(3);
                Debug.Log(range);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        //float objPos = _objPosition.x > _objPosition.y ?_objPosition.x : _objPosition.y;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, (_lastPosition.normalized * _dashSpeed) / 2);
        Gizmos.DrawWireSphere(splinePivot.transform.position, offsetEndPosProjectile);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + shootPointPos * radiusShootPoint, 0.25f);
        Gizmos.DrawWireSphere(posPlayer, radiusBeforeDash);
    }
}

/*
public void SmallMovementAttack()
{
    Vector2 dir = _lastPosition;
    transform.position =
        Vector2.Lerp(transform.position, dir + (Vector2) offsetAttackXY.position, _smallMovementFloat);
    Debug.Log(_smallMovementFloat);
}*/

/*
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
}*/
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