using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaternTimer : MonoBehaviour
{
    private BossEventManager _bossEventManager;
    [SerializeField] private float sensibility;
    
    [SerializeField] private int loop;
    private int loopCount;
    
    private float timerP1;
    private float P1Lenght;
    
    
    private float globalTimer;
    private bool isActive1;
    private bool isActive2;
    [SerializeField] private Timer[] timerPhase1;
    
    private bool phase2;
    private float timerP2;
    private float P2Lenght;
    [SerializeField] private Timer[] timerPhase2;
    // Start is called before the first frame update
    void Start()
    {
        _bossEventManager = GetComponent<BossEventManager>();
        Debug.Log("This boss legal duration is " + ((P1Lenght + P2Lenght) * loop) + " seconds");
    }

    // Update is called once per frame
    void Update()
    {
        //setup timers
        globalTimer += 1 * Time.deltaTime;
        
        if (phase2)
        {
            timerP1 = 0;
            timerP2 += 1 * Time.deltaTime;
        }
        else
        {
            timerP1 += 1 * Time.deltaTime;
            timerP2 = 0;
        }

        //switch phase
        if (timerP1 >= P1Lenght)
        {
            timerP1 = 0;
            loopCount++;
        }
        
        if (globalTimer >= P1Lenght * loop)
        {
            phase2 = true;
            loopCount = 0;
        }

        if (timerP2 >= P2Lenght)
        {
            timerP2 = 0;
            loopCount++;
        }

        if (globalTimer >= (P1Lenght + P2Lenght) * loop)
        {
            Time.timeScale = 0;
        }
        
        
        
        //phases event 
        foreach (Timer t in timerPhase1)
        {
            if (Math.Abs(timerP1 - t.timeCode) < sensibility)
            {
                Debug.Log(t.bossParterns + " happen at " + t.timeCode + " on " + t.target);
                isActive1 = true;
            }
            else
            {
                isActive1 = false;
            }

            if (isActive1)
            {
                switch (t.target)
                {
                    case Timer.TargetP.A:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(0);
                                break;
                
                            case Timer.BossParterns.FlameStrike:
                                LoadFS(0);
                                break;
                        }
                        break;
                
                    case Timer.TargetP.B:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(1);
                                break;
                
                            case Timer.BossParterns.FlameStrike:
                                LoadFS(1);
                                break;
                        }
                        break;
                
                    case Timer.TargetP.C:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(2);
                                break;
                
                            case Timer.BossParterns.FlameStrike:
                                LoadFS(2);
                                break;
                        }
                        break;
                
                    case Timer.TargetP.D:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(3);
                                break;
                
                            case Timer.BossParterns.FlameStrike:
                                LoadFS(3);
                                break;
                        }
                        break;
                }
            
              
            }


        }
        
        foreach (Timer t in timerPhase2)
        {
            if (Math.Abs(timerP2 - t.timeCode) < sensibility)
            {
                Debug.Log(t.bossParterns + " happen at " + t.timeCode + " on " + t.target);
                isActive2 = true;
            }
            else
            {
                isActive2 = false;
            }

            if (isActive2)
            {
                switch (t.target)
                {
                    case Timer.TargetP.A:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(0);
                                break;
                
                            case Timer.BossParterns.FlameStrike:
                                LoadFS(0);
                                break;
                        }
                        break;
                
                    case Timer.TargetP.B:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(1);
                                break;
                
                            case Timer.BossParterns.FlameStrike:
                                LoadFS(1);
                                break;
                        }
                        break;
                
                    case Timer.TargetP.C:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(2);
                                break;
                
                            case Timer.BossParterns.FlameStrike:
                                LoadFS(2);
                                break;
                        }
                        break;
                
                    case Timer.TargetP.D:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(3);
                                break;
                
                            case Timer.BossParterns.FlameStrike:
                                LoadFS(3);
                                break;
                        }
                        break;
                }
            
              
            }


        }

       
    }
    
    void LoadBeam(int selectPillard)
    {
            _bossEventManager.LoadBeam(selectPillard);
    }

    void LoadFS(int selectPillard)
    {
        _bossEventManager.LoadFS();
    }
    
    
    [Serializable]
    public struct Timer
    {
        public float timeCode;
        public BossParterns bossParterns;
        public TargetP target;

        
        public enum TargetP{A, B, C, D}
        public enum BossParterns{Laser, FlameStrike}
    }
}
