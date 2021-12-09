using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameStrike : MonoBehaviour
{
  private Animator _animator;
  private BossEventManager _bossEventManager;

  [SerializeField] private int damage;
  private bool playerOnIt;
  private GameObject player;
  private bool alreadyLoad;
  
  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _bossEventManager = FindObjectOfType<BossEventManager>();
  }
  

  public void LoadTint()
  {
    if (!alreadyLoad)
    {
      _animator.SetBool("activeTint", true);
      _animator.SetBool("disactive", false);
      alreadyLoad = true;
    }
  }

  public void ActiveBurst()
  {
    _animator.SetBool("activeTint", false);
    _animator.SetBool("activeBurst", true);
  }

  public void Disactive()
  {
    _animator.SetBool("disactive", true);
    _animator.SetBool("activeBurst", false);
    alreadyLoad = false;
  }


  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.transform.GetComponent<PlayerStatsManager>())
    {
      playerOnIt = true;
      player = other.gameObject;
    }
    else
    {
      playerOnIt = false;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.transform.GetComponent<PlayerStatsManager>())
    {
      playerOnIt = false;
      player = null;
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
