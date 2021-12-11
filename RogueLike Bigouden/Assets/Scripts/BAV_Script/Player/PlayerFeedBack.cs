using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFeedBack : MonoBehaviour
{
    public static PlayerFeedBack instance = null;
    public PlayerAttribut playerStat;

    //Force de la vibration dans la manette
    [Header("Force de Vibaration")]
    [Tooltip("Correspondance : X => Moteur de Gauche, Y => Moteur de Droite, Z => Time")]
    public Vector3 vibrationForce;

    [Header("Camera")] public Transform pivotCam;
    public float multiplicatorEffect = 5f;
    public Camera cam;
    public AnimationCurve curveAnim;

    //private float--------
    private Vector3 oldPivotCamPos;
    private float _current;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Vector3 position = pivotCam.transform.position;
        oldPivotCamPos = new Vector3(position.x, position.y, 0);
    }

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
        Gamepad.current.SetMotorSpeeds(leftVibration, rightVibration);
    }

    public void CameraMovement(Vector3 posInitial, Vector3 posMaximal, float current)
    {
        
        
        /*
        float posInitalX = posInitial.x;
        float posInitalY = posInitial.y;
        //----------------------------------------
        float posMaximalX = posMaximal.x;
        float posMaximalY = posMaximal.y;
        Transform camTransform = pivotCam.transform;
        Vector3 position = camTransform.position;
        Vector2 oldPosCam = new Vector2(position.x, position.y);
        Vector2 newPosCam1 = new Vector2(position.x - multiplicatorEffect, position.y - multiplicatorEffect);
        Vector2 newPosCam2 = new Vector2(position.x - multiplicatorEffect, position.y + multiplicatorEffect);
        Vector2 newPosCam3 = new Vector2(position.x + multiplicatorEffect, position.y - multiplicatorEffect);
        Vector2 newPosCam4 = new Vector2(position.x + multiplicatorEffect, position.y + multiplicatorEffect);

        if (posInitalX >= posMaximalX)
        {
            //BottomRight---------------------
            if (posInitalY <= posMaximalY)
            {
                //camTransform.position = new Vector2(position.x - multiplicatorEffect, position.y + multiplicatorEffect);
                camTransform.position = Vector3.Lerp(oldPosCam, newPosCam2, curveAnim.Evaluate(current));
            }

            //TopRight--------------------
            if (posInitalY >= posMaximalY)
            {
                camTransform.position = Vector3.Lerp(oldPosCam, newPosCam1, curveAnim.Evaluate(current));
            }
        }

        else if (posInitalX <= posMaximalX)
        {
            //TopLeft---------------------
            if (posInitalY >= posMaximalY)
            {
                //camTransform.position = new Vector2(position.x + multiplicatorEffect, position.y - multiplicatorEffect);
                camTransform.position = Vector3.Lerp(oldPosCam, newPosCam3, curveAnim.Evaluate(current));
            }

            //BottomLeft--------------------
            if (posInitalY <= posMaximalY)
            {
                //camTransform.position = new Vector2(position.x + multiplicatorEffect, position.y + multiplicatorEffect);
                camTransform.position = Vector3.Lerp(oldPosCam, newPosCam4, curveAnim.Evaluate(current));
            }
        }
    }

        private const float radiusClamp = 0.76f;
        public void MoveCameraInput(int isDetect, Vector3 posPlayer, Vector3 posTarget, float current)
        {
        /*float target = 0, duration = 0;
        if (isDetect)
        {
            target = target == 0 ? 1 : 0;
        }
        Debug.Log("Target : " + target);
        Debug.Log("Current : " + current);*/
        //current = Mathf.MoveTowards(current, target, duration * TimeManager.CustomDeltaTimeAttack);
        //CameraMovement(posPlayer, posTarget * radiusClamp, current);
        
        
        
        
    }

    public void ResetPosCam(float current)
    {
        pivotCam.position = oldPivotCamPos;
        //pivotCam.position = Vector3.Lerp(transform.position, oldPivotCamPos, curveAnim.Evaluate(current));
    }
}