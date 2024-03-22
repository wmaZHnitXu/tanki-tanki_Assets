using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystems : MonoBehaviour
{
    [SerializeField] private ParticleSystem crate;
    [SerializeField] private ParticleSystem tankDebris;
    private Transform trns;
    public static ParticleSystems instance;

    public enum ParticleType {
        Crate,
        TankDebris
    }

    public void Initialize() {
        instance = this;
        trns = crate.transform;
    }

    public void PlayOnPos(Vector2 position, ParticleType type) {
        switch (type) {
            case ParticleType.Crate:
                crate.transform.position = new Vector3(position.x, position.y, trns.position.z);
                crate.Play();
            break;
            case ParticleType.TankDebris:
                tankDebris.transform.position = new Vector3(position.x, position.y, trns.position.z);
                tankDebris.Play();
            break;
        }
    }
}
