using Unity.Mathematics;
using UnityEngine;

public class MathUtil
{
    public static float GetAngleFromVec(Vector2 vec) {
        vec = vec.normalized;
        return -math.atan2(vec.x, vec.y) * Mathf.Rad2Deg;
    }
}
