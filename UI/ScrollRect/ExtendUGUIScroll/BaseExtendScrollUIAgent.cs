using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameProject.UI.Scroll;

namespace GameProject.UI
{
    public partial class ExtendUGUIScroll
    {
        public abstract class BaseExtendScrollUIAgent
        {
            protected ExtendUGUIScroll m_ExtendScroll;
            protected ExtendUGUIScrollBar m_ScrollBar;

            public virtual void Init(BaseScrollRect scrollRect)
            {
                m_ExtendScroll = scrollRect as ExtendUGUIScroll;
                ScrollRect scrollrect = m_ExtendScroll.m_ScrollRect;
                SetScrollBar();
                InitReset();
            }
            abstract protected void InitReset();
            abstract protected void SetScrollBar();
        }
    }

    

}