using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFeedBack : MonoBehaviour
{
    //Force de la vibration dans la manette
    public Vector2 vibrationForce;
    
    public void MovingRumble(Vector2 force)
    {
        Gamepad.current.SetMotorSpeeds(force.x, force.y);
    }
}