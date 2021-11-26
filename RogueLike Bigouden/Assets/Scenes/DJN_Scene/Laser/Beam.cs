using System;
using System.Collections;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private LineRenderer line;
    private RaycastHit2D hit;
    
    [SerializeField] private float raySpeed;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] public Transform originPoint;
    [SerializeField] public Transform originTarget;
    [SerializeField] public Transform newTarget;
    [SerializeField] public Transform ghostTarget;

    private bool isMoving;
    public bool isActive;
    public float startingTime;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        
        isActive = false;
        line.enabled = false;
        originPoint.gameObject.layer = 2;
    }

    private void Update()
    {
        
        if (isActive)
        {
            line.enabled = true;
            Laser();
        }
        
        
    }

    public void Laser()
    {
        
        hit = Physics2D.Linecast(originPoint.position, ghostTarget.position,
            layerMask); //shoot a raycast from origin point to a ghost point which his position is the originTarget position

        line.SetPosition(0, originPoint.position);
        if (hit)
        {
            line.SetPosition(1, hit.point);
            //Debug.Log(hit.collider.name);
            
            if (hit.collider.CompareTag("Player"))
            {
                isActive = false;
                line.enabled = false;
            }
            
            Debug.Log(hit.collider.name);
        }
        else
        {
            line.SetPosition(1, ghostTarget.position);
        }
        
        StartCoroutine(BeamMoving());
    }

    IEnumerator BeamMoving()
    {
        yield return new WaitForSeconds(startingTime);
        ghostTarget.position = Vector2.MoveTowards(ghostTarget.position, newTarget.position, raySpeed * Time.deltaTime); //move the ghostTarget to the next point

        if (ghostTarget.position == newTarget.position)
        {
            line.enabled = false;
            isActive = false;
        }

       
        
        
    }
}
