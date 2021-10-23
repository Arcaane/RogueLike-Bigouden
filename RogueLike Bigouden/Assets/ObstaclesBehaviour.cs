using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ennemy"))
            if (other.gameObject.GetComponent<IARunner>() != null)
                other.gameObject.GetComponent<IARunner>().TakeObstacle();
    }
}
