using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCocktailBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask isPlayer;
    void Start()
    {
        InvokeRepeating(nameof(DamageAndSlowPlayer), 1, 1.5f); 
        Destroy(gameObject, 5f);
    }

    private void DamageAndSlowPlayer()
    {
        Collider2D[] playerCircleAll = Physics2D.OverlapCircleAll(transform.position, 2, isPlayer);
        foreach (var p in playerCircleAll)
        {
            p.GetComponent<PlayerStatsManager>().lifePoint -= 1;
            p.GetComponent<PlayerStatsManager>().movementSpeed *= 0.9f;
            Debug.Log("Player hit : " + p.name + " by yellow cocktail");
            StartCoroutine(ResetMoveSpeed(p));
        }
    }

    private IEnumerator ResetMoveSpeed(Collider2D p)
    {
        yield return new WaitForSeconds(3f);
        p.GetComponent<PlayerStatsManager>().movementSpeed *= 1.1f;
    }
}
