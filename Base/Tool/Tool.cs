using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public static class Tool
    {
        public static T[] RandomFrom<T>(int num, T[] arr)
        {
            if (num >= arr.Length)
                return arr;
            T[] result = new T[num];
            float[] randomArr = new float[arr.Length];

            for (int idx = 0; idx < randomArr.Length; ++idx)
            {
                randomArr[idx] = idx * 10 + UnityEngine.Random.value;
            }

            List<float> randomList = new List<float>();
            randomList.AddRange(randomArr);
            randomList.Sort(SortFunc);
            if (num < randomList.Count)
                randomList.RemoveRange(num, randomList.Count - num);
            randomList.Sort();

            int elementIdx = -1;
            for (int slot = 0; slot < randomList.Count; ++slot)
            {
                int chipIdx = ((int)randomList[slot] / 10);
                result[slot] = arr[chipIdx];
            }
            return result;
        }

        static int SortFunc(float va, float vb)
        {
            va = va % 10;
            vb = vb % 10;
            if (vb > va)
                return -1;
            else if (vb == va)
                return 0;

            return 1;
        }
    }

}