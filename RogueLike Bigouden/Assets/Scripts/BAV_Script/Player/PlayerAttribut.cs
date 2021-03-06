using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAttribut : MonoBehaviour
{
    [Header("Value Update In Background")]
    //Timer Value for the Delay.
    [SerializeField]
    private float timerDash;

    [SerializeField] private float timerAttack;
    [SerializeField] private float timerDelayAttack;

    [Header("Component Rigidbody")] [SerializeField]
    private Rigidbody2D rb;

    //Permet de relier ces vecteurs au Joystick dans le InputHandler.
    private Vector2 movementInput, lookAxis;

    [Header("Vitesse du joueur")]
    //Vitesse de déplacement du joueur.
    public float speed = 5;


    [Header("Etat du dash")]
    //Check Si le player est a déjà Dash ou si le joueur est en train de Dash.

    //float--------------------
    public float dashSpeed = 5;

    public float durationDash = 1f;
    public float durationCooldownDash = 1f;
    public float attackMoveSmall = 1f;

    //int--------------------
    public int dashCount = 3;

    //bool--------------------
    public bool isDash;
    public bool canDash;

    //private value for Dash------------------
    [SerializeField] private float dashCounter;


    [Header("Player Attack")] [SerializeField]
    private GameObject splinePivot;

    [SerializeField] public AttackSystemSpline attackSpline;
    public ProjectilePath attackPath;

    //float-------------------
    [SerializeField] public float valueBeforeResetAttack;
    [SerializeField] public float attackType;

    //bool--------------------
    public bool isAttacking;
    public bool launchFirstAttack;
    public bool launchSecondAttack;
    public float delayBeforeResetAttack = 1f;
    public float delayForSecondAttack = 4f;


    [Header("Animation et Sprite Renderer Joueur")] [SerializeField]
    public SpriteRenderer playerMesh;

    [SerializeField] private Animator animatorPlayer;


    [Header("FeedBack (Vibrations, etc)")]
    //Script permettant d'ajouter des FeedBack dans le jeu.
    [SerializeField]
    private PlayerFeedBack playerFeedBack;


    // Private Valor use just for this script----------------------------------
    [SerializeField] private PlayerStatsManager _playerStatsManager;
    [SerializeField] private ProjectilePath _attackPath;

    //float
    private float _durationResetDash;

    //int
    private int _dashCount;
    private int _dashCountMax;


    //Vector3
    [SerializeField] Vector3 _lastPosition;
    [SerializeField] Vector3 _lastPositionForRotor;
    [SerializeField] Vector3 _directionNormalized;
    [SerializeField] Vector3 _move;
    [SerializeField] Vector3 _look;

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
    }
    
    public void Move()
    {
        transform.Translate(_move * speed * Time.deltaTime);
    }


    public void MoveAnimation()
    {
        switch (lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0 && lookAxis != Vector2.zero)
        {
            case true:
                animatorPlayer.SetFloat("Horizontal", lookAxis.x);
                animatorPlayer.SetFloat("Vertical", lookAxis.y);
                animatorPlayer.SetFloat("Magnitude", movementInput.magnitude);
                break;
            case false:
                if (movementInput != Vector2.zero)
                {
                    animatorPlayer.SetFloat("Horizontal", movementInput.x);
                    animatorPlayer.SetFloat("Vertical", movementInput.y);
                }

                animatorPlayer.SetFloat("Magnitude", movementInput.magnitude);
                break;
        }
    }

    public void AttackAnimation()
    {
        switch (lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0 && lookAxis != Vector2.zero)
        {
            case true:
                animatorPlayer.SetBool("isAttacking", isAttacking);
                animatorPlayer.SetFloat("Horizontal", lookAxis.x);
                animatorPlayer.SetFloat("Vertical", lookAxis.y);
                break;
            case false:
                if (movementInput != Vector2.zero)
                {
                    animatorPlayer.SetBool("isAttacking", isAttacking);
                    animatorPlayer.SetFloat("Horizontal", movementInput.x);
                    animatorPlayer.SetFloat("Vertical", movementInput.y);
                }

                break;
        }
    }


    public void Attack(bool look)
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
        dashCounter++;
        canDash = true;
        if (dashCounter >= 1)
        {
            isDash = true;
            if (dashCounter >= dashCount + 1)
            {
                dashCounter = dashCount;
                canDash = false;
            }
        }

        if (canDash)
        {
            rb.velocity = Vector2.zero;
            launchDash();
        }
    }

    void launchDash()
    {
        Vector2 velocity = Vector2.zero;
        Vector2 dir = _lastPosition;
        velocity += dir.normalized * (dashSpeed * dashIntValue);
        rb.velocity = velocity;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        //playerFeedBack.MovingRumble(CheckPosition(_lastPositionForRotor));
        playerFeedBack.MovingRumble(playerFeedBack.vibrationForce);
        _isDashing = true;
        yield return new WaitForSeconds(durationDash / 2);
        playerFeedBack.MovingRumble(Vector2.zero);
        rb.velocity = Vector2.zero;
        _isDashing = false;
    }

    #endregion DashAttribut

    public void FixedUpdate()
    {
        SaveLastPosition();
        _attackPath.Path();
        _attackPath.OnMovement(attackSpline.arrayVector[0].pointAttack);
        Move();
        MoveAnimation();
        if (isDash || isAttacking)
        {
            Reset();
        }

        if (attackType > 0)
        {
            switch (attackType)
            {
                case 1:
                    ResetMovement(0);
                    break;
                case 2:
                    ResetMovement(1);
                    break;
            }
        }

        if (_launchDebug)
        {
            Debug();
        }
    }

    public void AttackType()
    {
        if (attackType < 2)
        {
            attackType++;
            isAttacking = true;
            attackPath.launchAttack = true;
            launchFirstAttack = true;
            launchSecondAttack = false;
            rb.velocity = Vector2.zero; 
            SmallMovementFirst();
        }


        if (attackType >= 2 && delayForSecondAttack >= timerAttack)
        {
            attackType = 2;
            attackPath.launchSecondAttack = true;
            launchFirstAttack = false;
            launchSecondAttack = true;
            isAttacking = true;
            rb.velocity = Vector2.zero; 
            SmallMovementSecond();
        }
        else
        {
            launchFirstAttack = false;
        }
    }


    public void Reset()
    {
        if (isDash)
        {
            timerDash += Time.deltaTime;
            if (timerDash >= durationCooldownDash)
            {
                canDash = true;
                isDash = false;
                dashCounter = 0f;
                timerDash = 0f;
            }
        }

        if (isAttacking)
        {
            timerAttack += Time.deltaTime;
            if (timerAttack >= delayBeforeResetAttack)
            {
                attackType = 0;
                isAttacking = false;
                attackPath.launchAttack = false;
                attackPath.launchSecondAttack = false;
                attackPath.progress = 0f;
                timerAttack = 0f;
                Vector2 velocity = Vector2.zero;
                rb.velocity = velocity;
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
            _lastPosition = _move;
            _lastPositionForRotor = _lastPosition;
        }

        if (lookAxis.x != 0 || lookAxis.y != 0)
        {
            _lastPosition = _look;
            _lastPositionForRotor = _lastPosition;
        }
    }

    void ResetMovement(int attack)
    {
        timerDelayAttack += Time.deltaTime;
        Vector2 velocity = Vector2.zero;
        float resetTimer = 0f;
        if (timerDelayAttack >= valueBeforeResetAttack)
        {
            timerDelayAttack = resetTimer;
            rb.velocity = velocity;
            if (attack == 0)
            {
                launchFirstAttack = false;
            }

            if (attack == 1)
            {
                launchSecondAttack = false;
            }
        }
    }


    public void SmallMovementFirst()
    {
        Vector2 velocity = Vector2.zero;
        Vector2 dir = _lastPosition.normalized;
        velocity += dir.normalized * (attackMoveSmall * dashIntValue);
        rb.velocity = velocity;
    }

    public void SmallMovementSecond()
    {
        Vector2 velocity = Vector2.zero;
        Vector2 dir = _lastPosition.normalized;
        velocity += dir.normalized * (attackMoveSmall * dashIntValue);
        rb.velocity = velocity;
    }

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

    public void Debug()
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

        Vector3 imagePosition = elementOfTextMeshPro[0].transform.position;
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
            isDashingText.color = Color.blue;
            pointColor.color = dashCounter >= dashCount ? Color.red : Color.green;
            dashCountText.color = dashCounter >= dashCount ? Color.red : Color.green;
            timeBeforeDashText.color = dashCount >= 3 ? Color.red : Color.green;
        }
        else
        {
            isDashingText.color = Color.red;
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
            "Time Before Next Dash: " + Mathf.Round((durationCooldownDash - timerDash) * 10) * 0.1;
        numberOfAttackText.text = "Number Of Attack " + (2 - attackType);
        isAttackingText.text = "Is Attacking : " + isAttacking;
        timeBeforeAttack.text = "Time Before Reset Attack : " +
                                Mathf.Round((delayBeforeResetAttack - timerAttack) * 10) * 0.1;
        axisCoord.text = "X : " + Mathf.Round(movementInput.x * 10) * 0.1 + "  " + "Y : " +
                         Mathf.Round(movementInput.y * 10) * 0.1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (_lastPosition.normalized * dashSpeed) / 2);
    }
}
