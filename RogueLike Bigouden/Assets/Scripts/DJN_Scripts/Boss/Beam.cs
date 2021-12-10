using System;
using System.Collections;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private LineRenderer line;
    private RaycastHit2D hit;
    [SerializeField] private int damage;
    [SerializeField] private float raySpeed;
    [SerializeField] private float startingTime;
    [SerializeField] private LayerMask layerMask;
    public Transform originPoint;
    public Transform originTarget;
    public Transform newTarget;
    public Transform ghostTarget;
    
    private PlayerStatsManager _playerStatsManager;
    private bool pHit;
    
    private bool isMoving;
    [HideInInspector] public bool isActive;
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
            
            if (hit.collider.GetComponent<PlayerStatsManager>())
            {
                _playerStatsManager = hit.collider.GetComponent<PlayerStatsManager>();

                if (!pHit)
                {
                    _playerStatsManager.TakeDamage(damage);
                    pHit = true;
                }
            }
        }
        else
        {
            line.SetPosition(1, ghostTarget.position);
            pHit = false;
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