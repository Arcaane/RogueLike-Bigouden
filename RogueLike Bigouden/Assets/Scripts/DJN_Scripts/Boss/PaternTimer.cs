using System;
using UnityEngine;

public class PaternTimer : MonoBehaviour
{
    private BossEventManager _bossEventManager;
    [SerializeField] private float sensibility;
    
    [SerializeField] private int loop;
    private int loopCount;
    
    [SerializeField]private float timerP1;
    [SerializeField] private float P1Lenght;
    
    
    [SerializeField]private float globalTimer;
    private bool isActive1;
    private bool isActive2;
    [SerializeField] private Timer[] timerPhase1;
    
    private bool phase2;
    [SerializeField] private float timerP2;
    [SerializeField] private float P2Lenght;
    [SerializeField] private Timer[] timerPhase2;

    private BossStatsManager _bossStatsManager;
    private CinematicBoss _cinematicBoss;
    public GameObject waveSpawner;

    [Header("P1")] 
    public float timerFS;

    private float backupTimerFS;
    private float currentTimerFS;
    
    [Header("P2")] 
    public GameObject RotationTurret;
    public Animator RotationTurretAnimator;

    [Header("TEST")] public GameObject player;
    
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
            RotationTurret.GetComponent<BoxCollider2D>().isTrigger = false;
            RotationTurretAnimator.Play("TM_Open");
            
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
            currentTimerFS = timerFS;
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

            if (isActive1)
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

            if (isActive2)
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
        _bossEventManager.LoadBeam(selectPillard);
    }

    void LoadFS(int selectPillard)
    {
        _bossEventManager.LoadFS();
    }

    void LoadRotationBeam(int selectedSide)
    {
        RotationBeam rotationBeam = _bossEventManager.rotationLaser[selectedSide].GetComponent<RotationBeam>();
        rotationBeam.isActive = true;
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
