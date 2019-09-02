using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject
{

    public abstract class BaseMonoSingleton<T> : MonoBehaviour where T:BaseMonoSingleton<T>,new ()
    {
        public static T singleton
        {
            get;
            protected set;
        }
    }

}