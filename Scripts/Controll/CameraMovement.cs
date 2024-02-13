using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    [SerializeField] private Transform camTrans;
    private PlayerController controller;
    [SerializeField] private float followSpeed;
    private Tank playerTank;
    private Vector2 oldAddition;
    [SerializeField] private Vector2 shakeVel;
    [SerializeField] private Vector2 shakeAcceleration;
    [SerializeField] private float shakeResistance;
    [SerializeField] private float shakeResistance2;
    [SerializeField] private float shakeChangeProbability;
    [SerializeField] private float shakeStrength = 1.0f;
    private Vector2 shakeAddition;
    
    public void Initialize(PlayerController controller, Tank playerTank) {
        instance = this;
        this.controller = controller;
        this.playerTank = playerTank;
    }

    void Update()
    {
        if (controller == null || playerTank == null) return;

        
        Vector2 lookPos = controller.GetLookVector();
        Vector2 addition = lookPos * 14.0f;
        addition = addition.normalized * Mathf.Min(addition.magnitude, 2.0f);
        Vector2 tankPos = playerTank.position;
        addition = Vector2.MoveTowards(oldAddition, addition, followSpeed * Time.deltaTime);

        shakeAddition += shakeVel * Time.deltaTime;

        var accelTarget = (Vector2.zero - shakeAddition) - shakeVel;
        
        if (shakeChangeProbability > UnityEngine.Random.Range(0.0f, 1.0f)) {

            var aadd = UnityEngine.Random.Range(-Mathf.PI, Mathf.PI) * 1.0f;
            var polarVel = MathUtil.GetPolarOfVector(shakeVel);
            var velAngle = polarVel.Item1 + aadd;
            var velLen = polarVel.Item2;
            shakeVel = MathUtil.GetVectorOfPolar(velAngle, velLen);

        }

        shakeAcceleration = Vector2.Lerp(shakeAcceleration, accelTarget, shakeResistance2 * Time.deltaTime);

        shakeVel += shakeAcceleration;

        lookPos = tankPos + addition + shakeAddition;
        oldAddition = addition;

        Vector3 newDesiredPos = new Vector3(lookPos.x, lookPos.y, camTrans.position.z);
        camTrans.position = newDesiredPos;
        
    }

    public void Shake(Vector2 dir) {
        shakeVel += dir * shakeStrength;
    }
}
