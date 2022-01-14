using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaternTimer : MonoBehaviour
{
    public static PaternTimer instance;
    private BossEventManager _bossEventManager;
    [SerializeField] private float sensibility;

    public List<GameObject> enemyInScene;

    [SerializeField] private int loop;
    private int loopCount;
    
    [SerializeField]private float timerP1;
    [SerializeField] private float P1Lenght;
    
    
    [SerializeField]private float globalTimer;
    private bool isActive1;
    private bool isActive2;
    [SerializeField] private Timer[] timerPhase1;
    
    public bool phase2;
    [SerializeField] private float timerP2;
    [SerializeField] private float P2Lenght;
    [SerializeField] private Timer[] timerPhase2;

    private BossStatsManager _bossStatsManager;
    private CinematicBoss _cinematicBoss;
    public GameObject waveSpawner;

    [Header("P1")] 
    public float timerFS;

    public float backupTimerFS;
    public float currentTimerFS;
    
    [Header("P2")] 
    public GameObject RotationTurret;
    public Animator RotationTurretAnimator;

    [Header("Boss")] public Animator bossAnimator;

    [Header("TEST")] public GameObject player;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTimerFS = timerFS;
        backupTimerFS = timerFS;
        ScoreManager.instance.timerStart = true;
        _bossEventManager = GetComponent<BossEventManager>();
        _bossStatsManager = FindObjectOfType<BossStatsManager>();
        _cinematicBoss = FindObjectOfType<CinematicBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        #region SETUP
        
            foreach (GameObject n in enemyInScene)
            {
                for (int i = 0; i < enemyInScene.Count; i++)
                {
                    if (_cinematicBoss.isCinematic)
                    {
                        //Unable NavMesh Script
                        //enemyInScene[i].GetComponent<NavMeshAgent>().enabled = false;
                        
                        //Unable Controll Script
                        if (enemyInScene[i].GetComponent<IAShooter>())
                        {
                            enemyInScene[i].GetComponent<IAShooter>().enabled = false;
                        }

                        if (enemyInScene[i].GetComponent<IABarman>())
                        {
                            enemyInScene[i].GetComponent<IABarman>().enabled = false;
                        }

                        if (enemyInScene[i].GetComponent<IARunner>())
                        {
                            enemyInScene[i].GetComponent<IARunner>().enabled = false;
                        }

                        if (enemyInScene[i].GetComponent<IACac>())
                        {
                            enemyInScene[i].GetComponent<IACac>().enabled = false;
                        }
                    }
                    else
                    {
                        //enemyInScene[i].GetComponent<NavMeshAgent>().enabled = true;
                        
                        if (enemyInScene[i].GetComponent<IAShooter>())
                        {
                            enemyInScene[i].GetComponent<IAShooter>().enabled = true;
                        }

                        if (enemyInScene[i].GetComponent<IABarman>())
                        {
                            enemyInScene[i].GetComponent<IABarman>().enabled = true;
                        }

                        if (enemyInScene[i].GetComponent<IARunner>())
                        {
                            enemyInScene[i].GetComponent<IARunner>().enabled = true;
                        }

                        if (enemyInScene[i].GetComponent<IACac>())
                        {
                            enemyInScene[i].GetComponent<IACac>().enabled = true;
                        }
                    }
                }
            }
        
        
        if (player)
        {
            if (!_cinematicBoss.isCinematic)
            {
                if (!_cinematicBoss.startEnded)
                {
                    _cinematicBoss.StartCoroutine(_cinematicBoss.StartCinematic());
                }
                else
                {
                    TimersPhases();
                    EventPhases();
                }
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        #endregion

        if (_bossStatsManager.isDead)
        {
            for (int i = 0; i < enemyInScene.Count; i++)
            {
                Destroy(enemyInScene[i]);
            }
            
            waveSpawner.SetActive(false);
        }
    }

    private void TimersPhases()
    {
        #region Timer Setup
        
        globalTimer += 1 * Time.deltaTime; //Global Timer is the time duration for the beginning to the end of fight regardless phases.
        
        if (phase2) //Load the good timer for the good phases.
        {
            timerP1 = 0;
            timerP2 += 1 * Time.deltaTime;
        }
        else
        {
            timerP1 += 1 * Time.deltaTime;
            timerP2 = 0;
        }

        #endregion

        #region Phases Transitions

        #region P1
        
        if (timerP1 >= P1Lenght)
        {
            timerP1 = 0;
            loopCount++;
        }
        
        //When the global timer is higher than the phase 1 total lenght and pillars aren't destroyed, load a enrage phase.
        if (globalTimer >= P1Lenght * loop && _bossEventManager.pillars.Count > 0)
        {
            timerFS = 0;
        }

        if (_bossEventManager.pillars.Count <= 0)
        {
            timerFS = backupTimerFS;
            
            if (!_cinematicBoss.transiEnded)
            {             
                _cinematicBoss.StartCoroutine(_cinematicBoss.TransitionCinematic());
            }
            
            else
            {
                phase2 = true;
                loopCount = 0;
            }
        }
        

        #endregion

        #region P2

        if (phase2)
        {

            if (timerP2 >= P2Lenght)
            {
                timerP2 = 0;
                loopCount++;
            }

            if (globalTimer >= (P1Lenght + P2Lenght) * loop && _bossEventManager.pillars.Count > 0)
            {
                timerFS = 0;
            }

            if (_bossStatsManager.isDead) //If boss is dead, it's win.
            {
                if (!_cinematicBoss.endEnded)
                {
                    _cinematicBoss.StartCoroutine(_cinematicBoss.EndCinematic());
                }
                else
                {
                    Debug.Log("Fight is over");
                }
            }
        }

        #endregion
        

        #endregion
    }

    private void EventPhases()
    {
        if (currentTimerFS > 0)
        {
            currentTimerFS -= Time.deltaTime;
        }
        else if (currentTimerFS <= 0)
        {
            currentTimerFS = backupTimerFS;
            LoadFS(1);
        } 
        
        #region Phase1
        
        foreach (Timer t in timerPhase1)
        {
            if (Math.Abs(timerP1 - t.timeCode) < sensibility)
            {
                isActive1 = true;
            }
            else
            {
                isActive1 = false;
            }

            if (isActive1 && !_bossStatsManager.isDead)
            {
                switch (t.target)
                {
                    case Timer.TargetP.AOrUp:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(0);
                                break;

                            case Timer.BossParterns.FlameStrike:
                                LoadFS(0);
                                break;
                            
                            case Timer.BossParterns.Wave:
                                    waveSpawner.SetActive(true);
                                break;
                            
                            case Timer.BossParterns.RotationLaser:
                                LoadRotationBeam(0);
                                break;
                        }

                        break;

                    case Timer.TargetP.BOrDown:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(1);
                                break;

                            case Timer.BossParterns.FlameStrike:
                                LoadFS(1);
                                break;
                            
                            case Timer.BossParterns.Wave:
                                waveSpawner.SetActive(true);
                                break;
                            
                            case Timer.BossParterns.RotationLaser:
                                LoadRotationBeam(1);
                                break;
                        }

                        break;

                    case Timer.TargetP.COrLeft:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(2);
                                break;

                            case Timer.BossParterns.FlameStrike:
                                LoadFS(2);
                                break;
                            
                            case Timer.BossParterns.Wave:
                                waveSpawner.SetActive(true);
                                break;
                            
                            case Timer.BossParterns.RotationLaser:
                                LoadRotationBeam(2);
                                break;
                        }

                        break;

                    case Timer.TargetP.DOrRight:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(3);
                                break;

                            case Timer.BossParterns.FlameStrike:
                                LoadFS(3);
                                break;
                            
                            case Timer.BossParterns.Wave:
                                waveSpawner.SetActive(true);
                                break;
                            
                            case Timer.BossParterns.RotationLaser:
                                LoadRotationBeam(3);
                                break;
                        }

                        break;
                }
            }
        }

        #endregion
        
        #region Phase2
        
        foreach (Timer t in timerPhase2)
        {
            if (Math.Abs(timerP2 - t.timeCode) < sensibility)
            {
                isActive2 = true;
            }
            else
            {
                isActive2 = false;
            }

            if (isActive2 && !_bossStatsManager.isDead)
            {
                switch (t.target)
                {
                    case Timer.TargetP.AOrUp:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(0);
                                break;

                            case Timer.BossParterns.FlameStrike:
                                LoadFS(0);
                                break;
                            
                            case Timer.BossParterns.RotationLaser:
                                LoadRotationBeam(0);
                                break;
                        }

                        break;

                    case Timer.TargetP.BOrDown:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(1);
                                break;

                            case Timer.BossParterns.FlameStrike:
                                LoadFS(1);
                                break;
                            
                            case Timer.BossParterns.RotationLaser:
                                LoadRotationBeam(1);
                                break;
                        }

                        break;

                    case Timer.TargetP.COrLeft:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(2);
                                break;

                            case Timer.BossParterns.FlameStrike:
                                LoadFS(2);
                                break;
                            
                            case Timer.BossParterns.RotationLaser:
                                LoadRotationBeam(2);
                                break;
                        }

                        break;

                    case Timer.TargetP.DOrRight:
                        switch (t.bossParterns)
                        {
                            case Timer.BossParterns.Laser:
                                LoadBeam(3);
                                break;

                            case Timer.BossParterns.FlameStrike:
                                LoadFS(3);
                                break;
                            
                            case Timer.BossParterns.RotationLaser:
                                LoadRotationBeam(3);
                                break;
                        }

                        break;
                }
            }
        }
        #endregion
        
    }

    void LoadBeam(int selectPillard)
    {
        if (!_bossStatsManager.isDead)
        {
            _bossEventManager.LoadBeam(selectPillard);
            bossAnimator.Play("DJ_Attack_1");
        }
    }

    void LoadFS(int selectPillard)
    {
        if (!_bossStatsManager.isDead)
        {
            _bossEventManager.LoadFS();
            bossAnimator.Play("DJ_Attack_2");
        }
    }

    void LoadRotationBeam(int selectedSide)
    {
        if (!_bossStatsManager.isDead)
        {
            RotationBeam rotationBeam = _bossEventManager.rotationLaser[selectedSide].GetComponent<RotationBeam>();
            rotationBeam.isActive = true;
            bossAnimator.Play("DJ_Attack_3");
        }
    }
    
    
    [Serializable]
    public struct Timer
    {
        public float timeCode;
        public BossParterns bossParterns;
        public TargetP target;
        
        public enum TargetP{AOrUp, BOrDown, COrLeft, DOrRight}
        public enum BossParterns{Laser, FlameStrike, Wave, RotationLaser}
    }
}
