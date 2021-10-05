using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicMovement_TestAnim : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        Movement();
        //LookRotation();
    }

    void Movement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        transform.position = transform.position + movement * Time.deltaTime;
    }

    void LookRotation()
    {
        Vector2 screenSize= Camera.main.ScreenToViewportPoint(Input.mousePosition);
        
        Vector2 lookRotation = new Vector2(screenSize.x, screenSize.y);
        
        animator.SetFloat("Horizontal", lookRotation.x);
        animator.SetFloat("Vertical", lookRotation.y);
    }
}
