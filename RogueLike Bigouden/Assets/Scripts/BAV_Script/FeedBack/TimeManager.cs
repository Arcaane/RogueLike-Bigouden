using System;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager _timeManager;

    //Speed of the Ennemy
    public float m_speedRalentiEnnemy = 0.4f;

    //Speed of the Projectile
    public float m_speedRalentiProj = 0.5f;

    //Speed of the Projectile
    public float m_speedRalentiPlayer = 0.8f;

    public void Awake()
    {
        if (_timeManager == null)
        {
            _timeManager = this;
        }
    }

    //Float to stop time before launch time.
    private float timeRalenti = float.MinValue;
    public float DurationRalenti = 1f; // in real time ! Float to changer for the duration of the Slow Down

    public bool isRalenti =>
        Time.time - timeRalenti < DurationRalenti; // did we ralenti more than 1f seconds ago ? so we are slown Down

    public float CustomDeltaTimeEnnemy =>
        isRalenti
            ? Time.deltaTime * m_speedRalentiEnnemy
            : Time.deltaTime; // is ralenti => delta Time * speedRalentiEnnemy ( on ralenti la speed * 0.3f sinon full speed )


    public float CustomDeltaTimeProjectile =>
        isRalenti
            ? Time.deltaTime * m_speedRalentiProj
            : Time.deltaTime; // is ralenti => delta Time * speedRalentiProj ( on ralenti la speed * 0.3f sinon full speed )

    public float CustomDeltaTimePlayer =>
        isRalenti
            ? Time.deltaTime * m_speedRalentiPlayer
            : Time.deltaTime; // is ralenti => delta Time * speedRalentiPlayer ( on ralenti la speed * 0.3f sinon full speed )


    /// <summary>
    /// Attack = 0
    /// Hit = 1
    /// Hurt = 2
    /// Dodge = 3
    /// Spawn = 4
    /// </summary>
    /// <param name="action"></param>
    public void SlowDownGame(int action)
    {
        timeRalenti = Time.time;
        switch (action)
        {
            //Attack
            case 0:
                Debug.Log(isRalenti);
                DurationRalenti = 0.2f;
                break;
            //Hit
            case 1:
                Debug.Log(isRalenti);
                DurationRalenti = 10f;
                break;
            //Hurt
            case 2:
                Debug.Log(isRalenti);
                DurationRalenti = 0.4f;
                break;
            //Dodge
            case 3:
                Debug.Log(isRalenti);
                DurationRalenti = 4f;
                break;
            //Spawn
            case 4:
                Debug.Log(timeRalenti);
                DurationRalenti = 0.25f;
                break;
        }
    }
    
    public void GainTime()
    {
        if (m_speedRalentiEnnemy < 1)
        {
            m_speedRalentiEnnemy += (Time.deltaTime/ (DurationRalenti * (1 / 0.4f)));
        }
        else
        {
            m_speedRalentiEnnemy = 1;
        }
        
        if (m_speedRalentiProj < 1)
        {
            m_speedRalentiProj += (Time.deltaTime/ (DurationRalenti * (1 / 0.5f)));
        }
        else
        {
            m_speedRalentiProj = 1;
        }
        
        if (m_speedRalentiPlayer < 1)
        {
            m_speedRalentiPlayer += (Time.deltaTime/ (DurationRalenti * (1 / 0.8f)));
        }
        else
        {
            m_speedRalentiPlayer = 1;
        }
    }

    public void ResetTime()
    {
        m_speedRalentiEnnemy = 0.4f;
        m_speedRalentiProj = 0.5f;
        m_speedRalentiPlayer = 0.8f;
    }

    private void Update()
    {
        if (isRalenti)
        {
            GainTime();
        }
        else
        {
            ResetTime();
        }
    }
}