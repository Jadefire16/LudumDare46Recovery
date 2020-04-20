using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class AAUtilities 
    {
        public static Vector3 GetNormalizedDirection(Vector3 vecAlpha, Vector3 vecBeta)
        {
            Vector3 dir = vecBeta - vecAlpha;
            Vector3 normalizedDir = dir.normalized;
            return normalizedDir;         
        }
        public static Vector2 GetNormalizedDirection(Vector2 vecAlpha, Vector2 vecBeta)
        {
            Vector2 dir = vecBeta - vecAlpha;
            Vector2 normalizedDir = dir.normalized;
            return normalizedDir;
        }
        public static Vector3 GetWorldPos(Camera cam)
        {
            Vector3 vec = cam.ScreenToWorldPoint(Input.mousePosition);
            return vec;
        }
        public static bool GetRandNumBool(int min, int max)
        {
            int targetVal = min + max / 2;
            int value = Random.Range(min, max);
            bool x = value > targetVal;
            return x;
        }
        public static int GetRandNumInt(int min, int max)
        {
            int value = Random.Range(min, max);        
            return value;
        }
    }
}