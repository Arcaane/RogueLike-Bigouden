using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FlameStrike : MonoBehaviour
{
  public Animator _animator;
  private BossEventManager _bossEventManager;

  [SerializeField] private int damage;
  public bool playerOnIt;
  public GameObject player;
  private bool alreadyLoad;
  private void Awake()
  {
    _bossEventManager = FindObjectOfType<BossEventManager>();
  }
  

  public void LoadTint()
  {
    if (!FindObjectOfType<BossStatsManager>().isDead)
    {
      _animator.Play("dancefloor_explode");
    }
  }


  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.GetComponent<PlayerStatsManager>() && other is BoxCollider2D)
    {
      playerOnIt = true;
      player = other.gameObject;
    }
  }

  public void ApplyDamage()
  {
    if (playerOnIt)
    {
      player.GetComponent<PlayerStatsManager>().TakeDamage(damage);
    }
  }
}
