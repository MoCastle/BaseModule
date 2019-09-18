using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject
{
    public abstract class BaseBehaviourSingleton<T> : MonoBehaviour where T :BaseBehaviourSingleton<T>
    {
        private static GameObject s_Container;
        static T s_Singleton;
        public static T singleton
        {
            get
            {
                if(s_Singleton == null)
                {
                    if(s_Container == null)
                    {
                        s_Container = new GameObject("StaticObjContainer");
                        GameObject.DontDestroyOnLoad(s_Container);
                    }

                    s_Singleton = s_Container.AddComponent<T>();
                }
                return s_Singleton;
            }
        }
    }
}
