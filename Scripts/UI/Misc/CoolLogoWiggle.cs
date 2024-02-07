using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolLogoWiggle : MonoBehaviour
{
    private Vector3 defPos;
    private float defAngle;
    private float t;
    void Start()
    {
        defPos = transform.position;
        defAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        t = Time.time;
        float angle = 0.0f;
        angle += GetCustSine(t, 10f, 1f, 2f);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, defAngle + angle);

        float x = defPos.x;
        float y = defPos.y;

        x += GetCustSine(t, 1f, 0.5f, 0.005f) * Screen.width;
        y += GetCustSine(t, 4f, 0.6f, 0.005f) * Screen.height;

        transform.position = new Vector3(x, y, defPos.z);
    }

    private float GetCustSine(float x, float offset, float speed, float amplitude) {
        return Mathf.Sin(x * speed + offset) * amplitude;
    }


}
