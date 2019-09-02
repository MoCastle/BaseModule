using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameWork
{
    public class FrameWorkEventArg : EventArgs
    {
        public static int GetHashCode<T>()
        {
            string name = typeof(T).Name;
            int hashCode = typeof(T).GetHashCode();
            return hashCode;
        }
    }
}