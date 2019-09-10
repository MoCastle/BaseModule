using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectBase
{
    public abstract class BaseSingleton<T> where T : BaseSingleton<T>, new()
    {
        protected static T m_Singleton;
        public  static T singleton
        {
            get
            {
                if (m_Singleton == null)
                {
                    m_Singleton = new T();
                }
                return m_Singleton;
            }
        }
    }

}