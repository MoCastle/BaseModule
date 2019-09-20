using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameProject.UI.Scroll;

namespace GameProject.UI
{
    public partial class ExtendUGUIScroll
    {
        public class HorizontalExtendUGUIScrollAgent : BaseExtendScrollUIAgent, IScrollAgent
        {
            #region 流程
            protected override void InitReset()
            {
                RectTransform contRect = m_ExtendScroll.m_ElementContainer;
                RectTransform sampleRect = m_ExtendScroll.m_SampleElement;
                Vector2 contPivot = contRect.pivot;
                contPivot.y = 0.5f;
                contPivot.x = m_ExtendScroll.m_IsSwitchDirection ? 1 : 0;

                Vector2 minContAnchor = contRect.anchorMin;
                Vector2 maxContAnchor = contRect.anchorMax;
                minContAnchor.x = m_ExtendScroll.m_IsSwitchDirection ? 1 : 0;
                maxContAnchor.x = m_ExtendScroll.m_IsSwitchDirection ? 1 : 0;
                contRect.anchorMin = minContAnchor;
                contRect.anchorMax = maxContAnchor;
                contRect.pivot = contPivot;

                Vector2 samplePivot = sampleRect.pivot;
                samplePivot = contPivot;
                Vector2 minSampleAnchor = sampleRect.anchorMin;
                Vector2 maxSampleAnchor = sampleRect.anchorMax;
                minSampleAnchor.x = m_ExtendScroll.m_IsSwitchDirection ? 1 : 0;
                maxSampleAnchor.x = m_ExtendScroll.m_IsSwitchDirection ? 1 : 0;

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
                    return m_ExtendScroll.m_SampleElement.rect.width;
                }
            }
            public Vector2 elementLineDirector
            {
                get
                {
                    return Vector2.right;
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
                Vector3 pos = Vector3.zero;
                float centerPos = (m_ExtendScroll.m_ViewAnchorsInContentPos[0].x + m_ExtendScroll.m_ViewAnchorsInContentPos[2].x)*m_ExtendScroll.m_MidelPivot;
                pos.x = centerPos;
                return pos;
            }
            #endregion
            public Vector2 CountHeadPosOfView()
            {
                return (m_ExtendScroll.m_ViewAnchorsInContentPos[0] + m_ExtendScroll.m_ViewAnchorsInContentPos[1]) / 2;
            }

            public Vector2 CountTailPosOfView()
            {
                return (m_ExtendScroll.m_ViewAnchorsInContentPos[2] + m_ExtendScroll.m_ViewAnchorsInContentPos[3]) / 2;
            }

            protected override void SetScrollBar()
            {
                Scrollbar scrollbar = m_ExtendScroll.m_ScrollRect.horizontalScrollbar;
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