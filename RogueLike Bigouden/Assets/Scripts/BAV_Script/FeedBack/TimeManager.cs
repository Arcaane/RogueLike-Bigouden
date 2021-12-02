using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class TimeManager
{
    //Speed of the Ennemy
    public const float m_speedRalentiEnnemy = 0.4f;

    //Speed of the Projectile
    public const float m_speedRalentiProj = 0.5f;

    //Speed of the Projectile
    public const float m_speedRalentiPlayer = 0.8f;

    //Float to stop time before launch time.
    private static float timeRalenti = float.MinValue;
    public static float DurationRalenti = 1f; // in real time ! Float to changer for the duration of the Slow Down

    public static bool isRalenti =>
        Time.time - timeRalenti < DurationRalenti; // did we ralenti more than 1f seconds ago ? so we are slown Down

    public static float CustomDeltaTimeEnnemy =>
        isRalenti
            ? Time.deltaTime * m_speedRalentiEnnemy
            : Time.deltaTime; // is ralenti => delta Time * speedRalentiEnnemy ( on ralenti la speed * 0.3f sinon full speed )


    public static float CustomDeltaTimeProjectile =>
        isRalenti
            ? Time.deltaTime * m_speedRalentiProj
            : Time.deltaTime; // is ralenti => delta Time * speedRalentiProj ( on ralenti la speed * 0.3f sinon full speed )

    public static float CustomDeltaTimePlayer =>
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
    public static void SlowDownGame(int action)
    {
        timeRalenti = Time.time;
        switch (action)
        {
            //Attack
            case 0:
                DurationRalenti = 0.2f;
                break;
            //Hit
            case 1:
                DurationRalenti = 0.05f;
                break;
            //Hurt
            case 2:
                DurationRalenti = 0.4f;
                break;
            //Dodge
            case 3:
                DurationRalenti = 2f;
                break;
            //Spawn
            case 4:
                DurationRalenti = 0.25f;
                break;
        }
    }
}