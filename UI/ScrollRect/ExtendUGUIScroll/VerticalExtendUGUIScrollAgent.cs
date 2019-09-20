using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameProject.UI.Scroll;

namespace GameProject.UI
{
    public partial class ExtendUGUIScroll
    {
        public class VerticalExtendUGUIScrollAgent :BaseExtendScrollUIAgent, IScrollAgent
        {
            #region 流程
            protected override void InitReset()
            {
                RectTransform contRect = m_ExtendScroll.m_ElementContainer;
                RectTransform sampleRect = m_ExtendScroll.m_SampleElement;
                Vector2 contPivot = contRect.pivot;
                contPivot.x = 0.5f;
                contPivot.y = m_ExtendScroll.m_IsSwitchDirection ? 0 : 1;

                Vector2 minContAnchor = contRect.anchorMin;
                Vector2 maxContAnchor = contRect.anchorMax;
                minContAnchor.y = m_ExtendScroll.m_IsSwitchDirection ? 0 : 1;
                maxContAnchor.y = m_ExtendScroll.m_IsSwitchDirection ? 0 : 1;
                contRect.anchorMin = minContAnchor;
                contRect.anchorMax = maxContAnchor;
                contRect.pivot = contPivot;

                Vector2 samplePivot = sampleRect.pivot;
                samplePivot = contPivot;
                Vector2 minSampleAnchor = sampleRect.anchorMin;
                Vector2 maxSampleAnchor = sampleRect.anchorMax;
                minSampleAnchor.y = m_ExtendScroll.m_IsSwitchDirection ? 0 : 1;
                maxSampleAnchor.y = m_ExtendScroll.m_IsSwitchDirection ? 0 : 1;

                sampleRect.anchorMin = minSampleAnchor;
                sampleRect.anchorMax = maxSampleAnchor;

                sampleRect.pivot = samplePivot;

            }
            #endregion

            #region 位置换算
            public float elementSpace
            {
                get
                {
                    return m_ExtendScroll.m_SampleElement.rect.height;
                }
            }
            public Vector2 elementLineDirector
            {
                get
                {
                    return Vector2.down;
                }
            }

            public bool isDraging
            {
                get
                {
                    return m_ScrollBar.isDragging;
                }
            }

            public Vector2 CountCenterPosOfView()
            {
                Vector2 pos = Vector2.zero;
                float centerPos = (m_ExtendScroll.m_ViewAnchorsInContentPos[1].y + m_ExtendScroll.m_ViewAnchorsInContentPos[3].y) ;
                pos.y = centerPos;
                return pos;
            }
            #endregion

            public Vector2 CountHeadPosOfView()
            {
                return (m_ExtendScroll.m_ViewAnchorsInContentPos[1] + m_ExtendScroll.m_ViewAnchorsInContentPos[2]) / 2;
            }

            public Vector2 CountTailPosOfView()
            {
                return (m_ExtendScroll.m_ViewAnchorsInContentPos[0] + m_ExtendScroll.m_ViewAnchorsInContentPos[3]) / 2;
            }

            protected override void SetScrollBar()
            {
                Scrollbar scrollbar = m_ExtendScroll.m_ScrollRect.verticalScrollbar;
                if (scrollbar != null)
                {
                    m_ScrollBar = scrollbar.GetComponent<ExtendUGUIScrollBar>();
                    if (m_ScrollBar == null)
                    {
                        m_ScrollBar = scrollbar.gameObject.AddComponent<ExtendUGUIScrollBar>();
                    }
                }
            }
        }
    }
}