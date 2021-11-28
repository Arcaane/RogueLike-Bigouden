using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class TimeManager
{
    public const float speedRalentiEnnemy = 0.3f;
    public const float speedRalentiProj = 0.3f;
    
    private static float timeRalenti = float.MinValue;
    public const float DurationRalenti = 0.75f; // in real time ! Float to changer for the duration of the Slow Down

    public static bool isRalenti =>
        Time.time - timeRalenti < DurationRalenti; // did we ralenti more than 0.5f seconds ago ? so we are slown Down
    public static float CustomDeltaTimeEnnemy =>
        isRalenti
            ? Time.deltaTime * speedRalentiEnnemy
            : Time.deltaTime; // is ralenti => delta Time * 0.3f ( on ralenti la speed * 0.3f sinon full speed )


    public static float CustomDeltaTimeAttack =>
        isRalenti
            ? Time.deltaTime * speedRalentiProj
            : Time.deltaTime; // is ralenti => delta Time * 0.3f ( on ralenti la speed * 0.3f sinon full speed )

    
    
    public static void SlowDownGame()
    {
        timeRalenti = Time.time;
        Debug.Log(timeRalenti);
    }
}
