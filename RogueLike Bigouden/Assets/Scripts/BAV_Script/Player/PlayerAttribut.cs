using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribut : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    //Permet de relier ces vecteurs au Joystick dans le InputHandler.
    private Vector2 movementInput, lookAxis;
    //Vitesse de déplacement du joueur.
    public float speed = 5;
    //Vitesse de force du dash.
    public float dashSpeed = 5;
    //Check Si le player est a déjà Dash ou si le joueur est en train de Dash.
    [SerializeField] private bool hasDashed, isDashing;
    //Script permettant d'ajouter des FeedBack dans le jeu.
    [SerializeField] private PlayerFeedBack playerFeedBack;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    //Animator
    [SerializeField] private Animator animatorPlayer;

    public void Move()
    {
        switch (lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0)
        {
            case true:
                animatorPlayer.SetFloat("Horizontal", lookAxis.x);
                animatorPlayer.SetFloat("Vertical", lookAxis.y);
                animatorPlayer.SetFloat("Magnitude", movementInput.magnitude);
                //Debug.Log("0");
                break;
            case false:
                animatorPlayer.SetFloat("Horizontal", movementInput.x);
                animatorPlayer.SetFloat("Vertical", movementInput.y);
                animatorPlayer.SetFloat("Magnitude", movementInput.magnitude);
                //animatorPlayer.SetFloat("Speed", speed);
                //Debug.Log("1");
                break;
        }

        transform.Translate(new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime);
    }

    public void SetInputVector(Vector2 direction, bool look)
    {
        switch (look)
        {
            case true:
                lookAxis = direction;
                break;
            case false:
                movementInput = direction;
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
}