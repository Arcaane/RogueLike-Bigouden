using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFeedBack : MonoBehaviour
{
    //Force de la vibration dans la manette
    [Header("Force de Vibaration")]
    [Tooltip("Correspondance : X => Moteur de Gauche, Y => Moteur de Droite, Z => Time")]
    public Vector3 vibrationForce;
    
    public void MovingRumble(Vector3 force)
    {
        float resetValue = 0;
        float duration = force.z * Time.deltaTime;
        if (duration >= force.y)
        {
            duration = force.y;
        }
        float leftVibration = Mathf.Lerp(force.x, resetValue, duration);
        float rightVibration = Mathf.Lerp(force.y, resetValue, duration);
        Gamepad.current.SetMotorSpeeds(leftVibration , rightVibration);
    }
}