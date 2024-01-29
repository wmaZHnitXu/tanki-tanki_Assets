using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float _yaw;
    public float yaw {
        get => _yaw;
        set {
            _yaw = value;
        }
    }
    private Tank owner;
}
