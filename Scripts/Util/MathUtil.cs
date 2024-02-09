using Unity.Mathematics;
using UnityEngine;

public class MathUtil
{
    public static float GetAngleFromVec(Vector2 vec) {
        vec = vec.normalized;
        return -math.atan2(vec.x, vec.y) * Mathf.Rad2Deg;
    }

    public static void GetCoefficientsForLine(Vector2 v1, Vector2 v2, out float k, out float b)
    {
        float mainDet = v1.x - v2.x;
        float detK = v1.y - v2.y;
        float detB = v1.x * v2.y - v2.x * v1.y;

        k = detK / mainDet; 
        b = detB / mainDet;

    }
    public static void SolveItersectionOfCircleAndLine(float k, float b, float x0, float y0, float r, out Vector2 v1, out Vector2 v2)
    {
        float middleX1;
        float middleY1; 
        float middleX2;
        float middleY2;

        if((Mathf.Pow((-2*x0 + 2*k*(b-y0)), 2) - 4*2*k*(x0*x0 + (b - y0)*(b - y0) - r*r) < 0)){
            v1 = Vector2.positiveInfinity; v2 = Vector2.positiveInfinity;
            return;
        }
        else {
            middleY1 = -SolvePartOfY() + (+k * k * y0 + k * x0 + b) / (k * k + 1);
            middleX1 = (middleY1 - b) / k;

            middleY2 = SolvePartOfY() + (+k * k * y0 + k * x0 + b) / (k * k + 1);
            middleX2 = (middleY2 - b) / k;
        }

        float SolvePartOfY()
        {
            return (k * Mathf.Sqrt(-(y0 * y0) + (2 * k * x0 + 2 * b) * y0 - (k * k * x0 * x0) - (2 * b * k * x0) + (k * k + 1) * r * r - (b * b))) / (k * k + 1);
        }

        v1 = new Vector2(middleX1, middleY1);
        v2 = new Vector2(middleX2, middleY2);
    }

    public Vector2 GetLineIntersection(float k1, float b1, float k2, float b2) {
        float y = (b2 - ((b1 * k2) / k1)) / (1 - (k2 / k1));

        float x = -((b1 - y) / k1);

        return new Vector2(x, y);
    }
}
