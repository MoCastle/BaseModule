using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject;
namespace GameProject.UI.Scroll
{
    public abstract class BaseScrollBarAgent
    {
        protected BaseScrollRect m_ScrollRect;

        public virtual bool isDragging
        {
            get;
            protected set;
        }

        public virtual void Init(BaseScrollRect scrollRect)
        {
            m_ScrollRect = scrollRect;
        }

        public void Update()
        {
            
        }
    }
}