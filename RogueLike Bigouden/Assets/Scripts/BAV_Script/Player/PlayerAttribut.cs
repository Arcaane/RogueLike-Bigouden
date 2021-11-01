using System;
using System.Collections;
using System.Collections.Generic;
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
    public float dashSpeed = 5;

    [Header("Etat du dash")]
    //Check Si le player est a déjà Dash ou si le joueur est en train de Dash.
    [SerializeField]
    private bool hasDashed;

    [SerializeField] private bool isDashing;

    [Header("Player Attack")] [SerializeField]
    private GameObject splinePivot;

    [SerializeField] public AttackSystemSpline attackSpline;
    public ProjectilePath attackPath;

    [Header("Animation et Sprite Renderer Joueur")] [SerializeField]
    public SpriteRenderer playerMesh;

    [SerializeField] private Animator animatorPlayer;

    [Header("FeedBack (Vibrations, etc)")]
    //Script permettant d'ajouter des FeedBack dans le jeu.
    [SerializeField]
    private PlayerFeedBack playerFeedBack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Animator

    public void Move()
    {
        transform.Translate(new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime);
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
        switch (look && lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0 && lookAxis != Vector2.zero)
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
        hasDashed = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(movementInput.x, movementInput.y);
        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        playerFeedBack.MovingRumble(playerFeedBack.vibrationForce);
        isDashing = true;
        yield return new WaitForSeconds(.3f);
        playerFeedBack.MovingRumble(Vector2.zero);
        rb.velocity = Vector2.zero;
        isDashing = false;
    }

    public void FixedUpdate()
    {
        Move();
        MoveAnimation();
    }
}