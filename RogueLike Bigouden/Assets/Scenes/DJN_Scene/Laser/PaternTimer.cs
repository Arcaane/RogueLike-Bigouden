using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaternTimer : MonoBehaviour
{
    public Timer[] timer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Timer t in timer)
        {
            switch (t.bossParterns)
            {
                case Timer.BossParterns.Laser:
                    
                    
                    break;
                
                case Timer.BossParterns.FlameStrike:

                    
                    break;
            }

            switch (t.target)
            {
                case Timer.TargetP.A:

                    break;
                
                case Timer.TargetP.B:

                    break;
                
                case Timer.TargetP.C:

                    break;
                
                case Timer.TargetP.D:

                    break;
            }
        }
    }

    [Serializable]
    public struct Timer
    {
        public float timeCode;
        public BossParterns bossParterns;
        public TargetP target;
        public float phase2Timer;

        
        public enum TargetP{A, B, C, D}
        public enum BossParterns{Laser, FlameStrike}
    }
}
