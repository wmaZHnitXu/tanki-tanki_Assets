using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarsController : MonoBehaviour
{
    private Queue<HealthBar> pool;
    private Dictionary<DestructiblePresentation, HealthBar> attached;
    [SerializeField] GameObject prefab;
    [SerializeField] Transform worldCanvas;
    public static HealthBarsController instance;

    public void Initialize() {
        instance = this;
        pool = new Queue<HealthBar>(FindObjectsOfType<HealthBar>());
        attached = new Dictionary<DestructiblePresentation, HealthBar>();
    }

    public void HighlightHealth(DestructiblePresentation presentation, float damage) {
        if (attached.ContainsKey(presentation)) {
            var bar = attached[presentation];
            bar.Show();
        }
        else {
            var bar = AllocateHealthBar();
            bar.ConnectToPresentation(presentation, damage);
            attached.Add(presentation, bar);
            bar.Show();
        }
    }

    public void ReturnMeToPool(HealthBar bar) {
        attached.Remove(bar.presentation);
        pool.Enqueue(bar);
    }

    private HealthBar AllocateHealthBar() {
        HealthBar result;
        if (pool.Count == 0) {
            result = Instantiate(prefab, Vector3.zero, Quaternion.identity, worldCanvas).GetComponent<HealthBar>();
        }
        else {
            result = pool.Dequeue();
        }

        return result;
    }

    public void DetachHealthBarFrom(DestructiblePresentation destructible) {
        attached.Remove(destructible);
    }
}
