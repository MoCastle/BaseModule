using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject
{

    public static class MathOf3D
    {
        public static Plane CountPlane(Vector3 point, Vector3 normal)
        {
            Plane newPlane = new Plane();
            normal = normal.normalized;
            newPlane.A = normal.x;
            newPlane.B = normal.y;
            newPlane.C = normal.z;
            newPlane.D = -(newPlane.A * point.x + newPlane.B * point.y + newPlane.C * point.z);
            return newPlane;
        }

        public static float DistBetweenPointAndPlane(Vector3 inPoint, Plane inPlane)
        {
            float dst = Mathf.Abs(inPlane.A * inPoint.x + inPlane.B * inPoint.y + inPlane.C * inPoint.z + inPlane.D) / Mathf.Sqrt(inPlane.A * inPlane.A + inPlane.B * inPlane.B + inPlane.C * inPlane.C);
            return dst;
        }

        public static Vector3 PointCastInPlane(Plane inPlane, Vector3 inPoint)
        {
            Vector3 point = new Vector3();
            float plA = inPlane.A;
            float plB = inPlane.B;
            float plC = inPlane.C;
            float plD = inPlane.D;
            float poX = inPoint.x;
            float poY = inPoint.y;
            float poZ = inPoint.z;
            point.x = ((Mathf.Pow(plB, 2) + Mathf.Pow(plC, 2)) * poX - plA * (plB * poY + plC * poZ + plD)) / (Mathf.Pow(plA, 2) + Mathf.Pow(plB, 2) + Mathf.Pow(plC, 2));
            point.y = (Mathf.Pow(plA, 2) + Mathf.Pow(plC, 2) * poY - plB * (plA * poX + plC * poZ + plD)) / (Mathf.Pow(plA, 2) + Mathf.Pow(plB, 2) + Mathf.Pow(plC, 2));
            point.z = ((Mathf.Pow(plA,2) +Mathf.Pow(plB,2))*poZ - plC*(plA*poX + plB*poY + plD)) / (Mathf.Pow(plA, 2) + Mathf.Pow(plB, 2) + Mathf.Pow(plC, 2));
            return point;
        }
    }

}