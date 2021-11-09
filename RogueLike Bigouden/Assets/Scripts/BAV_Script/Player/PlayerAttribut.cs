using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttribut : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    //Permet de relier ces vecteurs au Joystick dans le InputHandler.
    private Vector2 movementInput, lookAxis;

    [Header("Vitesse du joueur")]
    //Vitesse de déplacement du joueur.
    public float speed = 5;

    //Vitesse de force du dash.

    [Header("Etat du dash")]
    //Check Si le player est a déjà Dash ou si le joueur est en train de Dash.
    //float--------------------
    public float dashSpeed = 5;
    public float durationDash = 1f;
    
    //int--------------------
    public int dashCount = 3;

    [Header("Player Attack")] [SerializeField]
    private GameObject splinePivot;

    [SerializeField] public AttackSystemSpline attackSpline;
    public ProjectilePath attackPath;
    
    [SerializeField] public bool launchAttack;
    [SerializeField] public bool launchSecondAttack;
    [SerializeField] public float delayForSecondAttack = 4f;
    [SerializeField] private float timer;
    [SerializeField] public float incrementValue;

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


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerStatsManager = GetComponent<PlayerStatsManager>();
        _isDashing = _playerStatsManager.isDashing;
    }

    public void Move()
    {
        if (attackPath.launchAttack)
        {
            transform.Translate(_move * Time.deltaTime);
        }
        else
        {
            transform.Translate(_move * speed * Time.deltaTime);
        }
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

    public void Attack(bool look)
    {
        switch (look && lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 ||
                lookAxis.y < 0 && lookAxis != Vector2.zero)
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
    

    public void Dash()
    {
        Vector2 velocity = Vector2.zero;
        Vector2 dir = _lastPosition;
        velocity += dir.normalized * dashSpeed;
        rb.velocity = velocity;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        playerFeedBack.MovingRumble(playerFeedBack.vibrationForce);
        _isDashing = true;
        yield return new WaitForSeconds(durationDash/2);
        playerFeedBack.MovingRumble(Vector2.zero);
        rb.velocity = Vector2.zero;
        _isDashing = false;
    }

    void DownValue()
    {
        if (incrementValue >= 4)
        {
            timer += (Time.deltaTime / (speed / 10));
            if (timer >= speed)
            {
                incrementValue = 0;
                timer = 0f;
            }
        }
    }

    public void FixedUpdate()
    {
        SaveLastPosition();
        _attackPath.Path();
        _attackPath.OnMovement(attackSpline.arrayVector[0].pointAttack);
        Move();
        MoveAnimation();
        DownValue();
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
        }

        if (lookAxis.x != 0 || lookAxis.y != 0)
        {
            _lastPosition = _look;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (_lastPosition.normalized * dashSpeed)/2);
    }
}