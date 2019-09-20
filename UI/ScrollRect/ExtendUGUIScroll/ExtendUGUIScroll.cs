using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameProject.UI.Scroll;
using GameProject;

namespace GameProject.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public partial class ExtendUGUIScroll : BaseScrollRect, IDragHandler, IBeginDragHandler, IEndDragHandler,IPointerDownHandler,IPointerUpHandler
    {
        IScrollAgent m_ScrollAgent;
        Vector3[] m_ViewAnchorsInContentPos;
        Vector3[] m_ItemAchorsInContentPos;
        RectTransform m_ViewRect;
        ScrollRect m_ScrollRect;
        UGUIScrollBounceData m_BounceData;
        private Vector2 bounceSpeed;
        bool m_IsDragging;
        public bool isHorizontal = true;
        float m_HeadSpace;
        float m_TailSpace;
        int m_CurJumpingToIdx;

        [SerializeField]
        float m_MinScrollSpeed = 0;
        [SerializeField]
        float m_BounceBackTime = 1;
        [SerializeField]
        bool m_IsSwitchDirection;
        [SerializeField]
        float m_MidelPivot= 0.5f;
        [SerializeField]
        float m_MidelElementCenterPivot = 0.5f;

        #region 流程
        protected override void OnInit()
        {
            if (isHorizontal)
                m_ScrollAgent = new HorizontalExtendUGUIScrollAgent();
            else
                m_ScrollAgent = new VerticalExtendUGUIScrollAgent();
            base.OnInit();
            m_ViewAnchorsInContentPos = new Vector3[4];
            m_ItemAchorsInContentPos = new Vector3[4];
            m_ScrollRect = GetComponent<ScrollRect>();
            m_ViewRect = m_ScrollRect.viewport.GetComponent<RectTransform>();
            m_ElementContainer = m_ScrollRect.content.GetComponent<RectTransform>();
            m_ScrollAgent.Init(this);

            CountItemAnchorsPosOnContentPositon(m_ViewAnchorsInContentPos, m_ViewRect);
            Vector2 headPosOfView = CountHeadPosOfView();
            Vector2 tailPosOfView = CountTailPosOfView();
            float widthBetweenBound = (headPosOfView - tailPosOfView).magnitude;
            m_HeadSpace = m_AutoBounceToMidle? widthBetweenBound * m_MidelPivot : 0;
            m_TailSpace = m_AutoBounceToMidle ? widthBetweenBound * (1 - m_MidelPivot) : 0;
        }

        public override void Reset()
        {
            base.Reset();
            Vector2 size = m_ElementContainer.sizeDelta;
            Vector2 countSize = (Vector2)(elementEndShift * m_ElementsInfoAgent.GetInfoCount() - elementLineDirector * m_SpaceBTElements);
            size.x = Mathf.Abs(countSize.x) > 0 ? Mathf.Abs(countSize.x) : size.x;
            size.y = Mathf.Abs(countSize.y) > 0 ? Mathf.Abs(countSize.y) : size.y;
            Vector2 elementdir = elementLineDirector;
            elementdir.x = Mathf.Abs(elementdir.x);
            elementdir.y = Mathf.Abs(elementdir.y);
            size += elementdir * (m_HeadSpace + m_TailSpace);
            m_ElementContainer.sizeDelta = size;
            IEnumerator<ScrollElement> chipsEnumerator = m_ShowingElements.GetEnumerator();
            chipsEnumerator.Reset();
            while (chipsEnumerator.MoveNext())
            {
                DisPawnElement(chipsEnumerator.Current);
            }
            m_ShowingElements.Clear();
            int headIdx = CountHeadIdx();
            if (headIdx < m_ElementsInfoAgent.GetInfoCount() - 1)
            {
                ScrollElement headElement = SpawnElement();
                AddHead(headElement);
            }
        }
        #endregion
        #region 位置换算
        Vector2 startPos
        {
            get
            {
                return Vector2.zero;
            }
        }
        float headSpace
        {
            get
            {

                return m_HeadSpace;
            }
        }

        float tailSpace
        {
            get
            {
                return m_TailSpace;
            }
        }

        public int dirScale
        {
            get
            {
                return m_IsSwitchDirection ? -1 : 1;
            }
        }
        Vector2 elementCenterShift
        {
            get
            {
                return m_ScrollAgent.elementLineDirector * m_ScrollAgent.elementSpace * dirScale * m_MidelElementCenterPivot;
            }
        }

        Vector2 elementTailShift
        {
            get
            {
                return m_ScrollAgent.elementLineDirector * m_ScrollAgent.elementSpace * dirScale;
            }
        }

        Vector2 elementEndShift
        {
            get
            {
                return m_ScrollAgent.elementLineDirector * (m_ScrollAgent.elementSpace + m_SpaceBTElements) * dirScale;
            }
        }
        Vector2 elementLineDirector
        {
            get
            {
                return m_ScrollAgent.elementLineDirector * dirScale;
            }
        }
        Vector2 CountHeadPosOfView()
        {
            return m_IsSwitchDirection?m_ScrollAgent.CountTailPosOfView():  m_ScrollAgent.CountHeadPosOfView();
        }

        Vector2 CountTailPosOfView()
        {
            return m_IsSwitchDirection ? m_ScrollAgent.CountHeadPosOfView() : m_ScrollAgent.CountTailPosOfView();
        }

        protected void CountItemAnchorsPosOnContentPositon(Vector3[] anchors, RectTransform icon)
        {
            icon.GetWorldCorners(anchors);
            for (int idx = 0; idx < anchors.Length; ++idx)
            {
                anchors[idx] = m_ElementContainer.InverseTransformPoint(anchors[idx]);
            }
        }

        protected Vector2 CountCeneterPos()
        {
            Vector2 headViewPos = CountHeadPosOfView();
            Vector2 tailViewPos = CountTailPosOfView();
            return (tailViewPos - headViewPos) *m_MidelPivot + headViewPos;
        }

        public Vector2 CountPosByIdx(int idx)
        {
            Vector2 elementPos = elementEndShift * idx + elementLineDirector * m_HeadSpace;
            return elementPos;
        }
        #endregion
        #region 功能
        public void JumpTo(int idx)
        {
            m_IsForceToJumpTo = true;
            m_CurJumpingToIdx = idx;
            m_ScrollRect.velocity = Vector2.zero;
        }
        protected override bool scrolling
        {
            get
            {
                if (m_ScrollRect.velocity.sqrMagnitude > Mathf.Pow(m_MinScrollSpeed, 2))
                {
                    return true;
                }
                return false;
            }
        }

        protected override void OnScroll()
        {
            CountItemAnchorsPosOnContentPositon(m_ViewAnchorsInContentPos, m_ViewRect);
            base.OnScroll();
        }

        protected override void Bounce()
        {
            Vector2 centerPos = CountCeneterPos();
            if(!m_IsForceToJumpTo)
                m_CurJumpingToIdx = GetCenterNearlyElement();
            Vector2 pos = CountPosByIdx(m_CurJumpingToIdx);
            Vector2 dist = centerPos - pos - elementCenterShift;
            Vector2 targetPos = m_ElementContainer.anchoredPosition + dist;
            Vector2 finalyPos = new Vector2();
            finalyPos = Vector2.SmoothDamp(m_ElementContainer.anchoredPosition, targetPos, ref bounceSpeed, m_BounceBackTime,1000000f, Time.deltaTime);
            if (m_IsForceToJumpTo && (dist).magnitude < 0.1f)
                m_IsForceToJumpTo = false;
            m_ElementContainer.anchoredPosition = finalyPos;
        }
        
        int CountHeadIdx()
        {
            int idx = CountIdxByPos(CountHeadPosOfView());
            return idx;
        }

        int CountIdxByPos(Vector2 pos )
        {
            int idx = 0;
            float shadowOn = elementEndShift.magnitude;
            if(shadowOn <=0)
            {
                return 0;
            }
            float posVectorLength = (pos - startPos).magnitude;

            idx = (int)Mathf.Round( (posVectorLength - m_HeadSpace) / shadowOn);
            idx = idx < 0 ? 0 : idx;
            idx = idx >= m_ElementsInfoAgent.GetInfoCount() ? m_ElementsInfoAgent.GetInfoCount() - 1 : idx;
            return idx;
        }
        #endregion
        #region 元素管理
        protected override bool IsNeedAddHead()
        {
            if (showingElementsNum <= 0)
                return true;
            ScrollElement headElement = m_ShowingElements[0];
            if (headElement.idx <= 0)
                return false;
            Vector2 viewHeadPos = CountHeadPosOfView();
            Vector2 viewTailPos = CountTailPosOfView();

            Vector2 viewVector = viewTailPos - viewHeadPos;
            Vector2 elmentVector = viewHeadPos - headElement.rect.anchoredPosition;
            if (Vector2.Dot(viewVector, elmentVector)<0)
            {
                return true;
            }
            return false;
        }

        protected override bool IsNeedAddTail()
        {
            ScrollElement tailElement = m_ShowingElements[showingElementsNum - 1];
            if (tailElement.idx >= m_ElementsInfoAgent.GetInfoCount() - 1)
                return false;
            Vector2 viewHeadPos = CountHeadPosOfView();
            Vector2 viewTailPos = CountTailPosOfView();

            Vector2 viewVector = viewTailPos - viewHeadPos;
            Vector2 elementVector = viewTailPos - tailElement.rect.anchoredPosition - elementTailShift;
            if (Vector2.Dot(viewVector,elementVector)>0)
            {
                return true;
            }
            return false;
        }

        protected override bool IsOutHeadBounce()
        {
            ScrollElement headElement = m_ShowingElements[0];
            Vector2 viewHeadPos = CountHeadPosOfView();
            Vector2 viewTailPos = CountTailPosOfView();

            Vector2 viewVector = viewTailPos - viewHeadPos;
            Vector2 elementVector = viewHeadPos - headElement.rect.anchoredPosition - elementTailShift;
            if (Vector2.Dot(viewVector,elementVector)>0)
            {
                return true;
            }
            return false;
        }

        protected override bool IsOutTailBounce()
        {
            ScrollElement tailElement = m_ShowingElements[showingElementsNum - 1];
            Vector2 viewHeadPos = CountHeadPosOfView();
            Vector2 viewTailPos = CountTailPosOfView();

            Vector2 viewVector = viewTailPos - viewHeadPos;
            Vector2 elementVector = viewTailPos - tailElement.rect.anchoredPosition;
            if (Vector2.Dot(viewVector, elementVector) < 0)
            {
                return true;
            }
            return false;
        }

        protected override ScrollElement RemoveElementFromShoing(int Idx)
        {
            ScrollElement removedElement = m_ShowingElements[Idx];
            m_ShowingElements.RemoveAt(Idx);
            return removedElement;
        }

        protected override void AddHead(ScrollElement headElement)
        {
            if (showingElementsNum > 0)
            {
                ScrollElement perHeadEleemnt = m_ShowingElements[0];
                Vector2 anchorPositon = perHeadEleemnt.rect.anchoredPosition;
                anchorPositon -= elementEndShift;
                headElement.rect.anchoredPosition = anchorPositon;

                headElement = InitScrollElement(headElement, perHeadEleemnt.idx - 1);
                m_ShowingElements.Insert(0, headElement);
            }
            else
            {
                int headIdx = CountHeadIdx();
                Vector2 newPosition = CountPosByIdx(headIdx);
                headElement.rect.anchoredPosition = newPosition;

                headElement = InitScrollElement(headElement, headIdx);
                m_ShowingElements.Insert(0, headElement);
            }
        }

        protected override void AddTail(ScrollElement tailElement)
        {
            ScrollElement perTailEleemnt = m_ShowingElements[showingElementsNum - 1];
            Vector2 anchorPositon = perTailEleemnt.rect.anchoredPosition;
            anchorPositon += elementEndShift;
            tailElement.rect.anchoredPosition = anchorPositon;

            tailElement = InitScrollElement(tailElement, perTailEleemnt.idx + 1);
            m_ShowingElements.Add(tailElement);
        }

        protected override int GetCenterNearlyElement()
        {
            Vector2 centerPos = CountCeneterPos();
            Vector2 viewHeadPos = CountHeadPosOfView();
            Vector2 viewTailPos = CountTailPosOfView();
            Vector2 viewVector = viewTailPos - viewHeadPos;

            Vector2 pos = viewHeadPos + viewVector * m_MidelPivot;
            int nearlyIdx = CountIdxByPos(pos);

            return nearlyIdx;
        }
        #endregion
        #region 拖动
        protected override bool isDragging
        {
            get
            {
                return m_IsDragging || m_ScrollAgent.isDraging;
            }

            set
            {
                m_IsDragging = value;
            }
        }
        public virtual void OnDrag(PointerEventData eventData)
        {
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            m_IsForceToJumpTo = false;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }

        public IEnumerator<ScrollElement> GetShowingElement()
        {
            return m_ShowingElements.GetEnumerator();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }
        #endregion
    }

}