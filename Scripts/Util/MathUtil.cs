using System;
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
        
        float d = Mathf.Abs(-k * x0 + y0 - b) / Mathf.Sqrt((-k) * (-k) + 1);
        if(d > r) {
            v1 = Vector2.positiveInfinity; v2 = Vector2.positiveInfinity;
            return;
        }
        

        middleY1 = -SolvePartOfY() + (+k * k * y0 + k * x0 + b) / (k * k + 1);
        middleX1 = (middleY1 - b) / k;

        middleY2 = SolvePartOfY() + (+k * k * y0 + k * x0 + b) / (k * k + 1);
        middleX2 = (middleY2 - b) / k;

        float SolvePartOfY()
        {
            return (k * Mathf.Sqrt(-(y0 * y0) + (2 * k * x0 + 2 * b) * y0 - (k * k * x0 * x0) - (2 * b * k * x0) + (k * k + 1) * r * r - (b * b))) / (k * k + 1);
        }

        v1 = new Vector2(middleX1, middleY1);
        v2 = new Vector2(middleX2, middleY2);
    }

    public static Vector2 GetLineIntersection(float k1, float b1, float k2, float b2) {
        float y = (b2 - ((b1 * k2) / k1)) / (1 - (k2 / k1));

        float x = -((b1 - y) / k1);

        return new Vector2(x, y);
    }

    public static Tuple<float, float> GetPolarOfVector(Vector2 vector) {
        float length = vector.magnitude;
        vector = vector.normalized;
        float angle = math.atan2(vector.x, vector.y);

        return new Tuple<float, float>(angle, length);
    }

    public static Vector2 GetVectorOfPolar(float angle, float length) {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * length;
    }

    public static Vector2 GlobalToLocalCoords(Vector2 vector, IPosAndRot inIts) {
        var polar = GetPolarOfVector(vector - inIts.position);
        float newAngle = polar.Item1 - inIts.rotation * Mathf.Deg2Rad;
        float len = polar.Item2;

        return GetVectorOfPolar(newAngle, len);
    }

    public static Vector2 LocalToGlobalCoords(Vector2 vector, IPosAndRot inIts) {
        var polar = GetPolarOfVector(vector);
        float newAngle = polar.Item1 + inIts.rotation * Mathf.Deg2Rad;
        float len = polar.Item2;

        return inIts.position + GetVectorOfPolar(newAngle, len);
    }

    
}
