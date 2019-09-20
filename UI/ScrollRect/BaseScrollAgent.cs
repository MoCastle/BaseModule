using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject.UI.Scroll
{

    public class BaseScrollAgent
    {
        protected BaseScrollRect m_ScrollRect;
        public bool scrolling
        {
            get;
            protected set;
        }

        public virtual void Init( BaseScrollRect scrollRect )
        {
            m_ScrollRect = scrollRect;
        }

        public virtual void Update()
        {
        }
    }

}