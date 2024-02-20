using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolLogoWiggle : MonoBehaviour
{
    private Vector3 defPos;
    private float defAngle;
    [SerializeField] private float offst;
    [SerializeField] private float spd = 1.0f;
    [SerializeField] private float strength = 1.0f;
    private float t;
    void Start()
    {
        defPos = transform.position;
        defAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        t = Time.time * spd;
        float angle = 0.0f;
        angle += GetCustSine(t + offst, 10f, 1f, 2f) * strength;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, defAngle + angle);

        float x = defPos.x;
        float y = defPos.y;

        x += GetCustSine(t + offst, 1f, 0.5f, 0.005f) * Screen.width * strength;
        y += GetCustSine(t + offst, 4f, 0.6f, 0.005f) * Screen.height * strength;

        transform.position = new Vector3(x, y, defPos.z);
    }

    private float GetCustSine(float x, float offset, float speed, float amplitude) {
        return Mathf.Sin(x * speed + offset) * amplitude;
    }


}
