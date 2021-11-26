using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameStrike : MonoBehaviour
{
  private Animator _animator;
  private BossEventManager _bossEventManager;
  private void Awake()
  {
    _animator = GetComponent<Animator>();
    _bossEventManager = FindObjectOfType<BossEventManager>();
  }


  private void Update()
  {

  }

  public void LoadTint()
  {
    _animator.SetBool("activeTint", true);
    _animator.SetBool("disactive", false);
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
  }
}
