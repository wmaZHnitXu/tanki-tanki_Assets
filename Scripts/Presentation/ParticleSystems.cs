using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystems : MonoBehaviour
{
    [SerializeField] private ParticleSystem test;
    private Transform trns;
    public static ParticleSystems instance;

    public void Initialize() {
        instance = this;
        trns = test.transform;
    }

    public void PlayOnPos(Vector2 position) {
        test.transform.position = new Vector3(position.x, position.y, trns.position.z);
        test.Play();
    }
}
